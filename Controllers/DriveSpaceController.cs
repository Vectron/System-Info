using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trento_Library;

namespace System_Info.Controllers
{
	public class DriveSpaceController : IController
	{
		private CancellationTokenSource cancelSource = new CancellationTokenSource();
		private SortedDictionary<string, HDD> HddInfo { get; set; } = new SortedDictionary<string, HDD>();
		private Task[] task = new Task[2];
		private HDD HddTotal;
		private Panel panelDrives = new Panel();

		public DriveSpaceController()
		{
			task[0] = Task.Factory.StartNew(CreateHddList, cancelSource.Token);
		}

		private void CreateHddList()
		{
			HddInfo.Clear();

			HddTotal = new HDD();
			HddTotal.ctrHDD = new CtrDisplay(new List<IItemList>() { HddTotal }, "Tot:");
			HddTotal.ctrHDD.Padding = new Padding(0);
			HddTotal.ctrHDD.Margin = new Padding(0);

			int width = Properties.Settings.Default.HddBarWidth < 10 ? 100 : Properties.Settings.Default.HddBarWidth;
			int height = Properties.Settings.Default.HddBarHeight < 1 ? 4 : Properties.Settings.Default.HddBarHeight;
			HddTotal.ProgresBar.Size = new System.Drawing.Size(width, height);

			foreach (DriveInfo drive in DriveInfo.GetDrives())
			{
				if (drive.IsReady &&
					(drive.DriveType == DriveType.Fixed ||
					drive.DriveType == DriveType.Network))
				{
					HddInfo.Add(drive.Name, CreateDrive(drive));

					if (drive.DriveType == DriveType.Fixed)
					{
						HddTotal.TotalSize += drive.TotalSize;
						HddTotal.AvailableFreeSpace += drive.AvailableFreeSpace;
					}
				}
			}

			SetDisplayText(HddTotal);

			task[1] = Task.Factory.StartNew(UpdateHddInfo, cancelSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
		}

		private void UpdateHddInfo()
		{
			try
			{
				while (!cancelSource.Token.IsCancellationRequested)
				{
					bool Update = false;
					List<DriveInfo> Drives = DriveInfo.GetDrives().ToList();

					HddTotal.TotalSize = 0;
					HddTotal.AvailableFreeSpace = 0;

					foreach (DriveInfo drive in Drives)
					{
						if (drive.IsReady &&
							(drive.DriveType == DriveType.Fixed ||
							drive.DriveType == DriveType.Network))
						{
							if (HddInfo.ContainsKey(drive.Name))
							{
								HddInfo[drive.Name].AvailableFreeSpace = drive.AvailableFreeSpace;
								HddInfo[drive.Name].TotalSize = drive.TotalSize;

								SetDisplayText(HddInfo[drive.Name]);
							}
							else
							{
								HddInfo.Add(drive.Name, CreateDrive(drive));
								Update = true;
							}

							if (drive.DriveType == DriveType.Fixed)
							{
								HddTotal.TotalSize += drive.TotalSize;
								HddTotal.AvailableFreeSpace += drive.AvailableFreeSpace;
							}
						}
					}

					SetDisplayText(HddTotal);

					var itemsToDelete = HddInfo.Where(x => Drives.Find(Y => Y.Name == x.Value.Name) == null).ToList();

					foreach (var item in itemsToDelete)
					{
						RemoveFromPanel(item.Value);
						HddInfo.Remove(item.Key);
					}

					if (Update)
					{
						UpdatePanel();
					}
					cancelSource.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(10));
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		private HDD CreateDrive(DriveInfo drive)
		{
			HDD Drive = new HDD();
			Drive.Name = drive.Name;
			Drive.AvailableFreeSpace = drive.AvailableFreeSpace;
			Drive.TotalSize = drive.TotalSize;
			Drive.HddType = drive.DriveType;

			Drive.ctrHDD = new CtrDisplay(new List<IItemList>() { Drive }, Drive.Name);
			Drive.ctrHDD.Padding = new Padding(0);
			Drive.ctrHDD.Margin = new Padding(0, 0, 0, 5);

			int width = Properties.Settings.Default.HddBarWidth < 10 ? 100 : Properties.Settings.Default.HddBarWidth;
			int height = Properties.Settings.Default.HddBarHeight < 1 ? 4 : Properties.Settings.Default.HddBarHeight;
			Drive.ProgresBar.Size = new System.Drawing.Size(width, height);

			SetDisplayText(Drive);

			return Drive;
		}
		private void SetDisplayText(HDD Drive)
		{
			Drive.LoadPercentage = SystemInfo.FloatToPercent(((float)Drive.AvailableFreeSpace / (float)Drive.TotalSize) * 100);
			string text = TrentoGlobal.formatBytes(Drive.AvailableFreeSpace, 0) + "  " + Drive.LoadPercentage.ToString() + @"%";
			Drive.ctrHDD.UpdateCtrText(text);
			Drive.ProgresBar.Value = Drive.LoadPercentage;
		}

		private delegate void UpdatePanelConsumer();
		private void UpdatePanel()
		{
			if (panelDrives.InvokeRequired)
			{
				panelDrives.Invoke(new UpdatePanelConsumer(UpdatePanel));
			}
			else
			{
				panelDrives.SuspendLayout();
				panelDrives.Controls[0].Controls.Clear();
				foreach (var item in HddInfo)
				{
					panelDrives.Controls[0].Controls.Add(item.Value.ctrHDD);
				}

				HddTotal.ctrHDD.Margin = new Padding(0);
				panelDrives.Controls[0].Controls.Add(HddTotal.ctrHDD);
				panelDrives.ResumeLayout();
			}
		}

		private delegate void RemoveFromPanelConsumer(HDD DriveToRemove);
		private void RemoveFromPanel(HDD DriveToRemove)
		{
			if (panelDrives.InvokeRequired)
			{
				panelDrives.Invoke(new RemoveFromPanelConsumer(RemoveFromPanel), DriveToRemove);
			}
			else
			{
				panelDrives.SuspendLayout();
				panelDrives.Controls[0].Controls.Remove(DriveToRemove.ctrHDD);
				DriveToRemove.ctrHDD.Dispose();
				panelDrives.ResumeLayout();
			}
		}

		public Panel getPanel()
		{
			task[0].Wait(cancelSource.Token);
			if (!cancelSource.IsCancellationRequested)
			{
				panelDrives.Controls.Clear();
				panelDrives.Controls.Add(new FlowLayoutPanel()
				{
					FlowDirection = FlowDirection.TopDown,
					AutoSize = true,
					AutoSizeMode = AutoSizeMode.GrowAndShrink,
					Padding = new Padding(0),
					Margin = new Padding(0)
				});

				UpdatePanel();

				panelDrives.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				panelDrives.AutoSize = true;
			}

			return panelDrives;
		}

		public void Dispose()
		{
			cancelSource.Cancel();

			Task.WaitAll(task);

			foreach (var item in task)
			{
				item.Dispose();
			}

			cancelSource.Dispose();
		}
	}

	public class HDD : IItemList
	{
		public string Name { get; set; }
		public long AvailableFreeSpace { get; set; }
		public long TotalSize { get; set; }
		public int LoadPercentage { get; set; }
		public DriveType HddType { get; set; }
		public CtrDisplay ctrHDD { get; set; }
		public ProgressBar ProgresBar { get; set; } = new ProgressBar();
	}
}
