using System;

namespace System_Info.Controllers
{
	public interface IItemList : IDisposable
	{
		ProgressBar ProgresBar { get; }

		int Value { get; set; }
	}
}
