using System;
using System.Net.NetworkInformation;

namespace System_Info.Controllers.Network
{
	public class Network : IDisposable
	{
		public string Name { get; set; }

		public int TrafficSentKBSec { get; set; }

		public int TrafficReceivedKBSec { get; set; }

		public int TrafficSentKB { get; set; }

		public int TrafficReceivedKB { get; set; }

		public NetworkInterface InterFace { get; set; }

		public CtrNetworkTraffic CtrNetwork { get; set; }

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
				InterFace = null;
				CtrNetwork?.Dispose();
				CtrNetwork = null;
			}

			// free native resources if there are any.
		}
	}
}
