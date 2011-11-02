using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueVex.Core;
using System.Net;
using System.Windows.Forms;
using System.Windows.Controls;

namespace HeroMerc
{
    public class HeroMercPlugin :IPlugin
    {
        // A custom UI panel
        private HelloWorldPanel helloWorldPanel;

        // Collection used to store other running instances of this plugin so we can communicate witht hem
        private static List<HeroMercPlugin> runningPlugins = new List<HeroMercPlugin>();

        // Keep a reference to the proxy
        private Proxy proxy;

        private string gameName;
        private string gamePassword;

        // Initialize non UI elements here
        // NEVER use and UI elements in this method as it is executed in its own thread
        public void Initialize(Proxy proxy)
        {
            this.proxy = proxy;

            // Add event handlers
            this.proxy.OnJoinGameRequest += new RealmProxy.OnJoinGameRequestEventHandler(proxy_OnJoinGameRequest);
            this.proxy.OnJoinGameResponse += new RealmProxy.OnJoinGameResponseEventHandler(proxy_OnJoinGameResponse);
            this.proxy.OnSendMessage += new GameProxy.OnSendMessageEventHandler(proxy_OnSendMessage);

            this.proxy.OnPlayerLeaveGame += new GameProxy.OnPlayerLeaveGameEventHandler(proxy_OnPlayerLeaveGame);
         
            this.proxy.KeyDown +=new KeyEventHandler(proxy_KeyDown); 

            runningPlugins.Add(this);
        }


        void proxy_OnSendMessage(GameClient.SendMessage Packet, ref PacketFlag flag)
        {

            if (Packet.Message.Equals("debug"))
            {
            }
        }

        // UI elements must be created here as this method is called on the UI thread.
        public void InitializeUI()
        {
            helloWorldPanel = new HelloWorldPanel(this.proxy);
        }

        // Show or hide the custom UI panel
        void proxy_KeyDown(object sender, KeyEventArgs e)
        {
            // Make sure the proxy has a diablo window attached to it
            if (proxy.DiabloWindow != null)
            {
                // Check if the key down is the key we want to use to show our panel
                if (e.KeyCode == Keys.D8)
                {
                    // set handled to true so it does not send the keypress to diablo
                    e.Handled = true;
                    // set the diablo window on the panel if it hasnt been set already
                    // this is used so we can have a close button on the panel.
                    if (helloWorldPanel.DiabloWindow == null)
                    {
                        helloWorldPanel.DiabloWindow = proxy.DiabloWindow;
                    }
                    // Show/Hide the panel
                    proxy.DiabloWindow.ShowPanel(helloWorldPanel);
                }
            }
        }

        #region Game Joining

        void proxy_OnJoinGameResponse(RealmServer.JoinGameResponse packet, ref PacketFlag flag)
        {
            // If this instance receives a join game success packet
            if (packet.Result == RealmServer.JoinGameResult.Sucess)
            {
                try
                {
                    // If this is the active diablo 2 window (active tab or last clicked in overview tab)
                    if (this.proxy.DiabloWindow != null && this.proxy.DiabloWindow.IsActive())
                    {
                        Console.WriteLine(this.proxy.DiabloWindow.KeyOwner + " joined a game, telling hero mercs to join");
                        
                        // loop through all running heromerc plugins to tell them to join the same game
                        foreach (HeroMercPlugin heroMerc in runningPlugins)
                        {
                            // Ignore the current instance as we are already in the game
                            if (heroMerc != this)
                            {
                                // tell the other instance to join the game
                                heroMerc.JoinGame(gameName, gamePassword);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // Store the game name and password when joining a game
        void proxy_OnJoinGameRequest(RealmClient.JoinGameRequest packet, ref PacketFlag flag)
        {
            gameName = packet.Name;
            gamePassword = packet.Password;
        }

        // Helper function for joining a game
        public void JoinGame(string name, string password)
        {
            if (this.proxy.DiabloWindow != null)
            {
                Console.WriteLine(this.proxy.DiabloWindow.KeyOwner + " Merc Joining " + name ?? "" + "//" + password ?? "");
                this.proxy.DiabloWindow.JoinGame(name, password);
            }
            else
            {
                Console.WriteLine("This proxy does not have an attached diablo window");
            }
        }

        #endregion

        #region Game Exiting


        void proxy_OnPlayerLeaveGame(GameServer.PlayerLeaveGame Packet, ref PacketFlag flag)
        {
            Console.WriteLine(" Starting Exiting Script ");
            foreach (HeroMercPlugin heroMerc in runningPlugins)
            {
                if (heroMerc == this)
                {
                    Console.WriteLine(this.proxy.DiabloWindow.KeyOwner + " Merc Exiting ");
                    this.proxy.DiabloWindow.ExitGame();
                }
            }
        }

        #endregion

    }
}
