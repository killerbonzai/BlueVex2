using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Interop;
using BlueVex.Core;
using System.Threading;
using BlueVex.UI;
using System.Windows.Input;

namespace BlueVex2
{
    public class DiabloWindow : IDiabloWindow
    {

        #region Constants

        private const int DWM_TNP_SOURCECLIENTAREAONLY = 0x10;
        private const int DWM_TNP_VISIBLE = 0x8;
        private const int DWM_TNP_OPACITY = 0x4;
        private const int DWM_TNP_RECTDESTINATION = 0x1;

        private const int GWL_STYLE = -16;
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_APPWINDOW = 0x00040000;
        private const int WS_CAPTION = 0x00C00000;

        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOREDRAW = 0x0008;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_FRAMECHANGED = 0x0020;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint SWP_HIDEWINDOW = 0x0080;
        private const uint SWP_NOCOPYBITS = 0x0100;
        private const uint SWP_NOOWNERZORDER = 0x0200;
        private const uint SWP_NOSENDCHANGING = 0x0400;

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private const uint WM_MOUSEMOVE = 0x0200;
        private const uint WM_LBUTTONDOWN = 0x201;
        private const uint WM_LBUTTONUP = 0x202;
        private const uint WM_RBUTTONDOWN = 0x204;
        private const uint WM_RBUTTONUP = 0x205;
        private const uint WM_MBUTTONDOWN = 0x207;
        private const uint WM_MBUTTONUP = 0x208;

        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_CHAR = 0x102;
  
        #endregion

        #region User32 functions

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport(@"user32.dll", EntryPoint = "SetWindowPos", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref Rect lpRect);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // When you don't want the ProcessId, use this overload and pass IntPtr.Zero for the second parameter
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        #endregion

        #region DWM functions

        [DllImport("dwmapi.dll")]
        static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

        [DllImport("dwmapi.dll")]
        static extern int DwmUnregisterThumbnail(IntPtr thumb);

        [DllImport("dwmapi.dll")]
        static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out PSIZE size);

        [DllImport("dwmapi.dll")]
        static extern int DwmUpdateThumbnailProperties(IntPtr hThumb, ref DWM_THUMBNAIL_PROPERTIES props);

        //[DllImport("dwmapi.dll", PreserveSig = false)]
        //static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        #endregion

        #region Interop structs

        [StructLayout(LayoutKind.Sequential)]
        internal struct DWM_THUMBNAIL_PROPERTIES
        {
            public int dwFlags;
            public Rect rcDestination;
            public Rect rcSource;
            public byte opacity;
            public bool fVisible;
            public bool fSourceClientAreaOnly;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Rect
        {
            internal Rect(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PSIZE
        {
            public int x;
            public int y;
        }

        //[StructLayout(LayoutKind.Sequential)]
        //public struct MARGINS
        //{
        //    public int cxLeftWidth;      // width of left border that retains its size
        //    public int cxRightWidth;     // width of right border that retains its size
        //    public int cyTopHeight;      // height of top border that retains its size
        //    public int cyBottomHeight;   // height of bottom border that retains its size
        //};

        #endregion

        public static List<DiabloWindow> DiabloWindows = new List<DiabloWindow>();

        private string keyOwner;
        private bool isActive = false;

        private IntPtr handle;
        private IntPtr thumb;
        private Form mainWindow;
        private DiabloHostPanel targetControl;
        private Form underlayForm;
        private OverlayWindow overlayForm;
        
        private System.Windows.Controls.Image d2Cursor;

        private Rect originalRect;
        private Rect clientRect;

        public double Width = 800;
        public double Height = 600;
        private int DiabloOffsetX = 0;
        private int DiabloOffsetY = 0;

        private double scaleX;
        private double scaleY;

        private bool disposed = false;
        public bool Disposed
        {
            get
            {
                return disposed;
            }
        }

        public DiabloWindow(Form mainWindow, DiabloHostPanel overlayControl, IntPtr diablo2Handle, string keyOwner)
        {
            this.keyOwner = keyOwner;
            this.mainWindow = mainWindow;
            this.handle = diablo2Handle;
            this.targetControl = overlayControl;

            this.mainWindow.Move += new EventHandler(OnParentMove);
            this.targetControl.KeyDown += new System.Windows.Forms.KeyEventHandler(targetControl_KeyDown);
            this.targetControl.KeyUp += new System.Windows.Forms.KeyEventHandler(targetControl_KeyUp);

            this.targetControl.RegisterEvents();

            DiabloWindows.Add(this);

            underlayForm = new Form();
            underlayForm.FormBorderStyle = FormBorderStyle.None;
            underlayForm.Text = "BlueVex 2 Container Window";
            underlayForm.Controls.Add(new Panel() { Width = Screen.PrimaryScreen.WorkingArea.Width, Height = Screen.PrimaryScreen.WorkingArea.Height } );
            underlayForm.ShowInTaskbar = false;
            underlayForm.Show();

            Mouse.OverrideCursor = System.Windows.Input.Cursors.None;

            overlayForm = new OverlayWindow();
            overlayForm.MouseMove += new System.Windows.Input.MouseEventHandler(overlayForm_MouseMove);
            overlayForm.Width = 1600;
            overlayForm.Height = 1200;
            overlayForm.Show();
            
            HwndSource hwndSource = PresentationSource.FromVisual(overlayForm) as HwndSource;

            System.Windows.Point formSize = hwndSource.CompositionTarget.TransformFromDevice.Transform(new System.Windows.Point(targetControl.Width, targetControl.Height));

            scaleX = formSize.X / 800;
            scaleY = formSize.Y / 600;

            this.overlayForm.Canvas.RenderTransform = new ScaleTransform(scaleX, scaleY);

            System.IO.Stream fileStream = this.GetType().Assembly.GetManifestResourceStream("BlueVex2.Resources.D2Cursor.png");
            if (fileStream != null)
            {
                PngBitmapDecoder bitmapDecoder = new PngBitmapDecoder(fileStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                ImageSource imageSource = bitmapDecoder.Frames[0];
                
                d2Cursor = new System.Windows.Controls.Image();
                d2Cursor.IsHitTestVisible = false;
                //d2Cursor.MouseMove += new System.Windows.Input.MouseEventHandler(d2Cursor_MouseMove);
                d2Cursor.Source = imageSource;
                d2Cursor.Stretch = Stretch.Uniform;

                d2Cursor.Visibility = Visibility.Hidden;

                overlayForm.Canvas.Children.Add(d2Cursor);
            }

            RegisterThumbnail();
            Hide();

            OnParentMove(null, null);

            this.mainWindow.Activate();
        }

        void d2Cursor_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            e.Handled = true;
        }

        public void SetHostPanel(DiabloHostPanel hostPanel)
        {
            this.targetControl = hostPanel;
        }

        #region Window Management Methods

        public void Hide()
        {
            Panel panel = underlayForm.Controls[0] as Panel;

            SetParent(this.handle, panel.Handle);

            this.originalRect = new Rect();
            GetWindowRect(this.handle, ref this.originalRect);
            GetClientRect(this.handle, out clientRect);

            DiabloOffsetX = (Screen.PrimaryScreen.WorkingArea.Width / 2) - ((originalRect.Right - originalRect.Left) / 2);
            DiabloOffsetY = (Screen.PrimaryScreen.WorkingArea.Height / 2) - ((originalRect.Bottom - originalRect.Top) / 2);

            int frameWidth = ((originalRect.Right - originalRect.Left) - clientRect.Right) / 2;
            int captionHeight = (originalRect.Bottom - originalRect.Top) - clientRect.Bottom - frameWidth;

            panel.Left = -DiabloOffsetX - frameWidth;
            panel.Top = -DiabloOffsetY - captionHeight;

            underlayForm.ClientSize = new System.Drawing.Size(clientRect.Right, clientRect.Bottom);

            SetWindowPos(this.handle, IntPtr.Zero, DiabloOffsetX, DiabloOffsetY, 0, 0, SWP_NOSIZE | SWP_NOACTIVATE | SWP_NOZORDER | SWP_NOOWNERZORDER | SWP_FRAMECHANGED);
        }

        public void Show()
        {
            SetParent(this.handle, IntPtr.Zero);
        }

        private void OnParentMove(object sender, EventArgs e)
        {
            System.Drawing.Point location = targetControl.PointToScreen(new System.Drawing.Point(0, 0));

            System.Drawing.Point underlayLocation = this.mainWindow.PointToScreen(new System.Drawing.Point(0, 0));
            underlayForm.Left = underlayLocation.X + 14;
            underlayForm.Top = underlayLocation.Y + 63;

            HwndSource hwndSource = PresentationSource.FromVisual(overlayForm) as HwndSource;
            System.Windows.Point wpfLocation = hwndSource.CompositionTarget.TransformFromDevice.Transform(new System.Windows.Point(location.X, location.Y));

            overlayForm.Left = wpfLocation.X;
            overlayForm.Top = wpfLocation.Y;
        }

        #endregion

        #region Input Event Passing

        private void targetControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.keyDown(this, e);
        }

        private void targetControl_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.keyUp(this, e);
        }

        //private void overlayForm_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    System.Windows.Forms.Cursor.Show();
        //}

        //private void overlayForm_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    System.Windows.Forms.Cursor.Hide();
        //}

        private void overlayForm_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Point pos = e.GetPosition(this.overlayForm);
            MouseMove((int)(pos.X / this.scaleX), (int)(pos.Y / this.scaleY));
        }

        #endregion

        #region Input Spoofing

        public void MouseMove(int x, int y)
        {
            PostMessage(this.handle, WM_MOUSEMOVE, 0, y * 0x10000 + x);

            if (d2Cursor != null && overlayForm != null)
            {
                System.Windows.Point wpfLocation = ResolvePoint(x, y);

                d2Cursor.SetValue(System.Windows.Controls.Canvas.LeftProperty, wpfLocation.X);
                d2Cursor.SetValue(System.Windows.Controls.Canvas.TopProperty, wpfLocation.Y);
            }
        }

        void IDiabloWindow.Click(int x, int y)
        {
            LeftMouseDown(x, y);
            LeftMouseUp(x, y);
        }

        public void LeftMouseDown(int x, int y)
        {
            PostMessage(this.handle, WM_LBUTTONDOWN, 0, y * 0x10000 + x);
        }

        public void LeftMouseUp(int x, int y)
        {
            PostMessage(this.handle, WM_LBUTTONUP, 0, y * 0x10000 + x);
        }

        public void RightMouseDown(int x, int y)
        {
            PostMessage(this.handle, WM_RBUTTONDOWN, 0, y * 0x10000 + x);
        }

        public void RightMouseUp(int x, int y)
        {
            PostMessage(this.handle, WM_RBUTTONUP, 0, y * 0x10000 + x);
        }

        public void MiddleMouseDown(int x, int y)
        {
            PostMessage(this.handle, WM_MBUTTONDOWN, 0, y * 0x10000 + x);
        }

        public void MiddleMouseUp(int x, int y)
        {
            PostMessage(this.handle, WM_MBUTTONUP, 0, y * 0x10000 + x);
        }

        public void SendKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            PostMessage(this.handle, WM_KEYDOWN, e.KeyValue, 0);
            //PostMessage(this.handle, WM_CHAR, e.KeyValue, (int)lparam);
        }

        public void SendKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            uint lparam = 0x80000000;
            PostMessage(this.handle, WM_KEYUP, e.KeyValue, (int)lparam);
        }

        public void Char(int charValue)
        {
            PostMessage(this.handle, WM_CHAR, charValue, 0);
        }

        private System.Windows.Point ResolvePoint(int x, int y)
        {
            double nx = (this.targetControl.Width / this.Width) * x;
            double ny = (this.targetControl.Height / this.Height) * y;
            return new System.Windows.Point((int)nx, (int)ny);
        }

        #endregion

        #region DWM Thumbnail Stuff

        public void RegisterThumbnail()
        {
            if (thumb != IntPtr.Zero)
                DwmUnregisterThumbnail(thumb);

            int i = DwmRegisterThumbnail(mainWindow.Handle, underlayForm.Handle, out thumb);

            if (i == 0)
                UpdateThumb();
        }

        public void UnRegisterThumbnail()
        {
            if (thumb != IntPtr.Zero)
                DwmUnregisterThumbnail(thumb);
        }

        private void UpdateThumb()
        {
            if (thumb != IntPtr.Zero)
            {
                PSIZE size;
                DwmQueryThumbnailSourceSize(thumb, out size);

                DWM_THUMBNAIL_PROPERTIES props = new DWM_THUMBNAIL_PROPERTIES();

                props.fVisible = true;
                props.dwFlags = DWM_TNP_VISIBLE | DWM_TNP_RECTDESTINATION | DWM_TNP_OPACITY | DWM_TNP_SOURCECLIENTAREAONLY;
                props.opacity = 255;
                props.fSourceClientAreaOnly = true;

                Rect thumbRect = new Rect(targetControl.Left, targetControl.Top, targetControl.Right, targetControl.Bottom);

                Control parent = targetControl.Parent;
                while (parent != null && parent != mainWindow)
                {
                    thumbRect.Left += parent.Left;
                    thumbRect.Top += parent.Top;
                    thumbRect.Right += parent.Left;
                    thumbRect.Bottom += parent.Top;
                    parent = parent.Parent;
                }

                props.rcDestination = thumbRect;

                DwmUpdateThumbnailProperties(thumb, ref props);
            }
        }

        #endregion

        public void Close()
        {
            this.mainWindow.Move -= new EventHandler(OnParentMove);
            overlayForm.Close();
            underlayForm.Close();
            overlayForm = null;
            underlayForm = null;
            disposed = true;
        }

        public void MakeActive()
        {
            this.isActive = true;
        }

        public void MakeInactive()
        {
            this.isActive = false;
        }

        public void Activate()
        {
            this.isActive = true;
            RegisterThumbnail();
            OnParentMove(null, null);
            overlayForm.Show();
            mainWindow.Focus();
        }

        public void Deactivate()
        {
            this.isActive = false;
            UnRegisterThumbnail();
            overlayForm.Hide();
        }


        bool IDiabloWindow.IsActive()
        {
            return this.isActive;
        }

        string IDiabloWindow.KeyOwner
        {
            get { return this.keyOwner; }
        }

        void IDiabloWindow.CreateGame(string gameName, string password, string difficulty)
        {
            BlueVex2.DiabloInteraction.CreateGameDelegate createGameDelegate = new BlueVex2.DiabloInteraction.CreateGameDelegate(DiabloInteraction.CreateGame);
            createGameDelegate.BeginInvoke(gameName, password, difficulty, this, null, null);
        }

        void IDiabloWindow.JoinGame(string gameName, string password)
        {
            BlueVex2.DiabloInteraction.JoinGameDelegate joinGameDelegate = new BlueVex2.DiabloInteraction.JoinGameDelegate(DiabloInteraction.JoinGame);
            joinGameDelegate.BeginInvoke(gameName, password, this, null, null);
        }

        void IDiabloWindow.ExitGame()
        {
            BlueVex2.DiabloInteraction.ExitGameDelegate exitGameDelegate = new BlueVex2.DiabloInteraction.ExitGameDelegate(DiabloInteraction.ExitGame);
            exitGameDelegate.BeginInvoke(this, null, null);
        }

        void IDiabloWindow.QuitFromChat()
        {
            BlueVex2.DiabloInteraction.QuitFromChatDelegate quitFromChatDelegate = new BlueVex2.DiabloInteraction.QuitFromChatDelegate(DiabloInteraction.QuitFromChat);
            quitFromChatDelegate.BeginInvoke(this, null, null);
        }


        void IDiabloWindow.LoginToBattleNet(string defaultAccount)
        {
            BlueVex2.DiabloInteraction.LoginToBattleNetDelegate loginToBattleNetDelegate = new BlueVex2.DiabloInteraction.LoginToBattleNetDelegate(DiabloInteraction.LoginToBattleNet);
            loginToBattleNetDelegate.BeginInvoke(defaultAccount, 1000, this, null, null);
        }

        private D2LeftFrame leftFrame = new D2LeftFrame();
        private D2Panel currentLeftPanel;
        void IDiabloWindow.ShowPanel(D2Panel panel)
        {
            if (currentLeftPanel != panel)
            {
                if (leftFrame.Children.Count > 1)
                {
                    leftFrame.Children.RemoveAt(1);
                }               
                leftFrame.Children.Add(panel);
                
                panel.SetValue(System.Windows.Controls.Canvas.LeftProperty, (double)81);
                panel.SetValue(System.Windows.Controls.Canvas.TopProperty, (double)62);

                this.overlayForm.Canvas.Children.Insert(this.overlayForm.Canvas.Children.IndexOf(d2Cursor), leftFrame);
                currentLeftPanel = panel;
                d2Cursor.Visibility = Visibility.Visible;
            }
            else
            {
                this.overlayForm.Canvas.Children.Remove(leftFrame);
                currentLeftPanel = null;
                d2Cursor.Visibility = Visibility.Hidden;
            }
        }

        private System.Windows.Forms.KeyEventHandler keyDown = delegate { };
        event System.Windows.Forms.KeyEventHandler IDiabloWindow.KeyDown
        {
            add {
                lock(this) { keyDown += value; }
            }
            remove {
                lock(this) { keyDown -= value; }
            }
        }

        private System.Windows.Forms.KeyEventHandler keyUp = delegate { };
        event System.Windows.Forms.KeyEventHandler IDiabloWindow.KeyUp
        {
            add
            {
                lock (this) { keyUp += value; }
            }
            remove
            {
                lock (this) { keyUp -= value; }
            }
        }
    }
}
