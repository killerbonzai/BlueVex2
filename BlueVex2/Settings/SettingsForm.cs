using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BlueVex2.Settings;

namespace BlueVex2
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            if (BlueVex2.Properties.Settings.Default.Accounts == null)
            {
                BlueVex2.Properties.Settings.Default.Accounts = new System.Collections.Specialized.StringCollection();
            }

            if (BlueVex2.Properties.Settings.Default.Installations == null)
            {
                BlueVex2.Properties.Settings.Default.Installations = new System.Collections.Specialized.StringCollection();
            }
            if (BlueVex2.Properties.Settings.Default.Games == null)
            {
                BlueVex2.Properties.Settings.Default.Games = new System.Collections.Specialized.StringCollection();
            }
        }

        #region General Form Stuff

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.listBox1.SelectedItem.ToString())
            {
                case "Installations" :
                    HideAllPanels();
                    this.InstallationsPanel.Visible = true;
                    break;
                case "Accounts" :
                    HideAllPanels();
                    this.AccountsPanel.Visible = true;
                    break;
                case "Games":
                    HideAllPanels();
                    this.GamesPanel.Visible = true;
                    break;
                case "Core" :
                    HideAllPanels();
                    this.CorePanel.Visible = true;
                    break;
            }
        }

        private void HideAllPanels()
        {
            this.InstallationsPanel.Visible = false;
            this.AccountsPanel.Visible = false;
            this.GamesPanel.Visible = false;
            this.CorePanel.Visible = false;
            

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            PopulateInstallationsListView();
            PopulateAccountsListView();
            PopulateGamesListView();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            BlueVex2.Properties.Settings.Default.Save();
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            BlueVex2.Properties.Settings.Default.Reload();
            this.Close();
        }

        #endregion

        #region Installations

        private void PopulateInstallationsListView()
        {
            this.InstallationsListView.Items.Clear();

            // Populate the installations list view
            foreach (string installationString in BlueVex2.Properties.Settings.Default.Installations)
            {
                string[] parts = installationString.Split(',');
                if (parts.Length == 3)
                {
                    ListViewItem item = new ListViewItem(parts[0]);
                    item.SubItems.Add(parts[1]);
                    item.SubItems.Add(parts[2]);
                    this.InstallationsListView.Items.Add(item);
                }
            }
        }

        private void AddInstallButton_Click(object sender, EventArgs e)
        {
            InstallationForm newInstallation = new InstallationForm(""); //<------------------
            if (newInstallation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BlueVex2.Properties.Settings.Default.Installations.Add(newInstallation.KeyName + "," + newInstallation.Account + "," + newInstallation.Path);
                PopulateInstallationsListView();
            }
        }

        private void RemoveInstallButton_Click(object sender, EventArgs e)
        {
            if (this.InstallationsListView.SelectedItems.Count > 0)
            {
                string itemString = this.InstallationsListView.SelectedItems[0].Text + "," + this.InstallationsListView.SelectedItems[0].SubItems[1].Text + "," + this.InstallationsListView.SelectedItems[0].SubItems[2].Text;
                BlueVex2.Properties.Settings.Default.Installations.Remove(itemString);
                PopulateInstallationsListView();
            }
        }

        private void EditInstallButton_Click(object sender, EventArgs e)
        {
            if (this.InstallationsListView.SelectedItems.Count > 0)
            {
                string itemString = this.InstallationsListView.SelectedItems[0].Text + "," + this.InstallationsListView.SelectedItems[0].SubItems[1].Text + "," + this.InstallationsListView.SelectedItems[0].SubItems[2].Text;
                InstallationForm newInstallation = new InstallationForm(itemString);
                if (newInstallation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BlueVex2.Properties.Settings.Default.Installations.Remove(itemString);
                    BlueVex2.Properties.Settings.Default.Installations.Add(newInstallation.KeyName + "," + newInstallation.Account + "," + newInstallation.Path);
                    PopulateInstallationsListView();
                }
            }
        }

        private void InstallationsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.InstallationsListView.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    string itemString = this.InstallationsListView.SelectedItems[0].Text + "," + this.InstallationsListView.SelectedItems[0].SubItems[1].Text + "," + this.InstallationsListView.SelectedItems[0].SubItems[2].Text;
                    BlueVex2.Properties.Settings.Default.Installations.Remove(itemString);
                    PopulateInstallationsListView();
                }
            }
        }

        #endregion

        #region Accounts

        private void PopulateAccountsListView()
        {
            this.AccountsListView.Items.Clear();

            // Populate the accounts list view
            foreach (string accountString in BlueVex2.Properties.Settings.Default.Accounts)
            {
                string[] parts = accountString.Split(',');
                if (parts.Length == 5)
                {
                    ListViewItem item = new ListViewItem(parts[0]);
                    item.SubItems.Add(parts[1]);
                    item.SubItems.Add(parts[2]);
                    item.SubItems.Add(parts[3]);
                    item.SubItems.Add(parts[4]);
                    this.AccountsListView.Items.Add(item);
                }
            }
        }

        private void AddAccountButton_Click(object sender, EventArgs e)
        {
            AccountForm newAccount = new AccountForm("");
            if (newAccount.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BlueVex2.Properties.Settings.Default.Accounts.Add(newAccount.Username + "," + newAccount.Password + "," + newAccount.CharSlot + "," + newAccount.Master + "," + newAccount.Realm);
                PopulateAccountsListView();
            }
        }

        private void RemoveAccountButton_Click(object sender, EventArgs e)
        {
            if (this.AccountsListView.SelectedItems.Count > 0)
            {
                string itemString = this.AccountsListView.SelectedItems[0].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[1].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[2].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[3].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[4].Text;
                BlueVex2.Properties.Settings.Default.Accounts.Remove(itemString);
                PopulateAccountsListView();
            }
        }

        private void EditAccountButton_Click(object sender, EventArgs e)
        {
            if (this.AccountsListView.SelectedItems.Count > 0)
            {
                string itemString = this.AccountsListView.SelectedItems[0].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[1].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[2].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[3].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[4].Text;
                AccountForm newAccount = new AccountForm(itemString);
                if (newAccount.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BlueVex2.Properties.Settings.Default.Accounts.Remove(itemString);
                    BlueVex2.Properties.Settings.Default.Accounts.Add(newAccount.Username + "," + newAccount.Password + "," + newAccount.CharSlot + "," + newAccount.Master + "," + newAccount.Realm);
                    PopulateAccountsListView();
                }
            }
        }

        private void AccountsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.AccountsListView.SelectedItems.Count > 0)
                {
                    string itemString = this.AccountsListView.SelectedItems[0].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[1].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[2].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[3].Text + "," + this.AccountsListView.SelectedItems[0].SubItems[4].Text;
                    BlueVex2.Properties.Settings.Default.Accounts.Remove(itemString);
                    PopulateAccountsListView();
                }
            }
        }

        #endregion

        #region Games

        private void PopulateGamesListView()
        {
            this.GamesListView.Items.Clear();

            // Populate the games list view
            foreach (string gameString in BlueVex2.Properties.Settings.Default.Games)
            {
                string[] parts = gameString.Split(',');
                if (parts.Length == 2)
                {
                    ListViewItem item = new ListViewItem(parts[0]);
                    item.SubItems.Add(parts[1]);
                    this.GamesListView.Items.Add(item);
                }
            }
        }

        private void AddGamesButton_Click(object sender, EventArgs e)
        {
            GamesForm newGames = new GamesForm("");
            if (newGames.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BlueVex2.Properties.Settings.Default.Games.Add(newGames.GameName + "," + newGames.GamePW);
                PopulateGamesListView();
            }
        }

        private void RemoveGamesButton_Click(object sender, EventArgs e)
        {
            if (this.GamesListView.SelectedItems.Count > 0)
            {
                string itemString = this.GamesListView.SelectedItems[0].Text + "," + this.GamesListView.SelectedItems[0].SubItems[1].Text;
                BlueVex2.Properties.Settings.Default.Games.Remove(itemString);
                PopulateGamesListView();
            }
        }

        private void EditGamesButton_Click(object sender, EventArgs e)
        {
            if (this.GamesListView.SelectedItems.Count > 0)
            {
                string itemString = this.GamesListView.SelectedItems[0].Text + "," + this.GamesListView.SelectedItems[0].SubItems[1].Text;
                GamesForm newGames = new GamesForm(itemString);
                if (newGames.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BlueVex2.Properties.Settings.Default.Games.Remove(itemString);
                    BlueVex2.Properties.Settings.Default.Games.Add(newGames.GameName + "," + newGames.GamePW);
                    PopulateGamesListView();
                }
            }
        }

        private void GamesListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (this.GamesListView.SelectedItems.Count > 0)
                {
                    string itemString = this.GamesListView.SelectedItems[0].Text + "," + this.GamesListView.SelectedItems[0].SubItems[1].Text;
                    BlueVex2.Properties.Settings.Default.Games.Remove(itemString);
                    PopulateGamesListView();
                }
            }
        }

        #endregion

        private void InstallationsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
