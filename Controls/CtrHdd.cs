using System;
using System.Drawing;
using System.Windows.Forms;
using Trento_Library;

namespace System_Info
{
    public partial class CtrHdd : UserControl
    {
        public string HddName { set { LblHddName.Text = value; } }

        public CtrHdd()
        {
            InitializeComponent();
            short BarHeight = 0;

            if (System_Info.Properties.Settings.Default.HddBarHeight < 1)
            {
                BarHeight = 5;
            }
            else
            {
                BarHeight = System_Info.Properties.Settings.Default.CpuBarHeight;
            }
            PbFreeSpace.Height = BarHeight;
        }

        public void UpdateValue(long AvailableFreeSpace, long TotalSize)
        {
            float PercentFreeSpace = ((float)AvailableFreeSpace / (float)TotalSize) * 100;
            PercentFreeSpace = (float)Math.Truncate(PercentFreeSpace * 100) / 100;
            Global.SetControlPropertyThreadSafe(LblFreeSpace, "Text", Global.formatBytes(AvailableFreeSpace, 0) + "  " + ((int)PercentFreeSpace).ToString() + @"%");
            Global.SetControlPropertyThreadSafe(PbFreeSpace, "Value", 100 - PercentFreeSpace);
           // Global.SetControlPropertyThreadSafe(LblFreeSpace, "Location", new Point(100 - LblFreeSpace.Width, 0));
        }

        private void LblFreeSpace_SizeChanged(object sender, EventArgs e)
        {
            PbFreeSpace.Location = new Point(0, LblFreeSpace.Height + 2);
        }
    }
}
