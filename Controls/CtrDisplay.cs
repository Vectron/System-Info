using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System_Info.Controllers;

namespace System_Info
{
	public partial class CtrDisplay : UserControl
	{
		public CtrDisplay(List<IItemList> data, string name)
		{
			InitializeComponent();

			LblName.Text = name;
			CreateProgressbars(data);
		}

		public void UpdateCtrText(string value)
		{
			LblUse.Invoke(() => LblUse.Text = value);
		}

		private void CreateProgressbars(List<IItemList> data)
		{
			foreach (var item in data)
			{
				item.ProgresBar.Padding = new Padding(0);
				item.ProgresBar.Margin = new Padding(0, 1, 0, 0);
				item.ProgresBar.Value = 0;
				item.ProgresBar.ForeColor = Color.White;
				flowpanelProgresbars.Controls.Add(item.ProgresBar);
			}
		}
	}
}