using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System_Info.Controllers.Cpu
{
	public class CPUController : IController
	{
		private CancellationTokenSource cancelSource = new CancellationTokenSource();
		private List<CPU> cpu = new List<CPU>();
		private Task[] task = new Task[2];
		private Panel panelCpu = new Panel();

		public CPUController()
		{
			task[0] = Task.Factory.StartNew(CreateCPUList, cancelSource.Token);
		}

		public Panel GetPanel()
		{
			task[0].Wait(cancelSource.Token);

			panelCpu.Controls.Clear();

			panelCpu.Controls.Add(new FlowLayoutPanel()
			{
				FlowDirection = FlowDirection.TopDown,
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
				Padding = new Padding(0),
				Margin = new Padding(0)
			});

			if (!cancelSource.IsCancellationRequested)
			{
				foreach (var item in cpu)
				{
					panelCpu.Controls[0].Controls.Add(item.CtrCPU);
				}

				int lastIndex = panelCpu.Controls[0].Controls.Count - 1;
				panelCpu.Controls[0].Controls[lastIndex].Margin = new Padding(0);
			}

			panelCpu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			panelCpu.AutoSize = true;

			return panelCpu;
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

				if (cpu != null)
				{
					foreach (var item in cpu)
					{
						item.Dispose();
					}

					cpu.Clear();
					cpu = null;
				}

				panelCpu?.Dispose();
				panelCpu = null;
				cancelSource?.Dispose();
				cancelSource = null;
			}

			// free native resources if there are any.
		}

		private void CreateCPUList()
		{
			// clear the old list
			cpu.Clear();
			CPU info = new CPU();
			info.NumberOfCores = Environment.ProcessorCount;

			// create a counter for the total CPU use
			info.CpuTotalUse = new PerformanceCounter()
			{
				CategoryName = "Processor",
				CounterName = "% Processor Time",
				InstanceName = "_Total",
				ReadOnly = true
			};

			for (int i = 0; i < info.NumberOfCores; i++)
			{
				Core newCore = new Core();
				newCore.CpuCoreUse = new PerformanceCounter()
				{
					CategoryName = "Processor",
					CounterName = "% Processor Time",
					InstanceName = i.ToString(),
					ReadOnly = true
				};

				int width = Properties.Settings.Default.CpuBarWidth < 10 ? 100 : Properties.Settings.Default.CpuBarWidth;
				int height = Properties.Settings.Default.CpuBarHeight < 1 ? 4 : Properties.Settings.Default.CpuBarHeight;
				newCore.ProgresBar.Size = new System.Drawing.Size(width, height);

				info.Cores.Add(newCore);
			}

			info.CtrCPU = new CtrDisplay(info.Cores, "CPU:");
			info.CtrCPU.Padding = new Padding(0);
			info.CtrCPU.Margin = new Padding(0, 0, 0, 5);

			cpu.Add(info);

			// start a task to update the values
			task[1] = Task.Factory.StartNew(UpdateCPUInfo, cancelSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
		}

		private void UpdateCPUInfo()
		{
			CancellationToken token = cancelSource.Token;
			while (!token.IsCancellationRequested)
			{
				foreach (var cpu in cpu)
				{
					foreach (Core core in cpu.Cores)
					{
						core.LoadPercentage = SystemInfo.FloatToPercent(core.CpuCoreUse.NextValue());
						core.ProgresBar.Value = core.LoadPercentage;
					}

					cpu.TotalLoad = SystemInfo.FloatToPercent(cpu.CpuTotalUse.NextValue());
					cpu.CtrCPU.UpdateCtrText(cpu.TotalLoad.ToString() + @"%");
				}

				cancelSource.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(1));
			}
		}
	}
}