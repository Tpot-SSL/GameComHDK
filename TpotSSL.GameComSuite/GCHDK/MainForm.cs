using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TpotSSL.GameComTools.GCHDK {
    public partial class MainForm : Form {

        public GameComRom CurrentROM;
        public int CurrentBankIndex;

        public static readonly string DOSFolder     = Environment.CurrentDirectory + "\\ASM";
        public static readonly string ASMFolder     = $"{DOSFolder}\\Assembler";
        public static readonly string NewFolder     = $"{DOSFolder}\\project";
        public static readonly string SourceFolder  = $"{ASMFolder}\\source";
        public static readonly string ErrorFile     = $"{ASMFolder}\\ASM85.ERR";
        public static readonly byte[] BuildData     = File.ReadAllBytes($"{ASMFolder}\\data.bin");
        public GameComBank CurrentBank => CurrentROM.MemoryBanks[CurrentBankIndex];

        public MainForm() => InitializeComponent();

        private void loadRomButton_Click(object sender, EventArgs e) {
            if(loadRomDialog.ShowDialog() != DialogResult.OK)
                return;

            LoadRom(loadRomDialog.FileName);
        }

        public void SetBank(int index){
            CurrentBankIndex            = index;
            prevBankButton.Enabled      = CurrentBankIndex > 0;
            nextbankButton.Enabled      = CurrentBankIndex < CurrentROM.MemoryBanks.Count;
            bankLabel.Text              = "Bank: " + CurrentBankIndex.ToString("000");
            bankImage.Image             = CurrentROM.MemoryBanks[CurrentBankIndex].Image.Bitmap;
            fullRomImage.Invalidate();
        }

        public void LoadRom(string filename) {
            CurrentBankIndex    = 0;
            bankLabel.Text      = "Bank: 000";
            CurrentROM?.Dispose();
            CurrentROM          = new GameComRom(filename);
            SetBoxes();
        }

        public void SetBoxes() {
            if(CurrentROM == null)
                return;

            nameBox.Enabled         = true;
            gameIconBox.Enabled     = true;
            gameIdBox.Enabled       = true;
            iconXBox.Enabled        = true;
            iconYBox.Enabled        = true;
            iconBankBox.Enabled     = true;
            bankImage.Enabled       = true;
            fullRomImage.Enabled    = true;
            saveBankBin.Enabled     = true;
            loadBankBin.Enabled     = true;
            saveBankImage.Enabled   = true;
            loadBankImage.Enabled   = true;
            prevBankButton.Enabled  = true;
            nextbankButton.Enabled  = true;
            saveRomButton.Enabled   = true;

            nameBox.Text        = CurrentROM.GameName;
            gameIconBox.Image   = CurrentROM.GameIcon;
            gameIdBox.Text      = CurrentROM.GameId.ToString();
            iconXBox.Text       = CurrentROM.IconX.ToString();
            iconYBox.Text       = CurrentROM.IconY.ToString();
            iconBankBox.Text    = CurrentROM.IconBankNo.ToString();
            romSizeBox.Text     = (CurrentROM.SizeInBytes/1024).ToString();

            recgonizedGameLabel.Visible = GameComRom.KnownGamesById.ContainsKey(CurrentROM.GameId);
            if(recgonizedGameLabel.Visible)
                recgonizedGameLabel.Text = "Recognized Game ID: "+GameComRom.GetGameName(CurrentROM.GameId);

            fullRomImage.Image  = CurrentROM.FullImage.Bitmap;
            bankImage.Image     = CurrentROM.MemoryBanks[CurrentBankIndex].Image.Bitmap;

            fullRomImage.Invalidate();
        }

        private void fullRomImage_Paint(object sender, PaintEventArgs e){
            if(CurrentROM == null)
                return;

            double  xMult   = (double)fullRomImage.Width/CurrentROM.FullImage.Width;
            double  yMult   = (double)fullRomImage.Height/CurrentROM.FullImage.Height;

            int     x       = (int)(256*xMult*CurrentBankIndex);
            int     y       = 0;

            while(x >= fullRomImage.Width) {
                x -= fullRomImage.Width;
                y += (int)(256 * yMult);
            }

            Rectangle rect = new Rectangle(x, y, (int)(256*xMult), (int)(256 * yMult));
            e.Graphics.DrawRectangle(Pens.Red, rect);
        }

        private void nextbankButton_Click(object sender, EventArgs e) => SetBank(CurrentBankIndex + 1);
        private void prevBankButton_Click(object sender, EventArgs e) => SetBank(CurrentBankIndex - 1);

        private void saveBankBin_Click(object sender, EventArgs e){
            if(saveBankBinDialog.ShowDialog() != DialogResult.OK)
                return;

            CurrentBank.Image.SaveBinary(saveBankBinDialog.FileName);
        }

        private void saveBankImage_Click(object sender, EventArgs e) {
            if(saveBankImageDialog.ShowDialog() != DialogResult.OK)
                return;

            CurrentBank.Image.SaveImage(saveBankImageDialog.FileName);
        }

        private void loadBankImage_Click(object sender, EventArgs e) {
            if(loadBankImageDialog.ShowDialog() != DialogResult.OK)
                return;

            CurrentBank.Image       = GameComImage.FromFile(loadBankImageDialog.FileName, false);
            CurrentROM.ReplaceBank(CurrentBankIndex, CurrentBank.Image.RawBytes, CurrentBank.Image.Bitmap);
            fullRomImage.Image      = CurrentROM.FullImage.Bitmap;
            bankImage.Image         = CurrentROM.MemoryBanks[CurrentBankIndex].Image.Bitmap;

            fullRomImage.Refresh();
            bankImage.Refresh();
        }

        private void loadBankBin_Click(object sender, EventArgs e) {
            if(loadBankBinDialog.ShowDialog() != DialogResult.OK)
                return;

            CurrentBank.Image       = GameComImage.FromFile(loadBankBinDialog.FileName, false);
            CurrentROM.ReplaceBank(CurrentBankIndex, CurrentBank.Image.RawBytes, CurrentBank.Image.Bitmap);
            fullRomImage.Image      = CurrentROM.FullImage.Bitmap;
            bankImage.Image         = CurrentROM.MemoryBanks[CurrentBankIndex].Image.Bitmap;

            fullRomImage.Refresh();
            bankImage.Refresh();
        }

        private void asmFileButton_Click(object sender, EventArgs e) {
            if(asmFolderDialog.ShowDialog() != DialogResult.OK)
                return;

            compileASMFileBox.Text      = asmFolderDialog.SelectedPath;
        
        }

        private void compileASMButton_Click(object sender, EventArgs e){
            loadASMRomButton.Enabled = false;
            File.Delete($"{ASMFolder}\\build.bin");
            File.Delete($"{ASMFolder}\\ASM85.ERR");
            Directory.CreateDirectory(SourceFolder);

            string[] files = Directory.GetFiles(SourceFolder);
            for(int i = 0; i < files.Length; ++i)
                File.Delete(files[i]);

            files = Directory.GetFiles(compileASMFileBox.Text, "*.*", SearchOption.TopDirectoryOnly);

            Dictionary<int, GameComImage> images = new Dictionary<int, GameComImage>();
            for(int i = 0; i < files.Length; ++i)
                File.Copy(files[i], SourceFolder+"\\"+Path.GetFileName(files[i]));

            if(Directory.Exists(compileASMFileBox.Text + "\\gfx")) {
                files = Directory.GetFiles(compileASMFileBox.Text + "\\gfx", "*.bin", SearchOption.TopDirectoryOnly);
                for(int i = 0; i < files.Length; ++i) {
                    string path     = Path.GetFileNameWithoutExtension(files[i]);
                    int num         = path.StartsWith("bank") ? int.Parse(path.Substring(4)) : int.Parse(path);
                    images.Add(num, GameComImage.FromFile(files[i]));
                }
                files = Directory.GetFiles(compileASMFileBox.Text + "\\gfx", "*.png", SearchOption.TopDirectoryOnly);
                for(int i = 0; i < files.Length; ++i) {
                    string path     = Path.GetFileNameWithoutExtension(files[i]);
                    int num         = path.StartsWith("bank") ? int.Parse(path.Substring(4)) : int.Parse(path);
                    images.Add(num, GameComImage.FromFile(files[i]));
                }
            }

            Process.Start($"{DOSFolder}\\DosBoxPortable.exe", "\"..\\..\\Assembler\\compile.bat\" -noconsole -exit")?.WaitForExit();

            while(true){
                if(File.Exists(ErrorFile)) {
                    if(File.Exists($"{ASMFolder}\\build.bin") && new FileInfo($"{ASMFolder}\\build.bin").Length > 8) {
                        System.Threading.Thread.Sleep(100);
                        
                        byte[] asm      = File.ReadAllBytes($"{ASMFolder}\\build.bin");
                        int length      = Math.Max(asm.Length, BuildData.Length);
                        int maxlength   = (asm.Length + 0x40000 < 1048576) ? 1048576 : 2097152;
                       
                        byte[] fullFile = new byte[maxlength];
                        for(int i = 0; i < length; ++i) 
                            fullFile[0x40000 + i] = (i < asm.Length ? asm[i] : BuildData[i]);

                        int[] keys = images.Keys.ToArray();

                        for(int i = 0; i < images.Count; ++i) {
                            int num = keys[i];
                            Array.Copy(images[num].RawBytes, 0, fullFile, num * GameComBank.SizeInBytes, GameComBank.SizeInBytes);
                        }

                        File.Delete($"{SourceFolder}\\build.bin");
                        File.WriteAllBytes($"{compileASMFileBox.Text}\\build.bin", fullFile);
                        asmNameLabel.Text           = "Errors: None";
                        loadASMRomButton.Enabled    = true;
                        break;
                    } else if(new FileInfo(ErrorFile).Length > 1) {
                        try {
                            asmNameLabel.Text = "Errors:\n" + File.ReadAllText(ErrorFile);
                        }catch {
                            continue;
                        }
                        break;
                    }
                }
            }
        }

        private void saveRomButton_Click(object sender, EventArgs e) {
            if(saveRomDialog.ShowDialog() != DialogResult.OK)
                return;

            CurrentROM.SaveBinary(saveRomDialog.FileName);
        }

        private void fullRomImage_MouseClick(object sender, MouseEventArgs e) {
            double  xMult   = (double)fullRomImage.Width  / CurrentROM.FullImage.Width;
            double  yMult   = (double)fullRomImage.Height / CurrentROM.FullImage.Height;

            int     index   = (int)(Math.Floor(e.X/xMult/256) + Math.Floor(Math.Floor(e.Y/yMult/256d)*(CurrentROM.FullImage.Width/256d)));
            SetBank(index);
        }

        private void nameBox_TextChanged(object sender, EventArgs e)                    => CurrentROM.GameName = nameBox.Text.PadRight(9);

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)            => Application.Exit();

        private void gameIdBox_ValueChanged(object sender, EventArgs e)                 => CurrentROM.GameId = Convert.ToUInt16(gameIdBox.Text);
        private void iconBankBox_ValueChanged(object sender, EventArgs e)               => gameIconBox.Image = CurrentROM.RefreshIcon((byte)iconBankBox.Value);
        private void iconXBox_ValueChanged(object sender, EventArgs e)                  => gameIconBox.Image = CurrentROM.RefreshIcon((byte)iconXBox.Value, CurrentROM.IconY);
        private void iconYBox_ValueChanged(object sender, EventArgs e)                  => gameIconBox.Image = CurrentROM.RefreshIcon(CurrentROM.IconX, (byte)iconYBox.Value);

        private void assemblyFiles_SelectedIndexChanged(object sender, EventArgs e)     => openASMFileButton.Enabled = true;

        private void loadASMRomButton_Click(object sender, EventArgs e)                 => LoadRom($"{compileASMFileBox.Text}\\build.bin");
        private void openASMFileButton_Click(object sender, EventArgs e)                => Process.Start(assemblyFiles.SelectedItem as string);
        private void openASMFolderButton_Click(object sender, EventArgs e)              => Process.Start(compileASMFileBox.Text);
        private void newProjectName_TextChanged(object sender, EventArgs e)             => createProjectButton.Enabled = newProjectName.TextLength == 9;

        private void createProjectButton_Click(object sender, EventArgs e) {
            if(asmFolderDialog.ShowDialog() != DialogResult.OK)
                return;

            string[] files = Directory.GetFiles(NewFolder, "*.*", SearchOption.TopDirectoryOnly);
            bool warning = true;
            for(int i = 0; i < files.Length; ++i) {
                string filename = asmFolderDialog.SelectedPath + "\\" + Path.GetFileName(files[i]);
                if(File.Exists(filename)) {
                    if(warning) {
                        if(MessageBox.Show("One or more files already exists in the directory.\r\n" +
                          "Creating the project here will replace the existing files.\r\n" +
                          "Are you sure you'd like to continue?", "Replace directory files?", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;

                        warning = false;
                    }

                    File.Delete(filename);
                }
                File.Copy(files[i], filename);
            }

            Directory.CreateDirectory(asmFolderDialog.SelectedPath + "\\gfx");

            compileASMFileBox.Text = asmFolderDialog.SelectedPath;
        }

        private void compileASMFileBox_TextChanged(object sender, EventArgs e) {
            loadASMRomButton.Enabled    = false;
            openASMFileButton.Enabled   = false;
            compileASMButton.Enabled    = Directory.Exists(compileASMFileBox.Text);
            openASMFolderButton.Enabled = Directory.Exists(compileASMFileBox.Text);
            assemblyFiles.Items.Clear();
            assemblyFiles.Items.AddRange(Directory.GetFiles(compileASMFileBox.Text));
        }
    }
}
