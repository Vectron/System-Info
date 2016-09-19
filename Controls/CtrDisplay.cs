using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System_Info.Controllers;
using Trento_Library;

namespace System_Info
{
	public partial class CtrDisplay : UserControl
	{
		public CtrDisplay(List<IItemList> Data, string name)
		{
			InitializeComponent();

			LblName.Text = name;
			CreateProgressbars(Data);
		}

		private void CreateProgressbars(List<IItemList> Data)
		{
			foreach (var item in Data)
			{
				item.ProgresBar.Padding = new Padding(0);
				item.ProgresBar.Margin = new Padding(0, 1, 0, 0);
				item.ProgresBar.Value = 0;
				item.ProgresBar.ForeColor = Color.White;
				flowpanelProgresbars.Controls.Add(item.ProgresBar);
			}
		}

		public void UpdateCtrText(string value)
		{
			TrentoGlobal.SetControlPropertyThreadSafe(LblUse, "Text", value);
		}
	}
}

