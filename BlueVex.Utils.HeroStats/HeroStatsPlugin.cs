using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueVex.Core;

namespace BlueVex.Utils
{
    public class HeroStatsPlugin : IPlugin
    {

        private static List<HeroStatsPlugin> runningPlugins = new List<HeroStatsPlugin>();
        private Proxy proxy;

        public void Initialize(Proxy proxy)
        {
            this.proxy = proxy;

            // Add event handlers
            this.proxy.OnGameHandshake += new GameProxy.OnGameHandshakeEventHandler(proxy_OnGameHandshake);
            this.proxy.OnWalkVerify += new GameProxy.OnWalkVerifyEventHandler(proxy_OnWalkVerify);
            this.proxy.OnSendMessage += new GameProxy.OnSendMessageEventHandler(proxy_OnSendMessage);
            
            runningPlugins.Add(this);
        }

        void proxy_OnSendMessage(GameClient.SendMessage Packet, ref PacketFlag flag)
        {
            if (Packet.Message.Equals("debug"))
            {
                Console.WriteLine(this.proxy.DiabloWindow.KeyOwner + " said debug");
            }
        }

        public void InitializeUI()
        {
            //throw new NotImplementedException();
        }

        public static HeroStatsPlugin GetInstance(Proxy proxy)
        {
            foreach (HeroStatsPlugin plugin in runningPlugins)
            {
                if (plugin.proxy == proxy)
                {
                    return plugin;
                }
            }
            return null;
        }

        public uint PlayerId = 0;
        public int PlayerX = 0;
        public int PlayerY = 0;

        private void proxy_OnGameHandshake(GameServer.GameHandshake Packet, ref PacketFlag flag)
        {
            this.PlayerId = Packet.UID;
        }

        private void proxy_OnWalkVerify(GameServer.WalkVerify Packet, ref PacketFlag flag)
        {
            this.PlayerX = Packet.X;
            this.PlayerY = Packet.Y;
        }

    }
}
