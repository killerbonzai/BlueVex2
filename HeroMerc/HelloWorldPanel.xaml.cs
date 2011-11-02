using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlueVex.UI;
using BlueVex.Core;
using BlueVex.Utils;

namespace HeroMerc
{
    /// <summary>
    /// Interaction logic for HelloWorldPanel.xaml
    /// </summary>
    public partial class HelloWorldPanel : D2Panel
    {
        public IDiabloWindow DiabloWindow { get; set; }

        private Proxy proxy;

        public HelloWorldPanel(Proxy proxy)
        {
            this.proxy = proxy;
            InitializeComponent();
        }

        private void d2CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (DiabloWindow != null)
            {
                DiabloWindow.ShowPanel(this);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.proxy.WalkTo(this.proxy.PlayerX(), this.proxy.PlayerY() + 10);
        }

        private void D2Panel_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
