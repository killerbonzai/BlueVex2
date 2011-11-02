using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using BlueVex.Core;
using RealmClient;
using BlueVex2.Tabs;
using System.Threading;

namespace BlueVex2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void ReleaseAll()
        {
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                if (tab is DiabloTab)
                {
                    DiabloHostPanel diabloPanel = tab.Controls[0] as DiabloHostPanel;
                    if (diabloPanel != null)
                    {
                        diabloPanel.ReleaseDiabloWindow();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConsoleTab console = new ConsoleTab();
            this.tabControl1.TabPages.Add(console);

            ConsoleTab.WriteLine("BlueVex 2 Diablo 2 Proxy By Pleh");

            OverviewTab overview = new OverviewTab();
            this.tabControl1.TabPages.Add(overview);

            try
            {
                LoadDiabloTabs();
            }
            catch (Exception err)
            {
                MessageBox.Show("Make sure your settings are set." + "\r\n\n" + "Advanced:" + "\r\n\n" + err.ToString(), "BlueVex2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (BlueVex2.Properties.Settings.Default.UseBV2Proxy)
            {
                StartProxies();
                PlugInManager.FindAvailablePlugIns();
                List<Plugin> plugs = PlugInManager.AvailablePlugins;

                foreach (Plugin plug in plugs)
                {
                    ConsoleTab.WriteLine("Loading plugin: " + plug.TypeName);
                }
            }
        }

        private void LoadDiabloTabs()
        {
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                if (tab is DiabloTab)
                {
                    tabControl1.TabPages.Remove(tab);
                }
            }
            foreach (string installationString in BlueVex2.Properties.Settings.Default.Installations)
            {
                string[] parts = installationString.Split(',');
                if (parts.Length == 3)
                {
                    DiabloTab tab = new DiabloTab(parts[0], parts[1], parts[2]); //keyname , defacc , path
                    this.tabControl1.TabPages.Add(tab);
                    this.tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
                }
            }
        }

        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                if(tab is DiabloTab)
                {
                    ((DiabloTab)tab).Deactivate();
                }
                else if (tab is OverviewTab)
                {
                    ((OverviewTab)tab).Deactivate();
                }
            }

            if (this.tabControl1.SelectedTab is DiabloTab)
            {
                ((DiabloTab)this.tabControl1.SelectedTab).Activate();
            }
            else if (this.tabControl1.SelectedTab is OverviewTab)
            {
                ((OverviewTab)this.tabControl1.SelectedTab).Activate();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseAll();
        }

        private void StartProxies()
        {
            ConsoleTab.WriteLine("Starting Proxies...");
            ProxyServer bnetProxy = new ProxyServer(6112, ProxyType.Bnet);
            bnetProxy.ClientConnected += new ProxyServer.ClientConnectedHandler(bnetProxy_ClientConnected);
            bnetProxy.AttachDiabloWindow += new ProxyServer.AttachDiabloWindowHandler(bnetProxy_AttachDiabloWindow);
            ProxyServer realmProxy = new ProxyServer(6113, ProxyType.Realm); // bnet 6112
            ProxyServer gameProxy = new ProxyServer(4000, ProxyType.Game);
            ConsoleTab.WriteLine("Proxies running!");
        }

        void bnetProxy_AttachDiabloWindow(Proxy proxy, string keyOwner)
        {
            foreach (IDiabloWindow diabloWindow in DiabloWindow.DiabloWindows)
            {
                if (diabloWindow.KeyOwner.ToUpper() == keyOwner.ToUpper())
                {
                    proxy.DiabloWindow = diabloWindow;
                    return;
                }
            }
            ConsoleTab.WriteLine("Could not find window for " + keyOwner);
        }

        void bnetProxy_ClientConnected(object sender, EventArgs args)
        {
            foreach (Plugin plugin in PlugInManager.AvailablePlugins)
            {
                ConsoleTab.WriteLine("Loading " + plugin.TypeName);
                IPlugin loadedPlugin = plugin.CreateInstance();
                loadedPlugin.Initialize(((BnetProxy)sender).Proxy);
                this.BeginInvoke(new InitUIDelegate(InitUI), loadedPlugin);
            }
        }

        private delegate void InitUIDelegate(IPlugin plugin);
        private void InitUI(IPlugin plugin)
        {
            plugin.InitializeUI();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.ShowDialog();
            LoadDiabloTabs();
        }

        private void loadAllDiablo2InstallationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                if (tab is DiabloTab)
                {
                    ((DiabloTab)tab).LoadDiablo2(i * 5000);
                    i++;
                }
            }
            if (this.tabControl1.SelectedTab is OverviewTab)
            {
                ((OverviewTab)this.tabControl1.SelectedTab).Activate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Game Actions Menu

        private void everyoneExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string error = "";
            string temperror = "";
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                if (tab is DiabloTab)
                {
                    temperror = ((DiabloTab)tab).EveryoneExitGame();
                    if (temperror.Length > 0)
                    {
                        error += tab.Text + ": \r\n" + temperror;
                    }
                }
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error, "BlueVex2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region gamemakers
        private void normGame1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameCreaterHelperFunction(0, "Normal");
        }

        private void normGame2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameCreaterHelperFunction(1, "Normal");
        }

        private void normGame3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameCreaterHelperFunction(2, "Normal");
        }

        private void nightmareGame1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameCreaterHelperFunction(0, "Nightmare");
        }

        private void nightmareGame2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameCreaterHelperFunction(1, "Nightmare");
        }

        private void nightmareGame3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameCreaterHelperFunction(2, "Nightmare");
        }

        private void hellGame1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameCreaterHelperFunction(0, "Hell");
        }

        private void hellGame2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameCreaterHelperFunction(1, "Hell");
        }

        private void hellGame3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameCreaterHelperFunction(2, "Hell");
        }
        #endregion

        private void GameCreaterHelperFunction(int GameNum, string difficulty)
        {
            try
            {
                string GamesString = BlueVex2.Properties.Settings.Default.Games[GameNum];
                string[] Game_parts = GamesString.Split(',');

                foreach (TabPage tab in this.tabControl1.TabPages)
                {
                    if (tab is DiabloTab)
                    {
                        ((DiabloTab)tab).EveryoneGotoGame(Game_parts[0], Game_parts[1], difficulty);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Either your settings are wrong or you didn't start Diablo. Fix!", "BlueVex2", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void quitGameFromChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string error = "";
            string temperror = "";
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                if (tab is DiabloTab)
                {
                    temperror = ((DiabloTab)tab).QuitFromChat();
                    if (temperror.Length > 0)
                    {
                        error += tab.Text + ": \r\n" + temperror;
                    }
                }
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error, "BlueVex2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (this.tabControl1.SelectedTab is OverviewTab)
            {
                ((OverviewTab)this.tabControl1.SelectedTab).Activate();
            }
        }

        private void allExitANDQuitD2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string error = "";
            string temperror = "";
            // All exit current game
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                if (tab is DiabloTab)
                {
                    temperror = ((DiabloTab)tab).EveryoneExitGame();
                    if (temperror.Length > 0)
                    {
                        error += tab.Text + ": \r\n" + temperror;
                    }
                }
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error, "BlueVex2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // All close D2 windows
            // temp 'delayer' or it will try and close too fast and not always close them all
            MessageBox.Show("Click OK to close all running D2 windows", "BlueVex2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                if (tab is DiabloTab)
                {
                    temperror = ((DiabloTab)tab).QuitFromChat();
                    if (temperror.Length > 0)
                    {
                        error += tab.Text + ": \r\n" + temperror;
                    }
                }
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error, "BlueVex2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (this.tabControl1.SelectedTab is OverviewTab)
            {
                ((OverviewTab)this.tabControl1.SelectedTab).Activate();
            }
        }

        #endregion

        private void minimizeToTraytoolStripMenuItem3_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(1000);
            this.Hide();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }
    }
}
