using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlueVex2.Settings
{
    public partial class GamesForm : Form
    {

        public GamesForm(string itemString)
        {
            InitializeComponent();

            if (itemString != "")
            {
                string[] parts = itemString.Split(',');
                if (parts.Length == 2)
                {
                    GameNameTextBox.Text = parts[0];
                    GamePassTextBox.Text = parts[1];
                }
            }
        }

        public string GameName
        {
            get
            {
                return this.GameNameTextBox.Text;
            }
        }

        public string GamePW
        {
            get
            {
                return this.GamePassTextBox.Text;
            }
        }



        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
