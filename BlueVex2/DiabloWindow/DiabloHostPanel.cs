using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BlueVex2
{
    public class DiabloHostPanel : Control
    {
        private DiabloWindow diabloWindow = null;

        public DiabloWindow DiabloWindow { get { return diabloWindow; } }

        public DiabloHostPanel()
        {
            this.BackColor = Color.Black;
        }

        public void RegisterEvents()
        {
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            this.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            this.KeyDown += new KeyEventHandler(OnKeyDown);
            this.KeyUp += new KeyEventHandler(OnKeyUp);
        }

        public void BindDiabloWindow(DiabloWindow diabloWindow)
        {
            this.diabloWindow = diabloWindow;
        }

        public void ReleaseDiabloWindow()
        {
            if (this.diabloWindow != null)
            {
                this.diabloWindow.Show();
                this.diabloWindow.UnRegisterThumbnail();
                this.diabloWindow = null;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (diabloWindow != null)
            {
                Point point = ResolvePoint(e.Location);
                diabloWindow.MouseMove(point.X, point.Y);
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (diabloWindow != null)
            {
                foreach (DiabloWindow window in DiabloWindow.DiabloWindows)
                {
                    if (window != diabloWindow)
                    {
                        window.MakeInactive();
                    }
                }
                diabloWindow.MakeActive();

                Point point = ResolvePoint(e.Location);
                if (e.Button == MouseButtons.Left)
                {
                    diabloWindow.LeftMouseDown(point.X, point.Y);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    diabloWindow.RightMouseDown(point.X, point.Y);
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    diabloWindow.MiddleMouseDown(point.X, point.Y);
                }
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (diabloWindow != null)
            {
                Point point = ResolvePoint(e.Location);
                if (e.Button == MouseButtons.Left)
                {
                    diabloWindow.LeftMouseUp(point.X, point.Y);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    diabloWindow.RightMouseUp(point.X, point.Y);
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    diabloWindow.MiddleMouseUp(point.X, point.Y);
                }
            }

            this.Focus();
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (diabloWindow != null)
            {
                if (!e.Handled)
                {
                    diabloWindow.SendKeyDown(e);
                }
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (diabloWindow != null)
            {
                if (!e.Handled)
                {
                    diabloWindow.SendKeyUp(e);
                }
            }
        }

        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            return true;
        }

        private Point ResolvePoint(Point point)
        {
            double x = (diabloWindow.Width / this.Width) * point.X;
            double y = (diabloWindow.Height / this.Height) * point.Y;
            return new Point((int)x, (int)y);
        }
    }
}
