using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using D2Packets;
using System.Net;

namespace BlueVex.Core
{
    public class RealmProxy : BaseProxy
    {

        #region BattleNet to Diablo Events

        public event OnCharacterCreationResponseEventHandler OnCharacterCreationResponse = delegate { };
        public delegate void OnCharacterCreationResponseEventHandler(RealmServer.CharacterCreationResponse packet, ref PacketFlag flag);
        public event OnCharacterDeletionResponseEventHandler OnCharacterDeletionResponse = delegate { };
        public delegate void OnCharacterDeletionResponseEventHandler(RealmServer.CharacterDeletionResponse packet, ref PacketFlag flag);
        public event OnCharacterListEventHandler OnCharacterList = delegate { };
        public delegate void OnCharacterListEventHandler(RealmServer.CharacterList packet, ref PacketFlag flag);
        public event OnCharacterLogonResponseEventHandler OnCharacterLogonResponse = delegate { };
        public delegate void OnCharacterLogonResponseEventHandler(RealmServer.CharacterLogonResponse packet, ref PacketFlag flag);
        public event OnCharacterUpgradeResponseEventHandler OnCharacterUpgradeResponse = delegate { };
        public delegate void OnCharacterUpgradeResponseEventHandler(RealmServer.CharacterUpgradeResponse packet, ref PacketFlag flag);
        public event OnCreateGameResponseEventHandler OnCreateGameResponse = delegate { };
        public delegate void OnCreateGameResponseEventHandler(RealmServer.CreateGameResponse packet, ref PacketFlag flag);
        public event OnGameCreationQueueEventHandler OnGameCreationQueue = delegate { };
        public delegate void OnGameCreationQueueEventHandler(RealmServer.GameCreationQueue packet, ref PacketFlag flag);
        public event OnGameInfoEventHandler OnGameInfo = delegate { };
        public delegate void OnGameInfoEventHandler(RealmServer.GameInfo packet, ref PacketFlag flag);
        public event OnGameListEventHandler OnGameList = delegate { };
        public delegate void OnGameListEventHandler(RealmServer.GameList packet, ref PacketFlag flag);
        public event OnJoinGameResponseEventHandler OnJoinGameResponse = delegate { };
        public delegate void OnJoinGameResponseEventHandler(RealmServer.JoinGameResponse packet, ref PacketFlag flag);
        public event OnMessageOfTheDayEventHandler OnMessageOfTheDay = delegate { };
        public delegate void OnMessageOfTheDayEventHandler(RealmServer.MessageOfTheDay packet, ref PacketFlag flag);
        public event OnRealmStartupResponseEventHandler OnRealmStartupResponse = delegate { };
        public delegate void OnRealmStartupResponseEventHandler(RealmServer.RealmStartupResponse packet, ref PacketFlag flag);

        #endregion

        #region Diablo to BattleNet Events

        public event OnRealmStartupRequestEventHandler OnRealmStartupRequest = delegate { };
        public delegate void OnRealmStartupRequestEventHandler(RealmClient.RealmStartupRequest packet, ref PacketFlag flag);
        public event OnCharacterCreationRequestEventHandler OnCharacterCreationRequest = delegate { };
        public delegate void OnCharacterCreationRequestEventHandler(RealmClient.CharacterCreationRequest packet, ref PacketFlag flag);
        public event OnCreateGameRequestEventHandler OnCreateGameRequest = delegate { };
        public delegate void OnCreateGameRequestEventHandler(RealmClient.CreateGameRequest packet, ref PacketFlag flag);
        public event OnJoinGameRequestEventHandler OnJoinGameRequest = delegate { };
        public delegate void OnJoinGameRequestEventHandler(RealmClient.JoinGameRequest packet, ref PacketFlag flag);
        public event OnGameListRequestEventHandler OnGameListRequest = delegate { };
        public delegate void OnGameListRequestEventHandler(RealmClient.GameListRequest packet, ref PacketFlag flag);
        public event OnGameInfoRequestEventHandler OnGameInfoRequest = delegate { };
        public delegate void OnGameInfoRequestEventHandler(RealmClient.GameInfoRequest packet, ref PacketFlag flag);
        public event OnCharacterLogonRequestEventHandler OnCharacterLogonRequest = delegate { };
        public delegate void OnCharacterLogonRequestEventHandler(RealmClient.CharacterLogonRequest packet, ref PacketFlag flag);
        public event OnCharacterDeletionRequestEventHandler OnCharacterDeletionRequest = delegate { };
        public delegate void OnCharacterDeletionRequestEventHandler(RealmClient.CharacterDeletionRequest packet, ref PacketFlag flag);
        public event OnMessageOfTheDayRequestEventHandler OnMessageOfTheDayRequest = delegate { };
        public delegate void OnMessageOfTheDayRequestEventHandler(RealmClient.MessageOfTheDayRequest packet, ref PacketFlag flag);
        public event OnCancelGameCreationEventHandler OnCancelGameCreation = delegate { };
        public delegate void OnCancelGameCreationEventHandler(RealmClient.CancelGameCreation packet, ref PacketFlag flag);
        public event OnCharacterUpgradeRequestEventHandler OnCharacterUpgradeRequest = delegate { };
        public delegate void OnCharacterUpgradeRequestEventHandler(RealmClient.CharacterUpgradeRequest packet, ref PacketFlag flag);
        public event OnCharacterListRequestEventHandler OnCharacterListRequest = delegate { };
        public delegate void OnCharacterListRequestEventHandler(RealmClient.CharacterListRequest packet, ref PacketFlag flag);

        #endregion

        public BnetProxy bnetProxy;
        public IPAddress GameAddress { get; set; }
        public int GamePort { get; set; }
        public string CharacterName { get; set; }

        public Proxy Proxy { get; set; }

        public RealmProxy(TcpClient client) : base(client)
        {
            this.OnRealmStartupRequest += new OnRealmStartupRequestEventHandler(RealmProxy_OnRealmStartupRequest);
            this.OnCharacterLogonRequest += new OnCharacterLogonRequestEventHandler(RealmProxy_OnCharacterLogonRequest);
            this.OnJoinGameResponse += new OnJoinGameResponseEventHandler(RealmProxy_OnJoinGameResponse);
            this.OnJoinGameRequest += new OnJoinGameRequestEventHandler(RealmProxy_OnJoinGameRequest);
            
            BnetProxy bnetProxy = ProxyServer.FindBnetProxyForRealm(string.Empty);
            if (bnetProxy != null)
            {
                base.Connect(bnetProxy.RealmAddress, bnetProxy.RealmPort);
                base.Init();
            }
            else
            {
                base.Disconnect();
            }
        }

        void RealmProxy_OnJoinGameRequest(RealmClient.JoinGameRequest packet, ref PacketFlag flag)
        {
            ProxyServer.RealmProxyJoiningGame = this;
        }

        void RealmProxy_OnCharacterLogonRequest(RealmClient.CharacterLogonRequest packet, ref PacketFlag flag)
        {
            CharacterName = packet.Name;
        }

        void RealmProxy_OnJoinGameResponse(RealmServer.JoinGameResponse packet, ref PacketFlag flag)
        {
            if (packet.Result == RealmServer.JoinGameResult.Sucess)
            {
                this.GameAddress = packet.GameServerIP;
                this.GamePort = 4000;

                flag = PacketFlag.PacketFlag_Hidden;

                byte[] patchedBytes = packet.PatchedByteArray();
                this.SendToDiablo(patchedBytes);
            }
        }

        void RealmProxy_OnRealmStartupRequest(RealmClient.RealmStartupRequest packet, ref PacketFlag flag)
        {
            bnetProxy = ProxyServer.FindBnetProxyForRealm(packet.Username);
            if (bnetProxy != null)
            {
                bnetProxy.Proxy.RealmProxy = this;
                this.Proxy = bnetProxy.Proxy;
            }
        }

        private bool firstPacket = true;
        protected override void DiabloToBattleNet(byte[] data, ref PacketFlag flag)
        {
            byte[] packetData;

            if (firstPacket)
            {
                packetData = new byte[data.Length - 3];
                Buffer.BlockCopy(data, 3, packetData, 0, data.Length - 3);
                firstPacket = false;
            }
            else
            {
                packetData = new byte[data.Length - 2];
                Buffer.BlockCopy(data, 2, packetData, 0, data.Length - 2);
            }

            RealmClientPacket PacketID = (RealmClientPacket)packetData[0];
            
            try
            {
                switch (PacketID)
                {
                    //TODO: Add the rest of these...
                    case RealmClientPacket.RealmStartupRequest : OnRealmStartupRequest(new RealmClient.RealmStartupRequest(packetData), ref flag); break;
                    case RealmClientPacket.CharacterLogonRequest: OnCharacterLogonRequest(new RealmClient.CharacterLogonRequest(packetData), ref flag); break;
                    case RealmClientPacket.JoinGameRequest: OnJoinGameRequest(new RealmClient.JoinGameRequest(packetData), ref flag); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(PacketID.ToString() + ": " + ex.Message);
            }
        }

        protected override void BattleNetToDiablo(byte[] data, ref PacketFlag flag)
        {
            byte[] packetData = new byte[data.Length - 2];
            Buffer.BlockCopy(data, 2, packetData, 0, data.Length - 2);

            RealmServerPacket PacketID = (RealmServerPacket)packetData[0];
            
            try
            {
                switch (PacketID)
                {
                    //TODO: Add the rest of these...
                    case RealmServerPacket.JoinGameResponse: OnJoinGameResponse(new RealmServer.JoinGameResponse(packetData), ref flag); break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(PacketID.ToString() + ": " + ex.Message);
            }
        }

    }
}
