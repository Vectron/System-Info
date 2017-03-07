using System;
using System.IO;

namespace System_Info.Controllers.Drives
{
	public class HDD : IItemList
	{
		public string Name { get; set; }

		public long AvailableFreeSpace { get; set; }

		public long TotalSize { get; set; }

		public int LoadPercentage { get; set; }

		public DriveType HddType { get; set; }

		public CtrDisplay CtrHDD { get; set; }

		public ProgressBar ProgresBar { get; private set; } = new ProgressBar();

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
				CtrHDD?.Dispose();
				CtrHDD = null;
			}

			// free native resources if there are any.
		}
	}
}
