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
        }


        #region CPUBar
        private void CPUScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            CPUTextBox.Text = CPUScrollBar.Value.ToString();
        }

        private void CPUTextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(CPUTextBox.Text) < 1 || int.Parse(CPUTextBox.Text) > CPUScrollBar.Maximum)
            {
            }
            else
            {
                CPUScrollBar.Value = int.Parse(CPUTextBox.Text);
            }
        }

        private void CPUScrollBar_ValueChanged(object sender, EventArgs e)
        {
            CPUProgressBar.Height = CPUScrollBar.Value;
            CPUProgressBar.Invalidate();
        }

        private void CPUTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
                        int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }
        #endregion

        #region RamBar
        private void RamScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            RamTextBox.Text = RamScrollBar.Value.ToString();
        }

        private void RamTextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(RamTextBox.Text) < 1 || int.Parse(RamTextBox.Text) > RamScrollBar.Maximum)
            {
            }
            else
            {
                RamScrollBar.Value = int.Parse(RamTextBox.Text);
            }
        }

        private void RamScrollBar_ValueChanged(object sender, EventArgs e)
        {
            RamProgressBar.Height = RamScrollBar.Value;
            RamProgressBar.Invalidate();
        }

        private void RamTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }
        #endregion

        #region HddBar
        private void HddScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            HddTextBox.Text = HddScrollBar.Value.ToString();
        }

        private void HddTextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(HddTextBox.Text) < 1 || int.Parse(HddTextBox.Text) > HddScrollBar.Maximum)
            {
            }
            else
            {
                HddScrollBar.Value = int.Parse(HddTextBox.Text);
            }
        }

        private void HddScrollBar_ValueChanged(object sender, EventArgs e)
        {
            HddProgressBar.Height = HddScrollBar.Value;
            HddProgressBar.Invalidate();
        }

        private void HddTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }
        #endregion


    }
}
