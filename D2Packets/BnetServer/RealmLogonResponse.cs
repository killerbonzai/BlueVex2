using System;
using System.Collections.Generic;
using System.Text;
using ETUtils;

namespace BnetServer
{
    public class RealmLogonResponse : BSPacket
    {
        // Fields
        protected uint cookie;
        public static readonly int NULL_Int32 = -1;
        protected System.Net.IPAddress realmServerIP;
        protected int realmServerPort;
        protected RealmLogonResult result;
        protected ushort unknown;
        protected string username;

        // Properties
        public uint Cookie
        {
            get
            {
                return this.cookie;
            }
        }

        public System.Net.IPAddress RealmServerIP
        {
            get
            {
                return this.realmServerIP;
            }
        }

        public int RealmServerPort
        {
            get
            {
                return this.realmServerPort;
            }
        }

        public RealmLogonResult Result
        {
            get
            {
                return this.result;
            }
        }

        public byte[] StartupData
        {
            get
            {
                if (this.result != RealmLogonResult.Success)
                {
                    return null;
                }
                byte[] destinationArray = new byte[0x40];
                Array.Copy(base.Data, 3, destinationArray, 0, 0x10);
                Array.Copy(base.Data, 0x1b, destinationArray, 0x10, 0x30);
                return destinationArray;
            }
        }

        public ushort Unknown
        {
            get
            {
                return this.unknown;
            }
        }

        public string Username
        {
            get
            {
                return this.username;
            }
        }

        // Methods
        public RealmLogonResponse(byte[] data)
            : base(data)
        {
            this.realmServerPort = -1;
            this.cookie = BitConverter.ToUInt32(data, 3);
            if (base.Data.Length < 0x4a)
            {
                this.result = (RealmLogonResult)BitConverter.ToUInt32(data, 7);
            }
            else
            {
                this.result = RealmLogonResult.Success;
                this.realmServerIP = new System.Net.IPAddress((long)BitConverter.ToUInt32(data, 0x13));
                this.realmServerPort = BEBitConverter.ToUInt16(data, 0x17);
                this.username = ByteConverter.GetNullString(data, 0x4b);
                //this.unknown = BitConverter.ToUInt16(data, 0x4c + this.username.Length);
            }
        }

        public bool CompareStartupData(RealmClient.RealmStartupRequest realmStartup)
        {
            return realmStartup.CompareStartupData(this);
        }

        public bool CompareStartupData(byte[] bytes)
        {
            return this.CompareStartupData(bytes, 0);
        }

        public bool CompareStartupData(byte[] bytes, int offset)
        {
            for (int i = 0; i < 0x10; i++)
            {
                if (base.Data[i + 3] != bytes[i + offset])
                {
                    return false;
                }
            }
            offset += 0x10;
            for (int j = 0; j < 0x30; j++)
            {
                if (base.Data[j + 0x1b] != bytes[j + offset])
                {
                    return false;
                }
            }
            return true;
        }

        public byte[] PatchedByteArray()
        {
            byte[] patchedBytes = new byte[this.Data.Length + 1];
            Buffer.BlockCopy(this.Data, 0, patchedBytes, 1, this.Data.Length);

            patchedBytes[0] = 255;

            patchedBytes[20] = 127;
            patchedBytes[21] = 0;
            patchedBytes[22] = 0;
            patchedBytes[23] = 1;

            byte[] PortBytes = BitConverter.GetBytes((short)6113);
            patchedBytes[24] = PortBytes[1];
            patchedBytes[25] = PortBytes[0];

            return patchedBytes;
        }
    }
}
