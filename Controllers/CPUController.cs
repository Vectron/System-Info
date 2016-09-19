using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System_Info.Controllers
{
	public class CPUController : IController
	{
		private CancellationTokenSource cancelSource = new CancellationTokenSource();
		private List<CPU> Cpu = new List<CPU>();
		private Task[] task = new Task[2];
		Panel panelCpu = new Panel();

		public CPUController()
		{
			task[0] = Task.Factory.StartNew(CreateCPUList, cancelSource.Token);
		}

		private void CreateCPUList()
		{
			//clear the old list
			Cpu.Clear();
			CPU Info = new CPU();
			Info.NumberOfCores = Environment.ProcessorCount;

			//create a counter for the total CPU use
			Info.CpuTotalUse = new PerformanceCounter()
			{
				CategoryName = "Processor",
				CounterName = "% Processor Time",
				InstanceName = "_Total",
				ReadOnly = true
			};

			for (int i = 0; i < Info.NumberOfCores; i++)
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

				Info.Cores.Add(newCore);
			}

			Info.ctrCPU = new CtrDisplay(Info.Cores, "CPU:");
			Info.ctrCPU.Padding = new Padding(0);
			Info.ctrCPU.Margin = new Padding(0, 0, 0, 5);

			Cpu.Add(Info);

			//start a task to update the values
			task[1] = Task.Factory.StartNew(UpdateCPUInfo, cancelSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
		}

		private void UpdateCPUInfo()
		{
			CancellationToken Token = cancelSource.Token;
			while (!Token.IsCancellationRequested)
			{
				foreach (var Cpu in Cpu)
				{
					foreach (Core Core in Cpu.Cores)
					{
						Core.LoadPercentage = SystemInfo.FloatToPercent(Core.CpuCoreUse.NextValue());
						Core.ProgresBar.Value = Core.LoadPercentage;
					}
					Cpu.TotalLoad = SystemInfo.FloatToPercent(Cpu.CpuTotalUse.NextValue());
					Cpu.ctrCPU.UpdateCtrText(Cpu.TotalLoad.ToString() + @"%");
				}
				cancelSource.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(1));
			}
		}

		public Panel getPanel()
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
				foreach (var item in Cpu)
				{
					panelCpu.Controls[0].Controls.Add(item.ctrCPU);
				}

				int LastIndex = panelCpu.Controls[0].Controls.Count - 1;
				panelCpu.Controls[0].Controls[LastIndex].Margin = new Padding(0);
			}

			panelCpu.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			panelCpu.AutoSize = true;

			return panelCpu;
		}

		public void Dispose()
		{
			cancelSource.Cancel();

			Task.WaitAll(task);

			foreach (var item in task)
			{
				item.Dispose();
			}

			foreach (var item in Cpu)
			{
				item.CpuTotalUse.Dispose();

				foreach (Core subItem in item.Cores)
				{
					subItem.CpuCoreUse.Dispose();
				}
			}

			cancelSource.Dispose();
		}
	}

	public class CPU
	{
		public int NumberOfCores { get; set; }
		public List<IItemList> Cores { get; set; } = new List<IItemList>();
		public int TotalLoad { get; set; }
		public CtrDisplay ctrCPU { get; set; }
		public PerformanceCounter CpuTotalUse { get; set; }
	}
	public class Core : IItemList
	{
		public int CoreNumber { get; set; }
		public int LoadPercentage { get; set; }
		public ProgressBar ProgresBar { get; set; } = new ProgressBar();
		public PerformanceCounter CpuCoreUse { get; set; }
	}
}