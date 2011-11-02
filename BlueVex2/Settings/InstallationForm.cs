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
    public partial class InstallationForm : Form
    {
        public InstallationForm(string itemString)
        {
            InitializeComponent();
            
            if (itemString != "")
            {
                string[] parts = itemString.Split(',');
                if (parts.Length == 3)
                {
                    KeyNameTextBox.Text = parts[0];
                    AccountComboBox.Text = parts[1];
                    PathTextBox.Text = parts[2];
                }
            }

            this.accListview.Items.Clear();
            foreach (string accountString in BlueVex2.Properties.Settings.Default.Accounts)
            {
                string[] parts = accountString.Split(',');
                if (parts.Length == 5)
                {
                    ListViewItem item = new ListViewItem(parts[0]);
                    this.accListview.Items.Add(item);
                }
            }
        }

        public string KeyName
        {
            get
            {
                return this.KeyNameTextBox.Text;
            }
        }

        public string Path
        {
            get
            {
                return this.PathTextBox.Text;
            }
        }

        public string Account
        {
            get
            {
                return this.AccountComboBox.Text;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.PathTextBox.Text = this.openFileDialog1.FileName;
            }
        }

        private void accListview_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountComboBox.Text = accListview.FocusedItem.Text;
        }

    }
}
