using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Vectrons_Library;

namespace System_Info
{
	public partial class SideBar : Form
	{
		private bool dragging = false;

		private Point dragCursorPoint;
		private Point dragFormPoint;

		private Font font;

		private Color backgroundColor;
		private Color borderColor;
		private Color textColor;

		public SideBar()
		{
			font = Properties.Settings.Default.Font;
			backgroundColor = ColorTranslator.FromHtml(Properties.Settings.Default.BackgroundColor);
			borderColor = ColorTranslator.FromHtml(Properties.Settings.Default.BorderColor);
			textColor = ColorTranslator.FromHtml(Properties.Settings.Default.TextColor);
			InitializeComponent();

			graphicalOverlay1.Owner = this;
		}

		protected override CreateParams CreateParams
		{
			get
			{
				var parameters = base.CreateParams;
				parameters.ExStyle |= 0x80;
				return parameters;
			}
		}

		private GraphicsPath FormGraphic
		{
			get
			{
				GraphicsPath p = new GraphicsPath();
				p.StartFigure();
				p.AddArc(new Rectangle(0, 0, 40, 40), 180, 90);
				p.AddLine(40, 0, Width - 40, 0);
				p.AddArc(new Rectangle(Width - 40, 0, 40, 40), -90, 90);
				p.AddLine(Width, 40, Width, Height - 40);
				p.AddArc(new Rectangle(Width - 40, Height - 40, 40, 40), 0, 90);
				p.AddLine(Width - 40, Height, 40, Height);
				p.AddArc(new Rectangle(0, Height - 40, 40, 40), 90, 90);
				p.CloseFigure();
				return p;
			}
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
				ctr.ForeColor = textColor;
				ctr.Font = font;
				ctr.BackColor = backgroundColor;
				SetControlFontAndBackground(ctr.Controls);
			}
		}

		private void SideBar_Load(object sender, EventArgs e)
		{
			FormGeometry.GeometryFromString(Properties.Settings.Default.WindowGeometry, this);
			DrawForm();
		}

		private void SideBar_Paint(object sender, PaintEventArgs e)
		{
			using (Pen borderPen = new Pen(new SolidBrush(borderColor), 2))
			{
				e.Graphics.DrawPath(borderPen, FormGraphic);
			}

			using (Pen matrixPen = new Pen(new SolidBrush(borderColor), 1))
			{
				for (int i = 0; i < flowLayoutPanel1.Controls.Count - 1; i++)
				{
					if (flowLayoutPanel1.Controls[i].GetType() == typeof(Panel))
					{
						Panel item = (Panel)flowLayoutPanel1.Controls[i];
						int y = item.Location.Y + item.Size.Height + 5;
						e.Graphics.DrawLine(matrixPen, 0, y, Width, y);
					}
				}
			}

			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

			ShowInTaskbar = false;
		}

		private void SideBar_FormClosing(object sender, FormClosingEventArgs e)
		{
			// persist our geometry string.
			Properties.Settings.Default.WindowGeometry = FormGeometry.GeometryToString(this);
			Properties.Settings.Default.Save();
		}

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

		private void DrawForm()
		{
			SuspendLayout();
			flowLayoutPanel1.Controls.Clear();
			flowLayoutPanel1.Padding = new Padding(10);

			Panel panelCpu = SystemInfo.CPUController.GetPanel();
			panelCpu.Margin = new Padding(0, 0, 0, 0);
			flowLayoutPanel1.Controls.Add(panelCpu);

			Panel panelMemory = SystemInfo.MemoryController.GetPanel();
			panelMemory.Margin = new Padding(0, 10, 0, 0);
			flowLayoutPanel1.Controls.Add(panelMemory);

			Panel panelNetwork = SystemInfo.NetworkController.GetPanel();
			panelNetwork.Margin = new Padding(0, 10, 0, 0);
			flowLayoutPanel1.Controls.Add(panelNetwork);

			Panel panelHdd = SystemInfo.DriveController.GetPanel();
			panelHdd.Margin = new Padding(0, 10, 0, 0);
			flowLayoutPanel1.Controls.Add(panelHdd);

			SetControlEventHandlers(Controls);
			SetControlFontAndBackground(Controls);

			ResumeLayout();
		}

		private void SideBar_SizeChanged(object sender, EventArgs e)
		{
			SuspendLayout();
			Region = new Region(FormGraphic);
			SetControlFontAndBackground(Controls);
			ResumeLayout();
		}
	}
}
