using System;
using D2Data;
using D2Packets;
using ETUtils;

namespace BnetClient
{

    public class AdInfoRequest : BCPacket
{
    // Fields
    protected BattleNetClient client;
    protected uint id;
    protected BattleNetPlatform platform;
    protected DateTime timestamp;

    // Methods
    public AdInfoRequest(byte[] data) : base(data)
    {
        this.platform = (BattleNetPlatform) BitConverter.ToUInt32(data, 3);
        this.client = (BattleNetClient) BitConverter.ToUInt32(data, 7);
        this.id = BitConverter.ToUInt32(data, 11);
        this.timestamp = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, 15));
    }

    // Properties
    public BattleNetClient Client
    {
        get
        {
            return this.client;
        }
    }

    public uint ID
    {
        get
        {
            return this.id;
        }
    }

    public BattleNetPlatform Platform
    {
        get
        {
            return this.platform;
        }
    }

    public DateTime Timestamp
    {
        get
        {
            return this.timestamp;
        }
    }
}

    public class BCPacket : D2Packet
    {
        // Fields
        public readonly BnetClientPacket PacketID;

        // Methods
        public BCPacket(byte[] data)
            : base(data, Origin.BattleNetClient)
        {
            this.PacketID = (BnetClientPacket)data[1];
        }
    }

    public class BnetAuthRequest : BCPacket
{
    // Fields
    protected uint clientToken;
    protected uint gameHash;
    protected string gameInfo;
    protected uint gameVersion;
    protected uint keyCount;
    protected CDKeyInfo[] keys;
    protected string ownerName;
    protected bool useSpawn;

    // Methods
    public BnetAuthRequest(byte[] data) : base(data)
    {
        this.clientToken = BitConverter.ToUInt32(data, 3);
        this.gameVersion = BitConverter.ToUInt32(data, 7);
        this.gameHash = BitConverter.ToUInt32(data, 11);
        this.keyCount = BitConverter.ToUInt32(data, 15);
        this.useSpawn = BitConverter.ToUInt32(data, 0x13) == 1;
        this.keys = new CDKeyInfo[this.keyCount];
        int offset = 0x17;
        for (int i = 0; i < this.keyCount; i++)
        {
            this.keys[i] = new CDKeyInfo(data, offset);
            offset += 0x24;
        }
        this.gameInfo = ByteConverter.GetNullString(data, offset);
        this.ownerName = ByteConverter.GetNullString(data, (offset + this.gameInfo.Length) + 1);
    }

    // Properties
    public uint ClientToken
    {
        get
        {
            return this.clientToken;
        }
    }

    public uint GameHash
    {
        get
        {
            return this.gameHash;
        }
    }

    public string GameInfo
    {
        get
        {
            return this.gameInfo;
        }
    }

    public uint GameVersion
    {
        get
        {
            return this.gameVersion;
        }
    }

    public uint KeyCount
    {
        get
        {
            return this.keyCount;
        }
    }

    public CDKeyInfo[] Keys
    {
        get
        {
            return this.keys;
        }
    }

    public string OwnerName
    {
        get
        {
            return this.ownerName;
        }
    }

    public bool UseSpawn
    {
        get
        {
            return this.useSpawn;
        }
    }
}

    public class BnetConnectionRequest : BCPacket
{
    // Fields
    protected BattleNetClient client;
    protected string countryAbbreviation;
    protected string countryName;
    protected static uint CurrentD2LoDVersion = 11;
    protected static uint CurrentD2Version = 11;
    protected uint language;
    protected uint languageID;
    protected uint localeID;
    protected System.Net.IPAddress localIP;
    protected BattleNetPlatform platform;
    protected uint protocol;
    protected uint timeZoneBias;
    protected uint version;

    // Methods
    public BnetConnectionRequest(byte[] data) : base(data)
    {
        this.protocol = BitConverter.ToUInt32(data, 3);
        this.platform = (BattleNetPlatform) BitConverter.ToUInt32(data, 7);
        this.client = (BattleNetClient) BitConverter.ToUInt32(data, 11);
        this.version = BitConverter.ToUInt32(data, 15);
        this.language = BitConverter.ToUInt32(data, 0x13);
        this.localIP = new System.Net.IPAddress((long)BitConverter.ToUInt32(data, 0x17));
        this.timeZoneBias = BitConverter.ToUInt32(data, 0x1b);
        this.localeID = BitConverter.ToUInt32(data, 0x1f);
        this.languageID = BitConverter.ToUInt32(data, 0x23);
        this.countryAbbreviation = ByteConverter.GetNullString(data, 0x27);
        this.countryName = ByteConverter.GetNullString(data, 40 + this.countryAbbreviation.Length);
    }

    // Properties
    public BattleNetClient Client
    {
        get
        {
            return this.client;
        }
    }

    public string CountryAbbreviation
    {
        get
        {
            return this.countryAbbreviation;
        }
    }

    public string CountryName
    {
        get
        {
            return this.countryName;
        }
    }

    public uint Language
    {
        get
        {
            return this.language;
        }
    }

    public uint LanguageID
    {
        get
        {
            return this.languageID;
        }
    }

    public uint LocaleID
    {
        get
        {
            return this.localeID;
        }
    }

    public System.Net.IPAddress LocalIP
    {
        get
        {
            return this.localIP;
        }
    }

    public BattleNetPlatform Platform
    {
        get
        {
            return this.platform;
        }
    }

    public uint Protocol
    {
        get
        {
            return this.protocol;
        }
    }

    public uint TimeZoneBias
    {
        get
        {
            return this.timeZoneBias;
        }
    }

    public uint Version
    {
        get
        {
            return this.version;
        }
    }
}

    public class BnetLogonRequest : BCPacket
{
    // Fields
    protected uint clientToken;
    protected uint serverToken;
    protected string username;

    // Methods
    public BnetLogonRequest(byte[] data) : base(data)
    {
        this.clientToken = BitConverter.ToUInt32(data, 3);
        this.serverToken = BitConverter.ToUInt32(data, 7);
        this.username = ByteConverter.GetNullString(data, 0x1f);
    }

    // Properties
    public uint ClientToken
    {
        get
        {
            return this.clientToken;
        }
    }

    public uint ServerToken
    {
        get
        {
            return this.serverToken;
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

    public class BnetPong : BCPacket
{
    // Fields
    protected uint timestamp;

    // Methods
    public BnetPong(byte[] data) : base(data)
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

    public class ChannelListRequest : BCPacket
    {
        // Fields
        protected BattleNetClient client;

        // Methods
        public ChannelListRequest(byte[] data)
            : base(data)
        {
            this.client = (BattleNetClient)BitConverter.ToUInt32(data, 3);
        }

        // Properties
        public BattleNetClient Client
        {
            get
            {
                return this.client;
            }
        }
    }

    public class ChatCommand : BCPacket
    {
        // Fields
        protected string message;

        // Methods
        public ChatCommand(byte[] data)
            : base(data)
        {
            this.message = ByteConverter.GetNullString(data, 3);
        }

        // Properties
        public string Message
        {
            get
            {
                return this.message;
            }
        }
    }

    public class DisplayAd : BCPacket
{
    // Fields
    protected BattleNetClient client;
    protected string filename;
    protected uint id;
    protected BattleNetPlatform platform;
    protected string url;

    // Methods
    public DisplayAd(byte[] data) : base(data)
    {
        this.platform = (BattleNetPlatform) BitConverter.ToUInt32(data, 3);
        this.client = (BattleNetClient) BitConverter.ToUInt32(data, 7);
        this.id = BitConverter.ToUInt32(data, 11);
        if (data[15] != 0)
        {
            this.filename = ByteConverter.GetNullString(data, 15);
        }
        int index = 0x10 + ((this.filename == null) ? 0 : this.filename.Length);
        if (data[index] != 0)
        {
            this.url = ByteConverter.GetNullString(data, index);
        }
    }

    // Properties
    public BattleNetClient Client
    {
        get
        {
            return this.client;
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

    public BattleNetPlatform Platform
    {
        get
        {
            return this.platform;
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

    public class EnterChatRequest : BCPacket
{
    // Fields
    protected string name;
    protected string realm;

    // Methods
    public EnterChatRequest(byte[] data) : base(data)
    {
        this.name = ByteConverter.GetNullString(data, 3);
        this.realm = ByteConverter.GetString(data, 4 + this.name.Length, -1, 0x2c);
    }

    // Properties
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
}

    public class ExtraWorkResponse : BCPacket
{
    // Fields
    protected int client;
    protected int resultLength;

    // Methods
    public ExtraWorkResponse(byte[] data) : base(data)
    {
        this.client = BitConverter.ToUInt16(data, 3);
        this.resultLength = BitConverter.ToUInt16(data, 5);
    }

    // Properties
    public int Client
    {
        get
        {
            return this.client;
        }
    }

    public byte[] ResultData
    {
        get
        {
            byte[] destinationArray = new byte[this.resultLength];
            Array.Copy(base.Data, 7, destinationArray, 0, this.resultLength);
            return destinationArray;
        }
    }

    public int ResultLength
    {
        get
        {
            return this.resultLength;
        }
    }
}

    public class FileTimeRequest : BCPacket
{
    // Fields
    protected string filename;
    protected uint requestID;
    protected uint unknown;

    // Methods
    public FileTimeRequest(byte[] data) : base(data)
    {
        this.requestID = BitConverter.ToUInt32(data, 3);
        this.unknown = BitConverter.ToUInt32(data, 7);
        this.filename = ByteConverter.GetNullString(data, 11);
    }

    // Properties
    public string Filename
    {
        get
        {
            return this.filename;
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

    public class JoinChannel : BCPacket
{
    // Fields
    protected JoinChannelFlags flags;
    protected string name;

    // Methods
    public JoinChannel(byte[] data) : base(data)
    {
        this.flags = (JoinChannelFlags) BitConverter.ToUInt32(data, 3);
        this.name = ByteConverter.GetNullString(data, 7);
    }

    // Properties
    public JoinChannelFlags Flags
    {
        get
        {
            return this.flags;
        }
    }

    public string Name
    {
        get
        {
            return this.name;
        }
    }
}

    public enum JoinChannelFlags
{
    AutoJoin = 5,
    Create = 2,
    NormalJoin = 0
}

    public class KeepAlive : BCPacket
{
    // Methods
    public KeepAlive(byte[] data) : base(data)
    {
    }
}

    public class LeaveChat : BCPacket
{
    // Methods
    public LeaveChat(byte[] data) : base(data)
    {
    }
}

    public class LeaveGame : BCPacket
{
    // Methods
    public LeaveGame(byte[] data) : base(data)
    {
    }
}

    public class NewsInfoRequest : BCPacket
{
    // Fields
    protected DateTime since;

    // Methods
    public NewsInfoRequest(byte[] data) : base(data)
    {
        this.since = TimeUtils.ParseUnixTimeUtc(BitConverter.ToUInt32(data, 3));
    }

    // Properties
    public DateTime Since
    {
        get
        {
            return this.since;
        }
    }
}

    public class NotifyJoin : BCPacket
{
    // Fields
    protected BattleNetClient client;
    protected string name;
    protected string password;
    protected uint version;

    // Methods
    public NotifyJoin(byte[] data) : base(data)
    {
        this.client = (BattleNetClient) BitConverter.ToUInt32(data, 3);
        this.version = BitConverter.ToUInt32(data, 7);
        this.name = ByteConverter.GetNullString(data, 11);
        this.password = ByteConverter.GetNullString(data, 12 + this.name.Length);
    }

    // Properties
    public BattleNetClient Client
    {
        get
        {
            return this.client;
        }
    }

    public string Name
    {
        get
        {
            return this.name;
        }
    }

    public string Password
    {
        get
        {
            return this.password;
        }
    }

    public uint Version
    {
        get
        {
            return this.version;
        }
    }
}

    public class QueryRealms : BCPacket
{
    // Methods
    public QueryRealms(byte[] data) : base(data)
    {
    }
}

    public class RealmLogonRequest : BCPacket
{
    // Fields
    protected uint cookie;
    protected string realm;

    // Methods
    public RealmLogonRequest(byte[] data) : base(data)
    {
        this.cookie = BitConverter.ToUInt32(data, 3);
        this.realm = ByteConverter.GetNullString(data, 0x1b);
    }

    // Properties
    public uint Cookie
    {
        get
        {
            return this.cookie;
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

    public class StartGame : BCPacket
{
    // Fields
    protected StartGameFlags flags;
    protected string name;
    protected string password;
    protected string statString;

    // Methods
    public StartGame(byte[] data) : base(data)
    {
        this.flags = (StartGameFlags) BitConverter.ToUInt32(data, 3);
        this.name = ByteConverter.GetNullString(data, 0x17);
        if ((this.flags & StartGameFlags.Private) == StartGameFlags.Private)
        {
            this.password = ByteConverter.GetNullString(data, 0x18 + this.name.Length);
        }
        int offset = (0x19 + this.name.Length) + ((this.password == null) ? 0 : this.password.Length);
        if (data.Length > (offset + 1))
        {
            this.statString = ByteConverter.GetNullString(data, offset);
        }
    }

    // Properties
    public StartGameFlags Flags
    {
        get
        {
            return this.flags;
        }
    }

    public string Name
    {
        get
        {
            return this.name;
        }
    }

    public string Password
    {
        get
        {
            return this.password;
        }
    }

    public string StatString
    {
        get
        {
            return this.statString;
        }
    }

    public string Unknown7
    {
        get
        {
            return ByteConverter.ToHexString(base.Data, 7, 0x10);
        }
    }
}

    [Flags]
    public enum StartGameFlags
{
    Public,
    Private
}

}
 


