namespace BlueVex2
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAllDiablo2InstallationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normGame1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normGame2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normGame3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nightmareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nightmareGame1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nightmareGame2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nightmareGame3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hellGame1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hellGame2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hellGame3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.everyoneExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitGameFromChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToTraytoolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.allExitANDQuitD2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1276, 848);
            this.tabControl1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameActionsToolStripMenuItem,
            this.minimizeToTraytoolStripMenuItem3});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1276, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadAllDiablo2InstallationsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadAllDiablo2InstallationsToolStripMenuItem
            // 
            this.loadAllDiablo2InstallationsToolStripMenuItem.Name = "loadAllDiablo2InstallationsToolStripMenuItem";
            this.loadAllDiablo2InstallationsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.loadAllDiablo2InstallationsToolStripMenuItem.Text = "Load All Diablo 2 Installations";
            this.loadAllDiablo2InstallationsToolStripMenuItem.Click += new System.EventHandler(this.loadAllDiablo2InstallationsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(226, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(226, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // gameActionsToolStripMenuItem
            // 
            this.gameActionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem,
            this.nightmareToolStripMenuItem,
            this.hellToolStripMenuItem,
            this.toolStripSeparator1,
            this.everyoneExitToolStripMenuItem,
            this.quitGameFromChatToolStripMenuItem,
            this.allExitANDQuitD2ToolStripMenuItem});
            this.gameActionsToolStripMenuItem.Name = "gameActionsToolStripMenuItem";
            this.gameActionsToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.gameActionsToolStripMenuItem.Text = "Game Actions";
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normGame1ToolStripMenuItem,
            this.normGame2ToolStripMenuItem,
            this.normGame3ToolStripMenuItem});
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.normalToolStripMenuItem.Text = "Normal";
            // 
            // normGame1ToolStripMenuItem
            // 
            this.normGame1ToolStripMenuItem.Name = "normGame1ToolStripMenuItem";
            this.normGame1ToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.normGame1ToolStripMenuItem.Text = "Normal Game 1";
            this.normGame1ToolStripMenuItem.Click += new System.EventHandler(this.normGame1ToolStripMenuItem_Click);
            // 
            // normGame2ToolStripMenuItem
            // 
            this.normGame2ToolStripMenuItem.Name = "normGame2ToolStripMenuItem";
            this.normGame2ToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.normGame2ToolStripMenuItem.Text = "Normal Game 2";
            this.normGame2ToolStripMenuItem.Click += new System.EventHandler(this.normGame2ToolStripMenuItem_Click);
            // 
            // normGame3ToolStripMenuItem
            // 
            this.normGame3ToolStripMenuItem.Name = "normGame3ToolStripMenuItem";
            this.normGame3ToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.normGame3ToolStripMenuItem.Text = "Normal Game 3";
            this.normGame3ToolStripMenuItem.Click += new System.EventHandler(this.normGame3ToolStripMenuItem_Click);
            // 
            // nightmareToolStripMenuItem
            // 
            this.nightmareToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nightmareGame1ToolStripMenuItem,
            this.nightmareGame2ToolStripMenuItem,
            this.nightmareGame3ToolStripMenuItem});
            this.nightmareToolStripMenuItem.Name = "nightmareToolStripMenuItem";
            this.nightmareToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.nightmareToolStripMenuItem.Text = "Nightmare";
            // 
            // nightmareGame1ToolStripMenuItem
            // 
            this.nightmareGame1ToolStripMenuItem.Name = "nightmareGame1ToolStripMenuItem";
            this.nightmareGame1ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.nightmareGame1ToolStripMenuItem.Text = "Nightmare Game 1";
            this.nightmareGame1ToolStripMenuItem.Click += new System.EventHandler(this.nightmareGame1ToolStripMenuItem_Click);
            // 
            // nightmareGame2ToolStripMenuItem
            // 
            this.nightmareGame2ToolStripMenuItem.Name = "nightmareGame2ToolStripMenuItem";
            this.nightmareGame2ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.nightmareGame2ToolStripMenuItem.Text = "Nightmare Game 2";
            this.nightmareGame2ToolStripMenuItem.Click += new System.EventHandler(this.nightmareGame2ToolStripMenuItem_Click);
            // 
            // nightmareGame3ToolStripMenuItem
            // 
            this.nightmareGame3ToolStripMenuItem.Name = "nightmareGame3ToolStripMenuItem";
            this.nightmareGame3ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.nightmareGame3ToolStripMenuItem.Text = "Nightmare Game 3";
            this.nightmareGame3ToolStripMenuItem.Click += new System.EventHandler(this.nightmareGame3ToolStripMenuItem_Click);
            // 
            // hellToolStripMenuItem
            // 
            this.hellToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hellGame1ToolStripMenuItem,
            this.hellGame2ToolStripMenuItem,
            this.hellGame3ToolStripMenuItem});
            this.hellToolStripMenuItem.Name = "hellToolStripMenuItem";
            this.hellToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.hellToolStripMenuItem.Text = "Hell";
            // 
            // hellGame1ToolStripMenuItem
            // 
            this.hellGame1ToolStripMenuItem.Name = "hellGame1ToolStripMenuItem";
            this.hellGame1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hellGame1ToolStripMenuItem.Text = "Hell Game 1";
            this.hellGame1ToolStripMenuItem.Click += new System.EventHandler(this.hellGame1ToolStripMenuItem_Click);
            // 
            // hellGame2ToolStripMenuItem
            // 
            this.hellGame2ToolStripMenuItem.Name = "hellGame2ToolStripMenuItem";
            this.hellGame2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hellGame2ToolStripMenuItem.Text = "Hell Game 2";
            this.hellGame2ToolStripMenuItem.Click += new System.EventHandler(this.hellGame2ToolStripMenuItem_Click);
            // 
            // hellGame3ToolStripMenuItem
            // 
            this.hellGame3ToolStripMenuItem.Name = "hellGame3ToolStripMenuItem";
            this.hellGame3ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hellGame3ToolStripMenuItem.Text = "Hell Game 3";
            this.hellGame3ToolStripMenuItem.Click += new System.EventHandler(this.hellGame3ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
            // 
            // everyoneExitToolStripMenuItem
            // 
            this.everyoneExitToolStripMenuItem.Name = "everyoneExitToolStripMenuItem";
            this.everyoneExitToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.everyoneExitToolStripMenuItem.Text = "Everyone Exit";
            this.everyoneExitToolStripMenuItem.Click += new System.EventHandler(this.everyoneExitToolStripMenuItem_Click);
            // 
            // quitGameFromChatToolStripMenuItem
            // 
            this.quitGameFromChatToolStripMenuItem.Name = "quitGameFromChatToolStripMenuItem";
            this.quitGameFromChatToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.quitGameFromChatToolStripMenuItem.Text = "All quit D2 from Chat";
            this.quitGameFromChatToolStripMenuItem.Click += new System.EventHandler(this.quitGameFromChatToolStripMenuItem_Click);
            // 
            // minimizeToTraytoolStripMenuItem3
            // 
            this.minimizeToTraytoolStripMenuItem3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.minimizeToTraytoolStripMenuItem3.Name = "minimizeToTraytoolStripMenuItem3";
            this.minimizeToTraytoolStripMenuItem3.Size = new System.Drawing.Size(105, 20);
            this.minimizeToTraytoolStripMenuItem3.Text = "Minimize to tray";
            this.minimizeToTraytoolStripMenuItem3.Click += new System.EventHandler(this.minimizeToTraytoolStripMenuItem3_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Still running...";
            this.notifyIcon1.BalloonTipTitle = "BlueVex2";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "BlueVex2";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // allExitANDQuitD2ToolStripMenuItem
            // 
            this.allExitANDQuitD2ToolStripMenuItem.Name = "allExitANDQuitD2ToolStripMenuItem";
            this.allExitANDQuitD2ToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.allExitANDQuitD2ToolStripMenuItem.Text = "All exit AND quit D2";
            this.allExitANDQuitD2ToolStripMenuItem.Click += new System.EventHandler(this.allExitANDQuitD2ToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 872);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(940, 720);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BlueVex2 Alpha 1.2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAllDiablo2InstallationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem gameActionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem everyoneExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitGameFromChatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeToTraytoolStripMenuItem3;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normGame1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normGame2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normGame3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nightmareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nightmareGame1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nightmareGame2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nightmareGame3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hellGame1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hellGame2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hellGame3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem allExitANDQuitD2ToolStripMenuItem;
    }
}

