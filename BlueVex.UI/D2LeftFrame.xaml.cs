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

namespace BlueVex.UI
{
    /// <summary>
    /// Interaction logic for D2LeftFrame.xaml
    /// </summary>
    public partial class D2LeftFrame : Canvas
    {
        public D2LeftFrame()
        {
            InitializeComponent();
            this.MouseDown += new MouseButtonEventHandler(D2LeftFrame_MouseDown);
            this.MouseUp += new MouseButtonEventHandler(D2LeftFrame_MouseUp);
        }

        void D2LeftFrame_MouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        void D2LeftFrame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
