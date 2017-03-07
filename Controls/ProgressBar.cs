using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace System_Info
{
	public partial class ProgressBar : UserControl
	{
		private const short NumberOfBlocks = 20;
		private const short Multiplyer = 1;
		private const short WidthSpacing = 1;
		private static Brush colorBrush;
		private static SolidBrush backgroundBrush = new SolidBrush(Color.Gray);
		private float percent = 0.0f;
		private short totalWidthBlocks = 10;
		private short widthBlock = 9;
		private short width;
		private short fullBlocks;
		private Color[] colors =
		{
			Color.LawnGreen,
			Color.LawnGreen,
			Color.LawnGreen,
			Color.LawnGreen,
			Color.Yellow,
			Color.Gold,
			Color.Orange,
			Color.DarkOrange,
			Color.OrangeRed,
			Color.Red,
			Color.DarkRed
		};

		public ProgressBar()
		{
			InitializeComponent();
		}

		public bool ShowPercentage
		{
			get { return label1.Visible; }
			set { label1.Visible = value; }
		}

		public float Value
		{
			get
			{
				return percent;
			}

			set
			{
				// Maintain the Value between 0 and 100
				if (value < 0)
				{
					value = 0;
				}
				else if (value > 100)
				{
					value = 100;
				}

				if (percent != value)
				{
					percent = value;
					label1.Invoke(() => label1.Text = percent.ToString());
					Invalidate();
				}
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			int varX = 0;
			for (int i = 0; i < NumberOfBlocks; i++)
			{
				e.Graphics.FillRectangle(backgroundBrush, varX, 0, widthBlock, Height);
				varX += totalWidthBlocks;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			width = (short)((percent / 100) * Width);
			fullBlocks = (short)((width + WidthSpacing) / totalWidthBlocks);

			if (fullBlocks > NumberOfBlocks)
			{
				fullBlocks = NumberOfBlocks;
			}

			int varX = 0;
			short color1 = 0;
			short color2 = 0;
			for (int i = 0; i < fullBlocks; i++)
			{
				if (i % 2 == 0 && i != 0)
				{
					color2 = color1;
				}

				colorBrush = new LinearGradientBrush(
					new Rectangle(varX, 0, totalWidthBlocks * Multiplyer, Height),
					colors[color1],
					colors[color2],
					LinearGradientMode.Horizontal);

				e.Graphics.FillRectangle(colorBrush, varX, 0, widthBlock, Height);

				varX += totalWidthBlocks;

				color1 = color2;
				color2 += 1;
			}

			base.OnPaint(e);
		}

		private void ProgressBar_SizeChanged(object sender, System.EventArgs e)
		{
			totalWidthBlocks = (short)((Width + WidthSpacing) / NumberOfBlocks);
			widthBlock = (short)(totalWidthBlocks - WidthSpacing);
			float fontsize = Height < 1 ? 1 : Height;
			label1.Font = new Font("Microsoft Sans Serif", fontsize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
		}
	}
}
