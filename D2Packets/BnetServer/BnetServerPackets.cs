using System;
using D2Data;
using D2Packets;
using ETUtils;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BnetServer
{
    public class AdInfo : BSPacket
{
    // Fields
    protected string extension;
    protected string filename;
    protected uint id;
    protected DateTime timestamp;
    protected string url;

    // Methods
    public AdInfo(byte[] data) : base(data)
    {
        this.id = BitConverter.ToUInt32(data, 3);
        this.extension = ByteConverter.GetString(data, 7, 4);
        this.timestamp = DateTime.FromFileTimeUtc(BitConverter.ToInt64(data, 11));
        this.filename = ByteConverter.GetNullString(data, 0x13);
        this.url = ByteConverter.GetNullString(data, 20 + this.filename.Length);
    }

    // Properties
    public string Extension
    {
        get
        {
            return this.extension;
        }
    }

    public string Filename
    {
        get
        {
            return this.filename;
        }
    }

    public uint ID
    {
        get
        {
            return this.id;
        }
    }

    public DateTime Timestamp
    {
        get
        {
            return this.timestamp;
        }
    }

    public string URL
    {
        get
        {
            return this.url;
        }
    }
}

    public class BnetAuthResponse : BSPacket
{
    // Fields
    protected string info;
    protected BnetAuthResult result;

    // Methods
    public BnetAuthResponse(byte[] data) : base(data)
    {
        this.result = (BnetAuthResult) BitConverter.ToUInt32(data, 3);
        if (data.Length > 8)
        {
            this.info = ByteConverter.GetNullString(data, 7);
        }
    }

    // Properties
    public string Info
    {
        get
        {
            return this.info;
        }
    }

    public BnetAuthResult Result
    {
        get
        {
            return this.result;
        }
    }
}

    public enum BnetAuthResult
{
    BannedCDKey = 0x202,
    BuggedVersion = 0x102,
    CDKeyInUse = 0x201,
    InvalidCDKey = 0x200,
    InvalidVersion = 0x101,
    OldVersion = 0x100,
    Success = 0,
    WrongProduct = 0x203
}

    public class BnetConnectionResponse : BSPacket
{
    // Fields
    protected uint logonType;
    protected uint serverToken;
    protected uint udpValue;
    protected string versionFileName;
    protected DateTime versionFileTime;
    protected string versionFormulae;

    // Methods
    public BnetConnectionResponse(byte[] data) : base(data)
    {
        this.logonType = BitConverter.ToUInt32(data, 3);
        this.serverToken = BitConverter.ToUInt32(data, 7);
        this.udpValue = BitConverter.ToUInt32(data, 11);
        this.versionFileTime = DateTime.FromFileTimeUtc(BitConverter.ToInt64(data, 15));
        this.versionFileName = ByteConverter.GetNullString(data, 0x17);
        this.versionFormulae = ByteConverter.GetNullString(data, 0x18 + this.VersionFileName.Length);
    }

    // Properties
    public uint LogonType
    {
        get
        {
            return this.logonType;
        }
    }

    public uint UDPValue
    {
        get
        {
            return this.udpValue;
        }
    }

    public string VersionFileName
    {
        get
        {
            return this.versionFileName;
        }
    }

    public DateTime VersionFileTime
    {
        get
        {
            return this.versionFileTime;
        }
    }

    public string VersionFormulae
    {
        get
        {
            return this.versionFormulae;
        }
    }
}

    public class BnetLogonResponse : BSPacket
{
    // Fields
    protected string reason;
    protected BnetLogonResult result;

    // Methods
    public BnetLogonResponse(byte[] data) : base(data)
    {
        this.result = (BnetLogonResult) BitConverter.ToUInt32(data, 3);
        if (data.Length > 7)
        {
            this.reason = ByteConverter.GetNullString(data, 7);
        }
    }

    // Properties
    public string Reason
    {
        get
        {
            return this.reason;
        }
    }

    public BnetLogonResult Result
    {
        get
        {
            return this.result;
        }
    }
}

    public enum BnetLogonResult
{
    Success,
    AccountDoesNotExist,
    PasswordIncorrect
}

    public class BnetPing : BSPacket
{
    // Fields
    protected uint timestamp;

    // Methods
    public BnetPing(byte[] data) : base(data)
    {
        this.timestamp = BitConverter.ToUInt32(data, 3);
    }

    // Properties
    public uint Timestamp
    {
        get
        {
            return this.timestamp;
        }
    }
}

    public class BSPacket : D2Packet
{
    // Fields
    public readonly BnetServerPacket PacketID;

    // Methods
    public BSPacket(byte[] data) : base(data, Origin.BattleNetServer)
    {
        this.PacketID = (BnetServerPacket) data[0];
    }
}

    public class ChannelList : BSPacket
{
    // Fields
    protected List<string> channels;

    // Methods
    public ChannelList(byte[] data) : base(data)
    {
        this.channels = new List<string>();
        int offset = 3;
        for (int i = 0; offset < (data.Length - 1); i++)
        {
            this.channels.Add(ByteConverter.GetNullString(data, offset));
            offset += this.channels[i].Length + 1;
        }
    }

    // Properties
    public List<string> Channels
    {
        get
        {
            return this.channels;
        }
    }
}

    public class ChatEvent : BSPacket
{
    // Fields
    protected string account;
    protected int characterAct;
    protected CharacterFlags characterFlags;
    protected int characterLevel;
    protected CharacterTitle characterTitle;
    protected BattleNetCharacter characterType;
    protected BattleNetClient client;
    protected int clientVersion;
    protected ChatEventType eventType;
    protected uint flags;
    protected string message;
    protected string name;
    public static readonly int NULL_Int32 = -1;
    public static readonly int NULL_UInt32 = 0;
    protected uint ping;
    protected string realm;

    // Methods
    public ChatEvent(byte[] data) : base(data)
    {
        this.clientVersion = -1;
        this.characterType = BattleNetCharacter.Unknown;
        this.characterLevel = -1;
        this.characterAct = -1;
        this.characterTitle = CharacterTitle.None;
        this.eventType = (ChatEventType) BitConverter.ToUInt32(data, 3);
        this.flags = BitConverter.ToUInt32(data, 7);
        this.ping = BitConverter.ToUInt32(data, 11);
        int length = ByteConverter.GetByteOffset(data, 0, 0x1b);
        int num2 = ByteConverter.GetByteOffset(data, 0x2a, 0x1b, length);
        if (num2 > 0)
        {
            this.name = ByteConverter.GetString(data, 0x1b, num2);
            length -= num2 + 1;
            num2 += 0x1c;
        }
        else if (num2 == 0)
        {
            num2 = 0x1c;
            length--;
            this.characterType = BattleNetCharacter.OpenCharacter;
        }
        else
        {
            num2 = 0x1b;
        }
        this.account = ByteConverter.GetString(data, num2, length);
        length += num2 + 1;
        if (this.eventType != ChatEventType.ChannelLeave)
        {
            if ((this.eventType == ChatEventType.ChannelJoin) || (this.eventType == ChatEventType.ChannelUser))
            {
                if ((data.Length - length) > 3)
                {
                    this.client = (BattleNetClient) BitConverter.ToUInt32(data, length);
                    length += 4;
                }
                if ((((this.client != BattleNetClient.StarcraftShareware) && (this.client != BattleNetClient.Starcraft)) && (this.client != BattleNetClient.StarcraftBroodWar)) && ((this.client == BattleNetClient.Diablo2) || (this.client == BattleNetClient.Diablo2LoD)))
                {
                    if (this.client == BattleNetClient.Diablo2LoD)
                    {
                        this.characterFlags |= CharacterFlags.Expansion;
                    }
                    if ((data.Length - length) >= 4)
                    {
                        this.realm = ByteConverter.GetString(data, length, -1, 0x2c);
                        length += this.realm.Length + 1;
                        if (data.Length >= length)
                        {
                            length += ByteConverter.GetByteOffset(data, 0x2c, length) + 1;
                            if (((length != -1) && (data.Length > length)) && ((data.Length - length) >= 0x21))
                            {
                                StatString.ParseD2StatString(data, length, ref this.clientVersion, ref this.characterType, ref this.characterLevel, ref this.characterFlags, ref this.characterAct, ref this.characterTitle);
                            }
                        }
                    }
                }
            }
            else
            {
                this.message = ByteConverter.GetNullString(data, length);
            }
        }
    }

    // Properties
    public string Account
    {
        get
        {
            return this.account;
        }
    }

    public int CharacterAct
    {
        get
        {
            return this.characterAct;
        }
    }

    public CharacterFlags CharacterFlags
    {
        get
        {
            return this.characterFlags;
        }
    }

    public int CharacterLevel
    {
        get
        {
            return this.characterLevel;
        }
    }

    public CharacterTitle CharacterTitle
    {
        get
        {
            return this.characterTitle;
        }
    }

    public BattleNetCharacter CharacterType
    {
        get
        {
            return this.characterType;
        }
    }

    public BattleNetClient Client
    {
        get
        {
            return this.client;
        }
    }

    public int ClientVersion
    {
        get
        {
            return this.clientVersion;
        }
    }

    public ChatEventType Event
    {
        get
        {
            return this.eventType;
        }
    }

    public uint Flags
    {
        get
        {
            return this.flags;
        }
    }

    public string Message
    {
        get
        {
            return this.message;
        }
    }

    public string Name
    {
        get
        {
            return this.name;
        }
    }

    public uint Ping
    {
        get
        {
            return this.ping;
        }
    }

    public string Realm
    {
        get
        {
            return this.realm;
        }
    }
}

    public enum ChatEventType : uint
{
    Broadcast = 0x12,
    ChannelDoesNotExist = 14,
    ChannelFull = 13,
    ChannelInfo = 7,
    ChannelJoin = 2,
    ChannelLeave = 3,
    ChannelMessage = 5,
    ChannelRestricted = 15,
    ChannelUser = 1,
    Emote = 0x17,
    Error = 0x13,
    ReceiveWhisper = 4,
    ServerBroadcast = 6,
    UserFlags = 9,
    WhisperSent = 10
}

    public class EnterChatResponse : BSPacket
{
    // Fields
    protected string account;
    protected int characterAct;
    protected CharacterFlags characterFlags;
    protected int characterLevel;
    protected CharacterTitle characterTitle;
    protected BattleNetCharacter characterType;
    protected BattleNetClient client;
    protected int clientVersion;
    protected string name;
    protected string realm;
    protected string username;

    // Methods
    public EnterChatResponse(byte[] data) : base(data)
    {
        this.clientVersion = -1;
        this.characterType = BattleNetCharacter.Unknown;
        this.characterLevel = -1;
        this.characterAct = -1;
        this.characterTitle = CharacterTitle.None;
        this.username = ByteConverter.GetNullString(data, 3);
        int startIndex = 4 + this.username.Length;
        this.client = (BattleNetClient) BitConverter.ToUInt32(data, startIndex);
        if (data[startIndex += 4] == 0)
        {
            this.account = ByteConverter.GetNullString(data, startIndex + 1);
        }
        else
        {
            this.realm = ByteConverter.GetString(data, startIndex, -1, 0x2c);
            startIndex += 1 + this.realm.Length;
            this.name = ByteConverter.GetString(data, startIndex, -1, 0x2c);
            startIndex += 1 + this.name.Length;
            int num2 = ByteConverter.GetByteOffset(data, 0, startIndex);
            this.account = ByteConverter.GetNullString(data, (startIndex + num2) + 1);
            if (this.client == BattleNetClient.Diablo2LoD)
            {
                this.characterFlags |= CharacterFlags.Expansion;
            }
            StatString.ParseD2StatString(data, startIndex, ref this.clientVersion, ref this.characterType, ref this.characterLevel, ref this.characterFlags, ref this.characterAct, ref this.characterTitle);
        }
    }

    // Properties
    public string Account
    {
        get
        {
            return this.account;
        }
    }

    public int CharacterAct
    {
        get
        {
            return this.characterAct;
        }
    }

    public CharacterFlags CharacterFlags
    {
        get
        {
            return this.characterFlags;
        }
    }

    public int CharacterLevel
    {
        get
        {
            return this.characterLevel;
        }
    }

    public CharacterTitle CharacterTitle
    {
        get
        {
            return this.characterTitle;
        }
    }

    public BattleNetCharacter CharacterType
    {
        get
        {
            return this.characterType;
        }
    }

    public BattleNetClient Client
    {
        get
        {
            return this.client;
        }
    }

    public int ClientVersion
    {
        get
        {
            return this.clientVersion;
        }
    }

    public string Name
    {
        get
        {
            return this.name;
        }
    }

    public string Realm
    {
        get
        {
            return this.realm;
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

    public class ExtraWorkInfo : BSPacket
{
    // Fields
    protected string filename;

    // Methods
    public ExtraWorkInfo(byte[] data) : base(data)
    {
        this.filename = ByteConverter.GetNullString(data, 3);
    }

    // Properties
    public string Filename
    {
        get
        {
            return this.filename;
        }
    }
}

    public class FileTimeInfo : BSPacket
{
    // Fields
    protected string filename;
    protected DateTime filetime;
    protected uint requestID;
    protected uint unknown;

    // Methods
    public FileTimeInfo(byte[] data) : base(data)
    {
        this.requestID = BitConverter.ToUInt32(data, 3);
        this.unknown = BitConverter.ToUInt32(data, 7);
        this.filetime = DateTime.FromFileTimeUtc(BitConverter.ToInt64(data, 11));
        this.filename = ByteConverter.GetNullString(data, 0x13);
    }

    // Properties
    public string Filename
    {
        get
        {
            return this.filename;
        }
    }

    
    public DateTime FileTime
    {
        get
        {
            return this.filetime;
        }
    }
    
    public uint RequestID
    {
        get
        {
            return this.requestID;
        }
    }

    public uint Unknown
    {
        get
        {
            return this.unknown;
        }
    }
}

    public class KeepAlive : BSPacket
{
    // Methods
    public KeepAlive(byte[] data) : base(data)
    {
    }
}

    [StructLayout(LayoutKind.Sequential)]
    public struct NewsEntry
{
    private DateTime timestamp;
    private string content;
    public NewsEntry(byte[] data, int offset)
    {
        this.timestamp = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, offset));
        this.content = ByteConverter.GetNullString(data, offset + 4);
    }

    public DateTime Timestamp
    {
        get
        {
            return this.timestamp;
        }
    }
    public string Content
    {
        get
        {
            return this.content;
        }
    }
    public override string ToString()
    {
        return string.Format("Timestamp: {0}, Content: {1}", this.Timestamp, this.Content);
    }
}

    public class NewsInfo : BSPacket
{
    // Fields
    protected int count;
    protected NewsEntry[] entries;
    protected DateTime lastLogon;
    protected DateTime newestEntry;
    protected DateTime oldestEntry;

    // Methods
    public NewsInfo(byte[] data) : base(data)
    {
        this.count = data[3];
        this.lastLogon = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, 4));
        this.oldestEntry = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, 8));
        this.newestEntry = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, 12));
        this.entries = new NewsEntry[this.count];
        int offset = 0x10;
        for (int i = 0; i < this.entries.Length; i++)
        {
            this.entries[i] = new NewsEntry(data, offset);
            offset += 5 + this.entries[i].Content.Length;
        }
    }

    // Properties
    public int Count
    {
        get
        {
            return this.count;
        }
    }

    public NewsEntry[] Entries
    {
        get
        {
            return this.entries;
        }
    }

    public DateTime LastLogon
    {
        get
        {
            return this.lastLogon;
        }
    }

    public DateTime NewestEntry
    {
        get
        {
            return this.newestEntry;
        }
    }

    public DateTime OldestEntry
    {
        get
        {
            return this.oldestEntry;
        }
    }
}

    public class QueryRealmsResponse : BSPacket
{
    // Fields
    protected uint count;
    protected RealmInfo[] realms;
    protected uint unknown;

    // Methods
    public QueryRealmsResponse(byte[] data) : base(data)
    {
        this.unknown = BitConverter.ToUInt32(data, 3);
        this.count = BitConverter.ToUInt32(data, 7);
        this.realms = new RealmInfo[this.count];
        int offset = 11;
        for (int i = 0; i < this.count; i++)
        {
            this.realms[i] = new RealmInfo(data, offset);
            offset += (6 + this.realms[i].Name.Length) + this.realms[i].Description.Length;
        }
    }

    // Properties
    public uint Count
    {
        get
        {
            return this.count;
        }
    }

    public RealmInfo[] Realms
    {
        get
        {
            return this.realms;
        }
    }

    public uint Unknown
    {
        get
        {
            return this.unknown;
        }
    }
}

    public enum RealmLogonResult : uint
    {
        LogonFailed = 0x80000002,
        RealmUnavailable = 0x80000001,
        Success = 0
    }

    public class RequiredExtraWorkInfo : BSPacket
{
    // Fields
    protected string filename;

    // Methods
    public RequiredExtraWorkInfo(byte[] data) : base(data)
    {
        this.filename = ByteConverter.GetNullString(data, 3);
    }

    // Properties
    public string Filename
    {
        get
        {
            return this.filename;
        }
    }
}
}
