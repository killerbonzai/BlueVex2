namespace BlueVex2.Settings
{
    partial class InstallationForm
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
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.KeyNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.AccountComboBox = new System.Windows.Forms.ComboBox();
            this.accListview = new System.Windows.Forms.ListView();
            this.chAcc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(149, 255);
            this.OKButton.Margin = new System.Windows.Forms.Padding(2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(56, 19);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(210, 254);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(56, 19);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(244, 32);
            this.BrowseButton.Margin = new System.Windows.Forms.Padding(2);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(21, 19);
            this.BrowseButton.TabIndex = 2;
            this.BrowseButton.Text = "...";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // KeyNameTextBox
            // 
            this.KeyNameTextBox.Location = new System.Drawing.Point(94, 10);
            this.KeyNameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.KeyNameTextBox.Name = "KeyNameTextBox";
            this.KeyNameTextBox.Size = new System.Drawing.Size(172, 20);
            this.KeyNameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Key Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "exe Path";
            // 
            // PathTextBox
            // 
            this.PathTextBox.Location = new System.Drawing.Point(94, 32);
            this.PathTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(146, 20);
            this.PathTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Default Account";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "Game.exe";
            this.openFileDialog1.Filter = "Diablo 2 Game.exe|Game.exe";
            this.openFileDialog1.Title = "Please select the game.exe of your diablo 2 installation";
            // 
            // AccountComboBox
            // 
            this.AccountComboBox.FormattingEnabled = true;
            this.AccountComboBox.Location = new System.Drawing.Point(94, 57);
            this.AccountComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.AccountComboBox.Name = "AccountComboBox";
            this.AccountComboBox.Size = new System.Drawing.Size(172, 21);
            this.AccountComboBox.TabIndex = 3;
            // 
            // accListview
            // 
            this.accListview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAcc});
            this.accListview.FullRowSelect = true;
            this.accListview.GridLines = true;
            this.accListview.Location = new System.Drawing.Point(12, 83);
            this.accListview.Name = "accListview";
            this.accListview.ShowGroups = false;
            this.accListview.Size = new System.Drawing.Size(253, 166);
            this.accListview.TabIndex = 4;
            this.accListview.UseCompatibleStateImageBehavior = false;
            this.accListview.View = System.Windows.Forms.View.Details;
            this.accListview.SelectedIndexChanged += new System.EventHandler(this.accListview_SelectedIndexChanged);
            // 
            // chAcc
            // 
            this.chAcc.Text = "Account";
            this.chAcc.Width = 199;
            // 
            // InstallationForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(277, 284);
            this.ControlBox = false;
            this.Controls.Add(this.accListview);
            this.Controls.Add(this.AccountComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KeyNameTextBox);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InstallationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Diablo 2 Installation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.TextBox KeyNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox AccountComboBox;
        private System.Windows.Forms.ListView accListview;
        private System.Windows.Forms.ColumnHeader chAcc;
    }
}