namespace BlueVex2
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.InstallationsListView = new System.Windows.Forms.ListView();
            this.chKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDefaultAccount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.CorePanel = new System.Windows.Forms.Panel();
            this.ResolutionLabel = new System.Windows.Forms.Label();
            this.InstallationsPanel = new System.Windows.Forms.Panel();
            this.EditInstallButton = new System.Windows.Forms.Button();
            this.RemoveInstallButton = new System.Windows.Forms.Button();
            this.AddInstallButton = new System.Windows.Forms.Button();
            this.AccountsPanel = new System.Windows.Forms.Panel();
            this.EditAccountButton = new System.Windows.Forms.Button();
            this.RemoveAccountButton = new System.Windows.Forms.Button();
            this.AddAccountButton = new System.Windows.Forms.Button();
            this.AccountsListView = new System.Windows.Forms.ListView();
            this.chUsername = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPassword = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCharSlot = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMaster = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRealm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GamesPanel = new System.Windows.Forms.Panel();
            this.EditGamesButton = new System.Windows.Forms.Button();
            this.RemoveGamesButton = new System.Windows.Forms.Button();
            this.AddGamesButton = new System.Windows.Forms.Button();
            this.GamesListView = new System.Windows.Forms.ListView();
            this.chGame = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGamePW = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ResolutionUpDown = new System.Windows.Forms.NumericUpDown();
            this.AutoLoginCheckBox = new System.Windows.Forms.CheckBox();
            this.ProxyUseCheckBox = new System.Windows.Forms.CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.CorePanel.SuspendLayout();
            this.InstallationsPanel.SuspendLayout();
            this.AccountsPanel.SuspendLayout();
            this.GamesPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResolutionUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // InstallationsListView
            // 
            this.InstallationsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chKey,
            this.chDefaultAccount,
            this.chPath});
            this.InstallationsListView.FullRowSelect = true;
            this.InstallationsListView.GridLines = true;
            this.InstallationsListView.HideSelection = false;
            this.InstallationsListView.Location = new System.Drawing.Point(0, 0);
            this.InstallationsListView.Margin = new System.Windows.Forms.Padding(2);
            this.InstallationsListView.MultiSelect = false;
            this.InstallationsListView.Name = "InstallationsListView";
            this.InstallationsListView.Size = new System.Drawing.Size(521, 277);
            this.InstallationsListView.TabIndex = 0;
            this.InstallationsListView.UseCompatibleStateImageBehavior = false;
            this.InstallationsListView.View = System.Windows.Forms.View.Details;
            this.InstallationsListView.SelectedIndexChanged += new System.EventHandler(this.InstallationsListView_SelectedIndexChanged);
            this.InstallationsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InstallationsListView_KeyDown);
            // 
            // chKey
            // 
            this.chKey.Text = "Key Name";
            this.chKey.Width = 101;
            // 
            // chDefaultAccount
            // 
            this.chDefaultAccount.Text = "Default Account";
            this.chDefaultAccount.Width = 120;
            // 
            // chPath
            // 
            this.chPath.Text = "Diablo 2 Path";
            this.chPath.Width = 297;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Core",
            "Installations",
            "Accounts",
            "Games"});
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(115, 311);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(527, 327);
            this.OKButton.Margin = new System.Windows.Forms.Padding(2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(56, 19);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(588, 327);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(56, 19);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(9, 10);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.CorePanel);
            this.splitContainer1.Panel2.Controls.Add(this.InstallationsPanel);
            this.splitContainer1.Panel2.Controls.Add(this.AccountsPanel);
            this.splitContainer1.Panel2.Controls.Add(this.GamesPanel);
            this.splitContainer1.Size = new System.Drawing.Size(639, 311);
            this.splitContainer1.SplitterDistance = 115;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 4;
            // 
            // CorePanel
            // 
            this.CorePanel.Controls.Add(this.ResolutionUpDown);
            this.CorePanel.Controls.Add(this.ResolutionLabel);
            this.CorePanel.Controls.Add(this.AutoLoginCheckBox);
            this.CorePanel.Controls.Add(this.ProxyUseCheckBox);
            this.CorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CorePanel.Location = new System.Drawing.Point(0, 0);
            this.CorePanel.Margin = new System.Windows.Forms.Padding(2);
            this.CorePanel.Name = "CorePanel";
            this.CorePanel.Size = new System.Drawing.Size(521, 311);
            this.CorePanel.TabIndex = 1;
            // 
            // ResolutionLabel
            // 
            this.ResolutionLabel.AutoSize = true;
            this.ResolutionLabel.Location = new System.Drawing.Point(4, 52);
            this.ResolutionLabel.Name = "ResolutionLabel";
            this.ResolutionLabel.Size = new System.Drawing.Size(157, 13);
            this.ResolutionLabel.TabIndex = 2;
            this.ResolutionLabel.Text = "Choose Game Resolution Scale";
            // 
            // InstallationsPanel
            // 
            this.InstallationsPanel.Controls.Add(this.EditInstallButton);
            this.InstallationsPanel.Controls.Add(this.InstallationsListView);
            this.InstallationsPanel.Controls.Add(this.RemoveInstallButton);
            this.InstallationsPanel.Controls.Add(this.AddInstallButton);
            this.InstallationsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InstallationsPanel.Location = new System.Drawing.Point(0, 0);
            this.InstallationsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.InstallationsPanel.Name = "InstallationsPanel";
            this.InstallationsPanel.Size = new System.Drawing.Size(521, 311);
            this.InstallationsPanel.TabIndex = 0;
            this.InstallationsPanel.Visible = false;
            // 
            // EditInstallButton
            // 
            this.EditInstallButton.Location = new System.Drawing.Point(123, 281);
            this.EditInstallButton.Margin = new System.Windows.Forms.Padding(2);
            this.EditInstallButton.Name = "EditInstallButton";
            this.EditInstallButton.Size = new System.Drawing.Size(56, 19);
            this.EditInstallButton.TabIndex = 3;
            this.EditInstallButton.Text = "Edit";
            this.EditInstallButton.UseVisualStyleBackColor = true;
            this.EditInstallButton.Click += new System.EventHandler(this.EditInstallButton_Click);
            // 
            // RemoveInstallButton
            // 
            this.RemoveInstallButton.Location = new System.Drawing.Point(63, 281);
            this.RemoveInstallButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveInstallButton.Name = "RemoveInstallButton";
            this.RemoveInstallButton.Size = new System.Drawing.Size(56, 19);
            this.RemoveInstallButton.TabIndex = 2;
            this.RemoveInstallButton.Text = "Remove";
            this.RemoveInstallButton.UseVisualStyleBackColor = true;
            this.RemoveInstallButton.Click += new System.EventHandler(this.RemoveInstallButton_Click);
            // 
            // AddInstallButton
            // 
            this.AddInstallButton.Location = new System.Drawing.Point(3, 281);
            this.AddInstallButton.Margin = new System.Windows.Forms.Padding(2);
            this.AddInstallButton.Name = "AddInstallButton";
            this.AddInstallButton.Size = new System.Drawing.Size(56, 19);
            this.AddInstallButton.TabIndex = 1;
            this.AddInstallButton.Text = "Add";
            this.AddInstallButton.UseVisualStyleBackColor = true;
            this.AddInstallButton.Click += new System.EventHandler(this.AddInstallButton_Click);
            // 
            // AccountsPanel
            // 
            this.AccountsPanel.Controls.Add(this.EditAccountButton);
            this.AccountsPanel.Controls.Add(this.RemoveAccountButton);
            this.AccountsPanel.Controls.Add(this.AddAccountButton);
            this.AccountsPanel.Controls.Add(this.AccountsListView);
            this.AccountsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AccountsPanel.Location = new System.Drawing.Point(0, 0);
            this.AccountsPanel.Margin = new System.Windows.Forms.Padding(2);
            this.AccountsPanel.Name = "AccountsPanel";
            this.AccountsPanel.Size = new System.Drawing.Size(521, 311);
            this.AccountsPanel.TabIndex = 2;
            this.AccountsPanel.Visible = false;
            // 
            // EditAccountButton
            // 
            this.EditAccountButton.Location = new System.Drawing.Point(123, 281);
            this.EditAccountButton.Margin = new System.Windows.Forms.Padding(2);
            this.EditAccountButton.Name = "EditAccountButton";
            this.EditAccountButton.Size = new System.Drawing.Size(56, 19);
            this.EditAccountButton.TabIndex = 4;
            this.EditAccountButton.Text = "Edit";
            this.EditAccountButton.UseVisualStyleBackColor = true;
            this.EditAccountButton.Click += new System.EventHandler(this.EditAccountButton_Click);
            // 
            // RemoveAccountButton
            // 
            this.RemoveAccountButton.Location = new System.Drawing.Point(63, 281);
            this.RemoveAccountButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveAccountButton.Name = "RemoveAccountButton";
            this.RemoveAccountButton.Size = new System.Drawing.Size(56, 19);
            this.RemoveAccountButton.TabIndex = 2;
            this.RemoveAccountButton.Text = "Remove";
            this.RemoveAccountButton.UseVisualStyleBackColor = true;
            this.RemoveAccountButton.Click += new System.EventHandler(this.RemoveAccountButton_Click);
            // 
            // AddAccountButton
            // 
            this.AddAccountButton.Location = new System.Drawing.Point(3, 281);
            this.AddAccountButton.Margin = new System.Windows.Forms.Padding(2);
            this.AddAccountButton.Name = "AddAccountButton";
            this.AddAccountButton.Size = new System.Drawing.Size(56, 19);
            this.AddAccountButton.TabIndex = 1;
            this.AddAccountButton.Text = "Add";
            this.AddAccountButton.UseVisualStyleBackColor = true;
            this.AddAccountButton.Click += new System.EventHandler(this.AddAccountButton_Click);
            // 
            // AccountsListView
            // 
            this.AccountsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccountsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chUsername,
            this.chPassword,
            this.chCharSlot,
            this.chMaster,
            this.chRealm});
            this.AccountsListView.FullRowSelect = true;
            this.AccountsListView.GridLines = true;
            this.AccountsListView.Location = new System.Drawing.Point(0, 0);
            this.AccountsListView.Margin = new System.Windows.Forms.Padding(2);
            this.AccountsListView.Name = "AccountsListView";
            this.AccountsListView.Size = new System.Drawing.Size(521, 277);
            this.AccountsListView.TabIndex = 0;
            this.AccountsListView.UseCompatibleStateImageBehavior = false;
            this.AccountsListView.View = System.Windows.Forms.View.Details;
            this.AccountsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AccountsListView_KeyDown);
            // 
            // chUsername
            // 
            this.chUsername.Text = "Username";
            this.chUsername.Width = 121;
            // 
            // chPassword
            // 
            this.chPassword.Text = "Password";
            this.chPassword.Width = 125;
            // 
            // chCharSlot
            // 
            this.chCharSlot.Text = "Slot";
            this.chCharSlot.Width = 30;
            // 
            // chMaster
            // 
            this.chMaster.Text = "Master";
            // 
            // chRealm
            // 
            this.chRealm.Text = "Realm";
            this.chRealm.Width = 257;
            // 
            // GamesPanel
            // 
            this.GamesPanel.Controls.Add(this.EditGamesButton);
            this.GamesPanel.Controls.Add(this.RemoveGamesButton);
            this.GamesPanel.Controls.Add(this.AddGamesButton);
            this.GamesPanel.Controls.Add(this.GamesListView);
            this.GamesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GamesPanel.Location = new System.Drawing.Point(0, 0);
            this.GamesPanel.Margin = new System.Windows.Forms.Padding(2);
            this.GamesPanel.Name = "GamesPanel";
            this.GamesPanel.Size = new System.Drawing.Size(521, 311);
            this.GamesPanel.TabIndex = 6;
            this.GamesPanel.Visible = false;
            // 
            // EditGamesButton
            // 
            this.EditGamesButton.Location = new System.Drawing.Point(123, 281);
            this.EditGamesButton.Margin = new System.Windows.Forms.Padding(2);
            this.EditGamesButton.Name = "EditGamesButton";
            this.EditGamesButton.Size = new System.Drawing.Size(56, 19);
            this.EditGamesButton.TabIndex = 4;
            this.EditGamesButton.Text = "Edit";
            this.EditGamesButton.UseVisualStyleBackColor = true;
            this.EditGamesButton.Click += new System.EventHandler(this.EditGamesButton_Click);
            // 
            // RemoveGamesButton
            // 
            this.RemoveGamesButton.Location = new System.Drawing.Point(63, 281);
            this.RemoveGamesButton.Margin = new System.Windows.Forms.Padding(2);
            this.RemoveGamesButton.Name = "RemoveGamesButton";
            this.RemoveGamesButton.Size = new System.Drawing.Size(56, 19);
            this.RemoveGamesButton.TabIndex = 2;
            this.RemoveGamesButton.Text = "Remove";
            this.RemoveGamesButton.UseVisualStyleBackColor = true;
            this.RemoveGamesButton.Click += new System.EventHandler(this.RemoveGamesButton_Click);
            // 
            // AddGamesButton
            // 
            this.AddGamesButton.Location = new System.Drawing.Point(3, 281);
            this.AddGamesButton.Margin = new System.Windows.Forms.Padding(2);
            this.AddGamesButton.Name = "AddGamesButton";
            this.AddGamesButton.Size = new System.Drawing.Size(56, 19);
            this.AddGamesButton.TabIndex = 1;
            this.AddGamesButton.Text = "Add";
            this.AddGamesButton.UseVisualStyleBackColor = true;
            this.AddGamesButton.Click += new System.EventHandler(this.AddGamesButton_Click);
            // 
            // GamesListView
            // 
            this.GamesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chGame,
            this.chGamePW});
            this.GamesListView.FullRowSelect = true;
            this.GamesListView.GridLines = true;
            this.GamesListView.HideSelection = false;
            this.GamesListView.Location = new System.Drawing.Point(0, 0);
            this.GamesListView.Margin = new System.Windows.Forms.Padding(2);
            this.GamesListView.MultiSelect = false;
            this.GamesListView.Name = "GamesListView";
            this.GamesListView.Size = new System.Drawing.Size(521, 277);
            this.GamesListView.TabIndex = 0;
            this.GamesListView.UseCompatibleStateImageBehavior = false;
            this.GamesListView.View = System.Windows.Forms.View.Details;
            this.GamesListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GamesListView_KeyDown);
            // 
            // chGame
            // 
            this.chGame.Text = "Game Name";
            this.chGame.Width = 101;
            // 
            // chGamePW
            // 
            this.chGamePW.Text = "Game Password";
            this.chGamePW.Width = 120;
            // 
            // ResolutionUpDown
            // 
            this.ResolutionUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::BlueVex2.Properties.Settings.Default, "ResolutionScale", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ResolutionUpDown.DecimalPlaces = 2;
            this.ResolutionUpDown.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.ResolutionUpDown.Location = new System.Drawing.Point(27, 69);
            this.ResolutionUpDown.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            this.ResolutionUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ResolutionUpDown.Name = "ResolutionUpDown";
            this.ResolutionUpDown.Size = new System.Drawing.Size(51, 20);
            this.ResolutionUpDown.TabIndex = 3;
            this.ResolutionUpDown.Value = global::BlueVex2.Properties.Settings.Default.ResolutionScale;
            // 
            // AutoLoginCheckBox
            // 
            this.AutoLoginCheckBox.AutoSize = true;
            this.AutoLoginCheckBox.Checked = global::BlueVex2.Properties.Settings.Default.UseBV2AutoLogin;
            this.AutoLoginCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::BlueVex2.Properties.Settings.Default, "UseBV2AutoLogin", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AutoLoginCheckBox.Location = new System.Drawing.Point(4, 28);
            this.AutoLoginCheckBox.Name = "AutoLoginCheckBox";
            this.AutoLoginCheckBox.Size = new System.Drawing.Size(96, 17);
            this.AutoLoginCheckBox.TabIndex = 1;
            this.AutoLoginCheckBox.Text = "Use AutoLogin";
            this.AutoLoginCheckBox.UseVisualStyleBackColor = true;
            // 
            // ProxyUseCheckBox
            // 
            this.ProxyUseCheckBox.AutoSize = true;
            this.ProxyUseCheckBox.Checked = global::BlueVex2.Properties.Settings.Default.UseBV2Proxy;
            this.ProxyUseCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::BlueVex2.Properties.Settings.Default, "UseBV2Proxy", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ProxyUseCheckBox.Location = new System.Drawing.Point(4, 4);
            this.ProxyUseCheckBox.Name = "ProxyUseCheckBox";
            this.ProxyUseCheckBox.Size = new System.Drawing.Size(262, 17);
            this.ProxyUseCheckBox.TabIndex = 0;
            this.ProxyUseCheckBox.Text = "Use BlueVex2 Proxies (requires application restart)";
            this.ProxyUseCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(653, 353);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.CorePanel.ResumeLayout(false);
            this.CorePanel.PerformLayout();
            this.InstallationsPanel.ResumeLayout(false);
            this.AccountsPanel.ResumeLayout(false);
            this.GamesPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResolutionUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView InstallationsListView;
        private System.Windows.Forms.ColumnHeader chKey;
        private System.Windows.Forms.ColumnHeader chPath;
        private System.Windows.Forms.ColumnHeader chDefaultAccount;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel AccountsPanel;
        private System.Windows.Forms.Panel CorePanel;
        private System.Windows.Forms.Panel InstallationsPanel;
        private System.Windows.Forms.Button RemoveInstallButton;
        private System.Windows.Forms.Button AddInstallButton;
        private System.Windows.Forms.ListView AccountsListView;
        private System.Windows.Forms.ColumnHeader chUsername;
        private System.Windows.Forms.ColumnHeader chRealm;
        private System.Windows.Forms.Button RemoveAccountButton;
        private System.Windows.Forms.Button AddAccountButton;
        private System.Windows.Forms.ColumnHeader chCharSlot;
        private System.Windows.Forms.ColumnHeader chPassword;
        private System.Windows.Forms.ColumnHeader chMaster;
        private System.Windows.Forms.Panel GamesPanel;
        private System.Windows.Forms.Button RemoveGamesButton;
        private System.Windows.Forms.Button AddGamesButton;
        private System.Windows.Forms.ListView GamesListView;
        private System.Windows.Forms.ColumnHeader chGame;
        private System.Windows.Forms.ColumnHeader chGamePW;
        private System.Windows.Forms.CheckBox ProxyUseCheckBox;
        private System.Windows.Forms.CheckBox AutoLoginCheckBox;
        private System.Windows.Forms.NumericUpDown ResolutionUpDown;
        private System.Windows.Forms.Label ResolutionLabel;
        private System.Windows.Forms.Button EditInstallButton;
        private System.Windows.Forms.Button EditAccountButton;
        private System.Windows.Forms.Button EditGamesButton;
    }
}