using System;
using System.Threading.Tasks;
using System_Info.Controllers;
using System_Info.Controllers.Cpu;
using System_Info.Controllers.Drives;
using System_Info.Controllers.Memory;
using System_Info.Controllers.Network;

namespace System_Info
{
	public delegate void ChangedEventHandler();

	internal static class SystemInfo
	{
		public static IController DriveController { get; } = new DriveSpaceController();

		public static IController CPUController { get; } = new CPUController();

		public static IController MemoryController { get; } = new MemoryController();

		public static IController NetworkController { get; } = new NetworkController();

		public static int FloatToPercent(float nextValue)
		{
			double load = Math.Truncate(nextValue * 100) / 100;
			return Convert.ToInt32(load);
		}

		public static async Task Exit()
		{
			Task[] task = new Task[4];
			task[0] = Task.Factory.StartNew(CPUController.Dispose);
			task[1] = Task.Factory.StartNew(MemoryController.Dispose);
			task[2] = Task.Factory.StartNew(NetworkController.Dispose);
			task[3] = Task.Factory.StartNew(DriveController.Dispose);

			await Task.WhenAll(task);
		}
	}
}
