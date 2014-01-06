using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Trento_Library;

namespace System_Info
{

    public partial class SideBar : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        Panel panelCpu;
        private CtrCpu[] Cpu;
        Panel panelHdd;
        private CtrHdd[] Hdd;
        Panel panelHddTotal;
        private CtrHdd HddTotal;
        Panel panelMemory;
        private CtrRam Memory;
        Panel panelNetwork;
        private CtrNetworkTraffic[] Network;

        private int[] yTotal = new int[3];
        private Font font;

        private Color BackgroundColor;
        private Color BorderColor;
        private Color TextColor;

        public SideBar()
        {
            font = new System.Drawing.Font(System_Info.Properties.Settings.Default.Font, System_Info.Properties.Settings.Default.FontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            BackgroundColor = System.Drawing.ColorTranslator.FromHtml(System_Info.Properties.Settings.Default.BackgroundColor);
            BorderColor = System.Drawing.ColorTranslator.FromHtml(System_Info.Properties.Settings.Default.BorderColor);
            TextColor = System.Drawing.ColorTranslator.FromHtml(System_Info.Properties.Settings.Default.TextColor);

            InitializeComponent();
            CPUControls();
            MemoryControl();
            NetworkTrafficControl();
            HddControls();
            HddTotalSpace();
            SystemInfo.HDDRemovedAdded += new ChangedEventHandler(SystemInfo_HDDRemovedAdded);
        }

        #region Control Creation

        private void CPUControls()
        {
            int x = 0;
            int y = 0;
            panelCpu = new Panel();
            Cpu = new CtrCpu[(int)SystemInfo.ListCpuInfo.Count];

            for (int i = 0; i < (int)SystemInfo.ListCpuInfo.Count; i++)
            {
                Cpu[i] = new CtrCpu(SystemInfo.ListCpuInfo[i].NumberOfLogicalProcessors);
                Cpu[i].ForeColor = TextColor;
                Cpu[i].Font = font;
                Cpu[i].Location = new Point(x, y);
                Cpu[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                Cpu[i].MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                Cpu[i].MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
                panelCpu.Controls.Add(Cpu[i]);
                Cpu[i].Invalidate(true);
                y += Cpu[i].Height + 1;

                foreach (Control ctr in Cpu[i].Controls)
                {
                    ctr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                    ctr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                    ctr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
                }
            }
            panelCpu.Size = new System.Drawing.Size(Cpu[Cpu.Length - 1].Width, Cpu[Cpu.Length - 1].Location.Y + Cpu[Cpu.Length - 1].Height);
            SystemInfo.CPU_Changed += new ChangedEventHandler(CPUChanged);
        }
        private void MemoryControl()
        {
            int x = 0;
            int y = 0;
            panelMemory = new Panel();
            Memory = new CtrRam();

            Memory.Location = new Point(x, y);
            Memory.ForeColor = TextColor;
            Memory.Font = font;
            Memory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
            Memory.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
            Memory.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
            panelMemory.Controls.Add(Memory);
            SystemInfo.Ram_Changed += new ChangedEventHandler(MemoryChanged);

            y += Memory.Height + 5;

            foreach (Control ctr in Memory.Controls)
            {
                ctr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                ctr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                ctr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
            }
            panelMemory.Size = new System.Drawing.Size(Memory.Width, Memory.Location.Y + Memory.Height);
        }
        private void NetworkTrafficControl()
        {
            int x = 0;
            int y = 0;
            panelNetwork = new Panel();

            Network = new CtrNetworkTraffic[SystemInfo.ListNetworkStatistics.Count];

            for (int i = 0; i < SystemInfo.ListNetworkStatistics.Count; i++)
            {
                Network[i] = new CtrNetworkTraffic();
                Network[i].InterfaceName = SystemInfo.ListNetworkStatistics[i].Name;
                Network[i].UpdateValue(SystemInfo.ListNetworkStatistics[i].TrafficSentKBSec, SystemInfo.ListNetworkStatistics[i].TrafficReceivedKBSec);
                Network[i].Location = new Point(x, y);
                Network[i].ForeColor = TextColor;
                Network[i].Font = font;
                Network[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                Network[i].MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                Network[i].MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);

                foreach (Control ctr in Network[i].Controls)
                {
                    ctr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                    ctr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                    ctr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
                }

                y = Network[i].Height + 10;
                panelNetwork.Controls.Add(Network[i]);
                Network[i].Invalidate();
            }
            panelNetwork.Size = new System.Drawing.Size(Network[Network.Length - 1].Width, Network[Network.Length - 1].Location.Y + Network[Network.Length - 1].Height);
            SystemInfo.Network_Changed += new ChangedEventHandler(RxTxChanged);
        }
        private void HddControls()
        {
            int x = 0;
            int y = 0;
            panelHdd = new Panel();

            Hdd = new CtrHdd[SystemInfo.ListHddInfo.Count];

            for (int i = 0; i < SystemInfo.ListHddInfo.Count; i++)
            {
                Hdd[i] = new CtrHdd();
                Hdd[i].HddName = SystemInfo.ListHddInfo[i].Name;
                Hdd[i].UpdateValue(SystemInfo.ListHddInfo[i].AvailableFreeSpace, SystemInfo.ListHddInfo[i].TotalSize);
                Hdd[i].Location = new Point(x, y);
                Hdd[i].ForeColor = TextColor;
                Hdd[i].Font = font;
                Hdd[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                Hdd[i].MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                Hdd[i].MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);

                foreach (Control ctr in Hdd[i].Controls)
                {
                    ctr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                    ctr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                    ctr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
                }

                panelHdd.Controls.Add(Hdd[i]);
                y += Hdd[i].Height;
            }
            panelHdd.Size = new System.Drawing.Size(Hdd[Hdd.Length - 1].Width, Hdd[Hdd.Length - 1].Location.Y + Hdd[Hdd.Length - 1].Height);
            SystemInfo.HDD_Changed += new ChangedEventHandler(HDDChanged);
        }
        private void HddTotalSpace()
        {
            int x = 0;
            int y = 0;
            panelHddTotal = new Panel();
            HddTotal = new CtrHdd();

            HddTotal.HddName = "Total:";
            long TotalFreeSpace = 0;
            long AvailibleFreeSpace = 0;
            foreach (HDD drive in SystemInfo.ListHddInfo)
            {
                if (drive.HddType == System.IO.DriveType.Fixed)
                {
                    TotalFreeSpace += drive.TotalSize;
                    AvailibleFreeSpace += drive.AvailableFreeSpace;
                }
            }
            HddTotal.UpdateValue(AvailibleFreeSpace, TotalFreeSpace);
            HddTotal.Location = new Point(x, y);
            HddTotal.ForeColor = TextColor;
            HddTotal.Font = font;
            HddTotal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
            HddTotal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
            HddTotal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);

            foreach (Control ctr in HddTotal.Controls)
            {
                ctr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                ctr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                ctr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
            }

            panelHddTotal.Controls.Add(HddTotal);
            y += HddTotal.Height + 1;
            panelHddTotal.Size = new System.Drawing.Size(HddTotal.Width, HddTotal.Location.Y + HddTotal.Height);
        }

        #endregion Control Creation

        #region Changed Eventhandlers

        private void CPUChanged()
        {
            for (int i = 0; i < Cpu.Length; i++)
            {

                Cpu[i].UpdateCtr(SystemInfo.ListCpuInfo[i].LoadPercentageCore, Convert.ToInt16(SystemInfo.ListCpuInfo[i].CpuTotalUse.NextValue()));
            }
        }
        private void MemoryChanged()
        {
            Memory.UpdateValue(SystemInfo.Ram.AvailableRam, SystemInfo.Ram.TotalVisibleMemorySize);
        }
        private void HDDChanged()
        {
            for (int i = 0; i < Hdd.Length; i++)
            {
                Hdd[i].HddName = SystemInfo.ListHddInfo[i].Name;
                Hdd[i].UpdateValue(SystemInfo.ListHddInfo[i].AvailableFreeSpace, SystemInfo.ListHddInfo[i].TotalSize);
            }
            long TotalFreeSpace = 0;
            long AvailibleFreeSpace = 0;
            foreach (HDD drive in SystemInfo.ListHddInfo)
            {
                if (drive.HddType == System.IO.DriveType.Fixed)
                {
                    TotalFreeSpace += drive.TotalSize;
                    AvailibleFreeSpace += drive.AvailableFreeSpace;
                }
            }
            HddTotal.UpdateValue(AvailibleFreeSpace, TotalFreeSpace);
        }
        private void RxTxChanged()
        {
            for (int i = 0; i < Network.Length; i++)
            {
                Network[i].InterfaceName = SystemInfo.ListNetworkStatistics[i].Name;
                Network[i].UpdateValue(SystemInfo.ListNetworkStatistics[i].TrafficSentKBSec, SystemInfo.ListNetworkStatistics[i].TrafficReceivedKBSec);
            }
        }

        private void SystemInfo_HDDRemovedAdded()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    SystemInfo_HDDRemovedAdded();
                });
            }
            else
            {
                HddTotal.Dispose();
                foreach (CtrHdd hdd in Hdd)
                {
                    hdd.Dispose();
                }

                HddControls();
                HddTotalSpace();
                DrawForm();
            }
        }

        #endregion Changed Eventhandlers

        #region Eventhandlers

        private void SideBar_Load(object sender, EventArgs e)
        {
            FormGeometry.GeometryFromString(System_Info.Properties.Settings.Default.WindowGeometry, this);
            DrawForm();
        }

        private void SideBar_Paint(object sender, PaintEventArgs e)
        {
            this.BackColor = BackgroundColor;
            System.Drawing.Pen borderPen = new System.Drawing.Pen(new SolidBrush(BorderColor), 2);
            System.Drawing.Pen MatrixPen = new System.Drawing.Pen(new SolidBrush(BorderColor), 1);
            e.Graphics.DrawPath(borderPen, FormGraphic);
            for (int i = 0; i < yTotal.Length; i++)
            {
                e.Graphics.DrawLine(MatrixPen, 0, yTotal[i], this.Width, yTotal[i]);
            }
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            borderPen.Dispose();
            MatrixPen.Dispose();
        }
        private void SideBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemInfo.CPU_Changed -= CPUChanged;
            SystemInfo.Ram_Changed -= MemoryChanged;
            SystemInfo.Network_Changed -= RxTxChanged;
            SystemInfo.HDD_Changed -= HDDChanged;

            //persist our geometry string.
            System_Info.Properties.Settings.Default.WindowGeometry = FormGeometry.GeometryToString(this);
            System_Info.Properties.Settings.Default.Save();
        }
        #endregion Eventhandlers

        #region Control verplaatsen
        private void CtrMouseDown(object sender, MouseEventArgs e)
        {
            if (System_Info.Properties.Settings.Default.LockPlacement == false)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        private void CtrMouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void CtrMouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        #endregion Control verplaatsen

        private GraphicsPath FormGraphic
        {
            get
            {
                GraphicsPath p = new GraphicsPath();
                p.StartFigure();
                p.AddArc(new Rectangle(0, 0, 40, 40), 180, 90);
                p.AddLine(40, 0, this.Width - 40, 0);
                p.AddArc(new Rectangle(this.Width - 40, 0, 40, 40), -90, 90);
                p.AddLine(this.Width, 40, this.Width, this.Height - 40);
                p.AddArc(new Rectangle(this.Width - 40, this.Height - 40, 40, 40), 0, 90);
                p.AddLine(this.Width - 40, this.Height, 40, this.Height);
                p.AddArc(new Rectangle(0, this.Height - 40, 40, 40), 90, 90);
                p.CloseFigure();
                return p;
            }
        }

        private void DrawForm()
        {
            Controls.Clear();
            int x = 10;
            int y = 10;
            int PanelSpacing = 5;

            panelCpu.Location = new Point(x, y);
            panelCpu.Width = 100;
            Controls.Add(panelCpu);
            y += panelCpu.Height + PanelSpacing;
           
            yTotal[0] = y;
            y += 5;

            panelMemory.Location = new Point(x, y);
            panelMemory.Width = 100;
            Controls.Add(panelMemory);
            y += panelMemory.Height + PanelSpacing;

            yTotal[1] = y;
            y += 5;

            panelNetwork.Location = new Point(x, y);
            panelNetwork.Width = 100;
            Controls.Add(panelNetwork);
            y += panelNetwork.Height + PanelSpacing;

            yTotal[2] = y;
            y += 5;

            panelHdd.Location = new Point(x, y);
            panelHdd.Width = 100;
            Controls.Add(panelHdd);
            y += panelHdd.Height;

            panelHddTotal.Location = new Point(x, y);
            panelHddTotal.Width = 100;
            Controls.Add(panelHddTotal);
            y += panelHddTotal.Height;

            this.ClientSize = new Size(120, y + 10);
            this.Region = new Region(FormGraphic);
            this.Invalidate();
        }
    }
}
