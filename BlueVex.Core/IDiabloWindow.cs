using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueVex.UI;
using System.Windows.Forms;

namespace BlueVex.Core
{
    public interface IDiabloWindow
    {
        event KeyEventHandler KeyDown;
        event KeyEventHandler KeyUp;
        string KeyOwner { get; }
        bool IsActive();
        void Click(int x, int y);
        void CreateGame(string gameName, string password, string difficulty);
        void JoinGame(string gameName, string password);
        void ExitGame();
        void QuitFromChat();
        void LoginToBattleNet(string defaultAccount);
        void ShowPanel(D2Panel panel);
    }
}
