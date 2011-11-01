using System;
using System.Collections.Generic;
using System.Text;

namespace RealmServer
{
    /// <summary>
    /// RS Packet 0x04 - Join Game - Reply to join request (RC 0x04)
    /// </summary>
    public class JoinGameResponse : RSPacket
    {
        public readonly ushort RequestID;
        public readonly ushort GameToken;
        public readonly System.Net.IPAddress GameServerIP;
        public readonly uint GameHash;
        public readonly JoinGameResult Result;
        public readonly ushort Unknown;

        public JoinGameResponse(byte[] data)
            : base(data)
        {
            this.RequestID = BitConverter.ToUInt16(data, 1);
            this.GameToken = BitConverter.ToUInt16(data, 3);
            this.Unknown = BitConverter.ToUInt16(data, 5);
            this.GameServerIP = new System.Net.IPAddress(BitConverter.ToUInt32(data, 7));
            this.GameHash = BitConverter.ToUInt32(data, 11);
            this.Result = (JoinGameResult)BitConverter.ToUInt32(data, 15);
        }

        public byte[] PatchedByteArray()
        {
            byte[] patchedBytes = new byte[this.Data.Length + 2];
            Buffer.BlockCopy(this.Data, 0, patchedBytes, 2, 19);

            patchedBytes[0] = 21;
            patchedBytes[1] = 0;

            patchedBytes[9] = 127;
            patchedBytes[10] = 0;
            patchedBytes[11] = 0;
            patchedBytes[12] = 1;

            return patchedBytes;
        }
    }
}
