using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueVex.Core
{
    public interface IPlugin
    {
        void Initialize(Proxy proxy);
        void InitializeUI();
    }
}
