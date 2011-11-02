using System;
using D2Data;
using D2Packets;
using ETUtils;

namespace RealmClient
{
	/// <summary>
	/// Base class for Battle.net Client Packets
	/// </summary>
	public class RCPacket : D2Packet
	{
		public readonly RealmClientPacket PacketID;

		public RCPacket(byte[] data) : base(data, Origin.RealmClient)
		{
			this.PacketID = (RealmClientPacket) data[0];
		}
	}

	/// <summary>
	/// RC Packet 0x01 - Realm Startup Request - Request realm connection startup using the information from Bnet server.
	/// </summary>
    public class RealmStartupRequest : RCPacket
    {
        // Fields
        protected uint cookie;
        protected string username;

        // Methods
        public RealmStartupRequest(byte[] data)
            : base(data)
        {
            this.cookie = BitConverter.ToUInt32(data, 1);
            this.username = ByteConverter.GetNullString(data, 0x41);
        }

        public bool CompareStartupData(BnetServer.RealmLogonResponse realmLogon)
        {
            return realmLogon.CompareStartupData(base.Data, 1);
        }

        public bool CompareStartupData(byte[] bytes)
        {
            return this.CompareStartupData(bytes, 0);
        }

        public bool CompareStartupData(byte[] bytes, int offset)
        {
            for (int i = 0; i < 0x40; i++)
            {
                if (base.Data[i + 1] != bytes[i + offset])
                {
                    return false;
                }
            }
            return true;
        }

        // Properties
        public uint Cookie
        {
            get
            {
                return this.cookie;
            }
        }

        public byte[] StartupData
        {
            get
            {
                byte[] destinationArray = new byte[0x40];
                Array.Copy(base.Data, 1, destinationArray, 0, 0x40);
                return destinationArray;
            }
        }

        public string Username
        {
            get
            {
                return this.username;
            }
        }
    }

	/// <summary>
	/// RC Packet 0x02 - Character Creation Request - Request creation of a new realm character in the current account.
	/// </summary>
	public class CharacterCreationRequest : RCPacket
	{
		public readonly CharacterClass Class;
		public readonly CharacterFlags Flags;
		public readonly string Name;

        public CharacterCreationRequest(byte[] data)
            : base(data)
        {
            this.Class = (CharacterClass)BitConverter.ToUInt32(data, 1);
            this.Flags = (CharacterFlags)BitConverter.ToUInt16(data, 5);
            this.Name = ByteConverter.GetNullString(data, 7);
        }
	}

	/// <summary>
	/// RC Packet 0x03 - Create Game Request - Request to join a game - Must be sent after sucessful game creation
	/// </summary>
	public class CreateGameRequest : RCPacket
	{
		public static readonly sbyte NULL_SByte = -1;

		// Before sending the game name and password, Diablo 2 automatically changes their case. 
		// For example if the string "aBc DeF" is typed in Diablo 2, then the string sent is "Abc Def".
		public readonly ushort RequestID; // Starts at 2 at first game and increments by 2 for each consecutive game creation.
		public readonly GameDifficulty Difficulty;
		public readonly sbyte LevelRestriction;	
		public readonly byte MaxPlayers;
		public readonly string Name;
		public readonly string Password = null;
		public readonly string Description = null;
		public readonly byte Unknown1;				// Possibly unused (0) ?
		public readonly ushort Unknown2;			// Possibly unused (0) ?
		public readonly byte Unknown3;				// always 1 ?

        public CreateGameRequest(byte[] data)
            : base(data)
        {
            this.RequestID = BitConverter.ToUInt16(data, 1);
            this.Unknown1 = data[3];
            this.Difficulty = (GameDifficulty)(data[4] >> 4);
            this.Unknown2 = BitConverter.ToUInt16(data, 5);
            this.Unknown3 = data[7];
            this.LevelRestriction = (sbyte)data[8];
            this.MaxPlayers = data[9];
            this.Name = ByteConverter.GetNullString(data, 10);
            if (data.Length > (13 + this.Name.Length))
            {
                this.Password = ByteConverter.GetNullString(data, 11 + this.Name.Length);
            }
            if (data.Length > ((13 + this.Name.Length) + ((this.Password == null) ? 0 : this.Password.Length)))
            {
                this.Description = ByteConverter.GetNullString(data, (12 + this.Name.Length) + ((this.Password == null) ? 0 : this.Password.Length));
            }
        }

	}

	/// <summary>
	/// RC Packet 0x04 - Join Game Request - Request to join a game - Must be sent after sucessful game creation
	/// </summary>
	public class JoinGameRequest : RCPacket
	{
		public readonly ushort RequestID;
		public readonly string Name;
		public readonly string Password = null;

		public JoinGameRequest(byte[] data) : base (data)
		{
			this.RequestID = BitConverter.ToUInt16(data, 1);
			this.Name = ByteConverter.GetNullString(data, 3);
			if (data.Length > 5 + this.Name.Length)
				this.Password = ByteConverter.GetNullString(data, 4 + this.Name.Length);
		}
	}

	/// <summary>
	/// RC Packet 0x05 - Game List Request - Request a list of availiable games.
	/// </summary>
	public class GameListRequest : RCPacket
	{
		public readonly ushort RequestID;
		public readonly uint Unknown1;			// player / session ID ?
		public readonly string Unknown2 = null;

		public GameListRequest(byte[] data) : base (data)
		{
			this.RequestID = BitConverter.ToUInt16(data, 1);
			this.Unknown1 = BitConverter.ToUInt32(data, 3);
			if (data.Length > 8)
				this.Unknown2 = ByteConverter.GetNullString(data, 7);
		}
	}

	/// <summary>
	/// RC Packet 0x06 - Game Info Request - Request information for a particular game.
	/// </summary>
	public class GameInfoRequest : RCPacket
	{
		public readonly ushort RequestID;
		public readonly string Name;

		public GameInfoRequest(byte[] data) : base (data)
		{
			this.RequestID = BitConverter.ToUInt16(data, 1);
			this.Name = ByteConverter.GetNullString(data, 3);
		}
	}

	/// <summary>
    /// RC Packet 0x07 - Character Logon Request - Requests picking a character (sucess bringing you to lobby...)
	/// </summary>
	public class CharacterLogonRequest : RCPacket
	{
		public readonly string Name;

		public CharacterLogonRequest(byte[] data) : base (data)
		{
			this.Name = ByteConverter.GetNullString(data, 1);
		}
	}

	/// <summary>
	/// RC Packet 0x0A - Character Deletion Request - Request deletion of a realm character in the current account.
	/// </summary>
	public class CharacterDeletionRequest : RCPacket
	{
		public readonly uint Cookie;
		public readonly string Name;
		
		public CharacterDeletionRequest(byte[] data) : base (data)
		{
			this.Cookie = BitConverter.ToUInt16(data, 1);
			this.Name = ByteConverter.GetNullString(data, 3);
		}
	}

	/// <summary>
    /// RC Packet 0x12 - Message Of The Day Request - Sent after logon to request RS 0x12
	/// </summary>
	public class MessageOfTheDayRequest : RCPacket
	{
        public MessageOfTheDayRequest(byte[] data)
            : base(data)
		{
		}
	}

	/// <summary>
	/// RC Packet 0x13 - Cancel Game Creation - Cancels a currently pending game creation.
	/// Note: pressing the cancel button after the game was created and client attempts to join won't trigger this packet.
	/// </summary>
	public class CancelGameCreation : RCPacket
	{
		public CancelGameCreation(byte[] data) : base (data)
		{
		}
	}

	/// <summary>
	/// RC Packet 0x18 - Character Upgrade Request - Requests upgrading a chracter from classic to expansion.
	/// </summary>
	public class CharacterUpgradeRequest : RCPacket
	{
		public readonly string Name;

		public CharacterUpgradeRequest(byte[] data) : base (data)
		{
			this.Name = ByteConverter.GetNullString(data, 1);
		}
	}

	/// <summary>
	/// RC Packet 0x19 - Character List Request - Request a list of characters for the current account (with timestamps)
	/// </summary>
	public class CharacterListRequest : RCPacket
	{
		public readonly int Number;

		public CharacterListRequest(byte[] data) : base (data)
		{
			this.Number = BitConverter.ToInt32(data, 1);
		}
	}

}
