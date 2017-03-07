using System;
using System.Diagnostics;

namespace System_Info.Controllers.Cpu
{
	public class Core : IItemList
	{
		public int CoreNumber { get; set; }

		public int LoadPercentage { get; set; }

		public ProgressBar ProgresBar { get; private set; } = new ProgressBar();

		public PerformanceCounter CpuCoreUse { get; set; }

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
				ProgresBar?.Dispose();
				ProgresBar = null;
				CpuCoreUse?.Dispose();
				CpuCoreUse = null;
			}

			// free native resources if there are any.
		}
	}
}