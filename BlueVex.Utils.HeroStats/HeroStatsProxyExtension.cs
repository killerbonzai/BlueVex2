using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueVex.Core;

namespace BlueVex.Utils
{
    public static class HeroStatsProxyExtension
    {

        public static uint PlayerId(this Proxy proxy)
        {
            return HeroStatsPlugin.GetInstance(proxy).PlayerId;
        }

        public static int PlayerX(this Proxy proxy)
        {
            return HeroStatsPlugin.GetInstance(proxy).PlayerX;
        }

        public static int PlayerY(this Proxy proxy)
        {
            return HeroStatsPlugin.GetInstance(proxy).PlayerY;
        }

    }
}
