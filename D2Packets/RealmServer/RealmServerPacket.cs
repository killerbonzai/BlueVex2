using System;

namespace D2Packets
{
	public enum RealmServerPacket : byte
	{
        RealmStartupResponse        = 1,
        CharacterCreationResponse   = 2,
        CreateGameResponse          = 3,
        JoinGameResponse            = 4,
		GameList					= 5,
		GameInfo					= 6,
		CharacterLogonResponse		= 7,
        CharacterDeletionResponse   = 0x0A,
		MessageOfTheDay				= 0x12,
		GameCreationQueue			= 0x14,
        CharacterUpgradeResponse    = 0x18,
		CharacterList				= 0x19,

		Invalid						= 0x30,	// Probably 0x20, leaving some space for safety...
	}
}
