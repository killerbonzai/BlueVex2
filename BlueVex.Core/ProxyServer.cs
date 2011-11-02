using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace BlueVex.Core
{
    public class ProxyServer
    {
        private TcpListener tcpListener;
        private ProxyType proxyType;

        public delegate void ClientConnectedHandler(object sender, EventArgs args);
        public event ClientConnectedHandler ClientConnected = delegate { };
        
        public delegate void AttachDiabloWindowHandler(Proxy proxy, string keyOwner);
        public event AttachDiabloWindowHandler AttachDiabloWindow = delegate { };
        public void RaiseAttachDiabloWindow(Proxy proxy, string keyOwner)
        {
            this.AttachDiabloWindow(proxy, keyOwner);
        }

        private static List<BnetProxy> BnetProxies = new List<BnetProxy>();
        private static List<RealmProxy> RealmProxies = new List<RealmProxy>();
        private static List<GameProxy> GameProxies = new List<GameProxy>();

        public static RealmProxy RealmProxyJoiningGame { get; set; }

        public static BnetProxy FindBnetProxyForRealm(string username)
        {
            Console.WriteLine("Finding for user " + username);
            if (string.IsNullOrEmpty(username))
            {
                foreach (BnetProxy proxy in BnetProxies)
                {
                    if (proxy.RealmAddress != null)
                    {
                        return proxy;
                    }
                }
                return null;
            }

            BnetProxy result = null;

            foreach (BnetProxy proxy in BnetProxies)
            {
                if (proxy.Username == username)
                {
                    result = proxy;
                    break;
                }
            }

            return result;
        }

        public static RealmProxy FindRealmProxyForGame(string characterName)
        {
            if (string.IsNullOrEmpty(characterName))
            {
                return RealmProxyJoiningGame;
            }

            RealmProxy result = null;

            foreach (RealmProxy proxy in RealmProxies)
            {
                if (proxy.CharacterName == characterName)
                {
                    result = proxy;
                    break;
                }
            }

            return result;
        }

        public ProxyServer(int port, ProxyType proxyType)
        {
            this.proxyType = proxyType;

            tcpListener = new TcpListener(IPAddress.Loopback, port);
            tcpListener.Server.DontFragment = true;
            tcpListener.Server.NoDelay = true;
            tcpListener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

            try
            {
                tcpListener.Start();
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(OnAcceptTcpClient), null);
            }
            catch (Exception)
            {
                tcpListener = null;
                throw;
            }
        }

        private void OnAcceptTcpClient(IAsyncResult ar)
        {
            TcpClient tcpClient = tcpListener.EndAcceptTcpClient(ar);
            BaseProxy proxyClient = null;

            try
            {
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(OnAcceptTcpClient), null);
            }
            catch (Exception)
            {
                tcpListener = null;
                throw;
            }

            switch (proxyType)
            {
                case ProxyType.Bnet :
                    proxyClient = new BnetProxy(tcpClient);
                    proxyClient.ProxyServer = this;
                    BnetProxies.Add((BnetProxy)proxyClient);
                    break;

                case ProxyType.Realm  :
                    proxyClient = new RealmProxy(tcpClient);
                    RealmProxies.Add((RealmProxy)proxyClient);
                    break;

                case ProxyType.Game :
                    proxyClient = new GameProxy(tcpClient);
                    GameProxies.Add((GameProxy)proxyClient);
                    break;
            }

            this.ClientConnected(proxyClient, new EventArgs());

        }

    }
}
