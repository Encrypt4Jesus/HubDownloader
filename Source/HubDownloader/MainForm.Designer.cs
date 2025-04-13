namespace HubDownloader
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            tbInput_ViewKeys = new TextBox();
            label1 = new Label();
            flpOutput_Links = new FlowLayoutPanel();
            contextMenuFLP = new ContextMenuStrip(components);
            clearToolStripMenuItem = new ToolStripMenuItem();
            linkLabel1 = new LinkLabel();
            linkContextMenu = new ContextMenuStrip(components);
            toolStripMenuOpenLink = new ToolStripMenuItem();
            toolStripMenuCopyLink = new ToolStripMenuItem();
            renewLinkToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            label4 = new Label();
            label5 = new Label();
            richTextBox_Example = new RichTextBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btbSettings = new Button();
            timerInputToQueueProcessor = new System.Windows.Forms.Timer(components);
            panelBottom = new Panel();
            panelTop = new Panel();
            panelProcessing = new Panel();
            progressBarProcessing = new ProgressBar();
            labelProcessing = new Label();
            timerQueueProcessor = new System.Windows.Forms.Timer(components);
            toolTip = new ToolTip(components);
            flpOutput_Links.SuspendLayout();
            contextMenuFLP.SuspendLayout();
            linkContextMenu.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panelBottom.SuspendLayout();
            panelTop.SuspendLayout();
            panelProcessing.SuspendLayout();
            SuspendLayout();
            // 
            // tbInput_ViewKeys
            // 
            tbInput_ViewKeys.BorderStyle = BorderStyle.FixedSingle;
            tbInput_ViewKeys.Location = new Point(42, 43);
            tbInput_ViewKeys.Name = "tbInput_ViewKeys";
            tbInput_ViewKeys.Size = new Size(251, 29);
            tbInput_ViewKeys.TabIndex = 0;
            tbInput_ViewKeys.TextAlign = HorizontalAlignment.Center;
            tbInput_ViewKeys.TextChanged += tbInput_ViewKeys_TextChanged;
            tbInput_ViewKeys.KeyDown += tbInput_ViewKeys_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 8);
            label1.Name = "label1";
            label1.Size = new Size(250, 21);
            label1.TabIndex = 1;
            label1.Text = "1). Place the 'viewkey' into the box:";
            // 
            // flpOutput_Links
            // 
            flpOutput_Links.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpOutput_Links.AutoScroll = true;
            flpOutput_Links.BackColor = Color.FromArgb(247, 251, 253);
            flpOutput_Links.BorderStyle = BorderStyle.FixedSingle;
            flpOutput_Links.ContextMenuStrip = contextMenuFLP;
            flpOutput_Links.Controls.Add(linkLabel1);
            flpOutput_Links.Location = new Point(42, 38);
            flpOutput_Links.Name = "flpOutput_Links";
            flpOutput_Links.Size = new Size(989, 144);
            flpOutput_Links.TabIndex = 2;
            // 
            // contextMenuFLP
            // 
            contextMenuFLP.ImageScalingSize = new Size(20, 20);
            contextMenuFLP.Items.AddRange(new ToolStripItem[] { clearToolStripMenuItem });
            contextMenuFLP.Name = "contextMenuFLP";
            contextMenuFLP.Size = new Size(117, 30);
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(116, 26);
            clearToolStripMenuItem.Text = "Clear";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.ContextMenuStrip = linkContextMenu;
            linkLabel1.Location = new Point(11, 10);
            linkLabel1.Margin = new Padding(11, 10, 11, 10);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(81, 21);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "linkLabel1";
            // 
            // linkContextMenu
            // 
            linkContextMenu.ImageScalingSize = new Size(20, 20);
            linkContextMenu.Items.AddRange(new ToolStripItem[] { toolStripMenuOpenLink, toolStripMenuCopyLink, renewLinkToolStripMenuItem, removeToolStripMenuItem });
            linkContextMenu.Name = "linkContextMenu";
            linkContextMenu.ShowImageMargin = false;
            linkContextMenu.Size = new Size(136, 108);
            // 
            // toolStripMenuOpenLink
            // 
            toolStripMenuOpenLink.Name = "toolStripMenuOpenLink";
            toolStripMenuOpenLink.Size = new Size(135, 26);
            toolStripMenuOpenLink.Text = "&Open...";
            toolStripMenuOpenLink.ToolTipText = "Open link with configured browser";
            toolStripMenuOpenLink.Click += toolStripMenuOpenLink_Click;
            // 
            // toolStripMenuCopyLink
            // 
            toolStripMenuCopyLink.Name = "toolStripMenuCopyLink";
            toolStripMenuCopyLink.Size = new Size(135, 26);
            toolStripMenuCopyLink.Text = "&Copy URL";
            toolStripMenuCopyLink.ToolTipText = "Copy link address to the clipboard";
            toolStripMenuCopyLink.Click += toolStripMenuCopyLink_Click;
            // 
            // renewLinkToolStripMenuItem
            // 
            renewLinkToolStripMenuItem.Name = "renewLinkToolStripMenuItem";
            renewLinkToolStripMenuItem.Size = new Size(135, 26);
            renewLinkToolStripMenuItem.Text = "Re&new Link";
            renewLinkToolStripMenuItem.ToolTipText = "Grabs a new URL from the server. Use this if the link expires";
            renewLinkToolStripMenuItem.Click += renewLinkToolStripMenuItem_Click;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(135, 26);
            removeToolStripMenuItem.Text = "&Remove";
            removeToolStripMenuItem.ToolTipText = "Remove this link from the list control";
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(3, 3);
            label4.Margin = new Padding(3);
            label4.Name = "label4";
            label4.Size = new Size(645, 21);
            label4.TabIndex = 7;
            label4.Text = "NOTE: The viewkey is just the unique part of the URL., denoted in bold in the example below:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 4);
            label5.Name = "label5";
            label5.Size = new Size(765, 21);
            label5.TabIndex = 8;
            label5.Text = "2). A click-able URL wil appear below:    (Since the browser downloads files much faster, we will leverage that)";
            // 
            // richTextBox_Example
            // 
            richTextBox_Example.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox_Example.BorderStyle = BorderStyle.None;
            richTextBox_Example.Location = new Point(56, 37);
            richTextBox_Example.Margin = new Padding(56, 10, 11, 10);
            richTextBox_Example.MinimumSize = new Size(405, 21);
            richTextBox_Example.Name = "richTextBox_Example";
            richTextBox_Example.ReadOnly = true;
            richTextBox_Example.Size = new Size(584, 21);
            richTextBox_Example.TabIndex = 9;
            richTextBox_Example.Text = "...hub.com/view_video.php?viewkey=TheViewKey123";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.Controls.Add(label4);
            flowLayoutPanel1.Controls.Add(richTextBox_Example);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(320, 49);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(730, 143);
            flowLayoutPanel1.TabIndex = 10;
            // 
            // btbSettings
            // 
            btbSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btbSettings.Location = new Point(889, 6);
            btbSettings.Name = "btbSettings";
            btbSettings.Size = new Size(161, 37);
            btbSettings.TabIndex = 11;
            btbSettings.Text = "Settings...";
            btbSettings.UseVisualStyleBackColor = true;
            btbSettings.Click += btnSettings_Click;
            // 
            // timerInputToQueueProcessor
            // 
            timerInputToQueueProcessor.Interval = 300;
            timerInputToQueueProcessor.Tick += timerInputToQueueProcessor_Tick;
            // 
            // panelBottom
            // 
            panelBottom.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBottom.Controls.Add(label5);
            panelBottom.Controls.Add(flpOutput_Links);
            panelBottom.Location = new Point(7, 219);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(1059, 198);
            panelBottom.TabIndex = 12;
            // 
            // panelTop
            // 
            panelTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelTop.Controls.Add(panelProcessing);
            panelTop.Controls.Add(flowLayoutPanel1);
            panelTop.Controls.Add(btbSettings);
            panelTop.Controls.Add(tbInput_ViewKeys);
            panelTop.Controls.Add(label1);
            panelTop.Location = new Point(7, 6);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1059, 206);
            panelTop.TabIndex = 13;
            // 
            // panelProcessing
            // 
            panelProcessing.Controls.Add(progressBarProcessing);
            panelProcessing.Controls.Add(labelProcessing);
            panelProcessing.Location = new Point(9, 80);
            panelProcessing.Name = "panelProcessing";
            panelProcessing.Size = new Size(304, 83);
            panelProcessing.TabIndex = 12;
            // 
            // progressBarProcessing
            // 
            progressBarProcessing.Location = new Point(25, 38);
            progressBarProcessing.Minimum = 20;
            progressBarProcessing.Name = "progressBarProcessing";
            progressBarProcessing.Size = new Size(266, 28);
            progressBarProcessing.Step = 30;
            progressBarProcessing.Style = ProgressBarStyle.Marquee;
            progressBarProcessing.TabIndex = 1;
            progressBarProcessing.Value = 20;
            // 
            // labelProcessing
            // 
            labelProcessing.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelProcessing.Font = new Font("Segoe UI Variable Text Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelProcessing.Location = new Point(20, 6);
            labelProcessing.Name = "labelProcessing";
            labelProcessing.Size = new Size(282, 20);
            labelProcessing.TabIndex = 0;
            labelProcessing.Text = "Processing {0}...";
            labelProcessing.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // timerQueueProcessor
            // 
            timerQueueProcessor.Interval = 2000;
            timerQueueProcessor.Tick += timerQueueProcessor_Tick;
            // 
            // toolTip
            // 
            toolTip.ShowAlways = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1069, 423);
            Controls.Add(panelTop);
            Controls.Add(panelBottom);
            MinimumSize = new Size(875, 470);
            Name = "MainForm";
            Text = "Hub Downloader";
            flpOutput_Links.ResumeLayout(false);
            flpOutput_Links.PerformLayout();
            contextMenuFLP.ResumeLayout(false);
            linkContextMenu.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panelBottom.ResumeLayout(false);
            panelBottom.PerformLayout();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelProcessing.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox tbInput_ViewKeys;
        private Label label1;
        private FlowLayoutPanel flpOutput_Links;
        private Label label4;
        private Label label5;
        private RichTextBox richTextBox_Example;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btbSettings;
        private System.Windows.Forms.Timer timerInputToQueueProcessor;
        private Panel panelBottom;
        private Panel panelTop;
        private System.Windows.Forms.Timer timerQueueProcessor;
        private ContextMenuStrip linkContextMenu;
        private ToolStripMenuItem toolStripMenuOpenLink;
        private ToolStripMenuItem toolStripMenuCopyLink;
        private ToolTip toolTip;
        private LinkLabel linkLabel1;
        private ContextMenuStrip contextMenuFLP;
        private ToolStripMenuItem clearToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
        private Panel panelProcessing;
        private Label labelProcessing;
        private ProgressBar progressBarProcessing;
        private ToolStripMenuItem renewLinkToolStripMenuItem;
    }
}
