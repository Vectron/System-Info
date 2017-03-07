using System.Drawing;
using System.Windows.Forms;

namespace System_Info
{
	public partial class CtrNetworkTraffic : UserControl
	{
		public CtrNetworkTraffic()
		{
			InitializeComponent();
		}

		public string InterfaceName
		{
			set
			{
				if (value.Contains("Local Area Connection"))
				{
					LblName.Text = value.Replace("Local Area Connection", "LAN");
				}
				else if (value.Contains("Wireless Network Connection"))
				{
					LblName.Text = value.Replace("Wireless Network Connection", "Wan");
				}
				else
				{
					LblName.Text = value;
				}
			}
		}

		public void UpdateValue(int rx, int tx)
		{
			LblRx.Invoke(() => LblRx.Text = "Received: " + rx + " kB/Sec");
			LblTx.Invoke(() => LblTx.Text = "Sent: " + tx + " kB/Sec");
		}

		private void LblName_SizeChanged(object sender, System.EventArgs e)
		{
			int y = LblName.Height + 1;

			LblRx.Location = new Point(0, y);
			y += LblRx.Size.Height + 1;
			LblTx.Location = new Point(0, y);
			y += LblTx.Size.Height + 1;
		}
	}
}
