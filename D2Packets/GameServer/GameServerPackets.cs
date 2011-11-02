using System;
using System.Text;
using D2Data;
using D2Packets;
using ETUtils;
using System.Collections.Generic;

namespace GameServer
{
    public class AboutPlayer : GSPacket
    {
        // Fields
        protected bool isInMyParty;
        protected int level;
        protected short partyID;
        protected PlayerRelationshipType relationship;
        protected uint uid;
        protected byte unknown12;

        // Methods
        public AboutPlayer(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.partyID = BitConverter.ToInt16(data, 5);
            this.level = BitConverter.ToUInt16(data, 7);
            this.relationship = (PlayerRelationshipType) BitConverter.ToUInt16(data, 9);
            this.isInMyParty = BitConverter.ToBoolean(data, 11);
            this.unknown12 = data[12];
        }

        // Properties
        public bool IsInMyParty
        {
            get
            {
                return this.isInMyParty;
            }
        }

        public int Level
        {
            get
            {
                return this.level;
            }
        }

        public short PartyID
        {
            get
            {
                return this.partyID;
            }
        }

        public PlayerRelationshipType Relationship
        {
            get
            {
                return this.relationship;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public byte Unknown12
        {
            get
            {
                return this.unknown12;
            }
        }
    }

    public class AcceptTrade : GSPacket
    {
        // Fields
        protected string playerName;
        protected uint playerUID;

        // Methods
        public AcceptTrade(byte[] data) : base(data)
        {
            this.playerName = ByteConverter.GetNullString(data, 1, 0x10);
            this.playerUID = BitConverter.ToUInt32(data, 0x11);
        }

        public AcceptTrade(string name, uint uid) : base(Build(name, uid))
        {
            this.playerName = name;
            this.playerUID = uid;
        }

        public static byte[] Build(string name, uint uid)
        {
            if (((name == null) || (name.Length == 0)) || (name.Length > 0x10))
            {
                throw new ArgumentException("name");
            }
            byte[] buffer = new byte[0x15];
            buffer[0] = 120;
            buffer[0x11] = (byte) uid;
            buffer[0x12] = (byte) (uid >> 8);
            buffer[0x13] = (byte) (uid >> 0x10);
            buffer[20] = (byte) (uid >> 0x18);
            for (int i = 0; i < name.Length; i++)
            {
                buffer[1 + i] = (byte) name[i];
            }
            return buffer;
        }

        // Properties
        public string PlayerName
        {
            get
            {
                return this.playerName;
            }
        }

        public uint PlayerUID
        {
            get
            {
                return this.playerUID;
            }
        }
    }

    public class AddUnit : GSPacket
    {
        // Fields
        protected long offset;
        protected List<NPCState> states;
        protected uint uid;
        protected UnitType unitType;
        protected byte[] unknownEnd;

        // Methods
        public AddUnit(byte[] data) : base(data)
        {
            int num;
            int num2;
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.states = new List<NPCState>();
            BitReader reader = new BitReader(data, 7);
        Label_0030:
            num = reader.ReadInt32(8);
            if (num == 0xff)
            {
                this.offset = reader.Position;
                this.unknownEnd = reader.ReadByteArray();
                return;
            }
            if (!reader.ReadBoolean(1))
            {
                this.states.Add(new NPCState(BaseState.Get(num)));
                goto Label_0030;
            }
            List<StatBase> stats = new List<StatBase>();
        Label_0056:
            num2 = reader.ReadInt32(9);
            if (num2 != 0x1ff)
            {
                BaseStat stat = BaseStat.Get(num2);
                int val = reader.ReadInt32(stat.SendBits);
                if (stat.SendParamBits > 0)
                {
                    int param = reader.ReadInt32(stat.SendParamBits);
                    if (stat.Signed)
                    {
                        stats.Add(new SignedStatParam(stat, val, param));
                    }
                    else
                    {
                        stats.Add(new UnsignedStatParam(stat, (uint) val, (uint) param));
                    }
                }
                else if (stat.Signed)
                {
                    stats.Add(new SignedStat(stat, val));
                }
                else
                {
                    stats.Add(new UnsignedStat(stat, (uint) val));
                }
                goto Label_0056;
            }
            this.states.Add(new NPCState(BaseState.Get(num), stats));
            goto Label_0030;
        }

        // Properties
        public List<NPCState> States
        {
            get
            {
                return this.states;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public byte[] UnknownEnd
        {
            get
            {
                return this.unknownEnd;
            }
        }
    }

    public class AssignGameObject : GSPacket
    {
        // Fields
        protected AreaLevel destination;
        protected GameObjectInteractType interactType;
        protected GameObjectID objectID;
        protected GameObjectMode objectMode;
        protected uint uid;
        protected int x;
        protected int y;

        // Methods
        public AssignGameObject(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 2);
            this.objectID = (GameObjectID) BitConverter.ToUInt16(data, 6);
            this.x = BitConverter.ToUInt16(data, 8);
            this.y = BitConverter.ToUInt16(data, 10);
            this.objectMode = (GameObjectMode) data[12];
            if (this.objectID == GameObjectID.TownPortal)
            {
                this.interactType = GameObjectInteractType.TownPortal;
                this.destination = (AreaLevel) data[13];
            }
            else
            {
                this.interactType = (GameObjectInteractType) data[13];
                this.destination = AreaLevel.None;
            }
        }

        // Properties
        public AreaLevel Destination
        {
            get
            {
                return this.destination;
            }
        }

        public GameObjectInteractType InteractType
        {
            get
            {
                return this.interactType;
            }
        }

        public GameObjectID ObjectID
        {
            get
            {
                return this.objectID;
            }
        }

        public GameObjectMode ObjectMode
        {
            get
            {
                return this.objectMode;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public class AssignMerc : GSPacket
    {
        // Fields
        protected NPCCode id;
        protected uint ownerUID;
        protected uint uid;

        // Methods
        public AssignMerc(byte[] data) : base(data)
        {
            this.id = (NPCCode) BitConverter.ToUInt16(data, 2);
            this.ownerUID = BitConverter.ToUInt32(data, 4);
            this.uid = BitConverter.ToUInt32(data, 8);
        }

        // Properties
        public NPCCode ID
        {
            get
            {
                return this.id;
            }
        }

        public uint OwnerUID
        {
            get
            {
                return this.ownerUID;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public string Unknown1
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 1, 1);
            }
        }

        public string Unknown5
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 12, 8);
            }
        }
    }

    public class AssignNPC : GSPacket
    {
        // Fields
        protected NPCCode id;
        protected byte life;
        protected uint uid;
        protected int x;
        protected int y;

        // Methods
        public AssignNPC(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.id = (NPCCode) BitConverter.ToUInt16(data, 5);
            this.x = BitConverter.ToUInt16(data, 7);
            this.y = BitConverter.ToUInt16(data, 9);
            this.life = data[11];
        }

        // Properties
        public NPCCode ID
        {
            get
            {
                return this.id;
            }
        }

        public byte Life
        {
            get
            {
                return this.life;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public string Unknown13
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 13);
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public class AssignPlayer : GSPacket
    {
        // Fields
        protected CharacterClass charClass;
        protected string name;
        protected uint uid;
        protected int x;
        protected int y;

        // Methods
        public AssignPlayer(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.charClass = (CharacterClass) data[5];
            this.name = ByteConverter.GetNullString(data, 6, 0x10);
            this.x = BitConverter.ToUInt16(data, 0x16);
            this.y = BitConverter.ToUInt16(data, 0x18);
        }

        // Properties
        public CharacterClass Class
        {
            get
            {
                return this.charClass;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public class AssignPlayerCorpse : GSPacket
    {
        // Fields
        protected bool assign;
        protected uint corpseUID;
        protected uint playerUID;

        // Methods
        public AssignPlayerCorpse(byte[] data) : base(data)
        {
            this.assign = Convert.ToBoolean(data[1]);
            this.playerUID = BitConverter.ToUInt32(data, 2);
            this.corpseUID = BitConverter.ToUInt32(data, 6);
        }

        public AssignPlayerCorpse(bool assign, uint playerUID, uint corpseUID) : base(Build(assign, playerUID, corpseUID))
        {
            this.assign = assign;
            this.playerUID = playerUID;
            this.corpseUID = corpseUID;
        }

        public static byte[] Build(bool assign, uint playerUID, uint corpseUID)
        {
            return new byte[] { 0x8e, (assign ? ((byte) 1) : ((byte) 0)), ((byte) playerUID), ((byte) (playerUID >> 8)), ((byte) (playerUID >> 0x10)), ((byte) (playerUID >> 0x18)), ((byte) corpseUID), ((byte) (corpseUID >> 8)), ((byte) (corpseUID >> 0x10)), ((byte) (corpseUID >> 0x18)) };
        }

        // Properties
        public bool Assign
        {
            get
            {
                return this.assign;
            }
        }

        public uint CorpseUID
        {
            get
            {
                return this.corpseUID;
            }
        }

        public uint PlayerUID
        {
            get
            {
                return this.playerUID;
            }
        }
    }

    public class AssignPlayerToParty : GSPacket
    {
        // Fields
        protected short partyNumber;
        protected uint uid;

        // Methods
        public AssignPlayerToParty(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.partyNumber = BitConverter.ToInt16(data, 5);
        }

        public AssignPlayerToParty(uint uid, short partyNumber) : base(Build(uid, partyNumber))
        {
            this.uid = uid;
            this.partyNumber = partyNumber;
        }

        public static byte[] Build(uint uid, short partyNumber)
        {
            return new byte[] { 0x8d, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)), ((byte) partyNumber), ((byte) (partyNumber >> 8)) };
        }

        // Properties
        public short PartyNumber
        {
            get
            {
                return this.partyNumber;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

    public class AssignSkill : GSPacket
    {
        // Fields
        protected uint chargedItemUID;
        protected SkillHand hand;
        public static readonly uint NULL_UInt32 = uint.MaxValue;
        protected SkillType skill;
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public AssignSkill(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.hand = (SkillHand) data[6];
            this.skill = (SkillType) BitConverter.ToUInt16(data, 7);
            this.chargedItemUID = BitConverter.ToUInt32(data, 9);
        }

        // Properties
        public uint ChargedItemUID
        {
            get
            {
                return this.chargedItemUID;
            }
        }

        public SkillHand Hand
        {
            get
            {
                return this.hand;
            }
        }

        public SkillType Skill
        {
            get
            {
                return this.skill;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

    public class AssignSkillHotkey : GSPacket
    {
        // Fields
        protected uint chargedItemUID;
        public static readonly uint NULL_UInt32 = uint.MaxValue;
        protected SkillType skill;
        protected byte slot;

        // Methods
        public AssignSkillHotkey(byte[] data) : base(data)
        {
            this.slot = data[1];
            this.skill = (SkillType) BitConverter.ToUInt16(data, 2);
            this.chargedItemUID = BitConverter.ToUInt32(data, 4);
        }

        public AssignSkillHotkey(byte slot, SkillType skill) : this(slot, skill, uint.MaxValue)
        {
        }

        public AssignSkillHotkey(byte slot, SkillType skill, uint itemUID) : base(Build(slot, skill, itemUID))
        {
            this.slot = slot;
            this.skill = skill;
            this.chargedItemUID = itemUID;
        }

        public static byte[] Build(byte slot, SkillType skill)
        {
            return Build(slot, skill, uint.MaxValue);
        }

        public static byte[] Build(byte slot, SkillType skill, uint itemUID)
        {
            return new byte[] { 0x7b, slot, ((byte) skill), ((byte) (((ushort) skill) >> 8)), ((byte) itemUID), ((byte) (itemUID >> 8)), ((byte) (itemUID >> 0x10)), ((byte) (itemUID >> 0x18)) };
        }

        // Properties
        public uint ChargedItemUID
        {
            get
            {
                return this.chargedItemUID;
            }
        }

        public SkillType Skill
        {
            get
            {
                return this.skill;
            }
        }

        public byte Slot
        {
            get
            {
                return this.slot;
            }
        }
    }

    public class AssignWarp : GSPacket
    {
        // Fields
        protected WarpType id;
        protected uint uid;
        protected UnitType unitType;
        protected int x;
        protected int y;

        // Methods
        public AssignWarp(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.id = (WarpType) data[6];
            this.x = BitConverter.ToUInt16(data, 7);
            this.y = BitConverter.ToUInt16(data, 9);
        }

        // Properties
        public WarpType ID
        {
            get
            {
                return this.id;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public class AttributeByte : AttributeNotification
    {
        // Fields
        public static readonly bool WRAPPED = true;

        // Methods
        public AttributeByte(byte[] data) : base(data)
        {
            BaseStat stat = BaseStat.Get(data[1]);
            if (stat.Signed)
            {
                base.stat = new SignedStat(stat, data[2]);
            }
            else
            {
                base.stat = new UnsignedStat(stat, data[2]);
            }
        }
    }

    public class AttributeDWord : AttributeNotification
    {
        // Fields
        public static readonly bool WRAPPED = true;

        // Methods
        public AttributeDWord(byte[] data) : base(data)
        {
            BaseStat stat = BaseStat.Get(data[1]);
            int val = BitConverter.ToInt32(data, 2);
            if (stat.ValShift > 0)
            {
                val = val >> stat.ValShift;
            }
            if (stat.Signed)
            {
                base.stat = new SignedStat(stat, val);
            }
            else
            {
                base.stat = new UnsignedStat(stat, (uint) val);
            }
        }
    }

    public class AttributeNotification : GSPacket
    {
        // Fields
        protected StatBase stat;

        // Methods
        public AttributeNotification(byte[] data) : base(data)
        {
        }

        // Properties
        public StatBase Stat
        {
            get
            {
                return this.stat;
            }
        }
    }

    public class AttributeWord : AttributeNotification
    {
        // Fields
        public static readonly bool WRAPPED = true;

        // Methods
        public AttributeWord(byte[] data) : base(data)
        {
            BaseStat stat = BaseStat.Get(data[1]);
            int val = (data[2] == 0) ? data[3] : BitConverter.ToUInt16(data, 2);
            if (stat.Signed)
            {
                base.stat = new SignedStat(stat, val);
            }
            else
            {
                base.stat = new UnsignedStat(stat, (uint) val);
            }
        }
    }

    public class ByteToExperience : GainExperience
    {
        // Fields
        public static readonly bool WRAPPED = true;

        // Methods
        public ByteToExperience(byte[] data) : base(data)
        {
            base.experience = data[1];
        }
    }

    public class DelayedState : GSPacket
    {
        // Fields
        protected BaseState state;
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public DelayedState(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.state = BaseState.Get(data[6]);
        }

        // Properties
        public BaseState State
        {
            get
            {
                return this.state;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

    public class DWordToExperience : GainExperience
    {
        // Fields
        public static readonly bool WRAPPED = true;

        // Methods
        public DWordToExperience(byte[] data) : base(data)
        {
            base.experience = BitConverter.ToUInt32(data, 1);
        }
    }

    public class EndState : GSPacket
    {
        // Fields
        protected BaseState state;
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public EndState(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.state = BaseState.Get(data[6]);
        }

        // Properties
        public BaseState State
        {
            get
            {
                return this.state;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

    public class GainExperience : GSPacket
    {
        // Fields
        protected uint experience;

        // Methods
        public GainExperience(byte[] data) : base(data)
        {
        }

        // Properties
        public uint Experience
        {
            get
            {
                return this.experience;
            }
        }
    }

    public class GameHandshake : GSPacket
    {
        // Fields
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public GameHandshake(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
        }

        public GameHandshake(UnitType type, uint uid) : base(Build(type, uid))
        {
            this.unitType = type;
            this.uid = uid;
        }

        public static byte[] Build(UnitType type, uint uid)
        {
            return new byte[] { 11, (byte)type, ((byte)uid), ((byte)(uid >> 8)), ((byte)(uid >> 0x10)), ((byte)(uid >> 0x18)) };
        }

        // Properties
        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

    public class GameLoading : GSPacket
    {
        // Methods
        public GameLoading() : base(Build())
        {
        }

        public GameLoading(byte[] data) : base(data)
        {
        }

        public static byte[] Build()
        {
            return new byte[1];
        }
    }

    public class GameLogonReceipt : GSPacket
    {
        // Fields
        protected GameDifficulty difficulty;
        protected bool expansion;
        protected bool hardcore;
        protected bool ladder;
        protected byte unknown2;

        // Methods
        public GameLogonReceipt(byte[] data) : base(data)
        {
            this.difficulty = (GameDifficulty) data[1];
            this.unknown2 = data[2];
            this.hardcore = (data[3] & 8) == 8;
            this.expansion = data[6] == 1;
            this.ladder = data[7] == 1;
        }

        // Properties
        public GameDifficulty Difficulty
        {
            get
            {
                return this.difficulty;
            }
        }

        public bool Expansion
        {
            get
            {
                return this.expansion;
            }
        }

        public bool Hardcore
        {
            get
            {
                return this.hardcore;
            }
        }

        public bool Ladder
        {
            get
            {
                return this.ladder;
            }
        }

        public byte Unknown2
        {
            get
            {
                return this.unknown2;
            }
        }
    }

    public class GameLogonSuccess : GSPacket
    {
        // Methods
        public GameLogonSuccess() : base(Build())
        {
        }

        public GameLogonSuccess(byte[] data) : base(data)
        {
        }

        public static byte[] Build()
        {
            return new byte[] { 2 };
        }
    }

    public class GameLogoutSuccess : GSPacket
    {
        // Methods
        public GameLogoutSuccess() : base(Build())
        {
        }

        public GameLogoutSuccess(byte[] data) : base(data)
        {
        }

        public static byte[] Build()
        {
            return new byte[] { 6 };
        }
    }

    public class GameMessage : GSPacket
    {
        // Fields
        protected string message;
        protected GameMessageType messageType;
        public static readonly int NULL_Int32 = -1;
        public static readonly int NULL_UInt32 = 0;
        protected string playerName;
        protected int random;
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public GameMessage(byte[] data) : base(data)
        {
            this.unitType = UnitType.NotApplicable;
            this.random = -1;
            this.messageType = (GameMessageType) ((ushort) BitConverter.ToInt16(data, 1));
            if (this.messageType == GameMessageType.OverheadMessage)
            {
                this.unitType = (UnitType) data[3];
                this.uid = BitConverter.ToUInt32(data, 4);
                this.random = BitConverter.ToUInt16(data, 8);
                this.message = ByteConverter.GetNullString(data, 11);
            }
            else
            {
                this.playerName = ByteConverter.GetNullString(data, 10);
                this.message = ByteConverter.GetNullString(data, 11 + this.playerName.Length);
            }
        }

        public GameMessage(GameMessageType type, byte charFlags, string charName, string message) : base(Build(type, charFlags, charName, message))
        {
            this.unitType = UnitType.NotApplicable;
            this.random = -1;
            this.messageType = type;
            this.playerName = charName;
            this.message = message;
        }
        
        public GameMessage(GameMessageType type, string charName, string message)
            : base(Build(type, charName, message))
        {
            this.unitType = UnitType.NotApplicable;
            this.random = -1;
            this.messageType = type;
            this.playerName = charName;
            this.message = message;
        }

        public GameMessage(GameMessageType type, string message)
            : base(Build(type,message))
        {
            this.unitType = UnitType.NotApplicable;
            this.random = -1;
            this.messageType = type;
            this.playerName = null;
            this.message = message;
        }

        public GameMessage(UnitType type, uint uid, ushort random, string message) : base(Build(type, uid, random, message))
        {
            this.unitType = UnitType.NotApplicable;
            this.random = -1;
            this.messageType = GameMessageType.OverheadMessage;
            this.uid = uid;
            this.random = random;
            this.message = message;
        }


        public static byte[] Build(GameMessageType type, byte charFlags, string charName, string message)
        {
            if ((charName == null) || (charName.Length == 0))
            {
                throw new ArgumentException("charName");
            }
            if ((message == null) || (message.Length == 0))
            {
                throw new ArgumentException("message");
            }
            byte[] buffer = new byte[(12 + charName.Length) + message.Length];
            buffer[0] = 0x26;
            buffer[1] = (byte) type;
            buffer[3] = 2;
            buffer[9] = charFlags;
            for (int i = 0; i < charName.Length; i++)
            {
                buffer[10 + i] = (byte) charName[i];
            }
            int num2 = 0;
            int num3 = 11 + charName.Length;
            while (num2 < message.Length)
            {
                buffer[num3 + num2] = (byte) message[num2];
                num2++;
            }
            return buffer;
        }
        public static byte[] Build(GameMessageType type, string charName, string message)
        {
            if ((charName == null) || (charName.Length == 0))
            {
                throw new ArgumentException("charName");
            }
            if ((message == null) || (message.Length == 0))
            {
                throw new ArgumentException("message");
            }
            byte[] buffer = new byte[(12 + charName.Length) + message.Length];
            buffer[0] = 0x26;
            buffer[1] = (byte)type;
            buffer[3] = 2;
            buffer[9] = 5;
            for (int i = 0; i < charName.Length; i++)
            {
                buffer[10 + i] = (byte)charName[i];
            }
            int num2 = 0;
            int num3 = 11 + charName.Length;
            while (num2 < message.Length)
            {
                buffer[num3 + num2] = (byte)message[num2];
                num2++;
            }
            return buffer;
        }
        public static byte[] Build(GameMessageType type, string message)
        {

            if ((message == null) || (message.Length == 0))
            {
                throw new ArgumentException("message");
            }

            byte[] Buffer = new byte[((12+1) + message.Length)];
            Buffer[0] = 0x26;
            Buffer[1] = (byte)type;
            Buffer[3] = 2;
            Buffer[9] = 5;
            int num2 = 0;
            int num3 = 11;

            while ((num2 < message.Length))
            {
                Buffer[num3 + num2] = (byte)message[num2];
                num2++;
            }

            return Buffer;
        }

        public static byte[] Build(UnitType type, uint uid, ushort random, string message)
        {
            if ((message == null) || (message.Length == 0))
            {
                throw new ArgumentException("message");
            }
            byte[] buffer = new byte[12 + message.Length];
            buffer[0] = 0x26;
            buffer[1] = 5;
            buffer[3] = (byte) type;
            buffer[4] = (byte) uid;
            buffer[5] = (byte) (uid >> 8);
            buffer[6] = (byte) (uid >> 0x10);
            buffer[7] = (byte) (uid >> 0x18);
            buffer[8] = (byte) random;
            buffer[9] = (byte) (random >> 8);
            for (int i = 0; i < message.Length; i++)
            {
                buffer[11 + i] = (byte) message[i];
            }
            return buffer;
        }

        // Properties
        public string Message
        {
            get
            {
                return this.message;
            }
        }

        public GameMessageType MessageType
        {
            get
            {
                return this.messageType;
            }
        }

        public string PlayerName
        {
            get
            {
                return this.playerName;
            }
        }

        public int Random
        {
            get
            {
                return this.random;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public string Unknown3
        {
            get
            {
                if (this.messageType != GameMessageType.OverheadMessage)
                {
                    return ByteConverter.ToHexString(base.Data, 3, 7);
                }
                return null;
            }
        }
    }

    public class GameOver : GSPacket
    {
        // Methods
        public GameOver() : base(Build())
        {
        }

        public GameOver(byte[] data) : base(data)
        {
        }

        public static byte[] Build()
        {
            return new byte[] { 0xb0 };
        }
    }

    public class GoldTrade : GSPacket
    {
        // Fields
        protected uint amount;
        protected bool myGold;

        // Methods
        public GoldTrade(byte[] data) : base(data)
        {
            this.myGold = BitConverter.ToBoolean(data, 1);
            this.amount = BitConverter.ToUInt32(data, 2);
        }

        // Properties
        public uint Amount
        {
            get
            {
                return this.amount;
            }
        }

        public bool MyGold
        {
            get
            {
                return this.myGold;
            }
        }
    }

    public class GSPacket : D2Packet
    {
        // Fields
        public readonly GameServerPacket PacketID;

        // Methods
        public GSPacket(byte[] data) : base(data, Origin.GameServer)
        {
            this.PacketID = (GameServerPacket) data[0];
        }
    }

    public class InformationMessage : GSPacket
    {
        // Fields
        protected byte actionType;
        protected int amount;
        protected CharacterClass charClass;
        protected PlayerInformationActionType informationType;
        public static readonly int NULL_Int32 = -1;
        public static readonly int NULL_UInt32 = 0;
        protected string objectName;
        protected uint objectUID;
        protected PlayerRelationActionType relationType;
        protected NPCCode slayerMonster;
        protected GameObjectID slayerObject;
        protected UnitType slayerType;
        protected string subjectName;
        protected InformationMessageType type;

        // Methods
        public InformationMessage(byte[] data) : base(data)
        {
            this.slayerType = UnitType.NotApplicable;
            this.charClass = CharacterClass.NotApplicable;
            this.slayerObject = GameObjectID.NotApplicable;
            this.slayerMonster = NPCCode.NotApplicable;
            this.informationType = PlayerInformationActionType.None;
            this.relationType = PlayerRelationActionType.NotApplicable;
            this.amount = -1;
            this.type = (InformationMessageType) data[1];
            this.actionType = data[2];
            switch (this.type)
            {
                case InformationMessageType.DroppedFromGame:
                case InformationMessageType.JoinedGame:
                case InformationMessageType.LeftGame:
                    this.subjectName = ByteConverter.GetNullString(data, 8);
                    this.objectName = ByteConverter.GetNullString(data, 0x18);
                    return;

                case ((InformationMessageType) 1):
                case ((InformationMessageType) 5):
                    break;

                case InformationMessageType.NotInGame:
                    this.subjectName = ByteConverter.GetNullString(data, 8);
                    return;

                case InformationMessageType.PlayerSlain:
                    this.slayerType = (UnitType) data[7];
                    this.subjectName = ByteConverter.GetNullString(data, 8);
                    if (this.slayerType != UnitType.Player)
                    {
                        if (this.slayerType == UnitType.NPC)
                        {
                            this.slayerMonster = (NPCCode) BitConverter.ToUInt32(data, 3);
                            return;
                        }
                        if (this.slayerType == UnitType.GameObject)
                        {
                            this.slayerObject = (GameObjectID) BitConverter.ToUInt32(data, 3);
                            return;
                        }
                        break;
                    }
                    this.charClass = (CharacterClass) BitConverter.ToUInt32(data, 3);
                    this.objectName = ByteConverter.GetNullString(data, 0x18);
                    return;

                case InformationMessageType.PlayerRelation:
                    this.informationType = (PlayerInformationActionType) this.actionType;
                    this.objectUID = BitConverter.ToUInt32(data, 3);
                    this.relationType = (PlayerRelationActionType) data[7];
                    return;

                case InformationMessageType.SoJsSoldToMerchants:
                    this.amount = BitConverter.ToInt32(data, 3);
                    break;

                default:
                    return;
            }
        }

        // Properties
        public byte ActionType
        {
            get
            {
                return this.actionType;
            }
        }

        public int Amount
        {
            get
            {
                return this.amount;
            }
        }

        public CharacterClass Class
        {
            get
            {
                return this.charClass;
            }
        }

        public PlayerInformationActionType InformationType
        {
            get
            {
                return this.informationType;
            }
        }

        public string ObjectName
        {
            get
            {
                return this.objectName;
            }
        }

        public uint ObjectUID
        {
            get
            {
                return this.objectUID;
            }
        }

        public PlayerRelationActionType RelationType
        {
            get
            {
                return this.relationType;
            }
        }

        public NPCCode SlayerMonster
        {
            get
            {
                return this.slayerMonster;
            }
        }

        public GameObjectID SlayerObject
        {
            get
            {
                return this.slayerObject;
            }
        }

        public UnitType SlayerType
        {
            get
            {
                return this.slayerType;
            }
        }

        public string SubjectName
        {
            get
            {
                return this.subjectName;
            }
        }

        public InformationMessageType Type
        {
            get
            {
                return this.type;
            }
        }
    }

    public enum InformationMessageType
    {
        DiabloWalksTheEarth = 0x12,
        DroppedFromGame = 0,
        JoinedGame = 2,
        LeftGame = 3,
        NotInGame = 4,
        PlayerRelation = 7,
        PlayerSlain = 6,
        SoJsSoldToMerchants = 0x11
    }

    public class ItemAction : GSPacket
    {
        // Fields
        protected ItemActionType action;
        protected BaseItem baseItem;
        protected ItemCategory category;
        protected CharacterClass charClass;
        protected int color;
        protected ItemContainer container;
        protected ItemDestination destination;
        protected ItemFlags flags;
        protected int graphic;
        protected int level;
        protected EquipmentLocation location;
        protected List<MagicPrefixType> magicPrefixes;
        protected List<MagicSuffixType> magicSuffixes;
        protected List<StatBase> mods;
        protected string name;
        protected ItemAffix prefix;
        protected ItemQuality quality;
        protected BaseRuneword runeword;
        protected int runewordID;
        protected int runewordParam;
        protected List<StatBase>[] setBonuses;
        protected BaseSetItem setItem;
        protected List<StatBase> stats;
        protected ItemAffix suffix;
        protected SuperiorItemType superiorType;
        protected uint uid;
        protected BaseUniqueItem uniqueItem;
        protected int unknown1;
        protected int use;
        protected int usedSockets;
        protected ItemVersion version;
        protected int x;
        protected int y;

        // Methods
        public ItemAction(byte[] data) : base(data)
        {
            this.superiorType = SuperiorItemType.NotApplicable;
            this.charClass = CharacterClass.NotApplicable;
            this.level = -1;
            this.usedSockets = -1;
            this.use = -1;
            this.graphic = -1;
            this.color = -1;
            this.stats = new List<StatBase>();
            this.unknown1 = -1;
            this.runewordID = -1;
            this.runewordParam = -1;
            BitReader br = new BitReader(data, 1);
            this.action = (ItemActionType) br.ReadByte();
            br.SkipBytes(1);
            this.category = (ItemCategory) br.ReadByte();
            this.uid = br.ReadUInt32();
            if (data[0] == 0x9d)
            {
                br.SkipBytes(5);
            }
            this.flags = (ItemFlags) br.ReadUInt32();
            this.version = (ItemVersion) br.ReadByte();
            this.unknown1 = br.ReadByte(2);
            this.destination = (ItemDestination) br.ReadByte(3);
            if (this.destination == ItemDestination.Ground)
            {
                this.x = br.ReadUInt16();
                this.y = br.ReadUInt16();
            }
            else
            {
                this.location = (EquipmentLocation) br.ReadByte(4);
                this.x = br.ReadByte(4);
                this.y = br.ReadByte(3);
                this.container = (ItemContainer) br.ReadByte(4);
            }
            if ((this.action == ItemActionType.AddToShop) || (this.action == ItemActionType.RemoveFromShop))
            {
                int num = ((int) this.container) | 0x80;
                if ((num & 1) == 1)
                {
                    num--;
                    this.y += 8;
                }
                this.container = (ItemContainer) num;
            }
            else if (this.container == ItemContainer.Unspecified)
            {
                if (this.location == EquipmentLocation.NotApplicable)
                {
                    if ((this.Flags & ItemFlags.InSocket) == ItemFlags.InSocket)
                    {
                        this.container = ItemContainer.Item;
                        this.y = -1;
                    }
                    else if ((this.action == ItemActionType.PutInBelt) || (this.action == ItemActionType.RemoveFromBelt))
                    {
                        this.container = ItemContainer.Belt;
                        this.y = this.x / 4;
                        this.x = this.x % 4;
                    }
                }
                else
                {
                    this.x = -1;
                    this.y = -1;
                }
            }
            if ((this.flags & ItemFlags.Ear) == ItemFlags.Ear)
            {
                this.charClass = (CharacterClass) br.ReadByte(3);
                this.level = br.ReadByte(7);
                this.name = br.ReadString(7, '\0', 0x10);
                this.baseItem = BaseItem.Get(ItemType.Ear);
            }
            else
            {
                this.baseItem = BaseItem.GetByID(this.category, br.ReadUInt32());
                if (this.baseItem.Type == ItemType.Gold)
                {
                    this.stats.Add(new SignedStat(BaseStat.Get(StatType.Quantity), br.ReadInt32(br.ReadBoolean(1) ? 0x20 : 12)));
                }
                else
                {
                    this.usedSockets = br.ReadByte(3);
                    if ((this.flags & (ItemFlags.Compact | ItemFlags.Gamble)) == ItemFlags.None)
                    {
                        BaseStat stat;
                        int num2;
                        this.level = br.ReadByte(7);
                        this.quality = (ItemQuality) br.ReadByte(4);
                        if (br.ReadBoolean(1))
                        {
                            this.graphic = br.ReadByte(3);
                        }
                        if (br.ReadBoolean(1))
                        {
                            this.color = br.ReadInt32(11);
                        }
                        if ((this.flags & ItemFlags.Identified) == ItemFlags.Identified)
                        {
                            switch (this.quality)
                            {
                                case ItemQuality.Inferior:
                                    this.prefix = new ItemAffix(ItemAffixType.InferiorPrefix, br.ReadByte(3));
                                    break;

                                case ItemQuality.Superior:
                                    this.prefix = new ItemAffix(ItemAffixType.SuperiorPrefix, 0);
                                    this.superiorType = (SuperiorItemType) br.ReadByte(3);
                                    break;

                                case ItemQuality.Magic:
                                    this.prefix = new ItemAffix(ItemAffixType.MagicPrefix, br.ReadUInt16(11));
                                    this.suffix = new ItemAffix(ItemAffixType.MagicSuffix, br.ReadUInt16(11));
                                    break;

                                case ItemQuality.Set:
                                    this.setItem = BaseSetItem.Get(br.ReadUInt16(12));
                                    break;

                                case ItemQuality.Rare:
                                case ItemQuality.Crafted:
                                    this.prefix = new ItemAffix(ItemAffixType.RarePrefix, br.ReadByte(8));
                                    this.suffix = new ItemAffix(ItemAffixType.RareSuffix, br.ReadByte(8));
                                    break;

                                case ItemQuality.Unique:
                                    if (this.baseItem.Code != "std")
                                    {
                                        try
                                        {
                                            this.uniqueItem = BaseUniqueItem.Get(br.ReadUInt16(12));
                                        }
                                        catch{}
                                    }
                                    break;
                            }
                        }
                        if ((this.quality == ItemQuality.Rare) || (this.quality == ItemQuality.Crafted))
                        {
                            this.magicPrefixes = new List<MagicPrefixType>();
                            this.magicSuffixes = new List<MagicSuffixType>();
                            for (int i = 0; i < 3; i++)
                            {
                                if (br.ReadBoolean(1))
                                {
                                    this.magicPrefixes.Add((MagicPrefixType) br.ReadUInt16(11));
                                }
                                if (br.ReadBoolean(1))
                                {
                                    this.magicSuffixes.Add((MagicSuffixType) br.ReadUInt16(11));
                                }
                            }
                        }
                        if ((this.Flags & ItemFlags.Runeword) == ItemFlags.Runeword)
                        {
                            this.runewordID = br.ReadUInt16(12);
                            this.runewordParam = br.ReadUInt16(4);
                            num2 = -1;
                            if (this.runewordParam == 5)
                            {
                                num2 = this.runewordID - (this.runewordParam * 5);
                                if (num2 < 100)
                                {
                                    num2--;
                                }
                            }
                            else if (this.runewordParam == 2)
                            {
                                num2 = ((this.runewordID & 0x3ff) >> 5) + 2;
                            }
                            br.ByteOffset -= 2;
                            this.runewordParam = br.ReadUInt16();
                            this.runewordID = num2;
                            if (num2 == -1)
                            {
                                throw new Exception("Unknown Runeword: " + this.runewordParam);
                            }
                            this.runeword = BaseRuneword.Get(num2);
                        }
                        if ((this.Flags & ItemFlags.Personalized) == ItemFlags.Personalized)
                        {
                            this.name = br.ReadString(7, '\0', 0x10);
                        }
                        if (this.baseItem is BaseArmor)
                        {
                            stat = BaseStat.Get(StatType.ArmorClass);
                            this.stats.Add(new SignedStat(stat, br.ReadInt32(stat.SaveBits) - stat.SaveAdd));
                        }
                        if ((this.baseItem is BaseArmor) || (this.baseItem is BaseWeapon))
                        {
                            stat = BaseStat.Get(StatType.MaxDurability);
                            num2 = br.ReadInt32(stat.SaveBits);
                            this.stats.Add(new SignedStat(stat, num2));
                            if (num2 > 0)
                            {
                                stat = BaseStat.Get(StatType.Durability);
                                this.stats.Add(new SignedStat(stat, br.ReadInt32(stat.SaveBits)));
                            }
                        }
                        if ((this.Flags & (ItemFlags.None | ItemFlags.Socketed)) == (ItemFlags.None | ItemFlags.Socketed))
                        {
                            stat = BaseStat.Get(StatType.Sockets);
                            this.stats.Add(new SignedStat(stat, br.ReadInt32(stat.SaveBits)));
                        }
                        if (this.baseItem.Stackable)
                        {
                            if (this.baseItem.Useable)
                            {
                                this.use = br.ReadByte(5);
                            }
                            this.stats.Add(new SignedStat(BaseStat.Get(StatType.Quantity), br.ReadInt32(9)));
                        }
                        if ((this.Flags & ItemFlags.Identified) == ItemFlags.Identified)
                        {
                            StatBase base2;
                            int num4 = (this.Quality == ItemQuality.Set) ? br.ReadByte(5) : -1;
                            this.mods = new List<StatBase>();
                            while ((base2 = ReadStat(br)) != null)
                            {
                                this.mods.Add(base2);
                            }
                            if ((this.flags & ItemFlags.Runeword) == ItemFlags.Runeword)
                            {
                                while ((base2 = ReadStat(br)) != null)
                                {
                                    this.mods.Add(base2);
                                }
                            }
                            if (num4 > 0)
                            {
                                this.setBonuses = new List<StatBase>[5];
                                for (int j = 0; j < 5; j++)
                                {
                                    if ((num4 & (((int) 1) << j)) != 0)
                                    {
                                        this.setBonuses[j] = new List<StatBase>();
                                        while ((base2 = ReadStat(br)) != null)
                                        {
                                            this.setBonuses[j].Add(base2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static StatBase ReadStat(BitReader br)
        {
            int index = br.ReadInt32(9);
            if (index == 0x1ff)
            {
                return null;
            }
            BaseStat stat = BaseStat.Get(index);
            if (stat.SaveParamBits == -1)
            {
                if (stat.OpBase == StatType.Level)
                {
                    return new PerLevelStat(stat, br.ReadInt32(stat.SaveBits));
                }
                switch (stat.Type)
                {
                    case StatType.MaxDamagePercent:
                    case StatType.MinDamagePercent:
                        return new DamageRangeStat(stat, br.ReadInt32(stat.SaveBits), br.ReadInt32(stat.SaveBits));

                    case StatType.FireMinDamage:
                    case StatType.LightMinDamage:
                    case StatType.MagicMinDamage:
                        return new DamageRangeStat(stat, br.ReadInt32(stat.SaveBits), br.ReadInt32(BaseStat.Get((int) (stat.Index + 1)).SaveBits));

                    case StatType.FireMaxDamage:
                    case StatType.LightMaxDamage:
                    case StatType.MagicMaxDamage:
                        goto Label_0350;

                    case StatType.ColdMinDamage:
                        return new ColdDamageStat(stat, br.ReadInt32(stat.SaveBits), br.ReadInt32(BaseStat.Get(StatType.ColdMaxDamage).SaveBits), br.ReadInt32(BaseStat.Get(StatType.ColdLength).SaveBits));

                    case StatType.ReplenishDurability:
                    case StatType.ReplenishQuantity:
                        return new ReplenishStat(stat, br.ReadInt32(stat.SaveBits));

                    case StatType.PoisonMinDamage:
                        return new PoisonDamageStat(stat, br.ReadInt32(stat.SaveBits), br.ReadInt32(BaseStat.Get(StatType.PoisonMaxDamage).SaveBits), br.ReadInt32(BaseStat.Get(StatType.PoisonLength).SaveBits));
                }
            }
            else
            {
                switch (stat.Type)
                {
                    case StatType.SingleSkill:
                    case StatType.NonClassSkill:
                        return new SkillBonusStat(stat, br.ReadInt32(stat.SaveParamBits), br.ReadInt32(stat.SaveBits));

                    case StatType.ElementalSkillBonus:
                        return new ElementalSkillsBonusStat(stat, br.ReadInt32(stat.SaveParamBits), br.ReadInt32(stat.SaveBits));

                    case StatType.ClassSkillsBonus:
                        return new ClassSkillsBonusStat(stat, br.ReadInt32(stat.SaveParamBits), br.ReadInt32(stat.SaveBits));

                    case StatType.Aura:
                        return new AuraStat(stat, br.ReadInt32(stat.SaveParamBits), br.ReadInt32(stat.SaveBits));

                    case StatType.Reanimate:
                        return new ReanimateStat(stat, br.ReadUInt32(stat.SaveParamBits), br.ReadUInt32(stat.SaveBits));

                    case StatType.SkillOnAttack:
                    case StatType.SkillOnKill:
                    case StatType.SkillOnDeath:
                    case StatType.SkillOnStriking:
                    case StatType.SkillOnLevelUp:
                    case StatType.SkillOnGetHit:
                        return new SkillOnEventStat(stat, br.ReadInt32(6), br.ReadInt32(10), br.ReadInt32(stat.SaveBits));

                    case StatType.ChargedSkill:
                        return new ChargedSkillStat(stat, br.ReadInt32(6), br.ReadInt32(10), br.ReadInt32(8), br.ReadInt32(8));

                    case StatType.SkillTabBonus:
                        return new SkillTabBonusStat(stat, br.ReadInt32(3), br.ReadInt32(3), br.ReadInt32(10), br.ReadInt32(stat.SaveBits));
                }
                throw new Exception("Invalid stat: " + stat.Index.ToString());
            }
        Label_0350:
            if (stat.Signed)
            {
                int num2 = br.ReadInt32(stat.SaveBits);
                if (stat.SaveAdd > 0)
                {
                    num2 -= stat.SaveAdd;
                }
                return new SignedStat(stat, num2);
            }
            uint val = br.ReadUInt32(stat.SaveBits);
            if (stat.SaveAdd > 0)
            {
                val -= (uint) stat.SaveAdd;
            }
            return new UnsignedStat(stat, val);
        }

        // Properties
        public ItemActionType Action
        {
            get
            {
                return this.action;
            }
        }

        public BaseItem BaseItem
        {
            get
            {
                return this.baseItem;
            }
        }

        public ItemCategory Category
        {
            get
            {
                return this.category;
            }
        }

        public CharacterClass Class
        {
            get
            {
                return this.charClass;
            }
        }

        public int Color
        {
            get
            {
                return this.color;
            }
        }

        public ItemContainer Container
        {
            get
            {
                return this.container;
            }
        }

        public ItemDestination Destination
        {
            get
            {
                return this.destination;
            }
        }

        public ItemFlags Flags
        {
            get
            {
                return this.flags;
            }
        }

        public int Graphic
        {
            get
            {
                return this.graphic;
            }
        }

        public int Level
        {
            get
            {
                return this.level;
            }
        }

        public EquipmentLocation Location
        {
            get
            {
                return this.location;
            }
        }

        public List<MagicPrefixType> MagicPrefixes
        {
            get
            {
                return this.magicPrefixes;
            }
        }

        public List<MagicSuffixType> MagicSuffixes
        {
            get
            {
                return this.magicSuffixes;
            }
        }

        public List<StatBase> Mods
        {
            get
            {
                return this.mods;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public ItemAffix Prefix
        {
            get
            {
                return this.prefix;
            }
        }

        public ItemQuality Quality
        {
            get
            {
                return this.quality;
            }
        }

        public BaseRuneword Runeword
        {
            get
            {
                return this.runeword;
            }
        }

        public int RunewordID
        {
            get
            {
                return this.runewordID;
            }
        }

        public int RunewordParam
        {
            get
            {
                return this.runewordParam;
            }
        }

        public List<StatBase>[] SetBonuses
        {
            get
            {
                return this.setBonuses;
            }
        }

        public BaseSetItem SetItem
        {
            get
            {
                return this.setItem;
            }
        }

        public List<StatBase> Stats
        {
            get
            {
                return this.stats;
            }
        }

        public ItemAffix Suffix
        {
            get
            {
                return this.suffix;
            }
        }

        public SuperiorItemType SuperiorType
        {
            get
            {
                return this.superiorType;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public BaseUniqueItem UniqueItem
        {
            get
            {
                return this.uniqueItem;
            }
        }

        public int Unknown1
        {
            get
            {
                if (this.unknown1 != 0)
                {
                    return this.unknown1;
                }
                return -1;
            }
        }

        public int Use
        {
            get
            {
                return this.use;
            }
        }

        public int UsedSockets
        {
            get
            {
                if ((this.usedSockets == 0) && ((this.flags & (ItemFlags.None | ItemFlags.Socketed)) != (ItemFlags.None | ItemFlags.Socketed)))
                {
                    return -1;
                }
                return this.usedSockets;
            }
        }

        public ItemVersion Version
        {
            get
            {
                return this.version;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public enum ItemEventCause
    {
        Target,
        Owner
    }

    public enum ItemStateType
    {
        Broken = 1,
        Full = 2
    }

    public class ItemTriggerSkill : GSPacket
    {
        // Fields
        protected ItemEventCause cause;
        protected byte level;
        protected UnitType ownerType;
        protected uint ownerUID;
        protected SkillType skill;
        protected UnitType targetType;
        protected uint targetUID;

        // Methods
        public ItemTriggerSkill(byte[] data) : base(data)
        {
            this.ownerType = (UnitType) data[1];
            this.ownerUID = BitConverter.ToUInt32(data, 2);
            this.skill = (SkillType) BitConverter.ToUInt16(data, 6);
            this.level = data[8];
            this.targetType = (UnitType) data[9];
            this.targetUID = BitConverter.ToUInt32(data, 10);
            this.cause = (ItemEventCause) BitConverter.ToUInt16(data, 14);
        }

        // Properties
        public ItemEventCause Cause
        {
            get
            {
                return this.cause;
            }
        }

        public byte Level
        {
            get
            {
                return this.level;
            }
        }

        public UnitType OwnerType
        {
            get
            {
                return this.ownerType;
            }
        }

        public uint OwnerUID
        {
            get
            {
                return this.ownerUID;
            }
        }

        public SkillType Skill
        {
            get
            {
                return this.skill;
            }
        }

        public UnitType TargetType
        {
            get
            {
                return this.targetType;
            }
        }

        public uint TargetUID
        {
            get
            {
                return this.targetUID;
            }
        }
    }

    public class LoadAct : GSPacket
    {
        // Fields
        protected ActLocation act;
        protected uint mapId;
        protected AreaLevel townArea;

        // Methods
        public LoadAct(byte[] data) : base(data)
        {
            this.act = (ActLocation) data[1];
            this.mapId = BitConverter.ToUInt32(data, 2);
            this.townArea = (AreaLevel) BitConverter.ToUInt16(data, 6);
        }

        // Properties
        public ActLocation Act
        {
            get
            {
                return this.act;
            }
        }

        public uint MapId
        {
            get
            {
                return this.mapId;
            }
        }

        public AreaLevel TownArea
        {
            get
            {
                return this.townArea;
            }
        }

        public string Unknown8
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 8, 4);
            }
        }
    }

    public class LoadDone : GSPacket
    {
        // Methods
        public LoadDone() : base(Build())
        {
        }

        public LoadDone(byte[] data) : base(data)
        {
        }

        public static byte[] Build()
        {
            return new byte[] { 4 };
        }
    }

    public class MapAdd : GSPacket
    {
        // Fields
        protected AreaLevel area;
        protected int x;
        protected int y;

        // Methods
        public MapAdd(byte[] data) : base(data)
        {
            this.x = BitConverter.ToUInt16(data, 1);
            this.y = BitConverter.ToUInt16(data, 3);
            this.area = (AreaLevel) data[5];
        }

        public MapAdd(AreaLevel area, int x, int y) : base(Build(area, x, y))
        {
            this.x = x;
            this.y = y;
            this.area = area;
        }

        public static byte[] Build(AreaLevel area, int x, int y)
        {
            return new byte[] { 7, ((byte) x), ((byte) (x >> 8)), ((byte) y), ((byte) (y >> 8)), ((byte) area) };
        }

        // Properties
        public AreaLevel Area
        {
            get
            {
                return this.area;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public class MapRemove : GSPacket
    {
        // Fields
        protected AreaLevel area;
        protected int x;
        protected int y;

        // Methods
        public MapRemove(byte[] data) : base(data)
        {
            this.x = BitConverter.ToUInt16(data, 1);
            this.y = BitConverter.ToUInt16(data, 3);
            this.area = (AreaLevel) data[5];
        }

        public MapRemove(AreaLevel area, int x, int y) : base(Build(area, x, y))
        {
            this.x = x;
            this.y = y;
            this.area = area;
        }

        public static byte[] Build(AreaLevel area, int x, int y)
        {
            return new byte[] { 8, ((byte) x), ((byte) (x >> 8)), ((byte) y), ((byte) (y >> 8)), ((byte) area) };
        }

        // Properties
        public AreaLevel Area
        {
            get
            {
                return this.area;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public class MercAttributeByte : MercAttributeNotification
    {
        // Fields
        public static readonly bool WRAPPED = true;

        // Methods
        public MercAttributeByte(byte[] data) : base(data)
        {
            BaseStat stat = BaseStat.Get(data[1]);
            if (stat.Signed)
            {
                base.stat = new SignedStat(stat, data[6]);
            }
            else
            {
                base.stat = new UnsignedStat(stat, data[6]);
            }
        }
    }

    public class MercAttributeDWord : MercAttributeNotification
    {
        // Fields
        public static readonly bool WRAPPED = true;

        // Methods
        public MercAttributeDWord(byte[] data) : base(data)
        {
            BaseStat stat = BaseStat.Get(data[1]);
            int val = BitConverter.ToInt32(data, 6);
            if (stat.ValShift > 0)
            {
                val = val >> stat.ValShift;
            }
            if (stat.Signed)
            {
                base.stat = new SignedStat(stat, val);
            }
            else
            {
                base.stat = new UnsignedStat(stat, (uint) val);
            }
        }
    }

    public class MercAttributeNotification : GSPacket
    {
        // Fields
        protected StatBase stat;
        protected uint uid;

        // Methods
        public MercAttributeNotification(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 2);
        }

        // Properties
        public StatBase Stat
        {
            get
            {
                return this.stat;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

    public class MercAttributeWord : MercAttributeNotification
    {
        // Fields
        public static readonly bool WRAPPED = true;

        // Methods
        public MercAttributeWord(byte[] data) : base(data)
        {
            BaseStat stat = BaseStat.Get(data[1]);
            int val = (data[6] == 0) ? data[7] : BitConverter.ToUInt16(data, 6);
            if (stat.Signed)
            {
                base.stat = new SignedStat(stat, val);
            }
            else
            {
                base.stat = new UnsignedStat(stat, (uint) val);
            }
        }
    }

    public class MercByteToExperience : MercGainExperience
    {
        // Fields
        public static readonly bool WRAPPED = true;

        // Methods
        public MercByteToExperience(byte[] data) : base(data)
        {
            base.experience = data[6];
        }
    }

    public class MercForHire : GSPacket
    {
        // Fields
        protected int mercID;

        // Methods
        public MercForHire(byte[] data) : base(data)
        {
            this.mercID = BitConverter.ToUInt16(data, 1);
        }

        // Properties
        public int MercID
        {
            get
            {
                return this.mercID;
            }
        }

        public string Unknown3
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 3, 4);
            }
        }
    }

    public class MercForHireListStart : GSPacket
    {
        // Methods
        public MercForHireListStart() : base(Build())
        {
        }

        public MercForHireListStart(byte[] data) : base(data)
        {
        }

        public static byte[] Build()
        {
            return new byte[] { 0x4f };
        }
    }

    public class MercGainExperience : GSPacket
    {
        // Fields
        protected uint experience;
        protected byte id;
        protected uint uid;

        // Methods
        public MercGainExperience(byte[] data) : base(data)
        {
            this.id = data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
        }

        // Properties
        public uint Experience
        {
            get
            {
                return this.experience;
            }
        }

        protected byte ID
        {
            get
            {
                return this.id;
            }
        }

        protected uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

    public class MercWordToExperience : MercGainExperience
    {
        // Fields
        public static readonly bool WRAPPED = true;

        // Methods
        public MercWordToExperience(byte[] data) : base(data)
        {
            base.experience = BitConverter.ToUInt16(data, 6);
        }
    }

    public class MonsterAttack : GSPacket
    {
        // Fields
        protected ushort attackType;
        protected UnitType targetType;
        protected uint targetUID;
        protected uint uid;
        protected int x;
        protected int y;

        // Methods
        public MonsterAttack(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.attackType = BitConverter.ToUInt16(data, 5);
            this.targetUID = BitConverter.ToUInt32(data, 7);
            this.targetType = (UnitType) data[11];
            this.x = BitConverter.ToUInt16(data, 12);
            this.y = BitConverter.ToUInt16(data, 14);
        }

        // Properties
        public ushort AttackType
        {
            get
            {
                return this.attackType;
            }
        }

        public UnitType TargetType
        {
            get
            {
                return this.targetType;
            }
        }

        public uint TargetUID
        {
            get
            {
                return this.targetUID;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public class NPCAction : GSPacket
    {
        // Fields
        protected ushort actionType;
        protected uint uid;
        protected int x;
        protected int y;

        // Methods
        public NPCAction(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.actionType = data[5];
            this.x = BitConverter.ToUInt16(data, 12);
            this.y = BitConverter.ToUInt16(data, 14);
        }

        // Properties
        public ushort ActionType
        {
            get
            {
                return this.actionType;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public class NPCGetHit : GSPacket
    {
        // Fields
        protected int animation;
        protected byte life;
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public NPCGetHit(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.animation = BitConverter.ToUInt16(data, 6);
            this.life = data[8];
        }

        public NPCGetHit(UnitType type, uint uid, byte life, int anim) : base(Build(type, uid, life, anim))
        {
            this.unitType = type;
            this.uid = uid;
            this.animation = anim;
            this.life = life;
        }

        public static byte[] Build(UnitType type, uint uid, byte life, int anim)
        {
            if (life > 0x80)
            {
                throw new ArgumentOutOfRangeException("life");
            }
            return new byte[] { 12, (byte)type, ((byte)uid), ((byte)(uid >> 8)), ((byte)(uid >> 0x10)), ((byte)(uid >> 0x18)), ((byte)anim), ((byte)(anim >> 8)), life };
        }

        // Properties
        public int Animation
        {
            get
            {
                return this.animation;
            }
        }

        public byte Life
        {
            get
            {
                return this.life;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

    public class NPCHeal : GSPacket
    {
        // Fields
        protected byte life;
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public NPCHeal(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.life = data[6];
        }

        // Properties
        public byte Life
        {
            get
            {
                return this.life;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

     

     
    public class NPCInfo : GSPacket
    {
        // Fields
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public NPCInfo(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
        }

        // Properties
        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public string Unknown6
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 6, 0x22);
            }
        }
    }

     

     
    public enum NPCMode
    {
        Alive = 6,
        Dead = 9,
        Dying = 8
    }

     

     
    public class NPCMove : GSPacket
    {
        // Fields
        protected uint uid;
        protected byte unknown5;
        protected int x;
        protected int y;

        // Methods
        public NPCMove(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.unknown5 = data[5];
            this.x = BitConverter.ToUInt16(data, 6);
            this.y = BitConverter.ToUInt16(data, 8);
        }

        // Properties
        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public string Unknown10
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 10, 2);
            }
        }

        public string Unknown12
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 12, 4);
            }
        }

        public byte Unknown5
        {
            get
            {
                return this.unknown5;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

     

     public class NPCMoveToTarget : GSPacket
    {
        // Fields
        protected int currentX;
        protected int currentY;
        protected byte movementType;
        protected UnitType targetType;
        protected uint targetUID;
        protected uint uid;

        // Methods
        public NPCMoveToTarget(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.movementType = data[5];
            this.currentX = BitConverter.ToUInt16(data, 6);
            this.currentY = BitConverter.ToUInt16(data, 8);
            this.targetType = (UnitType) data[10];
            this.targetUID = BitConverter.ToUInt32(data, 11);
        }

        // Properties
        public int CurrentX
        {
            get
            {
                return this.currentX;
            }
        }

        public int CurrentY
        {
            get
            {
                return this.currentY;
            }
        }

        public byte MovementType
        {
            get
            {
                return this.movementType;
            }
        }

        public UnitType TargetType
        {
            get
            {
                return this.targetType;
            }
        }

        public uint TargetUID
        {
            get
            {
                return this.targetUID;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public string Unknown15
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 15, 2);
            }
        }

        public string Unknown17
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 0x11, 4);
            }
        }
    }

     

     
    public class NPCState
    {
        // Fields
        public readonly BaseState State;
        public readonly List<StatBase> Stats;

        // Methods
        public NPCState(BaseState state)
        {
            this.State = state;
            this.Stats = null;
        }

        public NPCState(BaseState state, List<StatBase> stats)
        {
            this.State = state;
            this.Stats = stats;
        }

        public override string ToString()
        {
            if ((this.Stats == null) || (this.Stats.Count == 0))
            {
                return this.State.ToString();
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(this.State);
            builder.Append(" (");
            int num = 0;
            while (true)
            {
                builder.Append(this.Stats[num].ToString());
                if (++num >= this.Stats.Count)
                {
                    break;
                }
                builder.Append(", ");
            }
            builder.Append(")");
            return builder.ToString();
        }
    }

     

     
    public class NPCStop : GSPacket
    {
        // Fields
        protected byte life;
        protected uint uid;
        protected int x;
        protected int y;

        // Methods
        public NPCStop(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.x = BitConverter.ToUInt16(data, 5);
            this.y = BitConverter.ToUInt16(data, 7);
            this.life = data[9];
        }

        // Properties
        public byte Life
        {
            get
            {
                return this.life;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

     

     
    public class NPCWantsInteract : GSPacket
    {
        // Fields
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public NPCWantsInteract(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
        }

        // Properties
        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

     

     
    public class OpenWaypoint : GSPacket
    {
        // Fields
        protected uint uid;
        protected WaypointsAvailiable waypoints;

        // Methods
        public OpenWaypoint(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.waypoints = (WaypointsAvailiable) BitConverter.ToUInt64(data, 7);
        }

        // Properties
        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public WaypointsAvailiable Waypoints
        {
            get
            {
                return this.waypoints;
            }
        }
    }

     

     
    public class OwnedItemAction : ItemAction
    {
        // Fields
        public static readonly int NULL_Int32 = -1;
        protected UnitType ownerType;
        protected uint ownerUID;
        public static readonly bool WRAPPED = true;

        // Methods
        public OwnedItemAction(byte[] data) : base(data)
        {
            this.ownerType = (UnitType) data[8];
            this.ownerUID = BitConverter.ToUInt32(data, 9);
        }

        // Properties
        public UnitType OwnerType
        {
            get
            {
                return this.ownerType;
            }
        }

        public uint OwnerUID
        {
            get
            {
                return this.ownerUID;
            }
        }
    }

     

     
    public class PartyMemberPulse : GSPacket
    {
        // Fields
        protected uint uid;
        protected int x;
        protected int y;

        // Methods
        public PartyMemberPulse(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.x = BitConverter.ToInt32(data, 5);
            this.y = BitConverter.ToInt32(data, 9);
        }

        public PartyMemberPulse(uint uid, int x, int y) : base(Build(uid, x, y))
        {
            this.uid = uid;
            this.x = x;
            this.y = y;
        }

        public static byte[] Build(uint uid, int x, int y)
        {
            return new byte[] { 0x90, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)), ((byte) x), ((byte) (x >> 8)), ((byte) (x >> 0x10)), ((byte) (x >> 0x18)), ((byte) y), ((byte) (y >> 8)), ((byte) (y >> 0x10)), ((byte) (y >> 0x18)) };
        }

        // Properties
        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

     

     
    public class PartyMemberUpdate : GSPacket
    {
        // Fields
        protected AreaLevel area;
        protected bool isPlayer;
        protected int lifePercent;
        protected uint uid;

        // Methods
        public PartyMemberUpdate(byte[] data) : base(data)
        {
            this.isPlayer = Convert.ToBoolean(data[1]);
            this.lifePercent = BitConverter.ToUInt16(data, 2);
            this.uid = BitConverter.ToUInt32(data, 4);
            this.area = (AreaLevel) BitConverter.ToUInt16(data, 8);
        }

        // Properties
        public AreaLevel Area
        {
            get
            {
                return this.area;
            }
        }

        public bool IsPlayer
        {
            get
            {
                return this.isPlayer;
            }
        }

        public int LifePercent
        {
            get
            {
                return this.lifePercent;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

     

     
    public class PartyRefresh : GSPacket
    {
        // Fields
        protected byte alternator;
        protected uint count;
        protected uint slotNumber;

        // Methods
        public PartyRefresh(byte[] data) : base(data)
        {
            this.slotNumber = BitConverter.ToUInt32(data, 1);
            this.alternator = data[5];
            this.count = BitConverter.ToUInt32(data, 4);
        }

        // Properties
        public byte Alternator
        {
            get
            {
                return this.alternator;
            }
        }

        public uint Count
        {
            get
            {
                return this.count;
            }
        }

        public uint SlotNumber
        {
            get
            {
                return this.slotNumber;
            }
        }
    }

     

     
    public class PlayerAttributeNotification : GSPacket
    {
        // Fields
        protected StatBase stat;
        protected uint uid;

        // Methods
        public PlayerAttributeNotification(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            BaseStat stat = BaseStat.Get(data[5]);
            int val = BitConverter.ToInt32(data, 6);
            if (stat.ValShift > 0)
            {
                val = val >> stat.ValShift;
            }
            if (stat.Signed)
            {
                this.stat = new SignedStat(stat, val);
            }
            else
            {
                this.stat = new UnsignedStat(stat, (uint) val);
            }
        }

        // Properties
        public StatBase Stat
        {
            get
            {
                return this.stat;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

     

     
    public class PlayerClearCursor : GSPacket
    {
        // Fields
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public PlayerClearCursor(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
        }

        public PlayerClearCursor(UnitType unitType, uint uid) : base(Build(unitType, uid))
        {
            this.unitType = unitType;
            this.uid = uid;
        }

        public static byte[] Build(UnitType unitType, uint uid)
        {
            return new byte[] { 0x42, (byte)unitType, ((byte)uid), ((byte)(uid >> 8)), ((byte)(uid >> 0x10)), ((byte)(uid >> 0x18)) };
        }

        // Properties
        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

     

     
    public class PlayerCorpseVisible : GSPacket
    {
        // Fields
        protected bool assign;
        protected uint corpseUID;
        protected uint playerUID;

        // Methods
        public PlayerCorpseVisible(byte[] data) : base(data)
        {
            this.assign = Convert.ToBoolean(data[1]);
            this.playerUID = BitConverter.ToUInt32(data, 2);
            this.corpseUID = BitConverter.ToUInt32(data, 6);
        }

        public PlayerCorpseVisible(bool assign, uint playerUID, uint corpseUID) : base(Build(assign, playerUID, corpseUID))
        {
            this.assign = assign;
            this.playerUID = playerUID;
            this.corpseUID = corpseUID;
        }

        public static byte[] Build(bool assign, uint playerUID, uint corpseUID)
        {
            return new byte[] { 0x74, (assign ? ((byte) 1) : ((byte) 0)), ((byte) playerUID), ((byte) (playerUID >> 8)), ((byte) (playerUID >> 0x10)), ((byte) (playerUID >> 0x18)), ((byte) corpseUID), ((byte) (corpseUID >> 8)), ((byte) (corpseUID >> 0x10)), ((byte) (corpseUID >> 0x18)) };
        }

        // Properties
        public bool Assign
        {
            get
            {
                return this.assign;
            }
        }

        public uint CorpseUID
        {
            get
            {
                return this.corpseUID;
            }
        }

        public uint PlayerUID
        {
            get
            {
                return this.playerUID;
            }
        }
    }

     

     
    public class PlayerInGame : GSPacket
    {
        // Fields
        protected CharacterClass charClass;
        protected short level;
        protected string name;
        protected short partyID;
        protected uint uid;

        // Methods
        public PlayerInGame(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 3);
            this.charClass = (CharacterClass) data[7];
            this.name = ByteConverter.GetNullString(data, 8);
            this.level = BitConverter.ToInt16(data, 0x18);
            this.partyID = BitConverter.ToInt16(data, 0x1a);
        }

        public PlayerInGame(uint uid, CharacterClass charClass, string name, short level, short partyID) : base(Build(uid, charClass, name, level, partyID))
        {
            this.uid = uid;
            this.charClass = charClass;
            this.name = name;
            this.level = level;
            this.partyID = partyID;
        }

        public static byte[] Build(uint uid, CharacterClass charClass, string name, short level, short partyID)
        {
            if (((name == null) || (name.Length == 0)) || (name.Length > 0x10))
            {
                throw new ArgumentException("name");
            }
            byte[] buffer = new byte[0x22];
            buffer[0] = 0x5b;
            buffer[1] = 0x22;
            buffer[3] = (byte) uid;
            buffer[4] = (byte) (uid >> 8);
            buffer[5] = (byte) (uid >> 0x10);
            buffer[6] = (byte) (uid >> 0x18);
            buffer[0x18] = (byte) level;
            buffer[0x19] = (byte) (level >> 8);
            buffer[0x1a] = (byte) partyID;
            buffer[0x1b] = (byte) (partyID >> 8);
            for (int i = 0; i < name.Length; i++)
            {
                buffer[8 + i] = (byte) name[i];
            }
            return buffer;
        }

        // Properties
        public CharacterClass Class
        {
            get
            {
                return this.charClass;
            }
        }

        public short Level
        {
            get
            {
                return this.level;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public short PartyID
        {
            get
            {
                return this.partyID;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public string Unknown2
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 0x1c);
            }
        }
    }

     

     
    public class PlayerInSight : GSPacket
    {
        // Fields
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public PlayerInSight(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
        }

        public PlayerInSight(UnitType unitType, uint uid) : base(Build(unitType, uid))
        {
            this.unitType = unitType;
            this.uid = uid;
        }

        public static byte[] Build(UnitType unitType, uint uid)
        {
            return new byte[] { 0x76, (byte)unitType, ((byte)uid), ((byte)(uid >> 8)), ((byte)(uid >> 0x10)), ((byte)(uid >> 0x18)) };
        }

        // Properties
        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

     

     
    public class PlayerKillCount : GSPacket
    {
        // Fields
        protected short count;
        protected uint uid;

        // Methods
        public PlayerKillCount(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.count = BitConverter.ToInt16(data, 5);
        }

        // Properties
        public short Count
        {
            get
            {
                return this.count;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

     

     
    public class PlayerLeaveGame : GSPacket
    {
        // Fields
        protected uint uid;

        // Methods
        public PlayerLeaveGame(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
        }

        public PlayerLeaveGame(uint uid) : base(Build(uid))
        {
            this.uid = uid;
        }

        public static byte[] Build(uint uid)
        {
            return new byte[] { 0x5c, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)) };
        }

        // Properties
        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

     

     
    public class PlayerLifeManaChange : GSPacket
    {
        // Fields
        protected int life;
        protected int mana;
        protected int stamina;
        protected byte[] unknown85b;
        protected int x;
        protected int y;

        // Methods
        public PlayerLifeManaChange(byte[] data) : base(data)
        {
            BitReader reader = new BitReader(data, 1);
            this.life = reader.ReadInt32(15);
            this.mana = reader.ReadInt32(15);
            this.stamina = reader.ReadInt32(15);
            this.x = reader.ReadInt32(0x10);
            this.y = reader.ReadInt32(0x10);
            this.unknown85b = reader.ReadByteArray();
        }

        // Properties
        public int Life
        {
            get
            {
                return this.life;
            }
        }

        public int Mana
        {
            get
            {
                return this.mana;
            }
        }

        public int Stamina
        {
            get
            {
                return this.stamina;
            }
        }

        public byte[] Unknown85b
        {
            get
            {
                return this.unknown85b;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

     

     
    public class PlayerMove : GSPacket
    {
        // Fields
        protected int currentX;
        protected int currentY;
        protected byte movementType;
        protected int targetX;
        protected int targetY;
        protected uint uid;
        protected UnitType unitType;
        protected byte unknown12;

        // Methods
        public PlayerMove(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.movementType = data[6];
            this.targetX = BitConverter.ToUInt16(data, 7);
            this.targetY = BitConverter.ToUInt16(data, 9);
            this.unknown12 = data[12];
            this.currentX = BitConverter.ToUInt16(data, 12);
            this.currentY = BitConverter.ToUInt16(data, 14);
        }

        public PlayerMove(D2Data.UnitType type, uint uid, byte movementtype, int targetX, int targetY, byte unknown12, int currentX, int currentY)
            : base(Build(type,uid,movementtype,targetX,targetY,unknown12,currentX,currentY ))
        {
            this.currentX =currentX;
            this.currentY = currentY;
            this.movementType = movementtype;
            this.targetX = targetX;
            this.targetY = targetY;
            this.uid = uid;
            this.unitType = type;
            this.unknown12 = unknown12;
        }

        public static byte[] Build(D2Data.UnitType type, uint uid, byte movementtype, int targetX, int targetY, byte unknown12, int currentX, int currentY)
        {
            byte[] buffer = {0x0F,(byte)type,
                                (byte)uid,(byte)(uid>>8),(byte)(uid>>16),(byte)(uid>>24),
                                (byte)movementtype,
                                (byte)targetX,(byte)(targetX >>8),
                                (byte)targetY,(byte)(targetY >>8),
                                unknown12,
                                (byte)currentX,(byte)(currentX >>8),
                                (byte)currentY,(byte)(currentY >>8)
                            };
            return buffer;
        }

        // Properties
        public int CurrentX
        {
            get
            {
                return this.currentX;
            }
        }

        public int CurrentY
        {
            get
            {
                return this.currentY;
            }
        }

        public byte MovementType
        {
            get
            {
                return this.movementType;
            }
        }

        public int TargetX
        {
            get
            {
                return this.targetX;
            }
        }

        public int TargetY
        {
            get
            {
                return this.targetY;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public byte Unknown12
        {
            get
            {
                return this.unknown12;
            }
        }
    }

    public class PlayerMoveToTarget : GSPacket
    {
        // Fields
        protected int currentX;
        protected int currentY;
        protected byte movementType;
        protected UnitType targetType;
        protected uint targetUID;
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public PlayerMoveToTarget(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.movementType = data[6];
            this.targetType = (UnitType) data[7];
            this.targetUID = BitConverter.ToUInt32(data, 8);
            this.currentX = BitConverter.ToUInt16(data, 12);
            this.currentY = BitConverter.ToUInt16(data, 14);
        }

        // Properties
        public int CurrentX
        {
            get
            {
                return this.currentX;
            }
        }

        public int CurrentY
        {
            get
            {
                return this.currentY;
            }
        }

        public byte MovementType
        {
            get
            {
                return this.movementType;
            }
        }

        public UnitType TargetType
        {
            get
            {
                return this.targetType;
            }
        }

        public uint TargetUID
        {
            get
            {
                return this.targetUID;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }
 
    public class PlayerPartyRelationship : GSPacket
    {
        // Fields
        protected PartyRelationshipType relationship;
        protected uint uid;

        // Methods
        public PlayerPartyRelationship(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.relationship = (PartyRelationshipType) data[5];
        }

        // Properties
        public PartyRelationshipType Relationship
        {
            get
            {
                return this.relationship;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

    public class PlayerReassign : GSPacket
    {
        // Fields
        protected bool reassign;
        protected uint uid;
        protected UnitType unitType;
        protected int x;
        protected int y;

        // Methods
        public PlayerReassign(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.x = BitConverter.ToUInt16(data, 6);
            this.y = BitConverter.ToUInt16(data, 8);
            this.reassign = data[10] != 0;
        }

        public PlayerReassign(UnitType type, uint uid, int x, int y, bool reassign) : base(Build(type, uid, x, y, reassign))
        {
            this.unitType = type;
            this.uid = uid;
        }

        public static byte[] Build(UnitType type, uint uid, int x, int y, bool reassign)
        {
            return new byte[] { 0x15, (byte)type, ((byte)uid), ((byte)(uid >> 8)), ((byte)(uid >> 0x10)), ((byte)(uid >> 0x18)), ((byte)x), ((byte)(y >> 8)), ((byte)y), ((byte)(y >> 8)), (reassign ? ((byte)1) : ((byte)0)) };
        }

        // Properties
        public bool Reassign
        {
            get
            {
                return this.reassign;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public class PlayerRelationship : GSPacket
    {
        // Fields
        protected uint objectUID;
        protected PlayerRelationshipType relations;
        protected uint subjectUID;

        // Methods
        public PlayerRelationship(byte[] data) : base(data)
        {
            this.subjectUID = BitConverter.ToUInt32(data, 1);
            this.objectUID = BitConverter.ToUInt32(data, 5);
            this.relations = (PlayerRelationshipType) BitConverter.ToUInt16(data, 9);
        }

        // Properties
        public uint ObjectUID
        {
            get
            {
                return this.objectUID;
            }
        }

        public PlayerRelationshipType Relations
        {
            get
            {
                return this.relations;
            }
        }

        public uint SubjectUID
        {
            get
            {
                return this.subjectUID;
            }
        }
    }

    public class PlayerStop : GSPacket
    {
        // Fields
        protected byte life;
        protected uint uid;
        protected UnitType unitType;
        protected byte unknown1;
        protected byte unknown2;
        protected int x;
        protected int y;

        // Methods
        public PlayerStop(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.unknown1 = data[6];
            this.x = BitConverter.ToUInt16(data, 7);
            this.y = BitConverter.ToUInt16(data, 9);
            this.unknown2 = data[11];
            this.life = data[12];
        }

        // Properties
        public byte Life
        {
            get
            {
                return this.life;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public byte Unknown1
        {
            get
            {
                return this.unknown1;
            }
        }

        public byte Unknown2
        {
            get
            {
                return this.unknown2;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public class PlaySound : GSPacket
    {
        // Fields
        protected GameSound sound;
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public PlaySound(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.sound = (GameSound) BitConverter.ToUInt16(data, 6);
        }

        public PlaySound(UnitType unitType, uint uid, GameSound sound) : base(Build(unitType, uid, sound))
        {
            this.unitType = unitType;
            this.uid = uid;
            this.sound = sound;
        }

        public static byte[] Build(UnitType unitType, uint uid, GameSound sound)
        {
            return new byte[] { 0x2c, (byte)unitType, ((byte)uid), ((byte)(uid >> 8)), ((byte)(uid >> 0x10)), ((byte)(uid >> 0x18)), ((byte)sound), ((byte)(((ushort)sound) >> 8)) };
        }

        // Properties
        public GameSound Sound
        {
            get
            {
                return this.sound;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

    public class Pong : GSPacket
    {
        // Methods
        public Pong() : base(Build())
        {
        }

        public Pong(byte[] data) : base(data)
        {
        }

        public static byte[] Build()
        {
            return new byte[] { 0x8f };
        }
    }

    public class PortalInfo : GSPacket
    {
        // Fields
        protected AreaLevel destination;
        protected TownPortalState state;
        protected uint uid;

        // Methods
        public PortalInfo(byte[] data) : base(data)
        {
            this.state = (TownPortalState) data[1];
            this.destination = (AreaLevel) data[2];
            this.uid = BitConverter.ToUInt32(data, 3);
        }

        // Properties
        public AreaLevel Destination
        {
            get
            {
                return this.destination;
            }
        }

        public TownPortalState State
        {
            get
            {
                return this.state;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

    public class PortalOwnership : GSPacket
    {
        // Fields
        protected string ownerName;
        protected uint ownerUID;
        protected uint portalLocalUID;
        protected uint portalRemoteUID;

        // Methods
        public PortalOwnership(byte[] data) : base(data)
        {
            this.ownerUID = BitConverter.ToUInt32(data, 1);
            this.ownerName = ByteConverter.GetNullString(data, 5, 0x10);
            this.portalLocalUID = BitConverter.ToUInt32(data, 0x15);
            this.portalRemoteUID = BitConverter.ToUInt32(data, 0x19);
        }

        // Properties
        public string OwnerName
        {
            get
            {
                return this.ownerName;
            }
        }

        public uint OwnerUID
        {
            get
            {
                return this.ownerUID;
            }
        }

        public uint PortalLocalUID
        {
            get
            {
                return this.portalLocalUID;
            }
        }

        public uint PortalRemoteUID
        {
            get
            {
                return this.portalRemoteUID;
            }
        }
    }

    public enum QuestInfoUpdateType
    {
        NPCInteract = 1,
        QuestLog = 6
    }

     

     
    public class QuestItemState : GSPacket
    {
        // Methods
        public QuestItemState(byte[] data) : base(data)
        {
        }

        // Properties
        public string Unknown1
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 1);
            }
        }
    }

     

     
    public class Relator1 : GSPacket
    {
        // Fields
        protected ushort param1;
        protected uint param2;
        protected uint uid;

        // Methods
        public Relator1(byte[] data) : base(data)
        {
            this.param1 = BitConverter.ToUInt16(data, 1);
            this.uid = BitConverter.ToUInt32(data, 3);
            this.param2 = BitConverter.ToUInt32(data, 7);
        }

        public Relator1(uint uid, ushort param1, uint param2) : base(Build(uid, param1, param2))
        {
            this.param1 = param1;
            this.uid = uid;
            this.param2 = param2;
        }

        public static byte[] Build(uint uid, ushort param1, uint param2)
        {
            return new byte[] { 0x47, ((byte) param1), ((byte) (param1 >> 8)), ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)), ((byte) param2), ((byte) (param2 >> 8)), ((byte) (param2 >> 0x10)), ((byte) (param2 >> 0x18)) };
        }

        // Properties
        public ushort Param1
        {
            get
            {
                return this.param1;
            }
        }

        public uint Param2
        {
            get
            {
                return this.param2;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

     

     
    public class Relator2 : GSPacket
    {
        // Fields
        protected ushort param1;
        protected uint param2;
        protected uint uid;

        // Methods
        public Relator2(byte[] data) : base(data)
        {
            this.param1 = BitConverter.ToUInt16(data, 1);
            this.uid = BitConverter.ToUInt32(data, 3);
            this.param2 = BitConverter.ToUInt32(data, 7);
        }

        public Relator2(uint uid, ushort param1, uint param2) : base(Build(uid, param1, param2))
        {
            this.param1 = param1;
            this.uid = uid;
            this.param2 = param2;
        }

        public static byte[] Build(uint uid, ushort param1, uint param2)
        {
            return new byte[] { 0x48, ((byte) param1), ((byte) (param1 >> 8)), ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)), ((byte) param2), ((byte) (param2 >> 8)), ((byte) (param2 >> 0x10)), ((byte) (param2 >> 0x18)) };
        }

        // Properties
        public ushort Param1
        {
            get
            {
                return this.param1;
            }
        }

        public uint Param2
        {
            get
            {
                return this.param2;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

     

     
    public class RemoveGroundUnit : GSPacket
    {
        // Fields
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public RemoveGroundUnit(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
        }

        public RemoveGroundUnit(UnitType type, uint uid) : base(Build(type, uid))
        {
            this.unitType = type;
            this.uid = uid;
        }

        public static byte[] Build(UnitType type, uint uid)
        {
            return new byte[] { 10, (byte)type, ((byte)uid), ((byte)(uid >> 8)), ((byte)(uid >> 0x10)), ((byte)(uid >> 0x18)) };
        }

        // Properties
        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

     

     
    public class ReportKill : GSPacket
    {
        // Fields
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public ReportKill(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
        }

        public ReportKill(UnitType type, uint uid) : base(Build(type, uid))
        {
            this.unitType = type;
            this.uid = uid;
        }

        public static byte[] Build(UnitType type, uint uid)
        {
            return new byte[] { 0x11, (byte)type, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)) };
        }

        // Properties
        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }
    }

     

     
    public class RequestLogonInfo : GSPacket
    {
        // Fields
        protected byte protocolVersion;

        // Methods
        public RequestLogonInfo(byte[] data) : base(data)
        {
            this.protocolVersion = data[1];
        }

        // Properties
        public byte ProtocolVersion
        {
            get
            {
                return this.protocolVersion;
            }
        }
    }

     

     
    public class SetGameObjectMode : GSPacket
    {
        // Fields
        protected bool canChangeBack;
        protected GameObjectMode mode;
        protected uint uid;
        protected UnitType unitType;
        protected byte unknown6;

        // Methods
        public SetGameObjectMode(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.unknown6 = data[6];
            this.canChangeBack = BitConverter.ToBoolean(data, 7);
            this.mode = (GameObjectMode) BitConverter.ToUInt32(data, 8);
        }

        // Properties
        public bool CanChangeBack
        {
            get
            {
                return this.canChangeBack;
            }
        }

        public GameObjectMode Mode
        {
            get
            {
                return this.mode;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public byte Unknown6
        {
            get
            {
                return this.unknown6;
            }
        }
    }

     

     
    public class SetItemState : GSPacket
    {
        // Fields
        protected uint itemUID;
        protected UnitType ownerType;
        protected uint ownerUID;
        protected ItemStateType state;
        protected ItemStateType state2;
        protected byte unknown10;
        protected byte unknown17;

        // Methods
        public SetItemState(byte[] data) : base(data)
        {
            this.ownerType = (UnitType) data[1];
            this.ownerUID = BitConverter.ToUInt32(data, 2);
            this.itemUID = BitConverter.ToUInt32(data, 6);
            this.unknown10 = data[10];
            this.state = (ItemStateType) BitConverter.ToUInt32(data, 11);
            this.state2 = (ItemStateType) BitConverter.ToUInt16(data, 15);
            this.unknown17 = data[0x11];
        }

        public SetItemState(UnitType ownerType, uint ownerUID, uint itemUID, ItemStateType state) : base(Build(ownerType, ownerUID, itemUID, state))
        {
            this.ownerType = ownerType;
            this.ownerUID = ownerUID;
            this.itemUID = itemUID;
            this.state = state;
            this.state2 = state;
        }

        public static byte[] Build(UnitType ownerType, uint ownerUID, uint itemUID, ItemStateType state)
        {
            byte[] buffer = new byte[0x12];
            buffer[0] = 0x7d;
            buffer[1] = (byte) ownerType;
            buffer[2] = (byte) ownerUID;
            buffer[3] = (byte) (ownerUID >> 8);
            buffer[4] = (byte) (ownerUID >> 0x10);
            buffer[5] = (byte) (ownerUID >> 0x18);
            buffer[6] = (byte) itemUID;
            buffer[7] = (byte) (itemUID >> 8);
            buffer[8] = (byte) (itemUID >> 0x10);
            buffer[9] = (byte) (itemUID >> 0x18);
            buffer[11] = (byte) state;
            buffer[12] = (byte) (((int) state) >> 8);
            buffer[13] = (byte) (((int) state) >> 0x10);
            buffer[14] = (byte) (((int) state) >> 0x18);
            buffer[15] = (byte) state;
            buffer[0x10] = (byte) (((ushort) state) >> 8);
            return buffer;
        }

        // Properties
        public uint ItemUID
        {
            get
            {
                return this.itemUID;
            }
        }

        public UnitType OwnerType
        {
            get
            {
                return this.ownerType;
            }
        }

        public uint OwnerUID
        {
            get
            {
                return this.ownerUID;
            }
        }

        public ItemStateType State
        {
            get
            {
                return this.state;
            }
        }

        public ItemStateType State2
        {
            get
            {
                return this.state2;
            }
        }

        public byte Unknown10
        {
            get
            {
                return this.unknown10;
            }
        }

        public byte Unknown17
        {
            get
            {
                return this.unknown17;
            }
        }
    }

     

     
    public class SetNPCMode : GSPacket
    {
        // Fields
        protected byte life;
        protected NPCMode mode;
        protected uint uid;
        protected byte unknown11;
        protected int x;
        protected int y;

        // Methods
        public SetNPCMode(byte[] data) : base(data)
        {
            this.uid = BitConverter.ToUInt32(data, 1);
            this.mode = (NPCMode) data[5];
            this.x = BitConverter.ToUInt16(data, 6);
            this.y = BitConverter.ToUInt16(data, 8);
            this.life = data[10];
            this.unknown11 = data[11];
        }

        // Properties
        public byte Life
        {
            get
            {
                return this.life;
            }
        }

        public NPCMode Mode
        {
            get
            {
                return this.mode;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public byte Unknown11
        {
            get
            {
                return this.unknown11;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

     

     
    public class SetState : GSPacket
    {
        // Fields
        protected BaseState state;
        protected List<StatBase> stats;
        protected uint uid;
        protected UnitType unitType;
        protected byte[] unknownEnd;

        // Methods
        public SetState(byte[] data) : base(data)
        {
            int num;
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.state = BaseState.Get(data[7]);
            this.stats = new List<StatBase>();
            BitReader reader = new BitReader(data, 8);
        Label_003E:
            num = reader.ReadInt32(9);
            if (num != 0x1ff)
            {
                BaseStat stat = BaseStat.Get(num);
                int val = reader.ReadInt32(stat.SendBits);
                if (stat.SendParamBits > 0)
                {
                    int param = reader.ReadInt32(stat.SendParamBits);
                    if (stat.Signed)
                    {
                        this.stats.Add(new SignedStatParam(stat, val, param));
                    }
                    else
                    {
                        this.stats.Add(new UnsignedStatParam(stat, (uint) val, (uint) param));
                    }
                }
                else if (stat.Signed)
                {
                    this.stats.Add(new SignedStat(stat, val));
                }
                else
                {
                    this.stats.Add(new UnsignedStat(stat, (uint) val));
                }
                goto Label_003E;
            }
            this.unknownEnd = reader.ReadByteArray();
        }

        // Properties
        public BaseState State
        {
            get
            {
                return this.state;
            }
        }

        public List<StatBase> Stats
        {
            get
            {
                return this.stats;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public byte[] UnknownEnd
        {
            get
            {
                return this.unknownEnd;
            }
        }
    }

     

     
    public class SkillsLog : GSPacket
    {
        // Fields
        protected BaseSkillLevel[] skills;
        protected uint uid;

        // Methods
        public SkillsLog(byte[] data) : base(data)
        {
            this.skills = new BaseSkillLevel[data[1]];
            this.uid = BitConverter.ToUInt32(data, 2);
            for (int i = 0; i < data[1]; i++)
            {
                this.skills[i] = new BaseSkillLevel(BitConverter.ToUInt16(data, 6 + (i * 3)), data[8 + (i * 3)]);
            }
        }

        // Properties
        public BaseSkillLevel[] Skills
        {
            get
            {
                return this.skills;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

     

     
    public class SmallGoldAdd : GSPacket
    {
        // Fields
        protected byte amount;

        // Methods
        public SmallGoldAdd(byte[] data) : base(data)
        {
            this.amount = data[1];
        }

        public SmallGoldAdd(byte amount) : base(Build(amount))
        {
            this.amount = amount;
        }

        public static byte[] Build(byte amount)
        {
            return new byte[] { 0x19, amount };
        }

        // Properties
        public byte Amount
        {
            get
            {
                return this.amount;
            }
        }
    }

     

     
    public enum SpecialItemType
    {
        TomeOrScroll = 4
    }

     

     
    public enum StackableItemClickType1 : sbyte
    {
        ActionClick = -1,
        NormalClick = 0
    }

     

     
    public enum StackableItemClickType2 : short
    {
        ActionClick = -1,
        NormalClick = 0xda
    }

     

     
    public class SummonAction : GSPacket
    {
        // Fields
        protected SummonActionType actionType;
        protected int petType;
        protected uint petUID;
        protected uint playerUID;
        protected byte skillTree;

        // Methods
        public SummonAction(byte[] data) : base(data)
        {
            this.actionType = (SummonActionType) data[1];
            this.skillTree = data[2];
            this.petType = BitConverter.ToUInt16(data, 3);
            this.playerUID = BitConverter.ToUInt32(data, 5);
            this.petUID = BitConverter.ToUInt32(data, 9);
        }

        // Properties
        public SummonActionType ActionType
        {
            get
            {
                return this.actionType;
            }
        }

        public int PetType
        {
            get
            {
                return this.petType;
            }
        }

        public uint PetUID
        {
            get
            {
                return this.petUID;
            }
        }

        public uint PlayerUID
        {
            get
            {
                return this.playerUID;
            }
        }

        public byte SkillTree
        {
            get
            {
                return this.skillTree;
            }
        }
    }

     

     
    public enum SummonActionType
    {
        UnsummonedOrLostSight,
        SummonedOrReassigned
    }

     

     
    public class SwitchWeaponSet : GSPacket
    {
        // Methods
        public SwitchWeaponSet() : base(Build())
        {
        }

        public SwitchWeaponSet(byte[] data) : base(data)
        {
        }

        public static byte[] Build()
        {
            return new byte[] { 0x97 };
        }
    }

     

     
    [Flags]
    public enum TownPortalState
    {
        IsOtherSide = 2,
        None = 0,
        Unknown1 = 1,
        Used = 4
    }

     

     
    public class TransactionComplete : GSPacket
    {
        // Fields
        protected uint goldLeft;
        protected TransactionType type;
        protected uint uid;

        // Methods
        public TransactionComplete(byte[] data) : base(data)
        {
            this.type = (TransactionType) data[1];
            this.uid = BitConverter.ToUInt32(data, 7);
            this.goldLeft = BitConverter.ToUInt32(data, 11);
        }

        // Properties
        public uint GoldLeft
        {
            get
            {
                return this.goldLeft;
            }
        }

        public TransactionType Type
        {
            get
            {
                return this.type;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public string Unknown2
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 2, 5);
            }
        }
    }

     

     
    public enum TransactionType
    {
        Buy = 4,
        Hire = 0,
        Repair = 1,
        Sell = 3,
        ToStack = 5
    }

     

     
    public class UnitUseSkill : GSPacket
    {
        // Fields
        protected SkillType skill;
        protected uint uid;
        protected UnitType unitType;
        protected int x;
        protected int y;

        // Methods
        public UnitUseSkill(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            if (this.unitType != UnitType.GameObject)
            {
                this.skill = (SkillType) ((ushort) BitConverter.ToUInt32(data, 6));
                this.x = BitConverter.ToUInt16(data, 11);
                this.y = BitConverter.ToUInt16(data, 13);
            }
        }

        // Properties
        public SkillType Skill
        {
            get
            {
                return this.skill;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public string Unknown10
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 10, 1);
            }
        }

        public string Unknown15
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 15, 2);
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

     

     
    public class UnitUseSkillOnTarget : GSPacket
    {
        // Fields
        protected SkillType skill;
        protected uint targetUID;
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public UnitUseSkillOnTarget(byte[] data) : base(data)
        {
            this.unitType = (UnitType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.skill = (SkillType) BitConverter.ToUInt16(data, 6);
            this.targetUID = BitConverter.ToUInt32(data, 10);
        }

        // Properties
        public SkillType Skill
        {
            get
            {
                return this.skill;
            }
        }

        public uint TargetUID
        {
            get
            {
                return this.targetUID;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public string Unknown14
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 14, 2);
            }
        }

        public string Unknown8
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 8, 2);
            }
        }
    }

     

     
    public class UnloadDone : GSPacket
    {
        // Methods
        public UnloadDone() : base(Build())
        {
        }

        public UnloadDone(byte[] data) : base(data)
        {
        }

        public static byte[] Build()
        {
            return new byte[] { 5 };
        }
    }

     

     
    public class UpdateGameQuestLog : GSPacket
    {
        // Fields
        protected GameQuestInfo[] quests;

        // Methods
        public UpdateGameQuestLog(byte[] data) : base(data)
        {
            this.quests = new GameQuestInfo[0x29];
            for (int i = 0; i < 0x29; i++)
            {
                this.quests[i] = new GameQuestInfo((QuestType) i, (GameQuestState) BitConverter.ToUInt16(data, 1 + (i * 2)));
            }
        }

        // Properties
        public GameQuestInfo[] Quests
        {
            get
            {
                return this.quests;
            }
        }

        public string Unknown82
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 0x53);
            }
        }
    }

     

     
    public class UpdateItemStats : GSPacket
    {
        // Fields
        public static readonly int NULL_Int32 = -1;
        protected long offset;
        protected List<StatBase> stats;
        protected uint uid;
        protected int unknown60b;
        protected int unknown61b;
        protected int unknown78b;
        protected int unknown8b;
        protected byte[] unknownEnd;

        // Methods
        public UpdateItemStats(byte[] data) : base(data)
        {
            this.stats = new List<StatBase>();
            this.unknown61b = -1;
            this.unknown78b = -1;
            BitReader reader = new BitReader(data, 1);
            this.unknown8b = reader.ReadInt32(10);
            this.uid = reader.ReadUInt32();
            while (reader.ReadBoolean(1))
            {
                BaseStat stat = BaseStat.Get(reader.ReadInt32(9));
                this.unknown60b = reader.ReadInt32(1);
                if (stat.Type == StatType.ChargedSkill)
                {
                    this.unknown61b = reader.ReadInt32(1);
                    int charges = reader.ReadInt32(8);
                    int maxCharges = reader.ReadInt32(8);
                    this.unknown78b = reader.ReadInt32(1);
                    int level = reader.ReadInt32(6);
                    int skill = reader.ReadInt32(10);
                    this.stats.Add(new ChargedSkillStat(stat, level, skill, charges, maxCharges));
                }
                else
                {
                    if (stat.Signed)
                    {
                        this.stats.Add(new SignedStat(stat, reader.ReadInt32(stat.SendBits)));
                        continue;
                    }
                    this.stats.Add(new UnsignedStat(stat, reader.ReadUInt32(stat.SendBits)));
                }
            }
            this.offset = reader.Position;
            this.unknownEnd = reader.ReadByteArray();
        }

        // Properties
        public List<StatBase> Stats
        {
            get
            {
                return this.stats;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public int Unknown60b
        {
            get
            {
                return this.unknown60b;
            }
        }

        public int Unknown61b
        {
            get
            {
                return this.unknown61b;
            }
        }

        public int Unknown78b
        {
            get
            {
                return this.unknown78b;
            }
        }

        public int Unknown8b
        {
            get
            {
                return this.unknown8b;
            }
        }

        public byte[] UnknownEnd
        {
            get
            {
                return this.unknownEnd;
            }
        }
    }

     

     
    public class UpdateItemUI : GSPacket
    {
        // Fields
        protected ItemUIAction action;

        // Methods
        public UpdateItemUI(byte[] data) : base(data)
        {
            this.action = (ItemUIAction) data[1];
        }

        public UpdateItemUI(ItemUIAction action) : base(Build(action))
        {
            this.action = action;
        }

        public static byte[] Build(ItemUIAction action)
        {
            return new byte[] { 0x77, (byte)action };
        }

        // Properties
        public ItemUIAction Action
        {
            get
            {
                return this.action;
            }
        }
    }

     

     
    public class UpdatePlayerItemSkill : GSPacket
    {
        // Fields
        protected uint playerUID;
        protected int quantity;
        protected SkillType skill;
        protected ushort unknown1;
        protected ushort unknown10;

        // Methods
        public UpdatePlayerItemSkill(byte[] data) : base(data)
        {
            this.unknown1 = BitConverter.ToUInt16(data, 1);
            this.playerUID = BitConverter.ToUInt32(data, 3);
            this.skill = (SkillType) BitConverter.ToUInt16(data, 7);
            this.quantity = data[9];
            this.unknown10 = BitConverter.ToUInt16(data, 10);
        }

        // Properties
        public uint PlayerUID
        {
            get
            {
                return this.playerUID;
            }
        }

        public int Quantity
        {
            get
            {
                return this.quantity;
            }
        }

        public SkillType Skill
        {
            get
            {
                return this.skill;
            }
        }

        public ushort Unknown1
        {
            get
            {
                return this.unknown1;
            }
        }

        public ushort Unknown10
        {
            get
            {
                return this.unknown10;
            }
        }
    }

     

     
    public class UpdateQuestInfo : GSPacket
    {
        // Fields
        public static readonly uint NULL_UInt32;
        protected QuestInfo[] quests;
        protected QuestInfoUpdateType type;
        protected uint uid;

        // Methods
        public UpdateQuestInfo(byte[] data) : base(data)
        {
            this.type = (QuestInfoUpdateType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
            this.quests = new QuestInfo[0x29];
            for (int i = 0; i < 0x29; i++)
            {
                this.quests[i] = new QuestInfo((QuestType) i, (QuestState) data[6 + (i * 2)], (QuestStanding) data[7 + (i * 2)]);
            }
        }

        // Properties
        public QuestInfo[] Quests
        {
            get
            {
                return this.quests;
            }
        }

        public QuestInfoUpdateType Type
        {
            get
            {
                return this.type;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public string Unknown88
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 0x58);
            }
        }
    }

     

     
    public class UpdateQuestLog : GSPacket
    {
        // Fields
        protected QuestLog[] quests;

        // Methods
        public UpdateQuestLog(byte[] data) : base(data)
        {
            this.quests = new QuestLog[0x29];
            for (int i = 0; i < 0x29; i++)
            {
                this.quests[i] = new QuestLog((QuestType) i, data[i + 1]);
            }
        }

        // Properties
        public QuestLog[] Quests
        {
            get
            {
                return this.quests;
            }
        }
    }

    public class UpdateSkill : GSPacket
    {
        // Fields
        protected int baseLevel;
        protected int bonus;
        protected SkillType skill;
        protected uint uid;
        protected UnitType unitType;

        // Methods
        public UpdateSkill(byte[] data) : base(data)
        {
            this.unitType = (UnitType) ((byte) BitConverter.ToUInt16(data, 1));
            this.uid = BitConverter.ToUInt32(data, 3);
            this.skill = (SkillType) BitConverter.ToUInt16(data, 7);
            this.baseLevel = data[9];
            this.bonus = data[10];
        }

        // Properties
        public int BaseLevel
        {
            get
            {
                return this.baseLevel;
            }
        }

        public int Bonus
        {
            get
            {
                return this.bonus;
            }
        }

        public SkillType Skill
        {
            get
            {
                return this.skill;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }

        public UnitType UnitType
        {
            get
            {
                return this.unitType;
            }
        }

        public string Unknown11
        {
            get
            {
                return ByteConverter.ToHexString(base.Data, 11, 1);
            }
        }
    }

     
    public class UseSpecialItem : GSPacket
    {
        // Fields
        protected SpecialItemType action;
        protected uint uid;

        // Methods
        public UseSpecialItem(byte[] data) : base(data)
        {
            this.action = (SpecialItemType) data[1];
            this.uid = BitConverter.ToUInt32(data, 2);
        }

        public UseSpecialItem(SpecialItemType action, uint uid) : base(Build(action, uid))
        {
            this.action = action;
            this.uid = uid;
        }

        public static byte[] Build(SpecialItemType action, uint uid)
        {
            return new byte[] { 0x7c, ((byte) action), ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)) };
        }

        // Properties
        public SpecialItemType Action
        {
            get
            {
                return this.action;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

    public class UseStackableItem : GSPacket
    {
        // Fields
        protected StackableItemClickType1 type1;
        protected StackableItemClickType2 type2;
        protected uint uid;

        // Methods
        public UseStackableItem(byte[] data) : base(data)
        {
            this.type1 = (StackableItemClickType1) ((sbyte) data[1]);
            this.uid = BitConverter.ToUInt32(data, 2);
            this.type2 = (StackableItemClickType2) BitConverter.ToInt16(data, 6);
        }

        // Properties
        public StackableItemClickType1 Type1
        {
            get
            {
                return this.type1;
            }
        }

        public StackableItemClickType2 Type2
        {
            get
            {
                return this.type2;
            }
        }

        public uint UID
        {
            get
            {
                return this.uid;
            }
        }
    }

    public class WalkVerify : GSPacket
    {
        // Fields
        protected int stamina;
        protected int state;
        protected int x;
        protected int y;

        // Methods
        public WalkVerify(byte[] data) : base(data)
        {
            BitReader reader = new BitReader(data, 1);
            this.stamina = reader.ReadInt32(15);
            this.x = reader.ReadInt32(0x10);
            this.y = reader.ReadInt32(0x10);
            this.state = reader.ReadInt32(0x11);
        }

        // Properties
        public int Stamina
        {
            get
            {
                return this.stamina;
            }
        }

        public int State
        {
            get
            {
                return this.state;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
        }
    }

    public class WardenCheck : GSPacket
    {
        // Fields
        protected int dataLength;

        // Methods
        public WardenCheck(byte[] data) : base(data)
        {
            this.dataLength = BitConverter.ToUInt16(data, 1);
        }

        // Properties
        public int DataLength
        {
            get
            {
                return this.dataLength;
            }
        }

        public byte[] WardenData
        {
            get
            {
                byte[] destinationArray = new byte[this.dataLength];
                Array.Copy(base.Data, 3, destinationArray, 0, this.dataLength);
                return destinationArray;
            }
        }
    }

    public class WordToExperience : GainExperience
    {
        // Fields
        public static readonly bool WRAPPED = true;

        // Methods
        public WordToExperience(byte[] data) : base(data)
        {
            base.experience = BitConverter.ToUInt16(data, 1);
        }
    }

    public class WorldItemAction : ItemAction
    {
        // Fields
        public static readonly int NULL_Int32 = -1;
        public static readonly bool WRAPPED = true;

        // Methods
        public WorldItemAction(byte[] data) : base(data)
        {
        }
    }

}