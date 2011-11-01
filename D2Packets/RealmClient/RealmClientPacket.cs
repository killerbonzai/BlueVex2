using System;

namespace D2Packets
{
	public enum RealmClientPacket : byte
	{
		RealmStartupRequest			= 0x01,
        CharacterCreationRequest    = 0x02,
		CreateGameRequest			= 0x03,
		JoinGameRequest				= 0x04,
		GameListRequest				= 0x05,
		GameInfoRequest				= 0x06,
		CharacterLogonRequest		= 0x07,
		CharacterDeletionRequest	= 0x0A,
        MessageOfTheDayRequest      = 0x12,
        CancelGameCreation          = 0x13,
		CharacterUpgradeRequest		= 0x18,
		CharacterListRequest		= 0x19,

		Invalid						= 0x30,	// Probably 0x20, leaving wiggle room just in case...

	}
}
