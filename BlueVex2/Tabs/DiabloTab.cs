using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using BlueVex.Core;

namespace BlueVex2.Tabs
{
    class DiabloTab : TabPage
    {
        private string exePath;
        private string defaultAccount;
        private string master;

        private IntPtr diabloHandle;
        private DiabloWindow diabloWindow;
        private DiabloHostPanel diabloPanel;

        public DiabloWindow DiabloWindow { get { return diabloWindow; } }


        public DiabloTab(string keyName, string defaultAccount, string path)
        {
            this.exePath = path;
            this.Text = keyName;
            this.defaultAccount = defaultAccount;

            diabloPanel = new DiabloHostPanel();
            diabloPanel.Width = Convert.ToInt32(800m*BlueVex2.Properties.Settings.Default.ResolutionScale);
            diabloPanel.Height = Convert.ToInt32(600m * BlueVex2.Properties.Settings.Default.ResolutionScale); ;
            diabloPanel.Location = new Point(10, 10);
            this.Controls.Add(diabloPanel);

            Button LoadDiabloButton = new Button();
            LoadDiabloButton.Text = "Load D2";
            LoadDiabloButton.Location = new Point(Convert.ToInt32(800m*BlueVex2.Properties.Settings.Default.ResolutionScale+20m), 10);
            LoadDiabloButton.Click += new EventHandler(LoadDiabloButton_Click);
            this.Controls.Add(LoadDiabloButton);

            // Crap Fett007 added to bring master variable
            foreach (string accountString in BlueVex2.Properties.Settings.Default.Accounts)
            {
                if (accountString.StartsWith(defaultAccount + ","))
                {
                    string[] parts = accountString.Split(',');
                    master = parts[3];
                }
            }
        }

        void LoadDiabloButton_Click(object sender, EventArgs e)
        {
            LoadDiablo2(0);
        }

        #region Fett007 Added Functions

        public string EveryoneExitGame()
        {
            try
            {
                ((IDiabloWindow)diabloWindow).ExitGame();
            }
            catch (Exception)
            {
                return "Can only exit game if IN a game. Doh!" + "\r\n\n";
            }
            return "";
        }

        public void EveryoneGotoGame(string gamename, string password, string difficulty)
        {
            switch (master)
            {
                case "True":
                    ((IDiabloWindow)diabloWindow).CreateGame(gamename, password, difficulty);
                    break;

                case "False":
                    ((IDiabloWindow)diabloWindow).JoinGame(gamename, password);
                    break;
            }
        }

        public string QuitFromChat()
        {
            try
            {
                ((IDiabloWindow)diabloWindow).QuitFromChat();
            }
            catch (Exception)
            {
                return "Can only quit from chat if IN the chat. Doh!" + "\r\n\n";
            }
            return "";
        }

        #endregion

        public void LoadDiablo2(int loginDelay)
        {
            if (diabloWindow == null)
            {
                if (!string.IsNullOrEmpty(exePath) && System.IO.File.Exists(exePath))
                {
                    Process diabloProcess = new Process();
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = exePath;
                    info.Arguments = "-w -ns";

                    // Crap Fett007 added to bring master variable
                    string username = string.Empty;
                    string password = string.Empty;
                    string charslot = string.Empty;
                    string master = string.Empty;

                    foreach (string accountString in BlueVex2.Properties.Settings.Default.Accounts)
                    {
                        if (accountString.StartsWith(defaultAccount + ","))
                        {
                            string[] parts = accountString.Split(',');
                            username = parts[0];
                            password = parts[1];
                            charslot = parts[2];
                            master = parts[3];
                        }
                    }


                    switch (master)
                    {
                        case "True":
                            info.Arguments = "-w -sndbkg";
                            break;

                        case "False":
                            info.Arguments = "-w -ns";
                            break;
                    }

                    ConsoleTab.WriteLine("Loading " + exePath);

                    diabloProcess = Process.Start(info);
                    diabloProcess.EnableRaisingEvents = true;
                    diabloProcess.Exited += new EventHandler(diabloProcess_Exited);

                    // Wait for the app to load
                    diabloProcess.WaitForInputIdle();

                    diabloHandle = diabloProcess.MainWindowHandle;

                    // If greater than Windows XP
                    if (System.Environment.OSVersion.Version.Major > 5)
                    {
                        diabloWindow = new DiabloWindow(Application.OpenForms[0], diabloPanel, diabloHandle, this.Text);
                        diabloPanel.BindDiabloWindow(diabloWindow);

                        if (((TabControl)this.Parent).SelectedTab == this)
                        {
                            diabloWindow.Activate();
                        }
                        else
                        {
                            diabloWindow.Deactivate();
                        }

                    }
                    if (BlueVex2.Properties.Settings.Default.UseBV2AutoLogin)
                    {
                        ((IDiabloWindow)diabloWindow).LoginToBattleNet(this.defaultAccount);
                    }
                }
                else
                {
                    ConsoleTab.WriteLine("Could not find diablo 2 game.exe " + exePath);
                }
            }
        }

        private delegate void ProcessExitedDelegate(object sender, EventArgs e);
        void diabloProcess_Exited(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new ProcessExitedDelegate(diabloProcess_Exited), sender, e);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                diabloPanel.ReleaseDiabloWindow();

                if (diabloWindow != null)
                {
                    diabloWindow.Close();
                    diabloWindow = null;
                }

                ConsoleTab.WriteLine("Closed " + ((Process)sender).StartInfo.FileName);
            }
        }

        public void Activate()
        {
            if (diabloWindow != null)
            {
                DiabloWindow.SetHostPanel(diabloPanel);
                diabloWindow.Activate();
            }
        }

        public void Deactivate()
        {
            if (diabloWindow != null)
            {
                diabloWindow.Deactivate();
            }
        }
    }
}
