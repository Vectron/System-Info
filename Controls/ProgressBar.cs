using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Trento_Library;

namespace System_Info
{

	public partial class ProgressBar : UserControl
	{
		private const short NumberOfBlocks = 20;
		private const short multiplyer = 1;
		private const short WidthSpacing = 1;
		private short TotalWidthBlocks = 10;
		private short WidthBlock = 9;
		private short width;
		private short FullBlocks;
		protected float percent = 0.0f;
		public bool ShowPercentage { get { return label1.Visible; } set { label1.Visible = value; } }
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
					value = 0;
				else if (value > 100)
					value = 100;
				if (percent != value)
				{
					percent = value;
					TrentoGlobal.SetControlPropertyThreadSafe(label1, "Text", percent.ToString());
					Invalidate();
				}
			}
		}
		private static Brush ColorBrush;
		private static SolidBrush BackgroundBrush = new SolidBrush(Color.Gray);
		private Color[] Colors = {  Color.LawnGreen,
									 Color.LawnGreen,
									 Color.LawnGreen,
									 Color.LawnGreen,
									 Color.Yellow,
									 Color.Gold,
									 Color.Orange,
									 Color.DarkOrange,
									 Color.OrangeRed,
									 Color.Red,
									 Color.DarkRed };

		public ProgressBar()
		{
			InitializeComponent();
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			int VarX = 0;
			for (int i = 0; i < NumberOfBlocks; i++)
			{
				e.Graphics.FillRectangle(BackgroundBrush, VarX, 0, WidthBlock, Height);
				VarX += TotalWidthBlocks;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			width = (short)((percent / 100) * Width);
			FullBlocks = (short)((width + WidthSpacing) / (TotalWidthBlocks));

			if (FullBlocks > NumberOfBlocks)
			{
				FullBlocks = NumberOfBlocks;
			}

			int VarX = 0;
			short color1 = 0;
			short color2 = 0;
			for (int i = 0; i < FullBlocks; i++)
			{
				if (i % 2 == 0 && i != 0)
				{
					color2 = color1;
				}
				ColorBrush = new LinearGradientBrush(
					new Rectangle(VarX, 0, (TotalWidthBlocks * multiplyer), Height),
					Colors[color1],
					Colors[color2],
					LinearGradientMode.Horizontal);

				e.Graphics.FillRectangle(ColorBrush, VarX, 0, WidthBlock, Height);

				VarX += TotalWidthBlocks;

				color1 = color2;
				color2 += 1;
			}
			base.OnPaint(e);
		}

		private void ProgressBar_SizeChanged(object sender, System.EventArgs e)
		{
			TotalWidthBlocks = (short)((Width + WidthSpacing) / NumberOfBlocks);
			WidthBlock = (short)(TotalWidthBlocks - (WidthSpacing));
			float fontsize = Height < 1 ? 1 : Height;
			label1.Font = new Font("Microsoft Sans Serif", fontsize, FontStyle.Regular, GraphicsUnit.Pixel, ((byte)(0)));
		}
	}
}
