using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace System_Info
{
    public partial class Options_Screen : Form
    {
        public Options_Screen()
        {
            InitializeComponent();
            progbarCPU.Value = 100;
            progbarHDD.Value = 100;
            progbarRAM.Value = 100;

            progbarCPU.BackColor = Color.Black;
            progbarHDD.BackColor = Color.Black;
            progbarRAM.BackColor = Color.Black;
            ResetToDefault();
        }

        private void scrollbarHSize_ValueChanged(object sender, EventArgs e)
        {
            ScrollBar bar = ((ScrollBar)sender);
            string type = bar.Name.Substring(bar.Name.Length - 3, 3);
            if (bar.Value > bar.Minimum || ((ScrollBar)sender).Value < bar.Maximum)
            {
                this.Controls.Find("txtboxHSize" + type, true)[0].Text = bar.Value.ToString();
                this.Controls.Find("progbar" + type, true)[0].Width = bar.Value;
                this.Controls.Find("progbar" + type, true)[0].Invalidate();
            }
        }

        private void txtboxHSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtbox = ((TextBox)sender);
            string type = txtbox.Name.Substring(txtbox.Name.Length - 3, 3);
            ScrollBar bar = ((ScrollBar)this.Controls.Find("scrollbarHSize" + type, true)[0]);


            if (Char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == (char)Keys.Return)
            {
                if (int.Parse(txtbox.Text) < bar.Minimum)
                {
                    bar.Value = bar.Minimum;
                }
                else if (int.Parse(txtbox.Text) > bar.Maximum)
                {
                    bar.Value = bar.Maximum;
                }
                else
                {
                    bar.Value = int.Parse(txtbox.Text);
                }
                return;
            }
            if (Char.IsControl(e.KeyChar)) return;

            e.Handled = true;
        }

        private void scrollbarVSize_ValueChanged(object sender, EventArgs e)
        {
            ScrollBar bar = ((ScrollBar)sender);
            string type = bar.Name.Substring(bar.Name.Length - 3, 3);
            if (bar.Value > bar.Minimum || ((ScrollBar)sender).Value < bar.Maximum)
            {
                this.Controls.Find("txtboxVSize" + type, true)[0].Text = bar.Value.ToString();
                this.Controls.Find("progbar" + type, true)[0].Height = bar.Value;
                this.Controls.Find("progbar" + type, true)[0].Invalidate();
            }
        }

        private void txtboxVSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtbox = ((TextBox)sender);
            string type = txtbox.Name.Substring(txtbox.Name.Length - 3, 3);
            ScrollBar bar = ((ScrollBar)this.Controls.Find("scrollbarVSize" + type, true)[0]);


            if (Char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar == (char)Keys.Return)
            {
                if (int.Parse(txtbox.Text) < bar.Minimum)
                {
                    bar.Value = bar.Minimum;
                }
                else if (int.Parse(txtbox.Text) > bar.Maximum)
                {
                    bar.Value = bar.Maximum;
                }
                else
                {
                    bar.Value = int.Parse(txtbox.Text);
                }
                return;
            }
            if (Char.IsControl(e.KeyChar)) return;

            e.Handled = true;
        }

        private void ResetToDefault()
        {
            scrollbarVSizeCPU.Value = 4;
            scrollbarVSizeRAM.Value = 5;
            scrollbarVSizeHDD.Value = 5;
            scrollbarHSizeCPU.Value = 100;
            scrollbarHSizeRAM.Value = 100;
            scrollbarHSizeHDD.Value = 100;
        }

        private void btnResetDefaults_Click(object sender, EventArgs e)
        {
            ResetToDefault();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            System_Info.Properties.Settings.Default.CpuBarHeight = (short)scrollbarVSizeCPU.Value;
            System_Info.Properties.Settings.Default.RamBarHeight = (short)scrollbarVSizeRAM.Value;
            System_Info.Properties.Settings.Default.HddBarHeight = (short)scrollbarVSizeHDD.Value;

            System_Info.Properties.Settings.Default.CpuBarWidth = (short)scrollbarHSizeCPU.Value;
            System_Info.Properties.Settings.Default.RamBarWidth = (short)scrollbarHSizeRAM.Value;
            System_Info.Properties.Settings.Default.HddBarWidth = (short)scrollbarHSizeHDD.Value;
        }
    }
}
