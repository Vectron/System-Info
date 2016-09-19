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

		private Font font;

		private Color BackgroundColor;
		private Color BorderColor;
		private Color TextColor;

		public SideBar()
		{
			font = Properties.Settings.Default.Font;
			BackgroundColor = ColorTranslator.FromHtml(Properties.Settings.Default.BackgroundColor);
			BorderColor = ColorTranslator.FromHtml(Properties.Settings.Default.BorderColor);
			TextColor = ColorTranslator.FromHtml(Properties.Settings.Default.TextColor);
			InitializeComponent();

			graphicalOverlay1.Owner = this;
		}

		private void SetControlEventHandlers(Control.ControlCollection coll)
		{
			foreach (Control ctr in coll)
			{
				ctr.MouseDown += new MouseEventHandler(CtrMouseDown);
				ctr.MouseMove += new MouseEventHandler(CtrMouseMove);
				ctr.MouseUp += new MouseEventHandler(CtrMouseUp);
				SetControlEventHandlers(ctr.Controls);
			}
		}

		private void SetControlFontAndBackground(Control.ControlCollection coll)
		{
			foreach (Control ctr in coll)
			{
				ctr.ForeColor = TextColor;
				ctr.Font = font;
				ctr.BackColor = BackgroundColor;
				SetControlFontAndBackground(ctr.Controls);
			}
		}

		#region Eventhandlers
		private void SideBar_Load(object sender, EventArgs e)
		{
			FormGeometry.GeometryFromString(Properties.Settings.Default.WindowGeometry, this);
			DrawForm();
		}

		private void SideBar_Paint(object sender, PaintEventArgs e)
		{
			using (Pen borderPen = new Pen(new SolidBrush(BorderColor), 2))
			{
				e.Graphics.DrawPath(borderPen, FormGraphic);
			}

			using (Pen MatrixPen = new Pen(new SolidBrush(BorderColor), 1))
			{
				for (int i = 0; i < flowLayoutPanel1.Controls.Count - 1; i++)
				{
					if (flowLayoutPanel1.Controls[i].GetType() == typeof(Panel))
					{
						Panel item = (Panel)flowLayoutPanel1.Controls[i];
						int y = item.Location.Y + item.Size.Height + 5;
						e.Graphics.DrawLine(MatrixPen, 0, y, Width, y);
					}
				}
			}

			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

			ShowInTaskbar = false;
		}

		private void SideBar_FormClosing(object sender, FormClosingEventArgs e)
		{
			//persist our geometry string.
			Properties.Settings.Default.WindowGeometry = FormGeometry.GeometryToString(this);
			Properties.Settings.Default.Save();
		}
		#endregion Eventhandlers

		#region Control verplaatsen
		private void CtrMouseDown(object sender, MouseEventArgs e)
		{
			if (Properties.Settings.Default.LockPlacement == false)
			{
				dragging = true;
				dragCursorPoint = Cursor.Position;
				dragFormPoint = Location;
			}
		}

		private void CtrMouseMove(object sender, MouseEventArgs e)
		{
			if (dragging)
			{
				Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
				Location = Point.Add(dragFormPoint, new Size(dif));
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
			this.SuspendLayout();
			flowLayoutPanel1.Controls.Clear();
			flowLayoutPanel1.Padding = new Padding(10);

			Panel panelCpu = SystemInfo.CPUController.getPanel();
			panelCpu.Margin = new Padding(0, 0, 0, 0);
			flowLayoutPanel1.Controls.Add(panelCpu);

			Panel panelMemory = SystemInfo.MemoryController.getPanel();
			panelMemory.Margin = new Padding(0, 10, 0, 0);
			flowLayoutPanel1.Controls.Add(panelMemory);

			Panel panelNetwork = SystemInfo.NetworkController.getPanel();
			panelNetwork.Margin = new Padding(0, 10, 0, 0);
			flowLayoutPanel1.Controls.Add(panelNetwork);

			Panel panelHdd = SystemInfo.DriveController.getPanel();
			panelHdd.Margin = new Padding(0, 10, 0, 0);
			flowLayoutPanel1.Controls.Add(panelHdd);

			SetControlEventHandlers(Controls);
			SetControlFontAndBackground(Controls);

			this.ResumeLayout();
		}

		protected override CreateParams CreateParams
		{
			get
			{
				var Params = base.CreateParams;
				Params.ExStyle |= 0x80;
				return Params;
			}
		}

		private void SideBar_SizeChanged(object sender, EventArgs e)
		{
			this.SuspendLayout();
			Region = new Region(FormGraphic);
			SetControlFontAndBackground(Controls);
			this.ResumeLayout();
		}
	}
}
