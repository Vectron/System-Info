using System;
using System.Threading.Tasks;
using System_Info.Controllers;

namespace System_Info
{
	public delegate void ChangedEventHandler();

	static class SystemInfo
	{
		public static IController DriveController = new DriveSpaceController();
		public static IController CPUController = new CPUController();
		public static IController MemoryController = new MemoryController();
		public static IController NetworkController = new NetworkController();

		public static int FloatToPercent(float NextValue)
		{
			double Load = Math.Truncate(NextValue * 100) / 100;
			return Convert.ToInt32(Load);
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
