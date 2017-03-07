using System;
using System.Diagnostics;

namespace System_Info.Controllers.Memory
{
	public class MemoryUse : IItemList
	{
		public int AvailableRam { get; set; }

		public int TotalVisibleMemorySize { get; set; }

		public int LoadPercentage { get; set; }

		public CtrDisplay CtrMemory { get; set; }

		public PerformanceCounter PC_AvailableRam { get; set; }

		public ProgressBar ProgresBar { get; set; } = new ProgressBar();

		public int Value { get; set; }

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
				PC_AvailableRam?.Dispose();
				PC_AvailableRam = null;
				ProgresBar?.Dispose();
				ProgresBar = null;
				CtrMemory?.Dispose();
				CtrMemory = null;
			}

			// free native resources if there are any.
		}
	}
}