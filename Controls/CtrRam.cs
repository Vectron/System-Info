using System;
using System.Drawing;
using System.Windows.Forms;

namespace System_Info
{
    public partial class CtrRam : UserControl
    {
        public CtrRam()
        {
            InitializeComponent();
            short BarHeight = 0;
            
            if (System_Info.Properties.Settings.Default.CpuBarHeight < 1) { BarHeight = 5; }
            else { BarHeight = System_Info.Properties.Settings.Default.RamBarHeight; }
            PbFreeRam.Size = new System.Drawing.Size(100, BarHeight);
        }

        public void UpdateValue(uint AvailibleRam, uint TotalRam)
        {
            float PercentUseRam = ((float)(TotalRam - AvailibleRam) / TotalRam) * 100;
            PercentUseRam = (float)Math.Truncate(PercentUseRam * 100) / 100;
            Global.SetControlPropertyThreadSafe(LblRamUsed, "Text", ((int)PercentUseRam).ToString() + @"%");
            Global.SetControlPropertyThreadSafe(PbFreeRam, "Value", PercentUseRam);
            Global.SetControlPropertyThreadSafe(LblFreeRam, "Text", "RAM: " + Global.formatBytes((TotalRam - AvailibleRam), 1));
            Graphics g = LblRamUsed.CreateGraphics();
            Global.SetControlPropertyThreadSafe(LblRamUsed,
                "Left",
                100 - (int)g.MeasureString(LblRamUsed.Text,
                LblRamUsed.Font,
                100 - (LblFreeRam.Width)).Width);
            g.Dispose();
        }

        private void LblRamUsed_SizeChanged(object sender, EventArgs e)
        {
            PbFreeRam.Location = new Point(0, LblRamUsed.Height + 2);
        }
    }
}
