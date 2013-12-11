using System.Drawing;
using System.Windows.Forms;

namespace System_Info
{
    public partial class CtrCpu : UserControl
    {
        private ProgressBar[] PbCpuCore;
        private short _NumberOfLogicalProcessors;

        public CtrCpu(short NumberOfLogicalProcessors)
        {
            InitializeComponent();
            _NumberOfLogicalProcessors = NumberOfLogicalProcessors;
            CreateProgressbars();
        }

        public void CreateProgressbars()
        {
            PbCpuCore = new ProgressBar[_NumberOfLogicalProcessors];
            LblCpuUse.Text = @"00%";
            int x = 0;
            int y = LblCpuUse.Height + 2;
            short BarHeight = 0;

            if (System_Info.Properties.Settings.Default.CpuBarHeight < 1) { BarHeight = 4; }
            else { BarHeight = System_Info.Properties.Settings.Default.CpuBarHeight; }

            for (int i = 0; i < (int)_NumberOfLogicalProcessors; i++)
            {
                PbCpuCore[i] = new ProgressBar();
                PbCpuCore[i].Value = 0;
                PbCpuCore[i].Location = new Point(x, y);
                PbCpuCore[i].Size = new System.Drawing.Size(100, BarHeight);
                PbCpuCore[i].ForeColor = Color.White;
                Controls.Add(PbCpuCore[i]);
                y += PbCpuCore[i].Size.Height + 1;
            }

            this.Size = new Size(100, y - 1);
        }

        public void UpdateCtr(short[] LoadPercentageCore, short LoadTotal)
        {
            Global.SetControlPropertyThreadSafe(LblCpuUse, "Text", ((int)LoadTotal).ToString() + @"%");
            //Graphics g = LblCpuUse.CreateGraphics();
            //Global.SetControlPropertyThreadSafe(LblCpuUse, "Left", 100 - (int)g.MeasureString(LblCpuUse.Text, LblCpuUse.Font, 100).Width);
            //g.Dispose();
            for (int i = 0; i < _NumberOfLogicalProcessors; i++)
            {
                Global.SetControlPropertyThreadSafe(PbCpuCore[i], "Value", (float)LoadPercentageCore[i]);
            }
        }

        private void LblCpuUse_SizeChanged(object sender, System.EventArgs e)
        {
            int x = 0;
            int y = LblCpuUse.Height + 2;

            for (int i = 0; i < PbCpuCore.Length; i++)
            {
                PbCpuCore[i].Location = new Point(x, y);
                y += PbCpuCore[i].Size.Height + 1;
            }
        }
    }
}

