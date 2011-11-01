using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;

namespace BlueVex2
{
    class OverlayWindow : System.Windows.Window
    {

        #region Non Focusable Window Stuff

        public const int WS_EX_NOACTIVATE = 0x8000000;
        public const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // Get this window's handle
            IntPtr hwnd = new WindowInteropHelper(this).Handle;

            // Change the extended window style to include WS_EX_NOACTIVATE
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_NOACTIVATE);

            // Add the Message Hook
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            source.AddHook(new HwndSourceHook(WndProc));
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_MOUSEACTIVATE = 0x0021;
            const int MA_NOACTIVATE = 3;

            if (msg == WM_MOUSEACTIVATE)
            {
                handled = true;
                return new IntPtr(MA_NOACTIVATE);
            }

            return IntPtr.Zero;
        }  

        #endregion

        private Canvas canvas;
        public Canvas Canvas { get { return canvas; } }

        public OverlayWindow()
        {
            this.BorderThickness = new Thickness(0);
            this.BorderBrush = System.Windows.Media.Brushes.Transparent;
            this.Topmost = true;
            this.ShowInTaskbar = false;
            this.Visibility = Visibility.Visible;
            this.AllowsTransparency = true;
            this.Background = System.Windows.Media.Brushes.Transparent;
            this.WindowStyle = WindowStyle.None;
            this.IsEnabled = true;

            canvas = new Canvas();
            this.Content = canvas;
        }

    }
}
