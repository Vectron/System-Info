using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vectrons_Library.Logging;

namespace System_Info.Controllers.Network
{
	public class NetworkController : IController
	{
		private CancellationTokenSource cancelSource = new CancellationTokenSource();
		private SortedDictionary<string, Network> networkStatistics = new SortedDictionary<string, Network>();
		private Panel panelNetwork = new Panel();
		private Task[] task = new Task[2];

		public NetworkController()
		{
			task[0] = Task.Factory.StartNew(CreateNetworkList, cancelSource.Token);
		}

		private delegate void UpdatePanelConsumer();

		private delegate void RemoveFromPanelConsumer(Network driveToRemove);

		public void UpdateTraffic()
		{
			while (!cancelSource.Token.IsCancellationRequested)
			{
				try
				{
					bool update = false;
					var interfaces = NetworkInterface.GetAllNetworkInterfaces().Where(x => x.OperationalStatus == OperationalStatus.Up).ToList();

					foreach (NetworkInterface networkInterface in interfaces)
					{
						if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
								networkInterface.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet ||
								networkInterface.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT ||
								networkInterface.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx ||
								networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
						{
							if (networkStatistics.ContainsKey(networkInterface.Name))
							{
								networkStatistics[networkInterface.Name].TrafficSentKBSec = Convert.ToInt32((networkStatistics[networkInterface.Name].InterFace.GetIPv4Statistics().BytesSent / 1024) - networkStatistics[networkInterface.Name].TrafficSentKB);
								networkStatistics[networkInterface.Name].TrafficReceivedKBSec = Convert.ToInt32((networkStatistics[networkInterface.Name].InterFace.GetIPv4Statistics().BytesReceived / 1024) - networkStatistics[networkInterface.Name].TrafficReceivedKB);
								networkStatistics[networkInterface.Name].TrafficReceivedKB = Convert.ToInt32(networkStatistics[networkInterface.Name].InterFace.GetIPv4Statistics().BytesReceived / 1024);
								networkStatistics[networkInterface.Name].TrafficSentKB = Convert.ToInt32(networkStatistics[networkInterface.Name].InterFace.GetIPv4Statistics().BytesSent / 1024);

								networkStatistics[networkInterface.Name].CtrNetwork.UpdateValue(networkStatistics[networkInterface.Name].TrafficSentKBSec, networkStatistics[networkInterface.Name].TrafficReceivedKBSec);
							}
							else
							{
								networkStatistics.Add(networkInterface.Name, CreateNewNetwork(networkInterface));
								update = true;
							}
						}
					}

					var itemsToDelete = networkStatistics.Where(x =>
						interfaces.Find(y => y.Name == x.Value.Name) == null ||
						x.Value.InterFace.OperationalStatus != OperationalStatus.Up).ToList();

					foreach (var item in itemsToDelete)
					{
						update = true;
						networkStatistics.Remove(item.Key);
					}

					if (update)
					{
						UpdatePanel();
					}
				}
				catch (Exception ex)
				{
					Logger.WriteToLogFile(ex.Message);
				}

				cancelSource.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(1));
			}
		}

		public Panel GetPanel()
		{
			task[0].Wait(cancelSource.Token);

			if (!cancelSource.IsCancellationRequested)
			{
				panelNetwork.Controls.Clear();
				panelNetwork.Controls.Add(new FlowLayoutPanel()
				{
					FlowDirection = FlowDirection.TopDown,
					AutoSize = true,
					AutoSizeMode = AutoSizeMode.GrowAndShrink,
					Padding = new Padding(0),
					Margin = new Padding(0),
				});

				UpdatePanel();

				panelNetwork.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				panelNetwork.AutoSize = true;
			}

			return panelNetwork;
		}

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

				if (networkStatistics != null)
				{
					foreach (var item in networkStatistics)
					{
						item.Value.Dispose();
					}

					networkStatistics.Clear();
					networkStatistics = null;
				}

				panelNetwork?.Dispose();
				panelNetwork = null;
				cancelSource?.Dispose();
				cancelSource = null;
			}

			// free native resources if there are any.
		}

		private void CreateNetworkList()
		{
			try
			{
				networkStatistics.Clear();
				foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
				{
					if (networkInterface.OperationalStatus == OperationalStatus.Up &&
						(networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
							networkInterface.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet ||
							networkInterface.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT ||
							networkInterface.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx ||
							networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
					{
						networkStatistics.Add(networkInterface.Name, CreateNewNetwork(networkInterface));
					}
				}

				task[1] = Task.Factory.StartNew(UpdateTraffic, cancelSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
			}
			catch (Exception ex)
			{
				Logger.WriteToLogFile(ex.Message);
			}
		}

		private Network CreateNewNetwork(NetworkInterface inter)
		{
			Network info = new Network();
			info.Name = inter.Name;
			info.InterFace = inter;
			info.TrafficSentKBSec = 0;
			info.TrafficReceivedKBSec = 0;
			info.TrafficReceivedKB = Convert.ToInt32(inter.GetIPv4Statistics().BytesReceived / 1024);
			info.TrafficSentKB = Convert.ToInt32(inter.GetIPv4Statistics().BytesSent / 1024);

			info.CtrNetwork = new CtrNetworkTraffic();
			info.CtrNetwork.Padding = new Padding(0);
			info.CtrNetwork.Margin = new Padding(0, 5, 0, 0);

			info.CtrNetwork.InterfaceName = inter.Name;
			info.CtrNetwork.UpdateValue(info.TrafficSentKBSec, info.TrafficReceivedKBSec);

			return info;
		}

		private void RemoveFromPanel(Network networkToRemove)
		{
			if (panelNetwork.InvokeRequired)
			{
				panelNetwork.Invoke(new RemoveFromPanelConsumer(RemoveFromPanel), networkToRemove);
			}
			else
			{
				panelNetwork.SuspendLayout();
				panelNetwork.Controls[0].Controls.Remove(networkToRemove.CtrNetwork);
				networkToRemove.CtrNetwork.Dispose();
				panelNetwork.ResumeLayout();
			}
		}

		private void UpdatePanel()
		{
			if (panelNetwork.InvokeRequired)
			{
				panelNetwork.Invoke(new UpdatePanelConsumer(UpdatePanel));
			}
			else
			{
				panelNetwork.SuspendLayout();

				panelNetwork.Controls[0].Controls.Clear();
				foreach (var item in networkStatistics)
				{
					panelNetwork.Controls[0].Controls.Add(item.Value.CtrNetwork);
				}

				int firstIndex = 0;
				panelNetwork.Controls[0].Controls[firstIndex].Margin = new Padding(0);

				panelNetwork.ResumeLayout();
			}
		}
	}
}
