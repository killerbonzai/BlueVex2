using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlueVex2.Settings
{
    public partial class AccountForm : Form
    {
        public string Username
        {
            get
            {
                return this.UsernameTextBox.Text;
            }
        }

        public string Password
        {
            get
            {
                return this.PasswordTextBox.Text;
            }
        }

        public string Realm
        {
            get
            {
                return this.RealmCheckBox.Text;
            }
        }

        public string CharSlot
        {
            get
            {
                return this.CharacterSlotCheckBox.Text;
            }
        }
        public string Master
        {
            get
            {
                return this.MasterCheckBox.Checked.ToString();
            }
        }

        public AccountForm(string itemString)
        {
            InitializeComponent();

            if (itemString != "")
            {
                string[] parts = itemString.Split(',');
                if (parts.Length == 5)
                {
                    //username password slot master realm
                    UsernameTextBox.Text = parts[0];
                    PasswordTextBox.Text = parts[1];
                    CharacterSlotCheckBox.Text = parts[2];
                    if (parts[3].ToString() == "True")
                    {
                        MasterCheckBox.Checked = true;
                    }
                    RealmCheckBox.Text = parts[4];
                }
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
