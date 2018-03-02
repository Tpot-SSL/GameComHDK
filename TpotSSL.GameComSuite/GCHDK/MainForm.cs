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
        public GameComBank CurrentBank => CurrentROM.MemoryBanks[CurrentBankIndex];

        public MainForm() {
            InitializeComponent();
        }

        private void loadRomButton_Click(object sender, EventArgs e) {
            if(loadRomDialog.ShowDialog() != DialogResult.OK)
                return;

            CurrentBankIndex = 0;
            bankLabel.Text = "Bank: 000";
            CurrentROM?.Dispose();
            CurrentROM = new GameComRom(loadRomDialog.FileName);
            SetBoxes();
        }

        public void SetBank(int index){
            CurrentBankIndex = index;
            prevBankButton.Enabled = CurrentBankIndex > 0;
            nextbankButton.Enabled = CurrentBankIndex < CurrentROM.MemoryBanks.Count;
            bankLabel.Text = "Bank: " + CurrentBankIndex.ToString("000");
            bankImage.Image = CurrentROM.MemoryBanks[CurrentBankIndex].Image.Image;
            fullRomImage.Invalidate();
        }

        public void SetBoxes() {
            if(CurrentROM == null)
                return;

            nameBox.Enabled = true;
            gameIconBox.Enabled = true;
            gameIdBox.Enabled = true;
            iconXBox.Enabled = true;
            iconYBox.Enabled = true;
            iconBankBox.Enabled = true;
            bankImage.Enabled = true;
            fullRomImage.Enabled = true;
            saveBankBin.Enabled = true;
            loadBankBin.Enabled = true;
            saveBankImage.Enabled = true;
            loadBankImage.Enabled = true;
            prevBankButton.Enabled = true;
            nextbankButton.Enabled = true;
            saveRomButton.Enabled = true;

            nameBox.Text = CurrentROM.GameName;
            gameIconBox.Image = CurrentROM.GameIcon;
            gameIdBox.Text = CurrentROM.GameId.ToString();
            iconXBox.Text = CurrentROM.IconX.ToString();
            iconYBox.Text = CurrentROM.IconY.ToString();
            iconBankBox.Text = CurrentROM.IconBankNo.ToString();
            romSizeBox.Text = (CurrentROM.SizeInBytes/1024).ToString();

            recgonizedGameLabel.Visible = GameComRom.KnownGamesById.ContainsKey(CurrentROM.GameId);
            if(recgonizedGameLabel.Visible)
                recgonizedGameLabel.Text = "Recognized Game ID: "+GameComRom.GetGameName(CurrentROM.GameId);

            fullRomImage.Image = CurrentROM.FullImage.Image;
            bankImage.Image = CurrentROM.MemoryBanks[CurrentBankIndex].Image.Image;

            fullRomImage.Invalidate();
        }

        private void fullRomImage_Paint(object sender, PaintEventArgs e){
            if(CurrentROM == null)
                return;

            double xMult = (double)fullRomImage.Width/CurrentROM.FullImage.Width;
            double yMult = (double)fullRomImage.Height/CurrentROM.FullImage.Height;

            int x = (int)(256*xMult*CurrentBankIndex);
            int y = 0;

            while(x >= fullRomImage.Width) {
                x -= fullRomImage.Width;
                y += (int)(256 * yMult);
            }

            Rectangle rect = new Rectangle(x, y, (int)(256*xMult), (int)(256 * yMult));
            e.Graphics.DrawRectangle(Pens.Red, rect);
        }


        private void nextbankButton_Click(object sender, EventArgs e) {
            SetBank(CurrentBankIndex + 1);
        }

        private void prevBankButton_Click(object sender, EventArgs e){
            SetBank(CurrentBankIndex - 1);
        }

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

            CurrentBank.Image = GameComImage.FromFile(loadBankImageDialog.FileName, false);
            CurrentROM.ReplaceBank(CurrentBankIndex, CurrentBank.Image.RawBytes, CurrentBank.Image.Image);
            fullRomImage.Image = CurrentROM.FullImage.Image;
            bankImage.Image = CurrentROM.MemoryBanks[CurrentBankIndex].Image.Image;
            fullRomImage.Refresh();
            bankImage.Refresh();
        }

        private void loadBankBin_Click(object sender, EventArgs e) {
            if(loadBankBinDialog.ShowDialog() != DialogResult.OK)
                return;

            CurrentBank.Image = GameComImage.FromFile(loadBankBinDialog.FileName, false);
            CurrentROM.ReplaceBank(CurrentBankIndex, CurrentBank.Image.RawBytes, CurrentBank.Image.Image);
            fullRomImage.Image = CurrentROM.FullImage.Image;
            bankImage.Image = CurrentROM.MemoryBanks[CurrentBankIndex].Image.Image;
            fullRomImage.Refresh();
            bankImage.Refresh();
        }

        private void asmFileButton_Click(object sender, EventArgs e) {
            if(openASMFileDialog.ShowDialog() != DialogResult.OK)
                return;

            compileASMFileBox.Text = openASMFileDialog.FileName;
            compileASMButton.Enabled = true;
        }

        private void compileASMButton_Click(object sender, EventArgs e){
            File.Delete(Environment.CurrentDirectory + "\\ASM\\Assembler\\source.asm");
            File.Delete(Environment.CurrentDirectory + "\\ASM\\Assembler\\gcbuild.bin");
            File.Delete(Environment.CurrentDirectory + "\\ASM\\Assembler\\ASM85.ERR");

            File.Copy(compileASMFileBox.Text, Environment.CurrentDirectory + "\\ASM\\Assembler\\source.asm");
            Process.Start(Environment.CurrentDirectory + "\\ASM\\DosBoxPortable.exe", "\"..\\..\\Assembler\\compile.bat\" -noconsole -exit")?.WaitForExit();

            while(true){
                if(File.Exists(Environment.CurrentDirectory + "\\ASM\\Assembler\\ASM85.ERR")) {
                    if(File.Exists(Environment.CurrentDirectory + "\\ASM\\Assembler\\gcbuild.bin")) {
                        System.Threading.Thread.Sleep(100);
                        byte[] startB = new byte[0x40000];
                        byte[] asm = File.ReadAllBytes(Environment.CurrentDirectory + "\\ASM\\Assembler\\gcbuild.bin");

                        int maxlength = (asm.Length + startB.Length < 1048576) ? 1048576 : 2097152;

                        byte[] end = new byte[maxlength - (asm.Length + startB.Length)];

                        byte[] fullFile = startB.Concat(asm).Concat(end).ToArray();

                        File.WriteAllBytes(Environment.CurrentDirectory + "\\ASM\\Assembler\\gcbuild.bin", fullFile);
                        File.Delete(Path.GetDirectoryName(compileASMFileBox.Text) + "\\gcbuild.bin");
                        File.Copy(Environment.CurrentDirectory + "\\ASM\\Assembler\\gcbuild.bin", Path.GetDirectoryName(compileASMFileBox.Text) + "\\gcbuild.bin");
                        break;
                    } else if(new FileInfo(Environment.CurrentDirectory + "\\ASM\\Assembler\\ASM85.ERR").Length > 1) {
                        MessageBox.Show("Compile Error.");
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
            double xMult = (double)fullRomImage.Width / CurrentROM.FullImage.Width;
            double yMult = (double)fullRomImage.Height / CurrentROM.FullImage.Height;

            int index = (int)(Math.Floor(e.X/xMult/256) + Math.Floor(Math.Floor(e.Y/yMult/256d)*(CurrentROM.FullImage.Width/256d)));
            SetBank(index);
        }

        private void nameBox_TextChanged(object sender, EventArgs e) => CurrentROM.GameName = nameBox.Text.PadRight(9);

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void gameIdBox_ValueChanged(object sender, EventArgs e) => CurrentROM.GameId = Convert.ToUInt16(gameIdBox.Text);
    }
}
