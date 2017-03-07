using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System_Info.Controllers.Cpu
{
	public class CPU : IDisposable
	{
		public int NumberOfCores { get; set; }

		public List<IItemList> Cores { get; set; } = new List<IItemList>();

		public int TotalLoad { get; set; }

		public CtrDisplay CtrCPU { get; set; }

		public PerformanceCounter CpuTotalUse { get; set; }

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
				CtrCPU?.Dispose();
				CtrCPU = null;
				CpuTotalUse?.Dispose();
				CpuTotalUse = null;

				foreach (var item in Cores)
				{
					item.Dispose();
				}
			}

			// free native resources if there are any.
		}
	}
}