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
            rButtonTransmiter = new RadioButton();
            rButtonReceiver = new RadioButton();
            btnSendFile = new Button();
            checkBox1 = new CheckBox();
            label6 = new Label();
            cBoxBAUDRATE = new ComboBox();
            btnSendData = new Button();
            label1 = new Label();
            cBoxCOMPORT1 = new ComboBox();
            groupBox2 = new GroupBox();
            progressBar1 = new ProgressBar();
            btnClose = new Button();
            btnOpen = new Button();
            tBoxDataInput = new TextBox();
            tBoxDataOutput = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rButtonTransmiter);
            groupBox1.Controls.Add(rButtonReceiver);
            groupBox1.Controls.Add(btnSendFile);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(cBoxBAUDRATE);
            groupBox1.Controls.Add(btnSendData);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cBoxCOMPORT1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(265, 210);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Com Port Control";
            // 
            // rButtonTransmiter
            // 
            rButtonTransmiter.AutoSize = true;
            rButtonTransmiter.Checked = true;
            rButtonTransmiter.Location = new Point(162, 94);
            rButtonTransmiter.Name = "rButtonTransmiter";
            rButtonTransmiter.Size = new Size(96, 19);
            rButtonTransmiter.TabIndex = 14;
            rButtonTransmiter.TabStop = true;
            rButtonTransmiter.Text = "TRANSMITER";
            rButtonTransmiter.UseVisualStyleBackColor = true;
            rButtonTransmiter.CheckedChanged += rButtonTransmiter_CheckedChanged;
            // 
            // rButtonReceiver
            // 
            rButtonReceiver.AutoSize = true;
            rButtonReceiver.Location = new Point(81, 94);
            rButtonReceiver.Name = "rButtonReceiver";
            rButtonReceiver.Size = new Size(75, 19);
            rButtonReceiver.TabIndex = 13;
            rButtonReceiver.Text = "RECEIVER";
            rButtonReceiver.UseVisualStyleBackColor = true;
            rButtonReceiver.CheckedChanged += rButtonReceiver_CheckedChanged;
            // 
            // btnSendFile
            // 
            btnSendFile.Location = new Point(23, 169);
            btnSendFile.Name = "btnSendFile";
            btnSendFile.Size = new Size(87, 41);
            btnSendFile.TabIndex = 5;
            btnSendFile.Text = "SEND FILE";
            btnSendFile.UseVisualStyleBackColor = true;
            btnSendFile.Click += btnSendFile_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(23, 93);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(49, 19);
            checkBox1.TabIndex = 12;
            checkBox1.Text = "CRC";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(23, 63);
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
            cBoxBAUDRATE.Location = new Point(110, 60);
            cBoxBAUDRATE.Name = "cBoxBAUDRATE";
            cBoxBAUDRATE.Size = new Size(121, 23);
            cBoxBAUDRATE.TabIndex = 10;
            cBoxBAUDRATE.Text = "9600";
            // 
            // btnSendData
            // 
            btnSendData.Cursor = Cursors.Hand;
            btnSendData.Location = new Point(22, 118);
            btnSendData.Name = "btnSendData";
            btnSendData.Size = new Size(87, 45);
            btnSendData.TabIndex = 2;
            btnSendData.Text = "SEND TEXT";
            btnSendData.UseVisualStyleBackColor = true;
            btnSendData.Click += btnSendData_Click;
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
            groupBox2.Size = new Size(265, 92);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(10, 51);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(249, 23);
            progressBar1.TabIndex = 2;
            // 
            // btnClose
            // 
            btnClose.Cursor = Cursors.Hand;
            btnClose.Location = new Point(146, 22);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(113, 23);
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
            btnOpen.Size = new Size(134, 23);
            btnOpen.TabIndex = 0;
            btnOpen.Text = "OPEN";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
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
            tBoxDataOutput.Size = new Size(193, 162);
            tBoxDataOutput.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(515, 349);
            Controls.Add(tBoxDataOutput);
            Controls.Add(tBoxDataInput);
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
        private ComboBox cBoxCOMPORT1;
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
        private CheckBox checkBox1;
        private RadioButton rButtonTransmiter;
        private RadioButton rButtonReceiver;
    }
}
