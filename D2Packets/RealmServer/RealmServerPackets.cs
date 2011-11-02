using System;
using D2Data;
using D2Packets;
using ETUtils;

namespace RealmServer
{
	/// <summary>
	/// Base class for Battle.net Client Packets
	/// </summary>
	public class RSPacket : D2Packet
	{
		public readonly RealmServerPacket PacketID;

		public RSPacket(byte[] data) : base(data, Origin.RealmServer)
		{
			this.PacketID = (RealmServerPacket) data[0];
		}
	}

	public enum RealmCharacterActionResult
	{
		Success = 0,
		/// <summary>
		/// Character already exists or account already has maximum number of characters (currently 8)
		/// </summary>
		CharacterOverlap = 0x14,
		/// <summary>
		/// Character name is longer than 15 characters or contains illegal characters.
		/// </summary>
		InvalidCharacterName = 0x15,
		/// <summary>
		/// Invalid character name specified for action
		/// </summary>
		CharacterNotFound = 0x46,
		/// <summary>
		/// Invalid character name specified for character deletion
		/// </summary>
		CharacterDoesNotExist = 0x49,
		/// <summary>
		/// The action (logon, upgrade, etc. has failed for an unspecified reason)
		/// </summary>
		Failed = 0x7A,
		/// <summary>
		/// Cannot perform any action but delete on expired characters...
		/// </summary>
		CharacterExpired = 0x7B,
		/// <summary>
		/// When trying to upgrade the character
		/// </summary>
		CharacterAlreadyExpansion = 0x7C,
	}

    public enum RealmStartupResult
	{
		Success = 0, 
		NoBattleNetConnection = 0x0C,
		InvalidCDKey = 0x7E,	//TESTME: key in use? key banned? key invalid?
		TemporaryIPBan = 0x7F,	// "Your connection has been temporarily restricted from this realm. Please try to log in at another time"
	}

	/// <summary>
	/// RS Packet 0x01 - Realm Startup - Result of connection start request (RC 0x01)
	/// </summary>
	public class RealmStartupResponse : RSPacket
	{
		public readonly RealmStartupResult Result;

        public RealmStartupResponse(byte[] data)
            : base(data)
		{
			this.Result = (RealmStartupResult) BitConverter.ToUInt32(data, 1);
		}
	}

	/// <summary>
    /// RS Packet 0x02 - Character Creation Response - Result of character creation request (reply to RC 0x02)
	/// </summary>
	public class CharacterCreationResponse : RSPacket
	{
		public readonly RealmCharacterActionResult Result;

        public CharacterCreationResponse(byte[] data)
            : base(data)
		{
			this.Result = (RealmCharacterActionResult) BitConverter.ToUInt32(data, 1);
		}
	}

	public enum CreateGameResult
	{
		Sucess					= 0,	// This does NOT automatically join the game - the client must also send packet RC 0x04
		InvalidGameName			= 0x1E,
		GameAlreadyExists		= 0x1F,
		DeadHardcoreCharacter	= 0x6E,
	}

	/// <summary>
	/// RS Packet 0x03 - Create Game - Reply to join request (RC 0x04)
	/// </summary>
	public class CreateGameResponse : RSPacket
	{
		public readonly ushort RequestID;
		public readonly CreateGameResult Result;
		public readonly uint Unknown;		// If game creation succeeded, this is a nonzero value whose meaning is unknown.

        public CreateGameResponse(byte[] data)
            : base(data)
		{
			this.RequestID = BitConverter.ToUInt16(data, 1);
			this.Unknown = BitConverter.ToUInt32(data, 3);
			this.Result = (CreateGameResult) BitConverter.ToUInt32(data, 7);
		}
	}

	public enum JoinGameResult
	{
		Sucess						= 0,	// Terminate the connection with the MCP and initiate with D2GS.
		PasswordIncorrect			= 0x29,
		GameDoesNotExist			= 0x2A,
		GameFull					= 0x2B,
		LevelRequirementsNotMet		= 0x2C,	// You do not meet the level requirements for this game.
		DeadHardcoreCharacter		= 0x6E,	// A dead hardcore character cannot join a game
		UnableToJoinHardcoreGame	= 0x71,	// A non-hardcore character cannot join a game created by a Hardcore character
		UnableToJoinNightmareGame	= 0x73,
		UnableToJoinHellGame		= 0x74,
		UnableToJoinExpansionGame	= 0x78,	// A non-expansion character cannot join a game created by an Expansion character.
		UnableToJoinClassicGame		= 0x79,	// A Expansion character cannot join a game created by a non-expansion character.
		UnableToJoinLadderGame		= 0x7D	// A non-ladder character cannot join a game created by a Ladder character.
	}

	/// <summary>
	/// RS Packet 0x05 - Game List - Availiable game to list (sent once for each game, in reply to RC 0x05)
	/// </summary>
    public class GameList : RSPacket
    {
        // Fields
        protected string description;
        protected GameFlags flags;
        protected uint index;
        protected string name;
        protected byte playerCount;
        protected ushort requestID;

        // Methods
        public GameList(byte[] data)
            : base(data)
        {
            this.requestID = BitConverter.ToUInt16(data, 1);
            this.index = BitConverter.ToUInt32(data, 3);
            this.playerCount = data[7];
            this.flags = (GameFlags)BitConverter.ToUInt32(data, 8);
            if ((this.flags & GameFlags.Valid) == GameFlags.Valid)
            {
                this.name = ByteConverter.GetNullString(data, 12);
                if (data.Length > (14 + this.name.Length))
                {
                    this.description = ByteConverter.GetNullString(data, 13 + this.name.Length);
                }
            }
        }

        // Properties
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        public GameFlags Flags
        {
            get
            {
                return this.flags;
            }
        }

        public uint Index
        {
            get
            {
                return this.index;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public byte PlayerCount
        {
            get
            {
                return this.playerCount;
            }
        }

        public ushort RequestID
        {
            get
            {
                return this.requestID;
            }
        }
    }

	public class CharacterBaseInfo
	{
		public string Name;
		public CharacterClass Class;
		public int Level;

		public CharacterBaseInfo(string name, int charClass, int level)
		{
			this.Name = name;
			this.Class = (CharacterClass) charClass;
			this.Level = level;
		}

		public override string ToString()
		{
			return StringUtils.ToFormatedInfoString(this, false, ": ", ", ");
		}
	}

    [Flags]
    public enum GameFlags : uint
    {
        Empty = 0x20000,
        Expansion = 0x100000,
        GameDestroyed = 0xfffffffe,
        Hardcore = 0x800,
        Hell = 0x2000,
        Ladder = 0x200000,
        Nightmare = 0x1000,
        ServerDown = 0xffffffff,
        Valid = 4
    }

	/// <summary>
	/// RS Packet 0x06 - Game Info - Provides information about a particular game (reply to RC 0x06)
	/// </summary>
    public class GameInfo : RSPacket
    {
        // Fields
        protected int creatorLevel;
        protected GameFlags flags;
        protected int levelRestriction;
        protected int maxLevel;
        protected int maxPlayers;
        protected int minLevel;
        public static readonly int NULL_Int32 = -1;
        protected int playerCount;
        protected CharacterBaseInfo[] players;
        protected ushort requestID;
        protected TimeSpan uptime;

        // Methods
        public GameInfo(byte[] data)
            : base(data)
        {
            this.minLevel = -1;
            this.maxLevel = -1;
            this.requestID = BitConverter.ToUInt16(data, 1);
            this.flags = (GameFlags)BitConverter.ToUInt32(data, 3);
            this.uptime = new TimeSpan(BitConverter.ToUInt32(data, 7) * 0x989680L);
            this.creatorLevel = data[11];
            this.levelRestriction = (sbyte)data[12];
            if (data[12] != 0xff)
            {
                this.minLevel = Math.Max(1, data[11] - data[12]);
                this.maxLevel = Math.Min(0x63, data[11] + data[12]);
            }
            this.maxPlayers = data[13];
            this.playerCount = data[14];
            this.players = new CharacterBaseInfo[this.playerCount];
            int offset = 0x30;
            for (int i = 0; i < this.playerCount; i++)
            {
                this.players[i] = new CharacterBaseInfo(ByteConverter.GetNullString(data, offset), data[15 + i], data[0x1f + i]);
                offset += this.players[i].Name.Length + 1;
            }
        }

        // Properties
        public int CreatorLevel
        {
            get
            {
                return this.creatorLevel;
            }
        }

        public GameFlags Flags
        {
            get
            {
                return this.flags;
            }
        }

        public int LevelRestriction
        {
            get
            {
                return this.levelRestriction;
            }
        }

        public int MaxLevel
        {
            get
            {
                return this.maxLevel;
            }
        }

        public int MaxPlayers
        {
            get
            {
                return this.maxPlayers;
            }
        }

        public int MinLevel
        {
            get
            {
                return this.minLevel;
            }
        }

        public int PlayerCount
        {
            get
            {
                return this.playerCount;
            }
        }

        public CharacterBaseInfo[] Players
        {
            get
            {
                return this.players;
            }
        }

        public ushort RequestID
        {
            get
            {
                return this.requestID;
            }
        }

        public TimeSpan Uptime
        {
            get
            {
                return this.uptime;
            }
        }
    }

	/// <summary>
	/// RS Packet 0x07 - Character Logon Response - Character log attempt result, sent in reply to RC 0x07
	/// </summary>
    public class CharacterLogonResponse : RSPacket
	{
		public readonly RealmCharacterActionResult Result;

		public CharacterLogonResponse(byte[] data) : base (data)
		{
			this.Result = (RealmCharacterActionResult) BitConverter.ToUInt32(data, 1);
		}
	}

	/// <summary>
    /// RS Packet 0x0A - Character Deletion Response - Result of character deletion, sent in reply to RC 0x0A
	/// </summary>
	public class CharacterDeletionResponse : RSPacket
	{
		public readonly RealmCharacterActionResult Result;

        public CharacterDeletionResponse(byte[] data)
            : base(data)
		{
			this.Result = (RealmCharacterActionResult) BitConverter.ToUInt32(data, 1);
		}
	}

	/// <summary>
	/// RS Packet 0x12 - Message of the Day - Sent after logon in reply to RC 0x12.
    /// More like message of the year ^^
	/// </summary>
	public class MessageOfTheDay : RSPacket
	{
		public readonly string Message;

		public MessageOfTheDay(byte[] data) : base (data)
		{
			// unknown : starting bytes before first 0...
			// supposedly some kind of header but sometimes null, otherwise control characters so unlikely to be a string...
			int offset = 1;
			while (data[offset++] != 0) continue;
			this.Message = ByteConverter.GetNullString(data, offset);
		}
	}

	/// <summary>
	/// RS Packet 0x14 - Game Creation Queue - Initinialise waiting queue or update queue position.
	/// </summary>
	public class GameCreationQueue : RSPacket
	{
		public readonly uint Position;

		public GameCreationQueue(byte[] data) : base (data)
		{
			this.Position = BitConverter.ToUInt32(data, 1);
		}
	}

	/// <summary>
	/// RS Packet 0x18 - CharacterUpgradeResult - Character upgrade attempt result, sent in reply to RC 0x18
	/// </summary>
	public class CharacterUpgradeResponse : RSPacket
	{
		public readonly RealmCharacterActionResult Result;

        public CharacterUpgradeResponse(byte[] data)
            : base(data)
		{
			this.Result = (RealmCharacterActionResult) BitConverter.ToUInt32(data, 1);
		}
	}

	/// <summary>
	/// RS Packet 0x19 - Character List - Request a list of characters for the current account (with timestamps)
	/// </summary>
	public class CharacterList : RSPacket
	{
		public readonly uint Requested;
		public readonly uint Total;
		public readonly uint Listed;
        public readonly D2Packets.CharacterInfo[] Characters;

        public CharacterList(byte[] data)
            : base(data)
        {
            this.Requested = BitConverter.ToUInt16(data, 1);
            this.Total = BitConverter.ToUInt32(data, 3);
            this.Listed = BitConverter.ToUInt16(data, 7);
            this.Characters = new D2Packets.CharacterInfo[this.Listed];
            int startIndex = 9;
            for (int i = 0; (i < this.Listed) && (startIndex < data.Length); i++)
            {
                this.Characters[i] = new D2Packets.CharacterInfo();
                this.Characters[i].Expires = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, startIndex));
                this.Characters[i].Name = ByteConverter.GetNullString(data, startIndex += 4);
                startIndex += this.Characters[i].Name.Length + 1;
                StatString.ParseD2StatString(data, startIndex, ref this.Characters[i].ClientVersion, ref this.Characters[i].Class, ref this.Characters[i].Level, ref this.Characters[i].Flags, ref this.Characters[i].Act, ref this.Characters[i].Title);
                startIndex = ByteConverter.GetBytePosition(data, 0, startIndex) + 1;
            }
        }
	}


}
