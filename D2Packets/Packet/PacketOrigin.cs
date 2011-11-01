using System;

namespace D2Packets
{
	public enum Origin
	{
		GameServer = 0,
		GameClient = 1,
		BattleNetServer = 2,
		BattleNetClient = 3,
		RealmServer = 4,
		RealmClient = 5,
		Undefined
	}
}
