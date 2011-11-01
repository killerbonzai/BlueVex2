using System;
using D2Data;
using D2Packets;
using ETUtils;

namespace GameClient
{
public class AddBeltItem : GCPacket
{
    // Fields
    protected uint uid;
    protected ushort x;
    protected ushort y;

    // Methods
    public AddBeltItem(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
        this.x = (ushort) (data[5] % 4);
        this.y = (ushort) (data[5] / 4);
    }

    public AddBeltItem(uint uid, ushort x, ushort y) : base(Build(uid, x, y))
    {
        this.uid = uid;
        this.x = x;
        this.y = y;
    }

    public static byte[] Build(uint uid, ushort x, ushort y)
    {
        return new byte[] { 0x23, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)), ((byte) x), ((byte) (x >> 8)), ((byte) y), ((byte) (y >> 8)) };
    }

    // Properties
    public uint UID
    {
        get
        {
            return this.uid;
        }
    }

    public ushort X
    {
        get
        {
            return this.x;
        }
    }

    public ushort Y
    {
        get
        {
            return this.y;
        }
    }
}

[Flags]
public enum BuyFlags : ushort
{
    FillStack = 0x8000,
    None = 0
}

public class BuyItem : GCPacket
{
    // Fields
    protected uint cost;
    protected uint dealerUID;
    protected BuyFlags flags;
    protected uint itemUID;
    protected TradeType tradeType;

    // Methods
    public BuyItem(byte[] data) : base(data)
    {
        this.dealerUID = BitConverter.ToUInt32(data, 1);
        this.itemUID = BitConverter.ToUInt32(data, 5);
        this.tradeType = (TradeType) BitConverter.ToUInt16(data, 9);
        this.flags = (BuyFlags) BitConverter.ToUInt16(data, 11);
        this.cost = BitConverter.ToUInt32(data, 13);
    }

    public BuyItem(uint dealerUID, uint itemUID, uint cost, BuyFlags flags) : base(Build(dealerUID, itemUID, cost, flags))
    {
        this.dealerUID = dealerUID;
        this.itemUID = itemUID;
        this.cost = cost;
        this.tradeType = TradeType.BuyItem;
        this.flags = flags;
    }

    public static byte[] Build(uint dealerUID, uint itemUID, uint cost, BuyFlags flags)
    {
        byte[] buffer = new byte[0x11];
        buffer[0] = 50;
        buffer[1] = (byte) dealerUID;
        buffer[2] = (byte) (dealerUID >> 8);
        buffer[3] = (byte) (dealerUID >> 0x10);
        buffer[4] = (byte) (dealerUID >> 0x18);
        buffer[5] = (byte) itemUID;
        buffer[6] = (byte) (itemUID >> 8);
        buffer[7] = (byte) (itemUID >> 0x10);
        buffer[8] = (byte) (itemUID >> 0x18);
        buffer[11] = (byte) flags;
        buffer[12] = (byte) (((ushort) flags) >> 8);
        buffer[13] = (byte) cost;
        buffer[14] = (byte) (cost >> 8);
        buffer[15] = (byte) (cost >> 0x10);
        buffer[0x10] = (byte) (cost >> 0x18);
        return buffer;
    }

    // Properties
    public uint Cost
    {
        get
        {
            return this.cost;
        }
    }

    public uint DealerUID
    {
        get
        {
            return this.dealerUID;
        }
    }

    public BuyFlags Flags
    {
        get
        {
            return this.flags;
        }
    }

    public uint ItemUID
    {
        get
        {
            return this.itemUID;
        }
    }

    public TradeType TradeType
    {
        get
        {
            return this.tradeType;
        }
    }
}

public class CainIdentifyItems : GCPacket
{
    // Fields
    protected uint uid;

    // Methods
    public CainIdentifyItems(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
    }

    public CainIdentifyItems(uint uid) : base(Build(uid))
    {
        this.uid = uid;
    }

    public static byte[] Build(uint uid)
    {
        return new byte[] { 0x34, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)) };
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

public class CastLeftSkill : GCPacket
{
    // Fields
    protected ushort x;
    protected ushort y;

    // Methods
    public CastLeftSkill(byte[] data) : base(data)
    {
        this.x = BitConverter.ToUInt16(data, 1);
        this.y = BitConverter.ToUInt16(data, 3);
    }

    public CastLeftSkill(ushort x, ushort y) : base(Build(x, y))
    {
        this.x = x;
        this.y = y;
    }

    public static byte[] Build(ushort x, ushort y)
    {
        return new byte[] { 5, ((byte) x), ((byte) (x >> 8)), ((byte) y), ((byte) (y >> 8)) };
    }

    // Properties
    public ushort X
    {
        get
        {
            return this.x;
        }
    }

    public ushort Y
    {
        get
        {
            return this.y;
        }
    }
}

public class CastLeftSkillOnTarget : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public CastLeftSkillOnTarget(byte[] data) : base(data)
    {
        this.unitType = (UnitType) ((byte) BitConverter.ToUInt32(data, 1));
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public CastLeftSkillOnTarget(UnitType unitType, uint uid) : base(Build(unitType, uid))
    {
        this.unitType = unitType;
        this.uid = uid;
    }

    public static byte[] Build(UnitType unitType, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 6;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
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

public class CastLeftSkillOnTargetStopped : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public CastLeftSkillOnTargetStopped(byte[] data) : base(data)
    {
        this.unitType = (UnitType) ((byte) BitConverter.ToUInt32(data, 1));
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public CastLeftSkillOnTargetStopped(UnitType unitType, uint uid) : base(Build(unitType, uid))
    {
        this.unitType = unitType;
        this.uid = uid;
    }

    public static byte[] Build(UnitType unitType, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 7;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
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

public class CastRightSkill : GCPacket
{
    // Fields
    protected ushort x;
    protected ushort y;

    // Methods
    public CastRightSkill(byte[] data) : base(data)
    {
        this.x = BitConverter.ToUInt16(data, 1);
        this.y = BitConverter.ToUInt16(data, 3);
    }

    public CastRightSkill(ushort x, ushort y) : base(Build(x, y))
    {
        this.x = x;
        this.y = y;
    }

    public static byte[] Build(ushort x, ushort y)
    {
        return new byte[] { 12, ((byte) x), ((byte) (x >> 8)), ((byte) y), ((byte) (y >> 8)) };
    }

    // Properties
    public ushort X
    {
        get
        {
            return this.x;
        }
    }

    public ushort Y
    {
        get
        {
            return this.y;
        }
    }
}

public class CastRightSkillOnTarget : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public CastRightSkillOnTarget(byte[] data) : base(data)
    {
        this.unitType = (UnitType) ((byte) BitConverter.ToUInt32(data, 1));
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public CastRightSkillOnTarget(UnitType unitType, uint uid) : base(Build(unitType, uid))
    {
        this.unitType = unitType;
        this.uid = uid;
    }

    public static byte[] Build(UnitType unitType, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 13;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
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

public class CastRightSkillOnTargetStopped : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public CastRightSkillOnTargetStopped(byte[] data) : base(data)
    {
        this.unitType = (UnitType) ((byte) BitConverter.ToUInt32(data, 1));
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public CastRightSkillOnTargetStopped(UnitType unitType, uint uid) : base(Build(unitType, uid))
    {
        this.unitType = unitType;
        this.uid = uid;
    }

    public static byte[] Build(UnitType unitType, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 14;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
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

public class ChangeMercEquipment : GCPacket
{
    // Fields
    protected EquipmentLocation location;
    protected bool unequip;

    // Methods
    public ChangeMercEquipment(byte[] data) : base(data)
    {
        this.location = (EquipmentLocation) BitConverter.ToUInt16(data, 1);
        if (this.location != EquipmentLocation.NotApplicable)
        {
            this.unequip = true;
        }
    }

    public ChangeMercEquipment(EquipmentLocation location) : base(Build(location))
    {
        this.location = location;
        if (location != EquipmentLocation.NotApplicable)
        {
            this.unequip = true;
        }
    }

    public static byte[] Build(EquipmentLocation location)
    {
        return new byte[] { 0x61, ((byte) location), ((byte) (((ushort) location) >> 8)) };
    }

    // Properties
    public EquipmentLocation Location
    {
        get
        {
            return this.location;
        }
    }

    public bool Unequip
    {
        get
        {
            return this.unequip;
        }
    }
}

public class ClickButton : GCPacket
{
    // Fields
    protected GameButton button;
    protected ushort complement;

    // Methods
    public ClickButton(byte[] data) : base(data)
    {
        this.button = (GameButton) BitConverter.ToUInt32(data, 1);
        this.complement = BitConverter.ToUInt16(data, 5);
    }

    public ClickButton(GameButton button, ushort complement) : base(Build(button, complement))
    {
        this.button = button;
        this.complement = complement;
    }

    public static byte[] Build(GameButton button, ushort complement)
    {
        return new byte[] { 0x4f, ((byte) button), ((byte) (((int) button) >> 8)), ((byte) (((int) button) >> 0x10)), ((byte) (((int) button) >> 0x18)), ((byte) complement), ((byte) (complement >> 8)) };
    }

    // Properties
    public GameButton Button
    {
        get
        {
            return this.button;
        }
    }

    public ushort Complement
    {
        get
        {
            return this.complement;
        }
    }
}

public class CloseQuest : GCPacket
{
    // Fields
    protected QuestType quest;

    // Methods
    public CloseQuest(byte[] data) : base(data)
    {
        this.quest = (QuestType) BitConverter.ToUInt16(data, 1);
    }

    public CloseQuest(QuestType quest) : base(Build(quest))
    {
        this.quest = quest;
    }

    public static byte[] Build(QuestType quest)
    {
        return new byte[] { 0x58, ((byte) quest), ((byte) (((ushort) quest) >> 8)) };
    }

    // Properties
    public QuestType Quest
    {
        get
        {
            return this.quest;
        }
    }
}

public class DisplayQuestMessage : GCPacket
{
    // Fields
    protected uint message;
    protected uint uid;

    // Methods
    public DisplayQuestMessage(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
        this.message = BitConverter.ToUInt32(data, 5);
    }

    public DisplayQuestMessage(uint uid, uint message) : base(Build(uid, message))
    {
        this.uid = uid;
        this.message = message;
    }

    public static byte[] Build(uint uid, uint message)
    {
        return new byte[] { 0x31, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)), ((byte) message), ((byte) (message >> 8)), ((byte) (message >> 0x10)), ((byte) (message >> 0x18)) };
    }

    // Properties
    public uint Message
    {
        get
        {
            return this.message;
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

public class DropGold : GCPacket
{
    // Fields
    protected uint amount;
    protected uint meUID;

    // Methods
    public DropGold(byte[] data) : base(data)
    {
        this.meUID = BitConverter.ToUInt32(data, 1);
        this.amount = BitConverter.ToUInt32(data, 5);
    }

    public DropGold(uint amount, uint meUID) : base(Build(amount, meUID))
    {
        this.amount = amount;
        this.meUID = meUID;
    }

    public static byte[] Build(uint amount, uint meUID)
    {
        return new byte[] { 80, ((byte) meUID), ((byte) (meUID >> 8)), ((byte) (meUID >> 0x10)), ((byte) (meUID >> 0x18)), ((byte) amount), ((byte) (amount >> 8)), ((byte) (amount >> 0x10)), ((byte) (amount >> 0x18)) };
    }

    // Properties
    public uint Amount
    {
        get
        {
            return this.amount;
        }
    }

    public uint MeUID
    {
        get
        {
            return this.meUID;
        }
    }
}

public class DropItem : GCPacket
{
    // Fields
    protected uint uid;

    // Methods
    public DropItem(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
    }

    public DropItem(uint uid) : base(Build(uid))
    {
        this.uid = uid;
    }

    public static byte[] Build(uint uid)
    {
        return new byte[] { 0x17, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)) };
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

public class DropItemToContainer : GCPacket
{
    // Fields
    protected ItemContainerGC container;
    protected uint uid;
    protected ushort x;
    protected ushort y;

    // Methods
    public DropItemToContainer(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
        this.x = data[5];
        this.y = data[9];
        this.container = (ItemContainerGC) data[13];
    }

    public DropItemToContainer(uint uid, ItemContainerGC container, ushort x, ushort y) : base(Build(uid, container, x, y))
    {
        this.uid = uid;
        this.container = container;
        this.x = x;
        this.y = y;
    }

    public static byte[] Build(uint uid, ItemContainerGC container, ushort x, ushort y)
    {
        byte[] buffer = new byte[0x11];
        buffer[0] = 0x18;
        buffer[1] = (byte) uid;
        buffer[2] = (byte) (uid >> 8);
        buffer[3] = (byte) (uid >> 0x10);
        buffer[4] = (byte) (uid >> 0x18);
        buffer[5] = (byte) x;
        buffer[9] = (byte) y;
        buffer[13] = (byte) container;
        return buffer;
    }

    // Properties
    public ItemContainerGC Container
    {
        get
        {
            return this.container;
        }
    }

    public uint UID
    {
        get
        {
            return this.uid;
        }
    }

    public ushort X
    {
        get
        {
            return this.x;
        }
    }

    public ushort Y
    {
        get
        {
            return this.y;
        }
    }
}

public class EmbedItem : GCPacket
{
    // Fields
    protected uint objectUID;
    protected uint subjectUID;

    // Methods
    public EmbedItem(byte[] data) : base(data)
    {
        this.subjectUID = BitConverter.ToUInt32(data, 1);
        this.objectUID = BitConverter.ToUInt32(data, 5);
    }

    public EmbedItem(uint subjectUID, uint objectUID) : base(Build(subjectUID, objectUID))
    {
        this.subjectUID = subjectUID;
        this.objectUID = objectUID;
    }

    public static byte[] Build(uint subjectUID, uint objectUID)
    {
        return new byte[] { 0x29, ((byte) subjectUID), ((byte) (subjectUID >> 8)), ((byte) (subjectUID >> 0x10)), ((byte) (subjectUID >> 0x18)), ((byte) objectUID), ((byte) (objectUID >> 8)), ((byte) (objectUID >> 0x10)), ((byte) (objectUID >> 0x18)) };
    }

    // Properties
    public uint ObjectUID
    {
        get
        {
            return this.objectUID;
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

public class EnterGame : GCPacket
{
    // Methods
    public EnterGame() : base(Build())
    {
    }

    public EnterGame(byte[] data) : base(data)
    {
    }

    public static byte[] Build()
    {
        return new byte[] { 0x6b };
    }
}

public class EquipItem : GCPacket
{
    // Fields
    protected EquipmentLocation location;
    protected uint uid;

    // Methods
    public EquipItem(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
        this.location = (EquipmentLocation) data[5];
    }

    public EquipItem(uint uid, EquipmentLocation location) : base(Build(uid, location))
    {
        this.uid = uid;
        this.location = location;
    }

    public static byte[] Build(uint uid, EquipmentLocation location)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 0x1a;
        buffer[1] = (byte) uid;
        buffer[2] = (byte) (uid >> 8);
        buffer[3] = (byte) (uid >> 0x10);
        buffer[4] = (byte) (uid >> 0x18);
        buffer[5] = (byte) location;
        return buffer;
    }

    // Properties
    public EquipmentLocation Location
    {
        get
        {
            return this.location;
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

public class ExitGame : GCPacket
{
    // Methods
    public ExitGame() : base(EnterGame.Build())
    {
    }

    public ExitGame(byte[] data) : base(data)
    {
    }

    public static byte[] Build()
    {
        return new byte[] { 0x69 };
    }
}

public class GameLogonRequest : GCPacket
{
    // Fields
    protected CharacterClass charClass;
    protected uint d2GShash;
    protected ushort d2GSToken;
    protected string name;
    protected uint version;

    // Methods
    public GameLogonRequest(byte[] data) : base(data)
    {
        this.d2GShash = BitConverter.ToUInt32(data, 1);
        this.d2GSToken = BitConverter.ToUInt16(data, 5);
        this.charClass = (CharacterClass) data[7];
        this.version = BitConverter.ToUInt32(data, 8);
        this.name = ByteConverter.GetNullString(data, 0x15);
    }

    // Properties
    public CharacterClass Class
    {
        get
        {
            return this.charClass;
        }
    }

    public uint D2GShash
    {
        get
        {
            return this.d2GShash;
        }
    }

    public ushort D2GSToken
    {
        get
        {
            return this.d2GSToken;
        }
    }

    public string Name
    {
        get
        {
            return this.name;
        }
    }

    public string Unknown12
    {
        get
        {
            return ByteConverter.ToHexString(base.Data, 12, 9);
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

public class GCPacket : D2Packet
{
    // Fields
    public readonly GameClientPacket PacketID;

    // Methods
    public GCPacket(byte[] data) : base(data, Origin.GameClient)
    {
        this.PacketID = (GameClientPacket) data[0];
    }
}

public class GoToLocation : GCPacket
{
    // Fields
    protected ushort x;
    protected ushort y;

    // Methods
    public GoToLocation(byte[] data) : base(data)
    {
        this.x = BitConverter.ToUInt16(data, 1);
        this.y = BitConverter.ToUInt16(data, 3);
    }

    // Properties
    public ushort X
    {
        get
        {
            return this.x;
        }
    }

    public ushort Y
    {
        get
        {
            return this.y;
        }
    }
}

    public class GoToTarget : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public GoToTarget(byte[] data) : base(data)
    {
        this.unitType = (UnitType) ((byte) BitConverter.ToUInt32(data, 1));
        this.uid = BitConverter.ToUInt32(data, 5);
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
 
    public class GoToTownFolk : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;
    protected uint x;
    protected uint y;

    // Methods
    public GoToTownFolk(byte[] data) : base(data)
    {
        this.unitType = (UnitType) ((byte) BitConverter.ToUInt32(data, 1));
        this.uid = BitConverter.ToUInt32(data, 5);
        this.x = BitConverter.ToUInt32(data, 9);
        this.y = BitConverter.ToUInt32(data, 13);
    }

    public GoToTownFolk(UnitType unitType, uint uid, uint x, uint y) : base(Build(unitType, uid, x, y))
    {
        this.unitType = unitType;
        this.uid = uid;
        this.x = x;
        this.y = y;
    }

    public static byte[] Build(UnitType unitType, uint uid, uint x, uint y)
    {
        byte[] buffer = new byte[0x11];
        buffer[0] = 0x59;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        buffer[9] = (byte) x;
        buffer[10] = (byte) (x >> 8);
        buffer[11] = (byte) (x >> 0x10);
        buffer[12] = (byte) (x >> 0x18);
        buffer[13] = (byte) y;
        buffer[14] = (byte) (y >> 8);
        buffer[15] = (byte) (y >> 0x10);
        buffer[0x10] = (byte) (y >> 0x18);
        return buffer;
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

    public uint X
    {
        get
        {
            return this.x;
        }
    }

    public uint Y
    {
        get
        {
            return this.y;
        }
    }
}

    public class HireMercenary : GCPacket
{
    // Fields
    protected uint dealerUID;
    protected uint mercID;

    // Methods
    public HireMercenary(byte[] data) : base(data)
    {
        this.dealerUID = BitConverter.ToUInt32(data, 1);
        this.mercID = BitConverter.ToUInt32(data, 5);
    }

    public HireMercenary(uint dealerUID, uint mercID) : base(Build(dealerUID, mercID))
    {
        this.dealerUID = dealerUID;
        this.mercID = mercID;
    }

    public static byte[] Build(uint dealerUID, uint mercID)
    {
        return new byte[] { 0x36, ((byte) dealerUID), ((byte) (dealerUID >> 8)), ((byte) (dealerUID >> 0x10)), ((byte) (dealerUID >> 0x18)), ((byte) mercID), ((byte) (mercID >> 8)), ((byte) (mercID >> 0x10)), ((byte) (mercID >> 0x18)) };
    }

    // Properties
    public uint DealerUID
    {
        get
        {
            return this.dealerUID;
        }
    }

    public uint MercID
    {
        get
        {
            return this.mercID;
        }
    }
}

    public class HoverUnit : GCPacket
{
    // Fields
    protected uint uid;

    // Methods
    public HoverUnit(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
    }

    public HoverUnit(uint uid) : base(Build(uid))
    {
        this.uid = uid;
    }

    public static byte[] Build(uint uid)
    {
        return new byte[] { 0x3d, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)) };
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

    public class IdentifyGambleItem : GCPacket
{
    // Fields
    protected uint uid;

    // Methods
    public IdentifyGambleItem(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
    }

    public IdentifyGambleItem(uint uid) : base(Build(uid))
    {
        this.uid = uid;
    }

    public static byte[] Build(uint uid)
    {
        return new byte[] { 0x37, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)) };
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

    public class IdentifyItem : GCPacket
{
    // Fields
    protected uint itemUID;
    protected uint scrollUID;

    // Methods
    public IdentifyItem(byte[] data) : base(data)
    {
        this.itemUID = BitConverter.ToUInt32(data, 1);
        this.scrollUID = BitConverter.ToUInt32(data, 5);
    }

    public IdentifyItem(uint itemUID, uint scrollUID) : base(Build(itemUID, scrollUID))
    {
        this.itemUID = itemUID;
        this.scrollUID = scrollUID;
    }

    public static byte[] Build(uint itemUID, uint scrollUID)
    {
        return new byte[] { 0x27, ((byte) itemUID), ((byte) (itemUID >> 8)), ((byte) (itemUID >> 0x10)), ((byte) (itemUID >> 0x18)), ((byte) scrollUID), ((byte) (scrollUID >> 8)), ((byte) (scrollUID >> 0x10)), ((byte) (scrollUID >> 0x18)) };
    }

    // Properties
    public uint ItemUID
    {
        get
        {
            return this.itemUID;
        }
    }

    public uint ScrollUID
    {
        get
        {
            return this.scrollUID;
        }
    }
}

    public class IncrementAttribute : GCPacket
{
    // Fields
    protected StatType attribute;

    // Methods
    public IncrementAttribute(byte[] data) : base(data)
    {
        this.attribute = (StatType) BitConverter.ToUInt16(data, 1);
    }

    public IncrementAttribute(StatType attribute) : base(Build(attribute))
    {
        this.attribute = attribute;
    }

    public static byte[] Build(StatType attribute)
    {
        return new byte[] { 0x3a, ((byte) attribute), ((byte) (((ushort) attribute) >> 8)) };
    }

    // Properties
    public StatType Attribute
    {
        get
        {
            return this.attribute;
        }
    }
}

    public class IncrementSkill : GCPacket
{
    // Fields
    protected SkillType skill;

    // Methods
    public IncrementSkill(byte[] data) : base(data)
    {
        this.skill = (SkillType) BitConverter.ToUInt16(data, 1);
    }

    public IncrementSkill(SkillType skill) : base(Build(skill))
    {
        this.skill = skill;
    }

    public static byte[] Build(SkillType skill)
    {
        return new byte[] { 0x3b, ((byte) skill), ((byte) (((ushort) skill) >> 8)) };
    }

    // Properties
    public SkillType Skill
    {
        get
        {
            return this.skill;
        }
    }
}

    public class InventoryItemToBelt : GCPacket
{
    // Fields
    protected uint uid;

    // Methods
    public InventoryItemToBelt(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
    }

    public InventoryItemToBelt(uint uid) : base(Build(uid))
    {
        this.uid = uid;
    }

    public static byte[] Build(uint uid)
    {
        return new byte[] { 0x63, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)) };
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

    public enum ItemContainerGC
    {
        Cube = 3,
        Inventory = 0,
        Stash = 4,
        Trade = 2
    }

public class ItemToCube : GCPacket
{
    // Fields
    protected uint cubeUID;
    protected uint itemUID;

    // Methods
    public ItemToCube(byte[] data) : base(data)
    {
        this.itemUID = BitConverter.ToUInt32(data, 1);
        this.cubeUID = BitConverter.ToUInt32(data, 5);
    }

    public ItemToCube(uint itemUID, uint cubeUID) : base(Build(itemUID, cubeUID))
    {
        this.itemUID = itemUID;
        this.cubeUID = cubeUID;
    }

    public static byte[] Build(uint itemUID, uint cubeUID)
    {
        return new byte[] { 0x2a, ((byte) itemUID), ((byte) (itemUID >> 8)), ((byte) (itemUID >> 0x10)), ((byte) (itemUID >> 0x18)), ((byte) cubeUID), ((byte) (cubeUID >> 8)), ((byte) (cubeUID >> 0x10)), ((byte) (cubeUID >> 0x18)) };
    }

    // Properties
    public uint CubeUID
    {
        get
        {
            return this.cubeUID;
        }
    }

    public uint ItemUID
    {
        get
        {
            return this.itemUID;
        }
    }
}

public enum PartyAction
{
    AcceptInvite = 8,
    CancelInvite = 7,
    Invite = 6
}

public class PartyRequest : GCPacket
{
    // Fields
    protected PartyAction action;
    protected uint playerUID;

    // Methods
    public PartyRequest(byte[] data) : base(data)
    {
        this.action = (PartyAction) data[1];
        this.playerUID = BitConverter.ToUInt32(data, 2);
    }

    public PartyRequest(PartyAction action, uint playerUID) : base(Build(action, playerUID))
    {
        this.action = action;
        this.playerUID = playerUID;
    }

    public static byte[] Build(PartyAction action, uint playerUID)
    {
        return new byte[] { 0x5e, ((byte) action), ((byte) playerUID), ((byte) (playerUID >> 8)), ((byte) (playerUID >> 0x10)), ((byte) (playerUID >> 0x18)) };
    }

    // Properties
    public PartyAction Action
    {
        get
        {
            return this.action;
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

public class PickItem : GCPacket
{
    // Fields
    protected uint requestID;
    protected bool toCursor;
    protected uint uid;

    // Methods
    public PickItem(byte[] data) : base(data)
    {
        this.requestID = BitConverter.ToUInt32(data, 1);
        this.uid = BitConverter.ToUInt32(data, 5);
        if (data[9] == 1)
        {
            this.toCursor = true;
        }
    }

    public PickItem(uint uid, bool toCursor, uint requestID) : base(Build(uid, toCursor, requestID))
    {
        this.uid = uid;
        this.toCursor = toCursor;
        this.requestID = requestID;
    }

    public static byte[] Build(uint uid, bool toCursor, uint requestID)
    {
        byte[] buffer = new byte[13];
        buffer[0] = 0x16;
        buffer[1] = (byte) requestID;
        buffer[2] = (byte) (requestID >> 8);
        buffer[3] = (byte) (requestID >> 0x10);
        buffer[4] = (byte) (requestID >> 0x18);
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        buffer[9] = toCursor ? ((byte) 1) : ((byte) 0);
        return buffer;
    }

    // Properties
    public uint RequestID
    {
        get
        {
            return this.requestID;
        }
    }

    public bool ToCursor
    {
        get
        {
            return this.toCursor;
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

public class PickItemFromContainer : GCPacket
{
    // Fields
    protected uint uid;

    // Methods
    public PickItemFromContainer(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
    }

    public PickItemFromContainer(uint uid) : base(Build(uid))
    {
        this.uid = uid;
    }

    public static byte[] Build(uint uid)
    {
        return new byte[] { 0x19, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)) };
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
 
public class Ping : GCPacket
{
    // Fields
    protected uint tickCount;

    // Methods
    public Ping(byte[] data) : base(data)
    {
        this.tickCount = BitConverter.ToUInt32(data, 1);
    }

    public Ping(uint tickCount, long unknown5) : base(Build(tickCount, unknown5))
    {
        this.tickCount = tickCount;
    }

    public static byte[] Build(uint tickCount, long unknown5)
    {
        return new byte[] { 0x6d, ((byte) tickCount), ((byte) (tickCount >> 8)), ((byte) (tickCount >> 0x10)), ((byte) (tickCount >> 0x18)), ((byte) unknown5), ((byte) (unknown5 >> 8)), ((byte) (unknown5 >> 0x10)), ((byte) (unknown5 >> 0x18)), ((byte) (unknown5 >> 0x20)), ((byte) (unknown5 >> 40)), ((byte) (unknown5 >> 0x30)), ((byte) (unknown5 >> 0x38)) };
    }

    // Properties
    public uint TickCount
    {
        get
        {
            return this.tickCount;
        }
    }

    public string Unknown5
    {
        get
        {
            return ByteConverter.ToHexString(base.Data, 5, 8);
        }
    }
}

public enum PlayerRelationType
{
    Hostile = 4,
    Loot = 1,
    Mute = 2,
    Squelch = 3
}

public class RecastLeftSkill : GCPacket
{
    // Fields
    protected ushort x;
    protected ushort y;

    // Methods
    public RecastLeftSkill(byte[] data) : base(data)
    {
        this.x = BitConverter.ToUInt16(data, 1);
        this.y = BitConverter.ToUInt16(data, 3);
    }

    public RecastLeftSkill(ushort x, ushort y) : base(Build(x, y))
    {
        this.x = x;
        this.y = y;
    }

    public static byte[] Build(ushort x, ushort y)
    {
        return new byte[] { 8, ((byte) x), ((byte) (x >> 8)), ((byte) y), ((byte) (y >> 8)) };
    }

    // Properties
    public ushort X
    {
        get
        {
            return this.x;
        }
    }

    public ushort Y
    {
        get
        {
            return this.y;
        }
    }
}

 

 
public class RecastLeftSkillOnTarget : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public RecastLeftSkillOnTarget(byte[] data) : base(data)
    {
        this.unitType = (UnitType) ((byte) BitConverter.ToUInt32(data, 1));
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public RecastLeftSkillOnTarget(UnitType unitType, uint uid) : base(Build(unitType, uid))
    {
        this.unitType = unitType;
        this.uid = uid;
    }

    public static byte[] Build(UnitType unitType, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 9;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
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

 

 
public class RecastLeftSkillOnTargetStopped : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public RecastLeftSkillOnTargetStopped(byte[] data) : base(data)
    {
        this.unitType = (UnitType) ((byte) BitConverter.ToUInt32(data, 1));
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public RecastLeftSkillOnTargetStopped(UnitType unitType, uint uid) : base(Build(unitType, uid))
    {
        this.unitType = unitType;
        this.uid = uid;
    }

    public static byte[] Build(UnitType unitType, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 10;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
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

 

 
public class RecastRightSkill : GCPacket
{
    // Fields
    protected ushort x;
    protected ushort y;

    // Methods
    public RecastRightSkill(byte[] data) : base(data)
    {
        this.x = BitConverter.ToUInt16(data, 1);
        this.y = BitConverter.ToUInt16(data, 3);
    }

    public RecastRightSkill(ushort x, ushort y) : base(Build(x, y))
    {
        this.x = x;
        this.y = y;
    }

    public static byte[] Build(ushort x, ushort y)
    {
        return new byte[] { 15, ((byte) x), ((byte) (x >> 8)), ((byte) y), ((byte) (y >> 8)) };
    }

    // Properties
    public ushort X
    {
        get
        {
            return this.x;
        }
    }

    public ushort Y
    {
        get
        {
            return this.y;
        }
    }
}

 

 
public class RecastRightSkillOnTarget : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public RecastRightSkillOnTarget(byte[] data) : base(data)
    {
        this.unitType = (UnitType) ((byte) BitConverter.ToUInt32(data, 1));
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public RecastRightSkillOnTarget(UnitType unitType, uint uid) : base(Build(unitType, uid))
    {
        this.unitType = unitType;
        this.uid = uid;
    }

    public static byte[] Build(UnitType unitType, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 0x10;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
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

 

 
public class RecastRightSkillOnTargetStopped : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public RecastRightSkillOnTargetStopped(byte[] data) : base(data)
    {
        this.unitType = (UnitType) ((byte) BitConverter.ToUInt32(data, 1));
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public RecastRightSkillOnTargetStopped(UnitType unitType, uint uid) : base(Build(unitType, uid))
    {
        this.unitType = unitType;
        this.uid = uid;
    }

    public static byte[] Build(UnitType target, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 0x11;
        buffer[1] = (byte) target;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
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

 

 
public class RemoveBeltItem : GCPacket
{
    // Fields
    protected uint uid;

    // Methods
    public RemoveBeltItem(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
    }

    public RemoveBeltItem(uint uid) : base(Build(uid))
    {
        this.uid = uid;
    }

    public static byte[] Build(uint uid)
    {
        return new byte[] { 0x24, ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)) };
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

 

 
public enum RepairType
{
    RepairAll,
    RepairItem
}

 

 
public class RequestQuestLog : GCPacket
{
    // Methods
    public RequestQuestLog() : base(Build())
    {
    }

    public RequestQuestLog(byte[] data) : base(data)
    {
    }

    public static byte[] Build()
    {
        return new byte[] { 0x40 };
    }
}

 

 
public class RequestReassign : GCPacket
{
    // Fields
    protected uint meUID;
    protected UnitType unitType;

    // Methods
    public RequestReassign(byte[] data) : base(data)
    {
        this.unitType = (UnitType) data[1];
        this.meUID = BitConverter.ToUInt32(data, 5);
    }

    public RequestReassign(UnitType unitType, uint meUID) : base(Build(unitType, meUID))
    {
        this.unitType = unitType;
        this.meUID = meUID;
    }

    public static byte[] Build(UnitType unitType, uint meUID)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 0x4b;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) meUID;
        buffer[6] = (byte) (meUID >> 8);
        buffer[7] = (byte) (meUID >> 0x10);
        buffer[8] = (byte) (meUID >> 0x18);
        return buffer;
    }

    // Properties
    public uint MeUID
    {
        get
        {
            return this.meUID;
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

 

 
public class Respawn : GCPacket
{
    // Methods
    public Respawn() : base(Build())
    {
    }

    public Respawn(byte[] data) : base(data)
    {
    }

    public static byte[] Build()
    {
        return new byte[] { 0x41 };
    }
}

 

 
public class ResurrectMerc : GCPacket
{
    // Fields
    protected uint dealerUID;

    // Methods
    public ResurrectMerc(byte[] data) : base(data)
    {
        this.dealerUID = BitConverter.ToUInt32(data, 1);
    }

    public ResurrectMerc(uint dealerUID) : base(Build(dealerUID))
    {
        this.dealerUID = dealerUID;
    }

    public static byte[] Build(uint dealerUID)
    {
        return new byte[] { 0x62, ((byte) dealerUID), ((byte) (dealerUID >> 8)), ((byte) (dealerUID >> 0x10)), ((byte) (dealerUID >> 0x18)) };
    }

    // Properties
    public uint DealerUID
    {
        get
        {
            return this.dealerUID;
        }
    }
}

 

 
public class RunToLocation : GoToLocation
{
    // Fields
    public static readonly bool WRAPPED = true;

    // Methods
    public RunToLocation(byte[] data) : base(data)
    {
    }

    public RunToLocation(ushort x, ushort y) : base(Build(x, y))
    {
    }

    public static byte[] Build(ushort x, ushort y)
    {
        return new byte[] { 3, ((byte) x), ((byte) (x >> 8)), ((byte) y), ((byte) (y >> 8)) };
    }
}

 

 
public class RunToTarget : GoToTarget
{
    // Fields
    public static readonly bool WRAPPED = true;

    // Methods
    public RunToTarget(byte[] data) : base(data)
    {
    }

    public RunToTarget(UnitType target, uint uid) : base(Build(target, uid))
    {
    }

    public static byte[] Build(UnitType target, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 4;
        buffer[1] = (byte) target;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
    }
}

 

 
public class SelectSkill : GCPacket
{
    // Fields
    protected uint chargedItemUID;
    protected SkillHand hand;
    public static readonly uint NULL_UInt32;
    protected SkillType skill;

    // Methods
    public SelectSkill(byte[] data) : base(data)
    {
        this.skill = (SkillType) BitConverter.ToUInt16(data, 1);
        if (data[4] == 0x80)
        {
            this.hand = SkillHand.Left;
        }
        this.chargedItemUID = BitConverter.ToUInt32(data, 5);
        if (this.chargedItemUID == uint.MaxValue)
        {
            this.chargedItemUID = 0;
        }
    }

    public SelectSkill(SkillType skill, SkillHand hand) : base(Build(skill, hand))
    {
        this.skill = skill;
        this.hand = hand;
        this.chargedItemUID = 0;
    }

    public SelectSkill(SkillType skill, SkillHand hand, uint chargedItemUID) : base(Build(skill, hand, chargedItemUID))
    {
        this.skill = skill;
        this.hand = hand;
        this.chargedItemUID = chargedItemUID;
    }

    public static byte[] Build(SkillType skill, SkillHand hand)
    {
        return Build(skill, hand, uint.MaxValue);
    }

    public static byte[] Build(SkillType skill, SkillHand hand, uint chargedItemUID)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 60;
        buffer[1] = (byte) skill;
        buffer[2] = (byte) (((ushort) skill) >> 8);
        buffer[4] = (hand == SkillHand.Left) ? ((byte) 0x80) : ((byte) 0);
        buffer[5] = (byte) chargedItemUID;
        buffer[6] = (byte) (chargedItemUID >> 8);
        buffer[7] = (byte) (chargedItemUID >> 0x10);
        buffer[8] = (byte) (chargedItemUID >> 0x18);
        return buffer;
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
}

 

 
public class SellItem : GCPacket
{
    // Fields
    protected uint cost;
    protected uint dealerUID;
    protected uint itemUID;
    protected TradeType tradeType;

    // Methods
    public SellItem(byte[] data) : base(data)
    {
        this.dealerUID = BitConverter.ToUInt32(data, 1);
        this.itemUID = BitConverter.ToUInt32(data, 5);
        this.tradeType = (TradeType) ((ushort) BitConverter.ToUInt32(data, 9));
        this.cost = BitConverter.ToUInt32(data, 13);
    }

    public SellItem(uint dealerUID, uint itemUID, uint cost) : base(Build(dealerUID, itemUID, cost))
    {
        this.dealerUID = dealerUID;
        this.itemUID = itemUID;
        this.cost = cost;
        this.tradeType = TradeType.SellItem;
    }

    public static byte[] Build(uint dealerUID, uint itemUID, uint cost)
    {
        byte[] buffer = new byte[0x11];
        buffer[0] = 0x33;
        buffer[1] = (byte) dealerUID;
        buffer[2] = (byte) (dealerUID >> 8);
        buffer[3] = (byte) (dealerUID >> 0x10);
        buffer[4] = (byte) (dealerUID >> 0x18);
        buffer[5] = (byte) itemUID;
        buffer[6] = (byte) (itemUID >> 8);
        buffer[7] = (byte) (itemUID >> 0x10);
        buffer[8] = (byte) (itemUID >> 0x18);
        buffer[9] = 4;
        buffer[13] = (byte) cost;
        buffer[14] = (byte) (cost >> 8);
        buffer[15] = (byte) (cost >> 0x10);
        buffer[0x10] = (byte) (cost >> 0x18);
        return buffer;
    }

    // Properties
    public uint Cost
    {
        get
        {
            return this.cost;
        }
    }

    public uint DealerUID
    {
        get
        {
            return this.dealerUID;
        }
    }

    public uint ItemUID
    {
        get
        {
            return this.itemUID;
        }
    }

    public TradeType TradeType
    {
        get
        {
            return this.tradeType;
        }
    }
}

 

 
public class SendCharacterSpeech : GCPacket
{
    // Fields
    protected GameSound speech;

    // Methods
    public SendCharacterSpeech(byte[] data) : base(data)
    {
        this.speech = (GameSound) BitConverter.ToUInt16(data, 1);
    }

    public SendCharacterSpeech(GameSound speech) : base(Build(speech))
    {
        this.speech = speech;
    }

    public static byte[] Build(GameSound speech)
    {
        return new byte[] { 0x3f, ((byte) speech), ((byte) (((ushort) speech) >> 8)) };
    }

    // Properties
    public GameSound Speech
    {
        get
        {
            return this.speech;
        }
    }
}

 

 
public class SendMessage : GCPacket
{
    // Fields
    protected string message;
    protected string recipient;
    protected GameMessageType type;

    // Methods
    public SendMessage(byte[] data) : base(data)
    {
        this.type = (GameMessageType) BitConverter.ToUInt16(data, 1);
        this.message = ByteConverter.GetNullString(data, 3);
        if (this.type == GameMessageType.GameWhisper)
        {
            this.recipient = ByteConverter.GetNullString(data, 4 + this.message.Length);
        }
    }

    public SendMessage(GameMessageType type, string message) : base(Build(type, message))
    {
        this.type = type;
        this.message = message;
    }

    public SendMessage(GameMessageType type, string message, string recipient) : base(Build(type, message, recipient))
    {
        this.type = type;
        this.message = message;
        this.recipient = recipient;
    }

    public static byte[] Build(GameMessageType type, string message)
    {
        return Build(type, message, null);
    }

    public static byte[] Build(GameMessageType type, string message, string recipient)
    {
        if ((message == null) || (message.Length == 0))
        {
            throw new ArgumentException();
        }
        int num = (recipient != null) ? recipient.Length : 0;
        byte[] buffer = new byte[(6 + message.Length) + num];
        buffer[0] = 0x15;
        buffer[1] = (byte) type;
        buffer[2] = (byte) (((byte) type) >> 8);
        for (int i = 0; i < message.Length; i++)
        {
            buffer[3 + i] = (byte) message[i];
        }
        if (num > 0)
        {
            num = 4 + message.Length;
            for (int j = 0; j < recipient.Length; j++)
            {
                buffer[num + j] = (byte) recipient[j];
            }
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

    public string Recipient
    {
        get
        {
            return this.recipient;
        }
    }

    public GameMessageType Type
    {
        get
        {
            return this.type;
        }
    }
}

 

 
public class SendOverheadMessage : GCPacket
{
    // Fields
    protected string message;

    // Methods
    public SendOverheadMessage(byte[] data) : base(data)
    {
        this.message = ByteConverter.GetNullString(data, 3);
    }

    public SendOverheadMessage(string message) : base(Build(message))
    {
        this.message = message;
    }

    public static byte[] Build(string message)
    {
        if ((message == null) || (message.Length == 0))
        {
            throw new ArgumentException("message");
        }
        byte[] buffer = new byte[6 + message.Length];
        buffer[0] = 20;
        for (int i = 0; i < message.Length; i++)
        {
            buffer[3 + i] = (byte) message[i];
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

    public string Unknown1
    {
        get
        {
            return ByteConverter.ToHexString(base.Data, 1, 2);
        }
    }

    public string UnknownEnd
    {
        get
        {
            return ByteConverter.ToHexString(base.Data, 4 + this.Message.Length, 2);
        }
    }
}

 

 
public class SetPlayerRelation : GCPacket
{
    // Fields
    protected PlayerRelationType relation;
    protected uint uid;
    protected bool value;

    // Methods
    public SetPlayerRelation(byte[] data) : base(data)
    {
        this.relation = (PlayerRelationType) data[1];
        this.value = BitConverter.ToBoolean(data, 2);
        this.uid = BitConverter.ToUInt32(data, 3);
    }

    public SetPlayerRelation(uint uid, PlayerRelationType relation, bool value) : base(Build(uid, relation, value))
    {
        this.uid = uid;
        this.relation = relation;
        this.value = value;
    }

    public static byte[] Build(uint uid, PlayerRelationType relation, bool value)
    {
        return new byte[] { 0x5d, ((byte) relation), (value ? ((byte) 1) : ((byte) 0)), ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)) };
    }

    // Properties
    public PlayerRelationType Relation
    {
        get
        {
            return this.relation;
        }
    }

    public uint UID
    {
        get
        {
            return this.uid;
        }
    }

    public bool Value
    {
        get
        {
            return this.value;
        }
    }
}

 

 
public class SetSkillHotkey : GCPacket
{
    // Fields
    protected uint chargedItemUID;
    public static readonly uint NULL_UInt32 = uint.MaxValue;
    protected SkillType skill;
    protected ushort slot;

    // Methods
    public SetSkillHotkey(byte[] data) : base(data)
    {
        this.skill = (SkillType) BitConverter.ToUInt16(data, 1);
        this.slot = BitConverter.ToUInt16(data, 3);
        this.chargedItemUID = BitConverter.ToUInt32(data, 5);
    }

    public SetSkillHotkey(ushort slot, SkillType skill) : this(slot, skill, uint.MaxValue)
    {
    }

    public SetSkillHotkey(ushort slot, SkillType skill, uint itemUID) : base(Build(slot, skill, itemUID))
    {
        this.slot = slot;
        this.skill = skill;
        this.chargedItemUID = itemUID;
    }

    public static byte[] Build(ushort slot, SkillType skill)
    {
        return Build(slot, skill, uint.MaxValue);
    }

    public static byte[] Build(ushort slot, SkillType skill, uint itemUID)
    {
        return new byte[] { 0x51, ((byte) skill), ((byte) (((ushort) skill) >> 8)), ((byte) slot), ((byte) (slot >> 8)), ((byte) itemUID), ((byte) (itemUID >> 8)), ((byte) (itemUID >> 0x10)), ((byte) (itemUID >> 0x18)) };
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

    public ushort Slot
    {
        get
        {
            return this.slot;
        }
    }
}

 

 
public class StackItems : GCPacket
{
    // Fields
    protected uint objectUID;
    protected uint subjectUID;

    // Methods
    public StackItems(byte[] data) : base(data)
    {
        this.subjectUID = BitConverter.ToUInt32(data, 1);
        this.objectUID = BitConverter.ToUInt32(data, 5);
    }

    public StackItems(uint subjectUID, uint objectUID) : base(Build(subjectUID, objectUID))
    {
        this.subjectUID = subjectUID;
        this.objectUID = objectUID;
    }

    public static byte[] Build(uint subjectUID, uint objectUID)
    {
        return new byte[] { 0x21, ((byte) subjectUID), ((byte) (subjectUID >> 8)), ((byte) (subjectUID >> 0x10)), ((byte) (subjectUID >> 0x18)), ((byte) objectUID), ((byte) (objectUID >> 8)), ((byte) (objectUID >> 0x10)), ((byte) (objectUID >> 0x18)) };
    }

    // Properties
    public uint ObjectUID
    {
        get
        {
            return this.objectUID;
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

 

 
public class SwapBeltItem : GCPacket
{
    // Fields
    protected readonly uint newItemUID;
    protected readonly uint oldItemUID;

    // Methods
    public SwapBeltItem(byte[] data) : base(data)
    {
        this.oldItemUID = BitConverter.ToUInt32(data, 1);
        this.newItemUID = BitConverter.ToUInt32(data, 5);
    }

    public SwapBeltItem(uint oldItemUID, uint newItemUID) : base(Build(oldItemUID, newItemUID))
    {
        this.oldItemUID = oldItemUID;
        this.newItemUID = newItemUID;
    }

    public static byte[] Build(uint oldItemUID, uint newItemUID)
    {
        return new byte[] { 0x25, ((byte) oldItemUID), ((byte) (oldItemUID >> 8)), ((byte) (oldItemUID >> 0x10)), ((byte) (oldItemUID >> 0x18)), ((byte) newItemUID), ((byte) (newItemUID >> 8)), ((byte) (newItemUID >> 0x10)), ((byte) (newItemUID >> 0x18)) };
    }

    // Properties
    public uint NewItemUID
    {
        get
        {
            return this.newItemUID;
        }
    }

    public uint OldItemUID
    {
        get
        {
            return this.oldItemUID;
        }
    }
}

 

 
public class SwapContainerItem : GCPacket
{
    // Fields
    protected uint objectUID;
    protected uint subjectUID;
    protected int x;
    protected int y;

    // Methods
    public SwapContainerItem(byte[] data) : base(data)
    {
        this.subjectUID = BitConverter.ToUInt32(data, 1);
        this.objectUID = BitConverter.ToUInt32(data, 5);
        this.x = BitConverter.ToInt32(data, 9);
        this.y = BitConverter.ToInt32(data, 13);
    }

    public SwapContainerItem(uint subjectUID, uint objectUID, int x, int y) : base(Build(subjectUID, objectUID, x, y))
    {
        this.subjectUID = subjectUID;
        this.objectUID = objectUID;
        this.x = x;
        this.y = y;
    }

    public static byte[] Build(uint subjectUID, uint objectUID, int x, int y)
    {
        return new byte[] { 
            0x1f, ((byte) subjectUID), ((byte) (subjectUID >> 8)), ((byte) (subjectUID >> 0x10)), ((byte) (subjectUID >> 0x18)), ((byte) objectUID), ((byte) (objectUID >> 8)), ((byte) (objectUID >> 0x10)), ((byte) (objectUID >> 0x18)), ((byte) x), ((byte) (x >> 8)), ((byte) (x >> 0x10)), ((byte) (x >> 0x18)), ((byte) y), ((byte) (y >> 8)), ((byte) (y >> 0x10)), 
            ((byte) (y >> 0x18))
         };
    }

    // Properties
    public uint ObjectUID
    {
        get
        {
            return this.objectUID;
        }
    }

    public uint SubjectUID
    {
        get
        {
            return this.subjectUID;
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

 

 
public class SwapEquippedItem : GCPacket
{
    // Fields
    protected EquipmentLocation location;
    protected uint uid;

    // Methods
    public SwapEquippedItem(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
        this.location = (EquipmentLocation) data[5];
    }

    public SwapEquippedItem(uint uid, EquipmentLocation location) : base(Build(uid, location))
    {
        this.uid = uid;
        this.location = location;
    }

    public static byte[] Build(uint uid, EquipmentLocation location)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 0x1d;
        buffer[1] = (byte) uid;
        buffer[2] = (byte) (uid >> 8);
        buffer[3] = (byte) (uid >> 0x10);
        buffer[4] = (byte) (uid >> 0x18);
        buffer[5] = (byte) location;
        return buffer;
    }

    // Properties
    public EquipmentLocation Location
    {
        get
        {
            return this.location;
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

 

 
public class SwitchWeapons : GCPacket
{
    // Methods
    public SwitchWeapons() : base(Build())
    {
    }

    public SwitchWeapons(byte[] data) : base(data)
    {
    }

    public static byte[] Build()
    {
        return new byte[] { 0x60 };
    }
}

 

 
public class TownFolkCancelInteraction : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public TownFolkCancelInteraction(byte[] data) : base(data)
    {
        this.unitType = (UnitType) data[1];
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public TownFolkCancelInteraction(UnitType unitType, uint uid) : base(Build(unitType, uid))
    {
        this.unitType = unitType;
        this.uid = uid;
    }

    public static byte[] Build(UnitType unitType, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 0x30;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
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

 

 
public class TownFolkInteract : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public TownFolkInteract(byte[] data) : base(data)
    {
        this.unitType = (UnitType) data[1];
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public TownFolkInteract(UnitType unitType, uint uid) : base(Build(unitType, uid))
    {
        this.unitType = unitType;
        this.uid = uid;
    }

    public static byte[] Build(UnitType unitType, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 0x2f;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
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

 

 
public class TownFolkMenuSelect : GCPacket
{
    // Fields
    protected TownFolkMenuItem selection;
    protected uint uid;

    // Methods
    public TownFolkMenuSelect(byte[] data) : base(data)
    {
        this.selection = (TownFolkMenuItem) BitConverter.ToUInt32(data, 1);
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public TownFolkMenuSelect(TownFolkMenuItem selection, uint uid, uint unknown9) : base(Build(selection, uid, unknown9))
    {
        this.selection = selection;
        this.uid = uid;
    }

    public static byte[] Build(TownFolkMenuItem selection, uint uid, uint unknown9)
    {
        return new byte[] { 0x38, ((byte) selection), ((byte) (((int) selection) >> 8)), ((byte) (((int) selection) >> 0x10)), ((byte) (((int) selection) >> 0x18)), ((byte) uid), ((byte) (uid >> 8)), ((byte) (uid >> 0x10)), ((byte) (uid >> 0x18)), ((byte) unknown9), ((byte) (unknown9 >> 8)), ((byte) (unknown9 >> 0x10)), ((byte) (unknown9 >> 0x18)) };
    }

    // Properties
    public TownFolkMenuItem Selection
    {
        get
        {
            return this.selection;
        }
    }

    public uint UID
    {
        get
        {
            return this.uid;
        }
    }

    public string Unknown9
    {
        get
        {
            return ByteConverter.ToHexString(base.Data, 9, 4);
        }
    }
}

 

 
public class TownFolkRepair : GCPacket
{
    // Fields
    protected uint dealerUID;
    protected uint itemUID;
    public static readonly uint NULL_UInt32;
    protected RepairType repairType;

    // Methods
    public TownFolkRepair(byte[] data) : base(data)
    {
        this.dealerUID = BitConverter.ToUInt32(data, 1);
        this.itemUID = BitConverter.ToUInt32(data, 5);
        this.repairType = (RepairType) BitConverter.ToUInt32(data, 9);
    }

    public TownFolkRepair(uint dealerUID) : base(Build(dealerUID))
    {
        this.dealerUID = dealerUID;
        this.itemUID = 0;
        this.repairType = RepairType.RepairAll;
    }

    public static byte[] Build(uint dealerUID)
    {
        byte[] buffer = new byte[0x11];
        buffer[0] = 0x35;
        buffer[1] = (byte) dealerUID;
        buffer[2] = (byte) (dealerUID >> 8);
        buffer[3] = (byte) (dealerUID >> 0x10);
        buffer[4] = (byte) (dealerUID >> 0x18);
        buffer[0x10] = 0x80;
        return buffer;
    }

    // Properties
    public uint DealerUID
    {
        get
        {
            return this.dealerUID;
        }
    }

    public uint ItemUID
    {
        get
        {
            return this.itemUID;
        }
    }

    public RepairType RepairType
    {
        get
        {
            return this.repairType;
        }
    }

    public string Unknown9
    {
        get
        {
            return ByteConverter.ToHexString(base.Data, 13, 4);
        }
    }
}

 

 
public enum TradeType : ushort
{
    BuyItem = 0,
    GambleItem = 2,
    SellItem = 4
}

 

 
public class UnequipItem : GCPacket
{
    // Fields
    protected readonly EquipmentLocation location;

    // Methods
    public UnequipItem(byte[] data) : base(data)
    {
        this.location = (EquipmentLocation) data[1];
    }

    public UnequipItem(EquipmentLocation location) : base(Build(location))
    {
        this.location = location;
    }

    public static byte[] Build(EquipmentLocation location)
    {
        byte[] buffer = new byte[3];
        buffer[0] = 0x1c;
        buffer[1] = (byte) location;
        return buffer;
    }

    // Properties
    public EquipmentLocation Location
    {
        get
        {
            return this.location;
        }
    }
}

 

 
public class UnitInteract : GCPacket
{
    // Fields
    protected uint uid;
    protected UnitType unitType;

    // Methods
    public UnitInteract(byte[] data) : base(data)
    {
        this.unitType = (UnitType) ((byte) BitConverter.ToUInt32(data, 1));
        this.uid = BitConverter.ToUInt32(data, 5);
    }

    public UnitInteract(UnitType unitType, uint uid) : base(Build(unitType, uid))
    {
        this.unitType = unitType;
        this.uid = uid;
    }

    public static byte[] Build(UnitType unitType, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 0x13;
        buffer[1] = (byte) unitType;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
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

 

 
public class UpdatePosition : GCPacket
{
    // Fields
    protected ushort x;
    protected ushort y;

    // Methods
    public UpdatePosition(byte[] data) : base(data)
    {
        this.x = BitConverter.ToUInt16(data, 1);
        this.y = BitConverter.ToUInt16(data, 3);
    }

    public UpdatePosition(ushort x, ushort y) : base(Build(x, y))
    {
        this.x = x;
        this.y = y;
    }

    public static byte[] Build(ushort x, ushort y)
    {
        return new byte[] { 0x5f, ((byte) x), ((byte) (x >> 8)), ((byte) y), ((byte) (y >> 8)) };
    }

    // Properties
    public ushort X
    {
        get
        {
            return this.x;
        }
    }

    public ushort Y
    {
        get
        {
            return this.y;
        }
    }
}

 

 
public class UseBeltItem : GCPacket
{
    // Fields
    protected bool toMerc;
    protected uint uid;

    // Methods
    public UseBeltItem(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
        if (BitConverter.ToUInt32(data, 5) == 1)
        {
            this.toMerc = true;
        }
    }

    public UseBeltItem(uint uid, bool toMerc) : base(Build(uid, toMerc))
    {
        this.uid = uid;
        this.toMerc = toMerc;
    }

    public UseBeltItem(uint uid, bool toMerc, uint unknown9) : base(Build(uid, toMerc, unknown9))
    {
        this.uid = uid;
        this.toMerc = toMerc;
    }

    public static byte[] Build(uint itemUID, bool toMerc)
    {
        return Build(itemUID, toMerc, 0);
    }

    public static byte[] Build(uint itemUID, bool toMerc, uint unknown9)
    {
        byte[] buffer = new byte[13];
        buffer[0] = 0x26;
        buffer[1] = (byte) itemUID;
        buffer[2] = (byte) (itemUID >> 8);
        buffer[3] = (byte) (itemUID >> 0x10);
        buffer[4] = (byte) (itemUID >> 0x18);
        buffer[5] = toMerc ? ((byte) 1) : ((byte) 0);
        buffer[9] = (byte) unknown9;
        buffer[10] = (byte) (unknown9 >> 8);
        buffer[11] = (byte) (unknown9 >> 0x10);
        buffer[12] = (byte) (unknown9 >> 0x18);
        return buffer;
    }

    // Properties
    public bool ToMerc
    {
        get
        {
            return this.toMerc;
        }
    }

    public uint UID
    {
        get
        {
            return this.uid;
        }
    }

    public string Unknown9
    {
        get
        {
            return ByteConverter.ToHexString(base.Data, 9, 4);
        }
    }
}

 

 
public class UseInventoryItem : GCPacket
{
    // Fields
    protected uint itemUID;
    protected int meX;
    protected int meY;

    // Methods
    public UseInventoryItem(byte[] data) : base(data)
    {
        this.itemUID = BitConverter.ToUInt32(data, 1);
        this.meX = BitConverter.ToInt32(data, 5);
        this.meY = BitConverter.ToInt32(data, 9);
    }

    public UseInventoryItem(uint itemUID, int meX, int meY) : base(Build(itemUID, meX, meY))
    {
        this.itemUID = itemUID;
        this.meX = meX;
        this.meY = meY;
    }

    public static byte[] Build(uint itemUID, int meX, int meY)
    {
        return new byte[] { 0x20, ((byte) itemUID), ((byte) (itemUID >> 8)), ((byte) (itemUID >> 0x10)), ((byte) (itemUID >> 0x18)), ((byte) meX), ((byte) (meX >> 8)), ((byte) (meX >> 0x10)), ((byte) (meX >> 0x18)), ((byte) meY), ((byte) (meY >> 8)), ((byte) (meY >> 0x10)), ((byte) (meY >> 0x18)) };
    }

    // Properties
    public uint ItemUID
    {
        get
        {
            return this.itemUID;
        }
    }

    public int MeX
    {
        get
        {
            return this.meX;
        }
    }

    public int MeY
    {
        get
        {
            return this.meY;
        }
    }
}

 
public class WalkToLocation : GoToLocation
{
    // Fields
    public static readonly bool WRAPPED = true;

    // Methods
    public WalkToLocation(byte[] data) : base(data)
    {
    }

    public WalkToLocation(ushort x, ushort y) : base(Build(x, y))
    {
    }

    public static byte[] Build(ushort x, ushort y)
    {
        return new byte[] { 1, ((byte) x), ((byte) (x >> 8)), ((byte) y), ((byte) (y >> 8)) };
    }
}

 

 
public class WalkToTarget : GoToTarget
{
    // Fields
    public static readonly bool WRAPPED = true;

    // Methods
    public WalkToTarget(byte[] data) : base(data)
    {
    }

    public WalkToTarget(UnitType target, uint uid) : base(Build(target, uid))
    {
    }

    public static byte[] Build(UnitType target, uint uid)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 2;
        buffer[1] = (byte) target;
        buffer[5] = (byte) uid;
        buffer[6] = (byte) (uid >> 8);
        buffer[7] = (byte) (uid >> 0x10);
        buffer[8] = (byte) (uid >> 0x18);
        return buffer;
    }
}

 

 
public class WardenResponse : GCPacket
{
    // Fields
    protected int dataLength;

    // Methods
    public WardenResponse(byte[] data) : base(data)
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

 

 
public class WaypointInteract : GCPacket
{
    // Fields
    protected WaypointDestination destination;
    protected uint uid;

    // Methods
    public WaypointInteract(byte[] data) : base(data)
    {
        this.uid = BitConverter.ToUInt32(data, 1);
        this.destination = (WaypointDestination) data[5];
    }

    public WaypointInteract(uint uid, WaypointDestination destination) : base(Build(uid, destination))
    {
        this.uid = uid;
        this.destination = destination;
    }

    public static byte[] Build(uint uid, WaypointDestination destination)
    {
        byte[] buffer = new byte[9];
        buffer[0] = 0x49;
        buffer[1] = (byte) uid;
        buffer[2] = (byte) (uid >> 8);
        buffer[3] = (byte) (uid >> 0x10);
        buffer[4] = (byte) (uid >> 0x18);
        buffer[5] = (byte) destination;
        return buffer;
    }

    // Properties
    public WaypointDestination Destination
    {
        get
        {
            return this.destination;
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

}