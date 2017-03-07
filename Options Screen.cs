using System;
using System.Drawing;
using System.Windows.Forms;

namespace System_Info
{
	public partial class Options_Screen : Form
	{
		public Options_Screen()
		{
			InitializeComponent();
			progbarCPU.Value = 100;
			progbarHDD.Value = 100;
			progbarRAM.Value = 100;

			progbarCPU.BackColor = Color.Black;
			progbarHDD.BackColor = Color.Black;
			progbarRAM.BackColor = Color.Black;
			LoadSettings();
		}

		private void ScrollbarHSize_ValueChanged(object sender, EventArgs e)
		{
			ScrollBar bar = (ScrollBar)sender;
			string type = bar.Name.Substring(bar.Name.Length - 3, 3);
			if (bar.Value > bar.Minimum || ((ScrollBar)sender).Value < bar.Maximum)
			{
				Controls.Find("txtboxHSize" + type, true)[0].Text = bar.Value.ToString();
				Controls.Find("progbar" + type, true)[0].Width = bar.Value;
				Controls.Find("progbar" + type, true)[0].Invalidate();
			}
		}

		private void TxtboxHSize_TextChanged(object sender, EventArgs e)
		{
			TextBox txtbox = (TextBox)sender;
			string type = txtbox.Name.Substring(txtbox.Name.Length - 3, 3);
			ScrollBar bar = (ScrollBar)Controls.Find("scrollbarHSize" + type, true)[0];

			if (!string.IsNullOrEmpty(txtbox.Text))
			{
				if (int.Parse(txtbox.Text) < bar.Minimum)
				{
					bar.Value = bar.Minimum;
				}
				else if (int.Parse(txtbox.Text) > bar.Maximum)
				{
					bar.Value = bar.Maximum;
				}
				else
				{
					bar.Value = int.Parse(txtbox.Text);
				}
			}
		}

		private void ScrollbarVSize_ValueChanged(object sender, EventArgs e)
		{
			ScrollBar bar = (ScrollBar)sender;
			string type = bar.Name.Substring(bar.Name.Length - 3, 3);
			if (bar.Value > bar.Minimum || ((ScrollBar)sender).Value < bar.Maximum)
			{
				Controls.Find("txtboxVSize" + type, true)[0].Text = bar.Value.ToString();
				Controls.Find("progbar" + type, true)[0].Height = bar.Value;
				Controls.Find("progbar" + type, true)[0].Invalidate();
			}
		}

		private void TxtboxVSize_TextChanged(object sender, EventArgs e)
		{
			TextBox txtbox = (TextBox)sender;
			string type = txtbox.Name.Substring(txtbox.Name.Length - 3, 3);
			ScrollBar bar = (ScrollBar)Controls.Find("scrollbarVSize" + type, true)[0];
			if (!string.IsNullOrEmpty(txtbox.Text))
			{
				if (int.Parse(txtbox.Text) < bar.Minimum)
				{
					bar.Value = bar.Minimum;
				}
				else if (int.Parse(txtbox.Text) > bar.Maximum)
				{
					bar.Value = bar.Maximum;
				}
				else
				{
					bar.Value = int.Parse(txtbox.Text);
				}
			}
		}

		private void ResetToDefault()
		{
			Properties.Settings.Default.Reset();
			Properties.Settings.Default.Save();
			LoadSettings();
		}

		private void LoadSettings()
		{
			scrollbarVSizeCPU.Value = Properties.Settings.Default.CpuBarHeight;
			scrollbarVSizeRAM.Value = Properties.Settings.Default.RamBarHeight;
			scrollbarVSizeHDD.Value = Properties.Settings.Default.HddBarHeight;
			scrollbarHSizeCPU.Value = Properties.Settings.Default.CpuBarWidth;
			scrollbarHSizeRAM.Value = Properties.Settings.Default.RamBarWidth;
			scrollbarHSizeHDD.Value = Properties.Settings.Default.HddBarWidth;
			txtboxFont.Text = FontToString(Properties.Settings.Default.Font);
		}

		private string FontToString(Font f)
		{
			FontConverter converter = new FontConverter();
			return converter.ConvertToString(f);
		}

		private Font StringToFont(string s)
		{
			FontConverter converter = new FontConverter();
			return (Font)converter.ConvertFromString(s);
		}

		private void BtnResetDefaults_Click(object sender, EventArgs e)
		{
			ResetToDefault();
		}

		private void BtnApply_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.CpuBarHeight = (short)scrollbarVSizeCPU.Value;
			Properties.Settings.Default.RamBarHeight = (short)scrollbarVSizeRAM.Value;
			Properties.Settings.Default.HddBarHeight = (short)scrollbarVSizeHDD.Value;

			Properties.Settings.Default.CpuBarWidth = (short)scrollbarHSizeCPU.Value;
			Properties.Settings.Default.RamBarWidth = (short)scrollbarHSizeRAM.Value;
			Properties.Settings.Default.HddBarWidth = (short)scrollbarHSizeHDD.Value;
			Properties.Settings.Default.Font = StringToFont(txtboxFont.Text);
			Properties.Settings.Default.Save();
			Close();
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void BtnFontSelection_Click(object sender, EventArgs e)
		{
			fontDialog1.Font = StringToFont(txtboxFont.Text);
			if (fontDialog1.ShowDialog() == DialogResult.OK)
			{
				txtboxFont.Text = FontToString(fontDialog1.Font);
			}
		}
	}
}
