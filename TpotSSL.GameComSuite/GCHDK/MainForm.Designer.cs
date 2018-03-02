namespace TpotSSL.GameComTools.GCHDK {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gameIdBox = new System.Windows.Forms.NumericUpDown();
            this.recgonizedGameLabel = new System.Windows.Forms.Label();
            this.romSizeBox = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.iconBankBox = new System.Windows.Forms.NumericUpDown();
            this.iconYBox = new System.Windows.Forms.NumericUpDown();
            this.iconXBox = new System.Windows.Forms.NumericUpDown();
            this.loadBankBin = new System.Windows.Forms.Button();
            this.loadBankImage = new System.Windows.Forms.Button();
            this.nextbankButton = new System.Windows.Forms.Button();
            this.prevBankButton = new System.Windows.Forms.Button();
            this.saveBankBin = new System.Windows.Forms.Button();
            this.saveBankImage = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.fullRomImage = new System.Windows.Forms.PictureBox();
            this.bankLabel = new System.Windows.Forms.Label();
            this.bankImage = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.gameIconBox = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.compileASMButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.asmFileButton = new System.Windows.Forms.Button();
            this.compileASMFileBox = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fileDropdown = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRomAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRomButton = new System.Windows.Forms.Button();
            this.saveRomButton = new System.Windows.Forms.Button();
            this.loadRomDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveBankBinDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveBankImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.loadBankImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.loadBankBinDialog = new System.Windows.Forms.OpenFileDialog();
            this.openASMFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveRomDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameIdBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.romSizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconBankBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconYBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconXBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fullRomImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameIconBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(903, 630);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gameIdBox);
            this.tabPage1.Controls.Add(this.recgonizedGameLabel);
            this.tabPage1.Controls.Add(this.romSizeBox);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.iconBankBox);
            this.tabPage1.Controls.Add(this.iconYBox);
            this.tabPage1.Controls.Add(this.iconXBox);
            this.tabPage1.Controls.Add(this.loadBankBin);
            this.tabPage1.Controls.Add(this.loadBankImage);
            this.tabPage1.Controls.Add(this.nextbankButton);
            this.tabPage1.Controls.Add(this.prevBankButton);
            this.tabPage1.Controls.Add(this.saveBankBin);
            this.tabPage1.Controls.Add(this.saveBankImage);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.fullRomImage);
            this.tabPage1.Controls.Add(this.bankLabel);
            this.tabPage1.Controls.Add(this.bankImage);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.nameBox);
            this.tabPage1.Controls.Add(this.gameIconBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(895, 604);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gameIdBox
            // 
            this.gameIdBox.Enabled = false;
            this.gameIdBox.Location = new System.Drawing.Point(114, 36);
            this.gameIdBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.gameIdBox.Name = "gameIdBox";
            this.gameIdBox.Size = new System.Drawing.Size(129, 20);
            this.gameIdBox.TabIndex = 28;
            this.gameIdBox.ValueChanged += new System.EventHandler(this.gameIdBox_ValueChanged);
            // 
            // recgonizedGameLabel
            // 
            this.recgonizedGameLabel.AutoSize = true;
            this.recgonizedGameLabel.ForeColor = System.Drawing.Color.LimeGreen;
            this.recgonizedGameLabel.Location = new System.Drawing.Point(260, 40);
            this.recgonizedGameLabel.Name = "recgonizedGameLabel";
            this.recgonizedGameLabel.Size = new System.Drawing.Size(166, 13);
            this.recgonizedGameLabel.TabIndex = 27;
            this.recgonizedGameLabel.Text = "Recognized Game ID: Sonic JAM";
            this.recgonizedGameLabel.Visible = false;
            // 
            // romSizeBox
            // 
            this.romSizeBox.Enabled = false;
            this.romSizeBox.Increment = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.romSizeBox.Location = new System.Drawing.Point(318, 6);
            this.romSizeBox.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.romSizeBox.Minimum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.romSizeBox.Name = "romSizeBox";
            this.romSizeBox.Size = new System.Drawing.Size(64, 20);
            this.romSizeBox.TabIndex = 26;
            this.romSizeBox.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(260, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Rom Size";
            // 
            // iconBankBox
            // 
            this.iconBankBox.Enabled = false;
            this.iconBankBox.Location = new System.Drawing.Point(93, 127);
            this.iconBankBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.iconBankBox.Name = "iconBankBox";
            this.iconBankBox.Size = new System.Drawing.Size(64, 20);
            this.iconBankBox.TabIndex = 24;
            // 
            // iconYBox
            // 
            this.iconYBox.Enabled = false;
            this.iconYBox.Location = new System.Drawing.Point(93, 101);
            this.iconYBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.iconYBox.Name = "iconYBox";
            this.iconYBox.Size = new System.Drawing.Size(64, 20);
            this.iconYBox.TabIndex = 23;
            // 
            // iconXBox
            // 
            this.iconXBox.Enabled = false;
            this.iconXBox.Location = new System.Drawing.Point(93, 75);
            this.iconXBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.iconXBox.Name = "iconXBox";
            this.iconXBox.Size = new System.Drawing.Size(64, 20);
            this.iconXBox.TabIndex = 22;
            // 
            // loadBankBin
            // 
            this.loadBankBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadBankBin.Enabled = false;
            this.loadBankBin.Location = new System.Drawing.Point(789, 311);
            this.loadBankBin.Name = "loadBankBin";
            this.loadBankBin.Size = new System.Drawing.Size(100, 23);
            this.loadBankBin.TabIndex = 21;
            this.loadBankBin.Text = "Load Bank (Bin)";
            this.loadBankBin.UseVisualStyleBackColor = true;
            this.loadBankBin.Click += new System.EventHandler(this.loadBankBin_Click);
            // 
            // loadBankImage
            // 
            this.loadBankImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadBankImage.Enabled = false;
            this.loadBankImage.Location = new System.Drawing.Point(677, 311);
            this.loadBankImage.Name = "loadBankImage";
            this.loadBankImage.Size = new System.Drawing.Size(106, 23);
            this.loadBankImage.TabIndex = 20;
            this.loadBankImage.Text = "Load Bank (Image)";
            this.loadBankImage.UseVisualStyleBackColor = true;
            this.loadBankImage.Click += new System.EventHandler(this.loadBankImage_Click);
            // 
            // nextbankButton
            // 
            this.nextbankButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nextbankButton.Enabled = false;
            this.nextbankButton.Location = new System.Drawing.Point(633, 311);
            this.nextbankButton.Name = "nextbankButton";
            this.nextbankButton.Size = new System.Drawing.Size(32, 23);
            this.nextbankButton.TabIndex = 19;
            this.nextbankButton.Text = "->";
            this.nextbankButton.UseVisualStyleBackColor = true;
            this.nextbankButton.Click += new System.EventHandler(this.nextbankButton_Click);
            // 
            // prevBankButton
            // 
            this.prevBankButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.prevBankButton.Enabled = false;
            this.prevBankButton.Location = new System.Drawing.Point(633, 282);
            this.prevBankButton.Name = "prevBankButton";
            this.prevBankButton.Size = new System.Drawing.Size(32, 23);
            this.prevBankButton.TabIndex = 18;
            this.prevBankButton.Text = "<-";
            this.prevBankButton.UseVisualStyleBackColor = true;
            this.prevBankButton.Click += new System.EventHandler(this.prevBankButton_Click);
            // 
            // saveBankBin
            // 
            this.saveBankBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBankBin.Enabled = false;
            this.saveBankBin.Location = new System.Drawing.Point(789, 282);
            this.saveBankBin.Name = "saveBankBin";
            this.saveBankBin.Size = new System.Drawing.Size(100, 23);
            this.saveBankBin.TabIndex = 17;
            this.saveBankBin.Text = "Save Bank (Bin)";
            this.saveBankBin.UseVisualStyleBackColor = true;
            this.saveBankBin.Click += new System.EventHandler(this.saveBankBin_Click);
            // 
            // saveBankImage
            // 
            this.saveBankImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBankImage.Enabled = false;
            this.saveBankImage.Location = new System.Drawing.Point(677, 282);
            this.saveBankImage.Name = "saveBankImage";
            this.saveBankImage.Size = new System.Drawing.Size(106, 23);
            this.saveBankImage.TabIndex = 16;
            this.saveBankImage.Text = "Save Bank (Image)";
            this.saveBankImage.UseVisualStyleBackColor = true;
            this.saveBankImage.Click += new System.EventHandler(this.saveBankImage_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 326);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Full ROM";
            // 
            // fullRomImage
            // 
            this.fullRomImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fullRomImage.Location = new System.Drawing.Point(3, 342);
            this.fullRomImage.Name = "fullRomImage";
            this.fullRomImage.Size = new System.Drawing.Size(886, 256);
            this.fullRomImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fullRomImage.TabIndex = 14;
            this.fullRomImage.TabStop = false;
            this.fullRomImage.Paint += new System.Windows.Forms.PaintEventHandler(this.fullRomImage_Paint);
            this.fullRomImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fullRomImage_MouseClick);
            // 
            // bankLabel
            // 
            this.bankLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bankLabel.AutoSize = true;
            this.bankLabel.Location = new System.Drawing.Point(833, 4);
            this.bankLabel.Name = "bankLabel";
            this.bankLabel.Size = new System.Drawing.Size(56, 13);
            this.bankLabel.TabIndex = 13;
            this.bankLabel.Text = "Bank: 000";
            // 
            // bankImage
            // 
            this.bankImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bankImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.bankImage.Location = new System.Drawing.Point(633, 20);
            this.bankImage.Name = "bankImage";
            this.bankImage.Size = new System.Drawing.Size(256, 256);
            this.bankImage.TabIndex = 12;
            this.bankImage.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Icon Bank No.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Icon Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Icon X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name";
            // 
            // nameBox
            // 
            this.nameBox.Enabled = false;
            this.nameBox.Location = new System.Drawing.Point(114, 6);
            this.nameBox.MaxLength = 9;
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(129, 20);
            this.nameBox.TabIndex = 2;
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            // 
            // gameIconBox
            // 
            this.gameIconBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gameIconBox.Location = new System.Drawing.Point(3, 6);
            this.gameIconBox.Name = "gameIconBox";
            this.gameIconBox.Size = new System.Drawing.Size(68, 68);
            this.gameIconBox.TabIndex = 1;
            this.gameIconBox.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.compileASMButton);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.asmFileButton);
            this.tabPage2.Controls.Add(this.compileASMFileBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(895, 604);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Compiler";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // compileASMButton
            // 
            this.compileASMButton.Enabled = false;
            this.compileASMButton.Location = new System.Drawing.Point(6, 52);
            this.compileASMButton.Name = "compileASMButton";
            this.compileASMButton.Size = new System.Drawing.Size(75, 23);
            this.compileASMButton.TabIndex = 3;
            this.compileASMButton.Text = "Compile";
            this.compileASMButton.UseVisualStyleBackColor = true;
            this.compileASMButton.Click += new System.EventHandler(this.compileASMButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Assembly file";
            // 
            // asmFileButton
            // 
            this.asmFileButton.Location = new System.Drawing.Point(309, 25);
            this.asmFileButton.Name = "asmFileButton";
            this.asmFileButton.Size = new System.Drawing.Size(32, 22);
            this.asmFileButton.TabIndex = 1;
            this.asmFileButton.Text = "...";
            this.asmFileButton.UseVisualStyleBackColor = true;
            this.asmFileButton.Click += new System.EventHandler(this.asmFileButton_Click);
            // 
            // compileASMFileBox
            // 
            this.compileASMFileBox.Location = new System.Drawing.Point(6, 26);
            this.compileASMFileBox.Name = "compileASMFileBox";
            this.compileASMFileBox.Size = new System.Drawing.Size(297, 20);
            this.compileASMFileBox.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileDropdown});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fileDropdown
            // 
            this.fileDropdown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileDropdown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRomToolStripMenuItem,
            this.saveRomToolStripMenuItem,
            this.saveRomAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileDropdown.Image = ((System.Drawing.Image)(resources.GetObject("fileDropdown.Image")));
            this.fileDropdown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileDropdown.Name = "fileDropdown";
            this.fileDropdown.Size = new System.Drawing.Size(38, 22);
            this.fileDropdown.Text = "File";
            // 
            // loadRomToolStripMenuItem
            // 
            this.loadRomToolStripMenuItem.Name = "loadRomToolStripMenuItem";
            this.loadRomToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.loadRomToolStripMenuItem.Text = "Load Rom";
            this.loadRomToolStripMenuItem.Click += new System.EventHandler(this.loadRomButton_Click);
            // 
            // saveRomToolStripMenuItem
            // 
            this.saveRomToolStripMenuItem.Enabled = false;
            this.saveRomToolStripMenuItem.Name = "saveRomToolStripMenuItem";
            this.saveRomToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.saveRomToolStripMenuItem.Text = "Save Rom";
            // 
            // saveRomAsToolStripMenuItem
            // 
            this.saveRomAsToolStripMenuItem.Name = "saveRomAsToolStripMenuItem";
            this.saveRomAsToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.saveRomAsToolStripMenuItem.Text = "Save Rom As";
            this.saveRomAsToolStripMenuItem.Click += new System.EventHandler(this.saveRomButton_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // loadRomButton
            // 
            this.loadRomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadRomButton.Location = new System.Drawing.Point(921, 61);
            this.loadRomButton.Name = "loadRomButton";
            this.loadRomButton.Size = new System.Drawing.Size(75, 23);
            this.loadRomButton.TabIndex = 0;
            this.loadRomButton.Text = "Load Rom";
            this.loadRomButton.UseVisualStyleBackColor = true;
            this.loadRomButton.Click += new System.EventHandler(this.loadRomButton_Click);
            // 
            // saveRomButton
            // 
            this.saveRomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveRomButton.Enabled = false;
            this.saveRomButton.Location = new System.Drawing.Point(921, 90);
            this.saveRomButton.Name = "saveRomButton";
            this.saveRomButton.Size = new System.Drawing.Size(75, 23);
            this.saveRomButton.TabIndex = 2;
            this.saveRomButton.Text = "Save Rom";
            this.saveRomButton.UseVisualStyleBackColor = true;
            this.saveRomButton.Click += new System.EventHandler(this.saveRomButton_Click);
            // 
            // loadRomDialog
            // 
            this.loadRomDialog.FileName = "gcbuild.bin";
            this.loadRomDialog.Filter = "Game.Com Roms|*.bin";
            // 
            // saveBankBinDialog
            // 
            this.saveBankBinDialog.Filter = "Game.Com Data|*.bin";
            // 
            // saveBankImageDialog
            // 
            this.saveBankImageDialog.Filter = "Image Files|*.png";
            // 
            // loadBankImageDialog
            // 
            this.loadBankImageDialog.FileName = "bank.png";
            this.loadBankImageDialog.Filter = "Image Files|*.png";
            // 
            // loadBankBinDialog
            // 
            this.loadBankBinDialog.FileName = "bank.bin";
            this.loadBankBinDialog.Filter = "Game.Com Data|*.bin";
            // 
            // openASMFileDialog
            // 
            this.openASMFileDialog.FileName = "source.asm";
            this.openASMFileDialog.Filter = "Assembly Files|*.asm";
            // 
            // saveRomDialog
            // 
            this.saveRomDialog.FileName = "gcbuild0.bin";
            this.saveRomDialog.Filter = "Game.Com Roms|*.bin";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 681);
            this.Controls.Add(this.saveRomButton);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.loadRomButton);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Game.Com Homebrew Development Kit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameIdBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.romSizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconBankBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconYBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconXBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fullRomImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameIconBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.PictureBox gameIconBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button loadRomButton;
        private System.Windows.Forms.Button saveRomButton;
        private System.Windows.Forms.OpenFileDialog loadRomDialog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button nextbankButton;
        private System.Windows.Forms.Button prevBankButton;
        private System.Windows.Forms.Button saveBankBin;
        private System.Windows.Forms.Button saveBankImage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox fullRomImage;
        private System.Windows.Forms.Label bankLabel;
        private System.Windows.Forms.PictureBox bankImage;
        private System.Windows.Forms.Button loadBankBin;
        private System.Windows.Forms.Button loadBankImage;
        private System.Windows.Forms.SaveFileDialog saveBankBinDialog;
        private System.Windows.Forms.SaveFileDialog saveBankImageDialog;
        private System.Windows.Forms.OpenFileDialog loadBankImageDialog;
        private System.Windows.Forms.OpenFileDialog loadBankBinDialog;
        private System.Windows.Forms.NumericUpDown romSizeBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown iconBankBox;
        private System.Windows.Forms.NumericUpDown iconYBox;
        private System.Windows.Forms.NumericUpDown iconXBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button compileASMButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button asmFileButton;
        private System.Windows.Forms.TextBox compileASMFileBox;
        private System.Windows.Forms.OpenFileDialog openASMFileDialog;
        private System.Windows.Forms.SaveFileDialog saveRomDialog;
        private System.Windows.Forms.Label recgonizedGameLabel;
        private System.Windows.Forms.NumericUpDown gameIdBox;
        private System.Windows.Forms.ToolStripDropDownButton fileDropdown;
        private System.Windows.Forms.ToolStripMenuItem loadRomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRomAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

