using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trento_Library;

namespace System_Info.Controllers
{
	public class MemoryController : IController
	{
		private CancellationTokenSource cancelSource = new CancellationTokenSource();
		private List<MemoryUse> MemoryInfo = new List<MemoryUse>();
		private Task[] task = new Task[2];
		private Panel panelMemory = new Panel();

		public MemoryController()
		{
			task[0] = Task.Factory.StartNew(CreateMemoryList, cancelSource.Token);
		}

		private void CreateMemoryList()
		{
			using (ManagementObjectSearcher MOS_System = new ManagementObjectSearcher("select TotalVisibleMemorySize from Win32_OperatingSystem"))
			{
				foreach (ManagementObject MO_System in MOS_System.Get())
				{
					MemoryUse Memory = new MemoryUse();
					Memory.PC_AvailableRam = new PerformanceCounter();
					Memory.PC_AvailableRam.CategoryName = "Memory";
					Memory.PC_AvailableRam.CounterName = "Available kBytes";
					Memory.PC_AvailableRam.ReadOnly = true;
					Memory.TotalVisibleMemorySize = Convert.ToInt32(MO_System.Properties["TotalVisibleMemorySize"].Value);

					Memory.ctrMemory = new CtrDisplay(new List<IItemList>() { Memory }, "RAM:");
					Memory.ctrMemory.Padding = new Padding(0);
					Memory.ctrMemory.Margin = new Padding(0);

					int width = Properties.Settings.Default.RamBarWidth < 10 ? 100 : Properties.Settings.Default.RamBarWidth;
					int height = Properties.Settings.Default.RamBarHeight < 1 ? 4 : Properties.Settings.Default.RamBarHeight;
					Memory.ProgresBar.Size = new System.Drawing.Size(width, height);

					SetDisplayText(Memory);

					MemoryInfo.Add(Memory);
				}
			}

			task[1] = Task.Factory.StartNew(UpdateMemory, cancelSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
		}

		private void UpdateMemory()
		{
			while (!cancelSource.Token.IsCancellationRequested)
			{
				foreach (var item in MemoryInfo)
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
			string text = TrentoGlobal.formatBytes(mem.TotalVisibleMemorySize - mem.AvailableRam, 1) + "   " + mem.LoadPercentage.ToString() + @"%";
			mem.ctrMemory.UpdateCtrText(text);
			mem.ProgresBar.Value = mem.LoadPercentage;
		}

		public Panel getPanel()
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

				foreach (var item in MemoryInfo)
				{
					panelMemory.Controls[0].Controls.Add(item.ctrMemory);
				}

				int LastIndex = panelMemory.Controls[0].Controls.Count - 1;
				panelMemory.Controls[0].Controls[LastIndex].Margin = new Padding(0);
			}

			panelMemory.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			panelMemory.AutoSize = true;

			return panelMemory;
		}

		public void Dispose()
		{
			cancelSource.Cancel();

			Task.WaitAll(task);

			foreach (var item in task)
			{
				item.Dispose();
			}

			foreach (var item in MemoryInfo)
			{
				item.PC_AvailableRam.Dispose();
			}

			cancelSource.Dispose();
		}
	}

	public class MemoryUse : IItemList
	{
		public int AvailableRam { get; set; }
		public int TotalVisibleMemorySize { get; set; }
		public int LoadPercentage { get; set; }
		public CtrDisplay ctrMemory { get; set; }
		public PerformanceCounter PC_AvailableRam { get; set; }
		public ProgressBar ProgresBar { get; set; } = new ProgressBar();
	}
}
