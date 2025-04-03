namespace HubDownloader
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label2 = new Label();
            tbChromeInstallationLocation = new TextBox();
            label3 = new Label();
            tbFirefoxInstallationLocation = new TextBox();
            label4 = new Label();
            comboPreferredBrowser = new ComboBox();
            label5 = new Label();
            cbPrivateMode = new CheckBox();
            label6 = new Label();
            comboPreferredVideoQuality = new ComboBox();
            label7 = new Label();
            comboFallbackVideoQuality = new ComboBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            tbEdgeInstallationLocation = new TextBox();
            label1 = new Label();
            label8 = new Label();
            btnCancel = new Button();
            btnOk = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.Location = new Point(30, 38);
            label2.Name = "label2";
            label2.Size = new Size(184, 20);
            label2.TabIndex = 1;
            label2.Text = "Edge Installation Location:";
            // 
            // tbChromeInstallationLocation
            // 
            tbChromeInstallationLocation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbChromeInstallationLocation.Font = new Font("Segoe UI", 9F);
            tbChromeInstallationLocation.Location = new Point(46, 121);
            tbChromeInstallationLocation.Name = "tbChromeInstallationLocation";
            tbChromeInstallationLocation.Size = new Size(690, 27);
            tbChromeInstallationLocation.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F);
            label3.Location = new Point(30, 96);
            label3.Name = "label3";
            label3.Size = new Size(202, 20);
            label3.TabIndex = 3;
            label3.Text = "Chrome Installation Location:";
            // 
            // tbFirefoxInstallationLocation
            // 
            tbFirefoxInstallationLocation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbFirefoxInstallationLocation.Font = new Font("Segoe UI", 9F);
            tbFirefoxInstallationLocation.Location = new Point(46, 180);
            tbFirefoxInstallationLocation.Name = "tbFirefoxInstallationLocation";
            tbFirefoxInstallationLocation.Size = new Size(690, 27);
            tbFirefoxInstallationLocation.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.Location = new Point(30, 154);
            label4.Name = "label4";
            label4.Size = new Size(195, 20);
            label4.TabIndex = 5;
            label4.Text = "Firefox Installation Location:";
            // 
            // comboPreferredBrowser
            // 
            comboPreferredBrowser.Font = new Font("Segoe UI", 9F);
            comboPreferredBrowser.FormattingEnabled = true;
            comboPreferredBrowser.Location = new Point(46, 63);
            comboPreferredBrowser.Name = "comboPreferredBrowser";
            comboPreferredBrowser.Size = new Size(272, 28);
            comboPreferredBrowser.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F);
            label5.Location = new Point(30, 37);
            label5.Name = "label5";
            label5.Size = new Size(248, 20);
            label5.TabIndex = 8;
            label5.Text = "Preferred browser for opening links::";
            // 
            // cbPrivateMode
            // 
            cbPrivateMode.AutoSize = true;
            cbPrivateMode.Checked = true;
            cbPrivateMode.CheckState = CheckState.Checked;
            cbPrivateMode.Font = new Font("Segoe UI", 9F);
            cbPrivateMode.Location = new Point(30, 108);
            cbPrivateMode.Name = "cbPrivateMode";
            cbPrivateMode.Size = new Size(304, 24);
            cbPrivateMode.TabIndex = 10;
            cbPrivateMode.Text = "Open Browser in Private/Incognito Mode:";
            cbPrivateMode.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.Location = new Point(30, 36);
            label6.Name = "label6";
            label6.Size = new Size(163, 20);
            label6.TabIndex = 12;
            label6.Text = "Preferred video quality:";
            // 
            // comboPreferredVideoQuality
            // 
            comboPreferredVideoQuality.FlatStyle = FlatStyle.System;
            comboPreferredVideoQuality.Font = new Font("Segoe UI", 9F);
            comboPreferredVideoQuality.FormattingEnabled = true;
            comboPreferredVideoQuality.Location = new Point(46, 62);
            comboPreferredVideoQuality.Name = "comboPreferredVideoQuality";
            comboPreferredVideoQuality.Size = new Size(272, 28);
            comboPreferredVideoQuality.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.Location = new Point(30, 106);
            label7.Name = "label7";
            label7.Size = new Size(161, 20);
            label7.TabIndex = 14;
            label7.Text = "Fall-back video quality:";
            // 
            // comboFallbackVideoQuality
            // 
            comboFallbackVideoQuality.Font = new Font("Segoe UI", 9F);
            comboFallbackVideoQuality.FormattingEnabled = true;
            comboFallbackVideoQuality.Location = new Point(46, 132);
            comboFallbackVideoQuality.Name = "comboFallbackVideoQuality";
            comboFallbackVideoQuality.Size = new Size(272, 28);
            comboFallbackVideoQuality.TabIndex = 13;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(comboPreferredVideoQuality);
            groupBox1.Controls.Add(comboFallbackVideoQuality);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(20, 505);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(773, 188);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Video Quality Preferences";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(comboPreferredBrowser);
            groupBox2.Controls.Add(cbPrivateMode);
            groupBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox2.Location = new Point(20, 329);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(773, 154);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            groupBox2.Text = "Opening Links Preferences";
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(tbEdgeInstallationLocation);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(tbFirefoxInstallationLocation);
            groupBox3.Controls.Add(tbChromeInstallationLocation);
            groupBox3.Controls.Add(label4);
            groupBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox3.Location = new Point(20, 71);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(773, 236);
            groupBox3.TabIndex = 17;
            groupBox3.TabStop = false;
            groupBox3.Text = "Browser Install Locations";
            // 
            // tbEdgeInstallationLocation
            // 
            tbEdgeInstallationLocation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbEdgeInstallationLocation.Font = new Font("Segoe UI", 9F);
            tbEdgeInstallationLocation.Location = new Point(46, 62);
            tbEdgeInstallationLocation.Name = "tbEdgeInstallationLocation";
            tbEdgeInstallationLocation.Size = new Size(690, 27);
            tbEdgeInstallationLocation.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(5, 9);
            label1.Name = "label1";
            label1.Size = new Size(382, 41);
            label1.TabIndex = 18;
            label1.Text = "Hub Downloader Settings";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label8.BackColor = Color.Black;
            label8.Location = new Point(5, 52);
            label8.Margin = new Padding(0);
            label8.MaximumSize = new Size(0, 2);
            label8.MinimumSize = new Size(0, 2);
            label8.Name = "label8";
            label8.Size = new Size(804, 2);
            label8.TabIndex = 19;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(567, 705);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 32);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(683, 705);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(110, 32);
            btnOk.TabIndex = 20;
            btnOk.Text = "&OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(818, 749);
            ControlBox = false;
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(label8);
            Controls.Add(label1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Hub Downloader Application Settings";
            TopMost = true;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private TextBox tbChromeInstallationLocation;
        private Label label3;
        private TextBox tbFirefoxInstallationLocation;
        private Label label4;
        private ComboBox comboPreferredBrowser;
        private Label label5;
        private CheckBox cbPrivateMode;
        private Label label6;
        private ComboBox comboPreferredVideoQuality;
        private Label label7;
        private ComboBox comboFallbackVideoQuality;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private TextBox tbEdgeInstallationLocation;
        private Label label1;
        private Label label8;
        private Button btnCancel;
        private Button btnOk;
    }
}