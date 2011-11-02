using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BlueVex2.Tabs
{
    class OverviewTab : TabPage
    {
        FlowLayoutPanel flowPanel;
        public OverviewTab()
        {
            this.Text = "Overview";

            flowPanel = new FlowLayoutPanel();
            flowPanel.Padding = new System.Windows.Forms.Padding(10);
            flowPanel.Dock = DockStyle.Fill;
            this.Controls.Add(flowPanel);
        }

        public void Activate()
        {

            foreach (TabPage tab in ((TabControl)this.Parent).TabPages)
            {
                if (tab is DiabloTab)
                {
                    // Look through the current panels to see if we have added it already.
                    DiabloHostPanel existingPanel = null;

                    foreach (DiabloHostPanel panel in this.flowPanel.Controls)
                    {
                        if (((DiabloTab)tab).DiabloWindow == panel.DiabloWindow)
                        {
                            existingPanel = panel;
                            break;
                        }
                    }
                    
                    // If we have, activate it.
                    if (existingPanel != null)
                    {
                        existingPanel.DiabloWindow.SetHostPanel(existingPanel);
                        existingPanel.DiabloWindow.Activate();
                    }
                    // If we havnt, Add it.
                    else
                    {
                        if (((DiabloTab)tab).DiabloWindow != null)
                        {
                            existingPanel = new DiabloHostPanel();
                            existingPanel.Width = Convert.ToInt32(400m * BlueVex2.Properties.Settings.Default.ResolutionScale);
                            existingPanel.Height = Convert.ToInt32(300m * BlueVex2.Properties.Settings.Default.ResolutionScale);
                            existingPanel.Location = new Point(10, 10);
                            this.flowPanel.Controls.Add(existingPanel);
                            existingPanel.BindDiabloWindow(((DiabloTab)tab).DiabloWindow);
                            existingPanel.DiabloWindow.SetHostPanel(existingPanel);
                            existingPanel.DiabloWindow.Activate();
                        }
                    }
                }
            }

            // Remove any inactive panels
            for (int i = this.flowPanel.Controls.Count; i > 0; i--)
            {
                DiabloHostPanel panel = this.flowPanel.Controls[i - 1] as DiabloHostPanel;
                if (panel != null)
                {
                    if (panel.DiabloWindow == null || panel.DiabloWindow.Disposed)
                    {
                        this.flowPanel.Controls.Remove(panel);
                    }
                }
            }
        }

        public void Deactivate()
        {
            
        }
    }
}
