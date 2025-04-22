namespace XModem
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            label6 = new Label();
            cBoxBAUDRATE = new ComboBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            cBoxPARITYBITS = new ComboBox();
            cBoxSTOPBITS = new ComboBox();
            cBoxDATABITS = new ComboBox();
            cBoxCOMPORT2 = new ComboBox();
            cBoxCOMPORT1 = new ComboBox();
            groupBox2 = new GroupBox();
            progressBar1 = new ProgressBar();
            btnClose = new Button();
            btnOpen = new Button();
            btnSendData = new Button();
            tBoxDataInput = new TextBox();
            tBoxDataOutput = new TextBox();
            btnSendFile = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(cBoxBAUDRATE);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cBoxPARITYBITS);
            groupBox1.Controls.Add(cBoxSTOPBITS);
            groupBox1.Controls.Add(cBoxDATABITS);
            groupBox1.Controls.Add(cBoxCOMPORT2);
            groupBox1.Controls.Add(cBoxCOMPORT1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(265, 210);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Com Port Control";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(23, 92);
            label6.Name = "label6";
            label6.Size = new Size(68, 15);
            label6.TabIndex = 11;
            label6.Text = "BAUD RATE";
            // 
            // cBoxBAUDRATE
            // 
            cBoxBAUDRATE.Cursor = Cursors.Hand;
            cBoxBAUDRATE.FormattingEnabled = true;
            cBoxBAUDRATE.Items.AddRange(new object[] { "2400", "4800", "9600" });
            cBoxBAUDRATE.Location = new Point(110, 89);
            cBoxBAUDRATE.Name = "cBoxBAUDRATE";
            cBoxBAUDRATE.Size = new Size(121, 23);
            cBoxBAUDRATE.TabIndex = 10;
            cBoxBAUDRATE.Text = "9600";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 179);
            label5.Name = "label5";
            label5.Size = new Size(71, 15);
            label5.TabIndex = 9;
            label5.Text = "PARITY BITS";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 150);
            label4.Name = "label4";
            label4.Size = new Size(61, 15);
            label4.TabIndex = 8;
            label4.Text = "STOP BITS";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 121);
            label3.Name = "label3";
            label3.Size = new Size(62, 15);
            label3.TabIndex = 7;
            label3.Text = "DATA BITS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 64);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 6;
            label2.Text = "COM PORT 2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 35);
            label1.Name = "label1";
            label1.Size = new Size(76, 15);
            label1.TabIndex = 5;
            label1.Text = "COM PORT 1";
            // 
            // cBoxPARITYBITS
            // 
            cBoxPARITYBITS.Cursor = Cursors.Hand;
            cBoxPARITYBITS.FormattingEnabled = true;
            cBoxPARITYBITS.Items.AddRange(new object[] { "None", "Odd", "Even" });
            cBoxPARITYBITS.Location = new Point(110, 175);
            cBoxPARITYBITS.Name = "cBoxPARITYBITS";
            cBoxPARITYBITS.Size = new Size(121, 23);
            cBoxPARITYBITS.TabIndex = 4;
            cBoxPARITYBITS.Text = "None";
            // 
            // cBoxSTOPBITS
            // 
            cBoxSTOPBITS.Cursor = Cursors.Hand;
            cBoxSTOPBITS.FormattingEnabled = true;
            cBoxSTOPBITS.Items.AddRange(new object[] { "One", "Two" });
            cBoxSTOPBITS.Location = new Point(110, 146);
            cBoxSTOPBITS.Name = "cBoxSTOPBITS";
            cBoxSTOPBITS.Size = new Size(121, 23);
            cBoxSTOPBITS.TabIndex = 3;
            cBoxSTOPBITS.Text = "One";
            // 
            // cBoxDATABITS
            // 
            cBoxDATABITS.Cursor = Cursors.Hand;
            cBoxDATABITS.FormattingEnabled = true;
            cBoxDATABITS.Items.AddRange(new object[] { "6", "7", "8" });
            cBoxDATABITS.Location = new Point(110, 117);
            cBoxDATABITS.Name = "cBoxDATABITS";
            cBoxDATABITS.Size = new Size(121, 23);
            cBoxDATABITS.TabIndex = 2;
            cBoxDATABITS.Text = "8";
            // 
            // cBoxCOMPORT2
            // 
            cBoxCOMPORT2.Cursor = Cursors.Hand;
            cBoxCOMPORT2.FormattingEnabled = true;
            cBoxCOMPORT2.Location = new Point(110, 60);
            cBoxCOMPORT2.Name = "cBoxCOMPORT2";
            cBoxCOMPORT2.Size = new Size(121, 23);
            cBoxCOMPORT2.TabIndex = 1;
            cBoxCOMPORT2.SelectedIndexChanged += cBoxCOMPORT2_SelectedIndexChanged;
            // 
            // cBoxCOMPORT1
            // 
            cBoxCOMPORT1.Cursor = Cursors.Hand;
            cBoxCOMPORT1.FormattingEnabled = true;
            cBoxCOMPORT1.Location = new Point(110, 31);
            cBoxCOMPORT1.Name = "cBoxCOMPORT1";
            cBoxCOMPORT1.Size = new Size(121, 23);
            cBoxCOMPORT1.TabIndex = 0;
            cBoxCOMPORT1.SelectedIndexChanged += cBoxCOMPORT1_SelectedIndexChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(progressBar1);
            groupBox2.Controls.Add(btnClose);
            groupBox2.Controls.Add(btnOpen);
            groupBox2.Location = new Point(12, 228);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(172, 92);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(10, 51);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(156, 23);
            progressBar1.TabIndex = 2;
            // 
            // btnClose
            // 
            btnClose.Cursor = Cursors.Hand;
            btnClose.Location = new Point(87, 22);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 1;
            btnClose.Text = "CLOSE";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnOpen
            // 
            btnOpen.Cursor = Cursors.Hand;
            btnOpen.Location = new Point(6, 22);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(75, 23);
            btnOpen.TabIndex = 0;
            btnOpen.Text = "OPEN";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // btnSendData
            // 
            btnSendData.Cursor = Cursors.Hand;
            btnSendData.Location = new Point(190, 228);
            btnSendData.Name = "btnSendData";
            btnSendData.Size = new Size(87, 45);
            btnSendData.TabIndex = 2;
            btnSendData.Text = "SEND TEXT";
            btnSendData.UseVisualStyleBackColor = true;
            btnSendData.Click += btnSendData_Click;
            // 
            // tBoxDataInput
            // 
            tBoxDataInput.Cursor = Cursors.IBeam;
            tBoxDataInput.Location = new Point(283, 23);
            tBoxDataInput.Multiline = true;
            tBoxDataInput.Name = "tBoxDataInput";
            tBoxDataInput.PlaceholderText = "Data to be send";
            tBoxDataInput.Size = new Size(193, 129);
            tBoxDataInput.TabIndex = 3;
            // 
            // tBoxDataOutput
            // 
            tBoxDataOutput.Cursor = Cursors.IBeam;
            tBoxDataOutput.Location = new Point(283, 158);
            tBoxDataOutput.Multiline = true;
            tBoxDataOutput.Name = "tBoxDataOutput";
            tBoxDataOutput.PlaceholderText = "Data red from the port";
            tBoxDataOutput.Size = new Size(193, 154);
            tBoxDataOutput.TabIndex = 4;
            // 
            // btnSendFile
            // 
            btnSendFile.Location = new Point(190, 279);
            btnSendFile.Name = "btnSendFile";
            btnSendFile.Size = new Size(87, 41);
            btnSendFile.TabIndex = 5;
            btnSendFile.Text = "SEND FILE";
            btnSendFile.UseVisualStyleBackColor = true;
            btnSendFile.Click += btnSendFile_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(515, 349);
            Controls.Add(btnSendFile);
            Controls.Add(tBoxDataOutput);
            Controls.Add(tBoxDataInput);
            Controls.Add(btnSendData);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "COM PORT SERIAL";
            Load += Form1_Load_1;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private ComboBox cBoxPARITYBITS;
        private ComboBox cBoxSTOPBITS;
        private ComboBox cBoxDATABITS;
        private ComboBox cBoxCOMPORT2;
        private ComboBox cBoxCOMPORT1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private GroupBox groupBox2;
        private ProgressBar progressBar1;
        private Button btnClose;
        private Button btnOpen;
        private Button btnSendData;
        private TextBox tBoxDataInput;
        private Label label6;
        private ComboBox cBoxBAUDRATE;
        private TextBox tBoxDataOutput;
        private Button btnSendFile;
    }
}
