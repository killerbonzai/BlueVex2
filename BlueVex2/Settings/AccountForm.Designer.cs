namespace BlueVex2.Settings
{
    partial class AccountForm
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
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.CharacterSlotCheckBox = new System.Windows.Forms.ComboBox();
            this.MasterCheckBox = new System.Windows.Forms.CheckBox();
            this.RealmCheckBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Location = new System.Drawing.Point(81, 10);
            this.UsernameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UsernameTextBox.Size = new System.Drawing.Size(182, 20);
            this.UsernameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.PasswordTextBox.Location = new System.Drawing.Point(81, 32);
            this.PasswordTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PasswordTextBox.Size = new System.Drawing.Size(182, 20);
            this.PasswordTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 58);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Realm";
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(206, 105);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(56, 19);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(146, 105);
            this.OKButton.Margin = new System.Windows.Forms.Padding(2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(56, 19);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 82);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Character";
            // 
            // CharacterSlotCheckBox
            // 
            this.CharacterSlotCheckBox.FormattingEnabled = true;
            this.CharacterSlotCheckBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.CharacterSlotCheckBox.Location = new System.Drawing.Point(81, 80);
            this.CharacterSlotCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.CharacterSlotCheckBox.Name = "CharacterSlotCheckBox";
            this.CharacterSlotCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CharacterSlotCheckBox.Size = new System.Drawing.Size(182, 21);
            this.CharacterSlotCheckBox.TabIndex = 3;
            this.CharacterSlotCheckBox.Text = "1";
            // 
            // MasterCheckBox
            // 
            this.MasterCheckBox.Location = new System.Drawing.Point(9, 105);
            this.MasterCheckBox.Name = "MasterCheckBox";
            this.MasterCheckBox.Size = new System.Drawing.Size(85, 19);
            this.MasterCheckBox.TabIndex = 4;
            this.MasterCheckBox.Text = "Master";
            this.MasterCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MasterCheckBox.UseVisualStyleBackColor = true;
            // 
            // RealmCheckBox
            // 
            this.RealmCheckBox.AllowDrop = true;
            this.RealmCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::BlueVex2.Properties.Settings.Default, "StandardGateway", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.RealmCheckBox.FormattingEnabled = true;
            this.RealmCheckBox.Items.AddRange(new object[] {
            "asia.battle.net",
            "europe.battle.net",
            "useast.battle.net",
            "uswest.battle.net",
            "classicbeta.battle.net"});
            this.RealmCheckBox.Location = new System.Drawing.Point(81, 55);
            this.RealmCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.RealmCheckBox.Name = "RealmCheckBox";
            this.RealmCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RealmCheckBox.Size = new System.Drawing.Size(182, 21);
            this.RealmCheckBox.TabIndex = 2;
            this.RealmCheckBox.Text = global::BlueVex2.Properties.Settings.Default.StandardGateway;
            // 
            // AccountForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(271, 135);
            this.ControlBox = false;
            this.Controls.Add(this.MasterCheckBox);
            this.Controls.Add(this.CharacterSlotCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.RealmCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UsernameTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Account";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox RealmCheckBox;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox MasterCheckBox;
        private System.Windows.Forms.ComboBox CharacterSlotCheckBox;
    }
}