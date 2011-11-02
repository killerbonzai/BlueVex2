using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueVex.Core;
using BlueVex.Utils;

namespace BlueVex.Utils
{
    public static class Navigation
    {

        public static void WalkTo(this Proxy proxy, int x, int y)
        {
            proxy.CompressSendToDiablo(GameServer.PlayerMove.Build(D2Data.UnitType.Player, proxy.PlayerId(), 1, x, y, 0, 0, 0));
        }

    }
}
