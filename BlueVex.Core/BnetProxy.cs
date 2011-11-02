using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using D2Packets;
using System.Net;

namespace BlueVex.Core
{
    public class BnetProxy : BaseProxy
    {
        #region BattleNet to Diablo Events

        public event OnAdInfoEventHandler OnAdInfo = delegate { };
        public delegate void OnAdInfoEventHandler(BnetServer.AdInfo packet, ref PacketFlag flag);
        public event OnBnetAuthResponseEventHandler OnBnetAuthResponse = delegate { };
        public delegate void OnBnetAuthResponseEventHandler(BnetServer.BnetAuthResponse packet, ref PacketFlag flag);
        public event OnBnetConnectionResponseEventHandler OnBnetConnectionResponse = delegate { };
        public delegate void OnBnetConnectionResponseEventHandler(BnetServer.BnetConnectionResponse packet, ref PacketFlag flag);
        public event OnBnetLogonResponseEventHandler OnBnetLogonResponse = delegate { };
        public delegate void OnBnetLogonResponseEventHandler(BnetServer.BnetLogonResponse packet, ref PacketFlag flag);
        public event OnBnetPingEventHandler OnBnetPing = delegate { };
        public delegate void OnBnetPingEventHandler(BnetServer.BnetPing packet, ref PacketFlag flag);
        public event OnChannelListEventHandler OnChannelList = delegate { };
        public delegate void OnChannelListEventHandler(BnetServer.ChannelList packet, ref PacketFlag flag);
        public event OnChatEventEventHandler OnChatEvent = delegate { };
        public delegate void OnChatEventEventHandler(BnetServer.ChatEvent packet, ref PacketFlag flag);
        public event OnEnterChatResponseEventHandler OnEnterChatResponse = delegate { };
        public delegate void OnEnterChatResponseEventHandler(BnetServer.EnterChatResponse packet, ref PacketFlag flag);
        public event OnExtraWorkInfoEventHandler OnExtraWorkInfo = delegate { };
        public delegate void OnExtraWorkInfoEventHandler(BnetServer.ExtraWorkInfo packet, ref PacketFlag flag);
        public event OnFileTimeInfoEventHandler OnFileTimeInfo = delegate { };
        public delegate void OnFileTimeInfoEventHandler(BnetServer.FileTimeInfo packet, ref PacketFlag flag);
        public event OnNewsInfoEventHandler OnNewsInfo = delegate { };
        public delegate void OnNewsInfoEventHandler(BnetServer.NewsInfo packet, ref PacketFlag flag);
        public event OnQueryRealmsResponseEventHandler OnQueryRealmsResponse = delegate { };
        public delegate void OnQueryRealmsResponseEventHandler(BnetServer.QueryRealmsResponse packet, ref PacketFlag flag);
        public event OnRealmLogonResponseEventHandler OnRealmLogonResponse = delegate { };
        public delegate void OnRealmLogonResponseEventHandler(BnetServer.RealmLogonResponse packet, ref PacketFlag flag);
        public event OnRequiredExtraWorkInfoEventHandler OnRequiredExtraWorkInfo = delegate { };
        public delegate void OnRequiredExtraWorkInfoEventHandler(BnetServer.RequiredExtraWorkInfo packet, ref PacketFlag flag);
        public event OnServerKeepAliveEventHandler OnServerKeepAlive = delegate { };
        public delegate void OnServerKeepAliveEventHandler(BnetServer.KeepAlive packet, ref PacketFlag flag);
        
        #endregion

        #region Diablo to BattleNet Events

        public event OnKeepAliveEventHandler OnKeepAlive = delegate { };
        public delegate void OnKeepAliveEventHandler(BnetClient.KeepAlive packet, ref PacketFlag flag);
        public event OnEnterChatRequestEventHandler OnEnterChatRequest = delegate { };
        public delegate void OnEnterChatRequestEventHandler(BnetClient.EnterChatRequest packet, ref PacketFlag flag);
        public event OnChannelListRequestEventHandler OnChannelListRequest = delegate { };
        public delegate void OnChannelListRequestEventHandler(BnetClient.ChannelListRequest packet, ref PacketFlag flag);
        public event OnJoinChannelEventHandler OnJoinChannel = delegate { };
        public delegate void OnJoinChannelEventHandler(BnetClient.JoinChannel packet, ref PacketFlag flag);
        public event OnChatCommandEventHandler OnChatCommand = delegate { };
        public delegate void OnChatCommandEventHandler(BnetClient.ChatCommand packet, ref PacketFlag flag);
        public event OnLeaveChatEventHandler OnLeaveChat = delegate { };
        public delegate void OnLeaveChatEventHandler(BnetClient.LeaveChat packet, ref PacketFlag flag);
        public event OnAdInfoRequestEventHandler OnAdInfoRequest = delegate { };
        public delegate void OnAdInfoRequestEventHandler(BnetClient.AdInfoRequest packet, ref PacketFlag flag);
        public event OnStartGameEventHandler OnStartGame = delegate { };
        public delegate void OnStartGameEventHandler(BnetClient.StartGame packet, ref PacketFlag flag);
        public event OnLeaveGameEventHandler OnLeaveGame = delegate { };
        public delegate void OnLeaveGameEventHandler(BnetClient.LeaveGame packet, ref PacketFlag flag);
        public event OnDisplayAdEventHandler OnDisplayAd = delegate { };
        public delegate void OnDisplayAdEventHandler(BnetClient.DisplayAd packet, ref PacketFlag flag);
        public event OnNotifyJoinEventHandler OnNotifyJoin = delegate { };
        public delegate void OnNotifyJoinEventHandler(BnetClient.NotifyJoin packet, ref PacketFlag flag);
        public event OnBnetPongEventHandler OnBnetPong = delegate { };
        public delegate void OnBnetPongEventHandler(BnetClient.BnetPong packet, ref PacketFlag flag);
        public event OnFileTimeRequestEventHandler OnFileTimeRequest = delegate { };
        public delegate void OnFileTimeRequestEventHandler(BnetClient.FileTimeRequest packet, ref PacketFlag flag);
        public event OnBnetLogonRequestEventHandler OnBnetLogonRequest = delegate { };
        public delegate void OnBnetLogonRequestEventHandler(BnetClient.BnetLogonRequest packet, ref PacketFlag flag);
        public event OnRealmLogonRequestEventHandler OnRealmLogonRequest = delegate { };
        public delegate void OnRealmLogonRequestEventHandler(BnetClient.RealmLogonRequest packet, ref PacketFlag flag);
        public event OnQueryRealmsEventHandler OnQueryRealms = delegate { };
        public delegate void OnQueryRealmsEventHandler(BnetClient.QueryRealms packet, ref PacketFlag flag);
        public event OnNewsInfoRequestEventHandler OnNewsInfoRequest = delegate { };
        public delegate void OnNewsInfoRequestEventHandler(BnetClient.NewsInfoRequest packet, ref PacketFlag flag);
        public event OnExtraWorkResponseEventHandler OnExtraWorkResponse = delegate { };
        public delegate void OnExtraWorkResponseEventHandler(BnetClient.ExtraWorkResponse packet, ref PacketFlag flag);
        public event OnBnetConnectionRequestEventHandler OnBnetConnectionRequest = delegate { };
        public delegate void OnBnetConnectionRequestEventHandler(BnetClient.BnetConnectionRequest packet, ref PacketFlag flag);
        public event OnBnetAuthRequestEventHandler OnBnetAuthRequest = delegate { };
        public delegate void OnBnetAuthRequestEventHandler(BnetClient.BnetAuthRequest packet, ref PacketFlag flag);

        #endregion

        private int battleNetPort = 6112;
        private string battleNetHost = "classicbeta.battle.net";

        public string Username;
        public IPAddress RealmAddress { get; set; }
        public int RealmPort { get; set; }

        public Proxy Proxy { get; set; }

        public BnetProxy(TcpClient client) : base(client)
        {
            IPAddress serverAddress = Dns.GetHostAddresses(battleNetHost)[0];
            base.Connect(serverAddress, battleNetPort);
            base.Init();
            
            this.Proxy = new Proxy();
            this.Proxy.BnetProxy = this;
            this.OnRealmLogonResponse += new OnRealmLogonResponseEventHandler(BnetProxy_OnRealmLogonResponse);
            this.OnBnetAuthRequest += new OnBnetAuthRequestEventHandler(BnetProxy_OnBnetAuthRequest);
        }

        void BnetProxy_OnBnetAuthRequest(BnetClient.BnetAuthRequest packet, ref PacketFlag flag)
        {
            this.ProxyServer.RaiseAttachDiabloWindow(this.Proxy, packet.OwnerName);
        }

        private void BnetProxy_OnRealmLogonResponse(BnetServer.RealmLogonResponse packet, ref PacketFlag flag)
        {
            this.RealmAddress = packet.RealmServerIP;
            this.RealmPort = packet.RealmServerPort;

            string ip = packet.RealmServerIP.ToString();
            this.Username = packet.Username;
            flag = PacketFlag.PacketFlag_Hidden;

            this.SendToDiablo(packet.PatchedByteArray());
        }

        protected override void DiabloToBattleNet(byte[] data, ref PacketFlag flag)
        {
            byte[] packetData = new byte[data.Length - 1];
            Buffer.BlockCopy(data, 1, packetData, 0, data.Length - 1);

            BnetClientPacket PacketID = (BnetClientPacket)packetData[0];

            try
            {
                switch (PacketID)
                {
                    //TODO: Add the rest of these...
                    case BnetClientPacket.RealmLogonRequest: OnRealmLogonRequest(new BnetClient.RealmLogonRequest(packetData), ref flag); break;
                    case BnetClientPacket.BnetAuthRequest: OnBnetAuthRequest(new BnetClient.BnetAuthRequest(packetData), ref flag); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(PacketID.ToString() + ": " + ex.Message);
            }
        }
     
        protected override void BattleNetToDiablo(byte[] data, ref PacketFlag flag)
        {
            if (data[0] == 0xff)
            {
                byte[] packetData = new byte[data.Length - 1];
                Buffer.BlockCopy(data, 1, packetData, 0, data.Length - 1);

                BnetServerPacket PacketID = (BnetServerPacket)packetData[0];

                try
                {
                    switch (PacketID)
                    {
                        case BnetServerPacket.AdInfo: OnAdInfo(new BnetServer.AdInfo(packetData), ref flag); break;
                        case BnetServerPacket.BnetAuthResponse: OnBnetAuthResponse(new BnetServer.BnetAuthResponse(packetData), ref flag); break;
                        case BnetServerPacket.BnetConnectionResponse: OnBnetConnectionResponse(new BnetServer.BnetConnectionResponse(packetData), ref flag); break;
                        case BnetServerPacket.BnetLogonResponse: OnBnetLogonResponse(new BnetServer.BnetLogonResponse(packetData), ref flag); break;
                        case BnetServerPacket.BnetPing: OnBnetPing(new BnetServer.BnetPing(packetData), ref flag); break;
                        case BnetServerPacket.ChannelList: OnChannelList(new BnetServer.ChannelList(packetData), ref flag); break;
                        case BnetServerPacket.ChatEvent: OnChatEvent(new BnetServer.ChatEvent(packetData), ref flag); break;
                        case BnetServerPacket.EnterChatResponse: OnEnterChatResponse(new BnetServer.EnterChatResponse(packetData), ref flag); break;
                        case BnetServerPacket.ExtraWorkInfo: OnExtraWorkInfo(new BnetServer.ExtraWorkInfo(packetData), ref flag); break;
                        case BnetServerPacket.FileTimeInfo: OnFileTimeInfo(new BnetServer.FileTimeInfo(packetData), ref flag); break;
                        case BnetServerPacket.KeepAlive: OnServerKeepAlive(new BnetServer.KeepAlive(packetData), ref flag); break;
                        case BnetServerPacket.NewsInfo: OnNewsInfo(new BnetServer.NewsInfo(packetData), ref flag); break;
                        case BnetServerPacket.QueryRealmsResponse: OnQueryRealmsResponse(new BnetServer.QueryRealmsResponse(packetData), ref flag); break;
                        case BnetServerPacket.RealmLogonResponse: OnRealmLogonResponse(new BnetServer.RealmLogonResponse(packetData), ref flag); break;
                        case BnetServerPacket.RequiredExtraWorkInfo: OnRequiredExtraWorkInfo(new BnetServer.RequiredExtraWorkInfo(packetData), ref flag); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(PacketID.ToString() + ": " + ex.Message);
                }
            }
        }

    }
}
