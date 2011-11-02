using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BlueVex2.Tabs
{
    class ConsoleTab : TabPage
    {
        TextBox logTextBox;

        public ConsoleTab()
        {
            this.Text = "Console";

            logTextBox = new TextBox();
            logTextBox.ReadOnly = true;
            logTextBox.Multiline = true;
            logTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            logTextBox.BackColor = Color.Navy;
            logTextBox.ForeColor = Color.White;
            logTextBox.Dock = DockStyle.Fill;
            this.Controls.Add(logTextBox);

            ConsoleTab.Console = logTextBox;
        }

        private static TextBox Console;
        private delegate void WriteLineDelegate(string text);

        public static void WriteLine(string text)
        {
            if (Console.InvokeRequired)
            {
                Console.Invoke(new WriteLineDelegate(WriteLine), text);
            }
            else
            {
                if (Console != null)
                {
                    Console.AppendText(text + Environment.NewLine);
                }
            }
        }

    }
}
