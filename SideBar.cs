using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;


namespace System_Info
{
   
    public partial class SideBar : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private CtrCpu[] Cpu;
        private CtrHdd[] Hdd;
        private CtrHdd HddTotal;
        private CtrRam Memory;
        private CtrNetworkTraffic[] Network;

        private int x = 10;
        private int y = 10;
        private int[] y3 = new int[3];
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
        }

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

        #region Control Creation

        private void CPUControls()
        {
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
                Cpu[i].MouseDoubleClick += new MouseEventHandler(SideBar_MouseDoubleClick);
                Controls.Add(Cpu[i]);
                Cpu[i].Invalidate(true);
                y += Cpu[i].Height + 10;
                y3[0] = y - 5;

                foreach (Control ctr in Cpu[i].Controls)
                {
                    ctr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                    ctr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                    ctr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
                    ctr.MouseDoubleClick += new MouseEventHandler(SideBar_MouseDoubleClick);
                }
            }
            SystemInfo.CPU_Changed += new ChangedEventHandler(CPUChanged);
        }
        private void MemoryControl()
        {
            Memory = new CtrRam();

            Memory.Location = new Point(x, y);
            Memory.ForeColor = TextColor;
            Memory.Font = font;
            Memory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
            Memory.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
            Memory.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
            Memory.MouseDoubleClick += new MouseEventHandler(SideBar_MouseDoubleClick);
            Controls.Add(Memory);
            SystemInfo.Ram_Changed += new ChangedEventHandler(MemoryChanged);

            y += Memory.Height + 10;
            y3[1] = y - 5;

            foreach (Control ctr in Memory.Controls)
            {
                ctr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                ctr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                ctr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
                ctr.MouseDoubleClick += new MouseEventHandler(SideBar_MouseDoubleClick);
            }
        }
        private void NetworkTrafficControl()
        {
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
                Network[i].MouseDoubleClick += new MouseEventHandler(SideBar_MouseDoubleClick);

                foreach (Control ctr in Network[i].Controls)
                {
                    ctr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                    ctr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                    ctr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
                    ctr.MouseDoubleClick += new MouseEventHandler(SideBar_MouseDoubleClick);
                }

                Controls.Add(Network[i]);
                Network[i].Invalidate();
                y += Network[i].Height + 12;
                y3[2] = y - 5;
            }
            SystemInfo.Network_Changed += new ChangedEventHandler(RxTxChanged);
        }
        private void HddControls()
        {
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
                Hdd[i].MouseDoubleClick += new MouseEventHandler(SideBar_MouseDoubleClick);

                foreach (Control ctr in Hdd[i].Controls)
                {
                    ctr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                    ctr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                    ctr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
                    ctr.MouseDoubleClick += new MouseEventHandler(SideBar_MouseDoubleClick);
                }

                Controls.Add(Hdd[i]);
                y += Hdd[i].Height + 1;
            }
            SystemInfo.HDD_Changed += new ChangedEventHandler(HDDChanged);
        }
        private void HddTotalSpace()
        {
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
            HddTotal.MouseDoubleClick += new MouseEventHandler(SideBar_MouseDoubleClick);


            foreach (Control ctr in HddTotal.Controls)
            {
                ctr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CtrMouseDown);
                ctr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CtrMouseMove);
                ctr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CtrMouseUp);
                ctr.MouseDoubleClick += new MouseEventHandler(SideBar_MouseDoubleClick);
            }

            Controls.Add(HddTotal);
            y += HddTotal.Height + 1;
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

        #endregion Changed Eventhandlers

        #region Eventhandlers

        private void SideBar_Load(object sender, EventArgs e)
        {
            GeometryFromString(System_Info.Properties.Settings.Default.WindowGeometry, this);
            this.ClientSize = new Size(120, y + 10);
            this.Region = new Region(FormGraphic);
        }

        private void SideBar_Paint(object sender, PaintEventArgs e)
        {
            
            this.BackColor = BackgroundColor;
            System.Drawing.Pen borderPen = new System.Drawing.Pen(new SolidBrush(BorderColor), 2);
            System.Drawing.Pen MatrixPen = new System.Drawing.Pen(new SolidBrush(BorderColor), 1);
            e.Graphics.DrawPath(borderPen, FormGraphic);
            for (int i = 0; i < y3.Length; i++)
            {
                e.Graphics.DrawLine(MatrixPen, 0, y3[i], this.Width, y3[i]);
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
            System_Info.Properties.Settings.Default.WindowGeometry = GeometryToString(this);
            System_Info.Properties.Settings.Default.Save();
        }
        private void SideBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < Cpu.Length; i++)
            {
                Controls.Remove(Cpu[i]);
                Cpu[i].Dispose();
                SystemInfo.CPU_Changed -= new ChangedEventHandler(CPUChanged);
                Controls.Add(Cpu[i]);
            }
            
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

        #region Methods for saving Form Geometry

        public static void GeometryFromString(string thisWindowGeometry, Form formIn)
        {
            if (string.IsNullOrEmpty(thisWindowGeometry) == true)
            {
                return;
            }
            string[] numbers = thisWindowGeometry.Split('|');
            string windowString = numbers[4];
            if (windowString == "Normal")
            {
                Point windowPoint = new Point(int.Parse(numbers[0]),
                    int.Parse(numbers[1]));
                Size windowSize = new Size(int.Parse(numbers[2]),
                    int.Parse(numbers[3]));

                bool locOkay = GeometryIsBizarreLocation(windowPoint, windowSize);
                bool sizeOkay = GeometryIsBizarreSize(windowSize);

                if (locOkay == true && sizeOkay == true)
                {
                    formIn.Location = windowPoint;
                    formIn.Size = windowSize;
                    formIn.StartPosition = FormStartPosition.Manual;
                    formIn.WindowState = FormWindowState.Normal;
                }
                else if (sizeOkay == true)
                {
                    formIn.Size = windowSize;
                }
            }
            else if (windowString == "Maximized")
            {
                formIn.Location = new Point(100, 100);
                formIn.StartPosition = FormStartPosition.Manual;
                formIn.WindowState = FormWindowState.Maximized;
            }
        }
        private static bool GeometryIsBizarreLocation(Point loc, Size size)
        {
            bool locOkay;
            int desktop_X = 0;
            int desktop_Y = 0;
            int desktop_width = 0;
            int desktop_height = 0;

            desktop_width = Screen.PrimaryScreen.WorkingArea.Width;
            desktop_height = Screen.PrimaryScreen.WorkingArea.Height;

            foreach (Screen scherm in Screen.AllScreens)
            {

                if (scherm.WorkingArea.X < desktop_X)
                {
                    desktop_X = scherm.WorkingArea.X;
                }

                if (scherm.WorkingArea.Y < desktop_Y)
                {
                    desktop_Y = scherm.WorkingArea.Y;
                }

                if (scherm.Primary == false)
                {
                    if (scherm.WorkingArea.X >= desktop_width)
                    {
                        desktop_width += scherm.WorkingArea.Width;
                    }
                    if (scherm.WorkingArea.Y >= desktop_height)
                    {
                        desktop_height += scherm.WorkingArea.Height;
                    }
                }
            }

            if (loc.X < desktop_X || loc.Y < desktop_Y)
            {
                locOkay = false;
            }

            else if (loc.X + size.Width > desktop_width)
            {
                locOkay = false;
            }
            else if (loc.Y + size.Height > desktop_height)
            {
                locOkay = false;
            }
            else
            {
                locOkay = true;
            }
            return locOkay;
        }
        private static bool GeometryIsBizarreSize(Size size)
        {
            return (size.Height <= Screen.PrimaryScreen.WorkingArea.Height &&
                size.Width <= Screen.PrimaryScreen.WorkingArea.Width);
        }
        public static string GeometryToString(Form mainForm)
        {
            return mainForm.Location.X.ToString() + "|" +
                mainForm.Location.Y.ToString() + "|" +
                mainForm.Size.Width.ToString() + "|" +
                mainForm.Size.Height.ToString() + "|" +
                mainForm.WindowState.ToString();
        }

        #endregion Methods for saving Form Geometry
    }
}
