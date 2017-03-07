using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vectrons_Library;

namespace System_Info.Controllers.Drives
{
	public class DriveSpaceController : IController
	{
		private CancellationTokenSource cancelSource = new CancellationTokenSource();
		private SortedDictionary<string, HDD> hddInfo = new SortedDictionary<string, HDD>();
		private Task[] task = new Task[2];
		private HDD hddTotal;
		private Panel panelDrives = new Panel();

		public DriveSpaceController()
		{
			task[0] = Task.Factory.StartNew(CreateHddList, cancelSource.Token);
		}

		private delegate void UpdatePanelConsumer();

		private delegate void RemoveFromPanelConsumer(HDD driveToRemove);

		public Panel GetPanel()
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

		// Dispose() calls Dispose(true)
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		// The bulk of the clean-up code is implemented in Dispose(bool)
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// free managed resources
				cancelSource.Cancel();

				if (task != null)
				{
					Task.WaitAll(task);

					foreach (var item in task)
					{
						item.Dispose();
					}

					task = null;
				}

				if (hddInfo != null)
				{
					foreach (var item in hddInfo)
					{
						item.Value.Dispose();
					}

					hddInfo.Clear();
					hddInfo = null;
				}

				hddTotal?.Dispose();
				hddTotal = null;
				panelDrives?.Dispose();
				panelDrives = null;
				cancelSource?.Dispose();
				cancelSource = null;
			}

			// free native resources if there are any.
		}

		private void CreateHddList()
		{
			hddInfo.Clear();

			hddTotal = new HDD();
			hddTotal.CtrHDD = new CtrDisplay(new List<IItemList>() { hddTotal }, "Tot:");
			hddTotal.CtrHDD.Padding = new Padding(0);
			hddTotal.CtrHDD.Margin = new Padding(0);

			int width = Properties.Settings.Default.HddBarWidth < 10 ? 100 : Properties.Settings.Default.HddBarWidth;
			int height = Properties.Settings.Default.HddBarHeight < 1 ? 4 : Properties.Settings.Default.HddBarHeight;
			hddTotal.ProgresBar.Size = new System.Drawing.Size(width, height);

			foreach (DriveInfo drive in DriveInfo.GetDrives())
			{
				if (drive.IsReady &&
					(drive.DriveType == DriveType.Fixed ||
					drive.DriveType == DriveType.Network))
				{
					hddInfo.Add(drive.Name, CreateDrive(drive));

					if (drive.DriveType == DriveType.Fixed)
					{
						hddTotal.TotalSize += drive.TotalSize;
						hddTotal.AvailableFreeSpace += drive.AvailableFreeSpace;
					}
				}
			}

			SetDisplayText(hddTotal);

			task[1] = Task.Factory.StartNew(UpdateHddInfo, cancelSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
		}

		private void UpdateHddInfo()
		{
			try
			{
				while (!cancelSource.Token.IsCancellationRequested)
				{
					bool update = false;
					List<DriveInfo> drives = DriveInfo.GetDrives().ToList();

					hddTotal.TotalSize = 0;
					hddTotal.AvailableFreeSpace = 0;

					foreach (DriveInfo drive in drives)
					{
						if (drive.IsReady &&
							(drive.DriveType == DriveType.Fixed ||
							drive.DriveType == DriveType.Network))
						{
							if (hddInfo.ContainsKey(drive.Name))
							{
								hddInfo[drive.Name].AvailableFreeSpace = drive.AvailableFreeSpace;
								hddInfo[drive.Name].TotalSize = drive.TotalSize;

								SetDisplayText(hddInfo[drive.Name]);
							}
							else
							{
								hddInfo.Add(drive.Name, CreateDrive(drive));
								update = true;
							}

							if (drive.DriveType == DriveType.Fixed)
							{
								hddTotal.TotalSize += drive.TotalSize;
								hddTotal.AvailableFreeSpace += drive.AvailableFreeSpace;
							}
						}
					}

					SetDisplayText(hddTotal);

					var itemsToDelete = hddInfo.Where(x => drives.Find(y => y.Name == x.Value.Name) == null).ToList();

					foreach (var item in itemsToDelete)
					{
						RemoveFromPanel(item.Value);
						hddInfo.Remove(item.Key);
					}

					if (update)
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
			HDD drive1 = new HDD();
			drive1.Name = drive.Name;
			drive1.AvailableFreeSpace = drive.AvailableFreeSpace;
			drive1.TotalSize = drive.TotalSize;
			drive1.HddType = drive.DriveType;

			drive1.CtrHDD = new CtrDisplay(new List<IItemList>() { drive1 }, drive1.Name);
			drive1.CtrHDD.Padding = new Padding(0);
			drive1.CtrHDD.Margin = new Padding(0, 0, 0, 5);

			int width = Properties.Settings.Default.HddBarWidth < 10 ? 100 : Properties.Settings.Default.HddBarWidth;
			int height = Properties.Settings.Default.HddBarHeight < 1 ? 4 : Properties.Settings.Default.HddBarHeight;
			drive1.ProgresBar.Size = new System.Drawing.Size(width, height);

			SetDisplayText(drive1);

			return drive1;
		}

		private void SetDisplayText(HDD drive)
		{
			long usedSpace = drive.TotalSize - drive.AvailableFreeSpace;
			drive.LoadPercentage = SystemInfo.FloatToPercent(((float)usedSpace / (float)drive.TotalSize) * 100);
			string text = Utils.FormatBytes(drive.AvailableFreeSpace, 0) + "  " + (100 - drive.LoadPercentage).ToString() + @"%";
			drive.CtrHDD.UpdateCtrText(text);
			drive.ProgresBar.Value = drive.LoadPercentage;
		}

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
				foreach (var item in hddInfo)
				{
					panelDrives.Controls[0].Controls.Add(item.Value.CtrHDD);
				}

				hddTotal.CtrHDD.Margin = new Padding(0);
				panelDrives.Controls[0].Controls.Add(hddTotal.CtrHDD);
				panelDrives.ResumeLayout();
			}
		}

		private void RemoveFromPanel(HDD driveToRemove)
		{
			if (panelDrives.InvokeRequired)
			{
				panelDrives.Invoke(new RemoveFromPanelConsumer(RemoveFromPanel), driveToRemove);
			}
			else
			{
				panelDrives.SuspendLayout();
				panelDrives.Controls[0].Controls.Remove(driveToRemove.CtrHDD);
				driveToRemove.CtrHDD.Dispose();
				panelDrives.ResumeLayout();
			}
		}
	}
}
