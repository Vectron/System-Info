using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System;

namespace System_Info
{

    public partial class ProgressBar : UserControl
    {
        private const short NumberOfBlocks = 20;
        private const short multiplyer = 1;
        private const short WidthSpacing = 1;
        private static short TotalWidthBlocks;
        private static short WidthBlock;
        private static short width;
        private static short FullBlocks;
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
                if (value < 0) value = 0;
                else if (value > 100) value = 100;
                if (percent != value)
                {
                    percent = value;
                                     label1.Text = percent.ToString();
                    this.Invalidate();
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
                e.Graphics.FillRectangle(BackgroundBrush, VarX, 0, WidthBlock, this.Height);
                VarX += TotalWidthBlocks;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            width = (short)((percent / 100) * this.Width);
            FullBlocks = (short)((width + WidthSpacing) / (TotalWidthBlocks));
            if (FullBlocks > NumberOfBlocks) { FullBlocks = NumberOfBlocks; }

            int VarX = 0;
            short color1 = 0;
            short color2 = 0;
            for (int i = 0; i < FullBlocks; i++)
            {
                if (i % 2 == 0 && i != 0) { color2 = color1; }
                ColorBrush = new LinearGradientBrush(
                    new Rectangle(VarX, 0, (TotalWidthBlocks * multiplyer), this.Height),
                    Colors[color1],
                    Colors[color2],
                    LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(ColorBrush, VarX, 0, WidthBlock, this.Height);
                VarX += TotalWidthBlocks;

                color1 = color2;
                color2 += 1;
            }
        }

        private void ProgressBar_SizeChanged(object sender, System.EventArgs e)
        {

            TotalWidthBlocks = (short)((this.Width + WidthSpacing) / NumberOfBlocks);
            WidthBlock = (short)(TotalWidthBlocks - (WidthSpacing));
            float fontsize = this.Height;
            if (fontsize < 1)
            {
                fontsize = 1;
            }
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", fontsize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
        }

    }
}
