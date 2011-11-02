using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BlueVex.Core;
using BlueVex2.Tabs;
using System.Windows.Forms;

namespace BlueVex2
{
    static class DiabloInteraction
    {

        public enum Diablo2Button
        {
            Create,
            Join,
            Normal,
            Nightmare,
            Hell,
            CreateGame,
            SaveAndExit,
            CharacterSlot1,
            CharacterSlot2,
            CharacterSlot3,
            CharacterSlot4,
            CharacterSlot5,
            CharacterSlot6,
            CharacterSlot7,
            CharacterSlot8,
            BattleNet
        }

        public static void DoubleClickButton(Diablo2Button Button, DiabloWindow window)
        {
            ClickButton(Button, window);
            Thread.Sleep(100);
            ClickButton(Button, window);
        }

        public static void ClickButton(Diablo2Button Button, IDiabloWindow window)
        {
            if (window == null) return;

            int x = 0;
            int y = 0;

            switch (Button)
            {
                case Diablo2Button.Create:
                    x = 590;
                    y = 460;
                    break;
                case Diablo2Button.Join:
                    x = 720;
                    y = 460;
                    break;
                case Diablo2Button.Normal:
                    x = 440;
                    y = 375;
                    break;
                case Diablo2Button.Nightmare:
                    x = 565;
                    y = 375;
                    break;
                case Diablo2Button.Hell:
                    x = 707;
                    y = 375;
                    break;
                case Diablo2Button.CreateGame:
                    x = 690;
                    y = 420;
                    break;
                case Diablo2Button.SaveAndExit:
                    x = 400;
                    y = 260;
                    break;
                case Diablo2Button.CharacterSlot1:
                    x = 175;
                    y = 135;
                    break;
                case Diablo2Button.CharacterSlot2:
                    x = 450;
                    y = 135;
                    break;
                case Diablo2Button.CharacterSlot3:
                    x = 175;
                    y = 230;
                    break;
                case Diablo2Button.CharacterSlot4:
                    x = 450;
                    y = 230;
                    break;
                case Diablo2Button.CharacterSlot5:
                    x = 175;
                    y = 320;
                    break;
                case Diablo2Button.CharacterSlot6:
                    x = 450;
                    y = 320;
                    break;
                case Diablo2Button.CharacterSlot7:
                    x = 175;
                    y = 414;
                    break;
                case Diablo2Button.CharacterSlot8:
                    x = 450;
                    y = 414;
                    break;
                case Diablo2Button.BattleNet:
                    x = 400;
                    y = 350;
                    break;
            }

            window.Click(x, y);
        }

        public static void WriteString(string text, DiabloWindow window)
        {
            if (window == null) return;

            for (int i = 0; i < text.Length; i++)
			{
                window.Char(Char.ConvertToUtf32(text, i));
                Thread.Sleep(100);
			}
        }

        public delegate void LoginToBattleNetDelegate(string defaultAccount, int delay, DiabloWindow diabloWindow);
        public static void LoginToBattleNet(string defaultAccount, int delay, DiabloWindow diabloWindow)
        {
            if (!string.IsNullOrEmpty(defaultAccount))
            {
                string username = string.Empty;
                string password = string.Empty;
                string charslot = string.Empty;
                string master = string.Empty;

                foreach (string accountString in BlueVex2.Properties.Settings.Default.Accounts)
                {
                    if (accountString.StartsWith(defaultAccount + ","))
                    {
                        string[] parts = accountString.Split(',');
                        username = parts[0];
                        password = parts[1];
                        charslot = parts[2];
                        master   = parts[3];
                    }
                }

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return;
                }

                Thread.Sleep((int)delay);
                ConsoleTab.WriteLine("Logging into Battle Net as " + username);

                // Click to load menu
                Thread.Sleep(2000);
                DiabloInteraction.ClickButton(DiabloInteraction.Diablo2Button.BattleNet, diabloWindow);

                // Click BattleNet button
                Thread.Sleep(500);
                DiabloInteraction.ClickButton(DiabloInteraction.Diablo2Button.BattleNet, diabloWindow);

                // Tab
                Thread.Sleep(2000);
                diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Tab));
                diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Tab));

                //Username
                Thread.Sleep(500);
                DiabloInteraction.WriteString(username, diabloWindow);

                //Tab
                Thread.Sleep(500);
                diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Tab));
                diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Tab));

                //Password
                Thread.Sleep(500);
                DiabloInteraction.WriteString(password, diabloWindow);

                // Enter
                Thread.Sleep(500);
                diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Enter));
                diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Enter));

                // Double Click Character Slot
                Thread.Sleep(5000);
                switch (charslot)
                {
                    case "1":
                        DiabloInteraction.DoubleClickButton(DiabloInteraction.Diablo2Button.CharacterSlot1, diabloWindow);
                        break;

                    case "2":
                        DiabloInteraction.DoubleClickButton(DiabloInteraction.Diablo2Button.CharacterSlot2, diabloWindow);
                        break;

                    case "3":
                        DiabloInteraction.DoubleClickButton(DiabloInteraction.Diablo2Button.CharacterSlot3, diabloWindow);
                        break;

                    case "4":
                        DiabloInteraction.DoubleClickButton(DiabloInteraction.Diablo2Button.CharacterSlot4, diabloWindow);
                        break;

                    case "5":
                        DiabloInteraction.DoubleClickButton(DiabloInteraction.Diablo2Button.CharacterSlot5, diabloWindow);
                        break;

                    case "6":
                        DiabloInteraction.DoubleClickButton(DiabloInteraction.Diablo2Button.CharacterSlot6, diabloWindow);
                        break;

                    case "7":
                        DiabloInteraction.DoubleClickButton(DiabloInteraction.Diablo2Button.CharacterSlot7, diabloWindow);
                        break;

                    case "8":
                        DiabloInteraction.DoubleClickButton(DiabloInteraction.Diablo2Button.CharacterSlot8, diabloWindow);
                        break;

                    default:
                        DiabloInteraction.DoubleClickButton(DiabloInteraction.Diablo2Button.CharacterSlot1, diabloWindow);
                        break;
                }

            }
        }



        public delegate void CreateGameDelegate(string gameName, string password, string difficulty, DiabloWindow diabloWindow);
        public static void CreateGame(string gameName, string password, string difficulty, DiabloWindow diabloWindow)
        {
            //Click Create
            Thread.Sleep(200);
            DiabloInteraction.ClickButton(Diablo2Button.Create, diabloWindow);

            // Game Name
            Thread.Sleep(200);
            DiabloInteraction.WriteString(gameName, diabloWindow);

            // Tab
            Thread.Sleep(200);
            diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Tab));
            diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Tab));

            // Password
            Thread.Sleep(200);
            DiabloInteraction.WriteString(password, diabloWindow);

            // Difficulty
            Thread.Sleep(200);
            switch (difficulty)
            {
                case "Normal":
                    DiabloInteraction.ClickButton(Diablo2Button.Normal, diabloWindow);
                    break;

                case "Nightmare":
                    DiabloInteraction.ClickButton(Diablo2Button.Nightmare, diabloWindow);
                    break;

                case "Hell":
                    DiabloInteraction.ClickButton(Diablo2Button.Hell, diabloWindow);
                    break;
            }

            // Enter
            Thread.Sleep(200);
            diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Enter));
            diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Enter));



        }



        public delegate void JoinGameDelegate(string gameName, string password, DiabloWindow diabloWindow);
        public static void JoinGame(string gameName, string password, DiabloWindow diabloWindow)
        {
            // Click Join
            Thread.Sleep(5000);
            DiabloInteraction.ClickButton(Diablo2Button.Join, diabloWindow);

            // Game Name
            Thread.Sleep(1000);
            DiabloInteraction.WriteString(gameName, diabloWindow);

            // Tab
            Thread.Sleep(1000);
            diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Tab));
            diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Tab));

            // Password
            Thread.Sleep(1000);
            DiabloInteraction.WriteString(password, diabloWindow);

            // Enter
            Thread.Sleep(1000);
            diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Enter));
            diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Enter));
           
        }

        public delegate void ExitGameDelegate(DiabloWindow diabloWindow);
        public static void ExitGame(DiabloWindow diabloWindow)
        {
            // Escape
            Thread.Sleep(100);
            diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Escape));
            diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Escape));

            // Up
            Thread.Sleep(200);
            diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Up));
            diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Up));

            // Enter
            Thread.Sleep(200);
            diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Enter));
            diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Enter));


        }

        public delegate void QuitFromChatDelegate(DiabloWindow diabloWindow);
        public static void QuitFromChat(DiabloWindow diabloWindow)
        {
            // Escape
            Thread.Sleep(400);
            diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Escape));
            diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Escape));

            // Escape
            Thread.Sleep(400);
            diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Escape));
            diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Escape));

            // Escape
            Thread.Sleep(400);
            diabloWindow.SendKeyDown(new KeyEventArgs(Keys.Escape));
            diabloWindow.SendKeyUp(new KeyEventArgs(Keys.Escape));


        }

    }
}
