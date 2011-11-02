using System;

namespace D2Packets
{
    public enum BnetClientPacket
    {
        KeepAlive = 0x00,
        EnterChatRequest = 0x0A,
        ChannelListRequest = 0x0B,
        JoinChannel = 0x0C,
        ChatCommand = 0x0E,
        LeaveChat = 0x10,
        AdInfoRequest = 0x15,
        StartGame = 0x1c,
        LeaveGame = 0x1f,
        DisplayAd = 0x21,
        NotifyJoin = 0x22,
        BnetPong = 0x25,
        FileTimeRequest = 0x33,
        BnetLogonRequest = 0x3a,
        RealmLogonRequest = 0x3e,
        QueryRealms = 0x40,
        NewsInfoRequest = 0x46,
        ExtraWorkResponse = 0x4b,
        BnetConnectionRequest =0x50,
        BnetAuthRequest = 0x51,
        Invalid = 0x83
    }
}
