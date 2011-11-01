using System;

namespace D2Packets
{
    public enum BnetServerPacket
    {
        KeepAlive = 0x00,
        EnterChatResponse = 0x0A,
        ChannelList = 0x0B,
        ChatEvent = 0x0F,
        AdInfo = 0x15,
        BnetPing = 0x25,
        FileTimeInfo = 0x33,
        BnetLogonResponse = 0x3a,
        RealmLogonResponse = 0x3e,
        QueryRealmsResponse = 0x40,
        NewsInfo = 0x46,
        ExtraWorkInfo = 0x4a,
        RequiredExtraWorkInfo = 0x4c,
        BnetConnectionResponse = 0x50,
        BnetAuthResponse = 0x51,
        Invalid = 0x83 
    }
}
