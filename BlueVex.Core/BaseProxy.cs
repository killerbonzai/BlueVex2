using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using BnetServer;
using RealmServer;
using RealmClient;
using BnetClient;
using D2Packets;

namespace BlueVex.Core
{
    public abstract class BaseProxy
    {
        public ProxyServer ProxyServer { get; set; }

        // Diablo II
        private TcpClient diablo;
        protected NetworkStream diabloStream;
        private byte[] diabloBuffer = new byte[24576];

        // BattleNet
        private TcpClient battleNet;
        protected NetworkStream battleNetStream;
        private byte[] battleNetBuffer = new byte[24576];

        protected bool isCompressed = false;

        public BaseProxy(TcpClient client)
        {
            this.diablo = client;
        }

        protected void Init()
        {
            diabloStream = diablo.GetStream();
            
            try
            {
                diabloStream.BeginRead(diabloBuffer, 0, diablo.ReceiveBufferSize, new AsyncCallback(OnClientReceive), diabloStream);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Connect(IPAddress serverAddress, int serverPort)
        {
            battleNet = new TcpClient();
            battleNet.Client.DontFragment = true;
            battleNet.Client.NoDelay = true;
            battleNet.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

            try
            {
                battleNet.Connect(serverAddress, serverPort);
                battleNetStream = battleNet.GetStream();
                battleNetStream.BeginRead(battleNetBuffer, 0, battleNet.ReceiveBufferSize, new AsyncCallback(OnServerReceive), battleNetStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + ":" + ex.Message);
                //throw;
            }
        }

        public void Disconnect()
        {
            if (battleNetStream != null)
            {
                battleNetStream.Close();
                battleNetStream.Dispose();
            }
            if (battleNet != null && battleNet.Client.Connected)
                battleNet.Client.Disconnect(false);

            if (diabloStream != null)
            {
                diabloStream.Close();
                diabloStream.Dispose();
            }
            if (diablo != null && diablo.Client.Connected)
                diablo.Client.Disconnect(false);

        }

        protected void OnClientSend(IAsyncResult ar)
        {
            try
            {
                diabloStream.EndWrite(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void OnClientReceive(IAsyncResult ar)
        {
            int bytesRead = 0;

            try
            {
                if (diabloStream == null || !diablo.Connected) return;
                bytesRead = diabloStream.EndRead(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + ":" + "Client Recieve Error" + ex.Message);
                bytesRead = -1;
            }

            if (bytesRead <= 0 ){
                Console.WriteLine(this.GetType().Name + ":" + "Client Closed the Connection");

                // If diablo closes the connection, close the proxy connection to battle net
                Disconnect();
                return;
            }

            byte[] packetBytes = new byte[bytesRead];
            Buffer.BlockCopy(diabloBuffer, 0, packetBytes, 0, bytesRead);

            // Handle the packet and send it on if necessary
            HandleClientReceive(packetBytes);

            try
            {
                diabloStream.BeginRead(diabloBuffer, 0, diablo.ReceiveBufferSize, new AsyncCallback(OnClientReceive), diabloStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + ":" + "Client Read Error" + ex.Message);
            }
        }

        protected virtual void HandleClientReceive(byte[] packetBytes)
        {
            PacketFlag flag = PacketFlag.PacketFlag_Normal;
            DiabloToBattleNet(packetBytes, ref flag);

            try
            {
                if (flag == PacketFlag.PacketFlag_Normal)
                {
                    if (battleNetStream != null)
                    {
                        battleNetStream.BeginWrite(packetBytes, 0, packetBytes.Length, new AsyncCallback(OnServerSend), battleNetStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + ":" + "BattleNet Send Error" + ex.Message);
                Disconnect();
            }
        }

        protected void OnServerSend(IAsyncResult ar)
        {
            try
            {
                battleNetStream.EndWrite(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void OnServerReceive(IAsyncResult ar)
        {
            int bytesRead = 0;

            try
            {
                if (battleNetStream == null || !battleNet.Connected) return;
                bytesRead = battleNetStream.EndRead(ar);
            }
            catch (Exception ex)
            {
                bytesRead = -1;
                Console.WriteLine(this.GetType().Name + ":" + "Server Recieve Error" + ex.Message);
            }

            if (bytesRead <= 0)
            {
                Console.WriteLine(this.GetType().Name + ":" + "Server Closed the Connection");

                Disconnect();
                return;
            }

            byte[] packetBytes = new byte[bytesRead];
            Buffer.BlockCopy(battleNetBuffer, 0, packetBytes, 0, bytesRead);

            // Handle the packet and send it on if necessary
            HandleServerReceive(packetBytes);

            try
            {
                battleNetStream.BeginRead(battleNetBuffer, 0, diablo.ReceiveBufferSize, new AsyncCallback(OnServerReceive), battleNetStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + ":" + "Server Read Error" + ex.Message);
            }
        }

        protected virtual void HandleServerReceive(byte[] packetBytes)
        {
            PacketFlag flag = PacketFlag.PacketFlag_Normal;
            BattleNetToDiablo(packetBytes, ref flag);

            try
            {
                if (flag == PacketFlag.PacketFlag_Normal)
                {
                    diabloStream.BeginWrite(packetBytes, 0, packetBytes.Length, new AsyncCallback(OnClientSend), diabloStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + ":" + "Client Write Error" + ex.Message);
                Disconnect();
            }
        }

        protected abstract void BattleNetToDiablo(byte[] data, ref PacketFlag flag);
        protected abstract void DiabloToBattleNet(byte[] data, ref PacketFlag flag);

        public void SendToDiablo(byte[] data)
        {
            SendToDiablo(data, data.Length);
        }

        public void SendToDiablo(byte[] data, int length)
        {
            try
            {
                diabloStream.BeginWrite(data, 0, length, new AsyncCallback(OnClientSend), diabloStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + ":" + "Send to diablo Error" + ex.Message);
                Disconnect();
            }
        }

        public void SendToBattleNet(byte[] data)
        {
            SendToBattleNet(data, data.Length);
        }

        public void SendToBattleNet(byte[] data, int length)
        {
            try
            {
                battleNetStream.BeginWrite(data, 0, length, new AsyncCallback(OnServerSend), battleNetStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.GetType().Name + ":" + "Send to Bnet Error" + ex.Message);
                Disconnect();
            }
        }

    }
}
