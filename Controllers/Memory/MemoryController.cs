using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vectrons_Library;

namespace System_Info.Controllers.Memory
{
	public class MemoryController : IController
	{
		private CancellationTokenSource cancelSource = new CancellationTokenSource();
		private List<MemoryUse> memoryInfo = new List<MemoryUse>();
		private Task[] task = new Task[2];
		private Panel panelMemory = new Panel();

		public MemoryController()
		{
			task[0] = Task.Factory.StartNew(CreateMemoryList, cancelSource.Token);
		}

		public Panel GetPanel()
		{
			task[0].Wait(cancelSource.Token);

			panelMemory.Controls.Clear();

			panelMemory.Controls.Add(new FlowLayoutPanel()
			{
				FlowDirection = FlowDirection.TopDown,
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
				Padding = new Padding(0),
				Margin = new Padding(0)
			});

			if (!cancelSource.IsCancellationRequested)
			{
				panelMemory.Controls[0].Controls.Clear();

				foreach (var item in memoryInfo)
				{
					panelMemory.Controls[0].Controls.Add(item.CtrMemory);
				}

				int lastIndex = panelMemory.Controls[0].Controls.Count - 1;
				panelMemory.Controls[0].Controls[lastIndex].Margin = new Padding(0);
			}

			panelMemory.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			panelMemory.AutoSize = true;

			return panelMemory;
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

				if (memoryInfo != null)
				{
					foreach (var item in memoryInfo)
					{
						item.Dispose();
					}

					memoryInfo.Clear();
					memoryInfo = null;
				}

				panelMemory?.Dispose();
				panelMemory = null;
				cancelSource?.Dispose();
				cancelSource = null;
			}

			// free native resources if there are any.
		}

		private void CreateMemoryList()
		{
			using (ManagementObjectSearcher mos_System = new ManagementObjectSearcher("select TotalVisibleMemorySize from Win32_OperatingSystem"))
			{
				foreach (ManagementObject mo_System in mos_System.Get())
				{
					MemoryUse memory = new MemoryUse();
					memory.PC_AvailableRam = new PerformanceCounter();
					memory.PC_AvailableRam.CategoryName = "Memory";
					memory.PC_AvailableRam.CounterName = "Available kBytes";
					memory.PC_AvailableRam.ReadOnly = true;
					memory.TotalVisibleMemorySize = Convert.ToInt32(mo_System.Properties["TotalVisibleMemorySize"].Value);

					memory.CtrMemory = new CtrDisplay(new List<IItemList>() { memory }, "RAM:");
					memory.CtrMemory.Padding = new Padding(0);
					memory.CtrMemory.Margin = new Padding(0);

					int width = Properties.Settings.Default.RamBarWidth < 10 ? 100 : Properties.Settings.Default.RamBarWidth;
					int height = Properties.Settings.Default.RamBarHeight < 1 ? 4 : Properties.Settings.Default.RamBarHeight;
					memory.ProgresBar.Size = new System.Drawing.Size(width, height);

					SetDisplayText(memory);

					memoryInfo.Add(memory);
				}
			}

			task[1] = Task.Factory.StartNew(UpdateMemory, cancelSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
		}

		private void UpdateMemory()
		{
			while (!cancelSource.Token.IsCancellationRequested)
			{
				foreach (var item in memoryInfo)
				{
					item.AvailableRam = SystemInfo.FloatToPercent(item.PC_AvailableRam.NextValue());
					SetDisplayText(item);

					item.ProgresBar.Value = item.LoadPercentage;
				}

				cancelSource.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(1));
			}
		}

		private void SetDisplayText(MemoryUse mem)
		{
			mem.LoadPercentage = SystemInfo.FloatToPercent(((float)(mem.TotalVisibleMemorySize - mem.AvailableRam) / mem.TotalVisibleMemorySize) * 100);
			string text = Utils.FormatBytes(mem.TotalVisibleMemorySize - mem.AvailableRam, 1) + "   " + mem.LoadPercentage.ToString() + @"%";
			mem.CtrMemory.UpdateCtrText(text);
			mem.ProgresBar.Value = mem.LoadPercentage;
		}
	}
}
