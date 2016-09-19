using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trento_Library;

namespace System_Info.Controllers
{
	public class NetworkController : IController
	{
		private CancellationTokenSource cancelSource = new CancellationTokenSource();
		private SortedDictionary<string, Network> NetworkStatistics = new SortedDictionary<string, Network>();
		private Panel panelNetwork = new Panel();
		private Task[] task = new Task[2];

		public NetworkController()
		{
			task[0] = Task.Factory.StartNew(CreateNetworkList, cancelSource.Token);
		}

		private void CreateNetworkList()
		{
			try
			{
				NetworkStatistics.Clear();
				foreach (NetworkInterface Inter in NetworkInterface.GetAllNetworkInterfaces())
				{
					if (Inter.OperationalStatus == OperationalStatus.Up &&
						(Inter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
							Inter.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet ||
							Inter.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT ||
							Inter.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx ||
							Inter.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
					{
						NetworkStatistics.Add(Inter.Name, CreateNewNetwork(Inter));
					}
				}

				task[1] = Task.Factory.StartNew(UpdateTraffic, cancelSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
			}
			catch (Exception ex)
			{
				TrentoGlobal.WriteToLogFile(ex.Message);
			}

		}

		private Network CreateNewNetwork(NetworkInterface Inter)
		{
			Network Info = new Network();
			Info.Name = Inter.Name;
			Info.InterFace = Inter;
			Info.TrafficSentKBSec = 0;
			Info.TrafficReceivedKBSec = 0;
			Info.TrafficReceivedKB = Convert.ToInt32(Inter.GetIPv4Statistics().BytesReceived / 1024);
			Info.TrafficSentKB = Convert.ToInt32(Inter.GetIPv4Statistics().BytesSent / 1024);

			Info.ctrNetwork = new CtrNetworkTraffic();
			Info.ctrNetwork.Padding = new Padding(0);
			Info.ctrNetwork.Margin = new Padding(0, 5, 0, 0);

			Info.ctrNetwork.InterfaceName = Inter.Name;
			Info.ctrNetwork.UpdateValue(Info.TrafficSentKBSec, Info.TrafficReceivedKBSec);

			return Info;
		}

		public void UpdateTraffic()
		{
			while (!cancelSource.Token.IsCancellationRequested)
			{
				try
				{
					bool Update = false;
					var Interfaces = NetworkInterface.GetAllNetworkInterfaces().Where(x => x.OperationalStatus == OperationalStatus.Up).ToList();

					foreach (NetworkInterface Inter in Interfaces)
					{
						if ((Inter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
								Inter.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet ||
								Inter.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT ||
								Inter.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx ||
								Inter.NetworkInterfaceType == NetworkInterfaceType.Ethernet))
						{
							if (NetworkStatistics.ContainsKey(Inter.Name))
							{
								NetworkStatistics[Inter.Name].TrafficSentKBSec = Convert.ToInt32(((NetworkStatistics[Inter.Name].InterFace.GetIPv4Statistics().BytesSent / 1024) - NetworkStatistics[Inter.Name].TrafficSentKB));
								NetworkStatistics[Inter.Name].TrafficReceivedKBSec = Convert.ToInt32(((NetworkStatistics[Inter.Name].InterFace.GetIPv4Statistics().BytesReceived / 1024) - NetworkStatistics[Inter.Name].TrafficReceivedKB));
								NetworkStatistics[Inter.Name].TrafficReceivedKB = Convert.ToInt32(NetworkStatistics[Inter.Name].InterFace.GetIPv4Statistics().BytesReceived / 1024);
								NetworkStatistics[Inter.Name].TrafficSentKB = Convert.ToInt32(NetworkStatistics[Inter.Name].InterFace.GetIPv4Statistics().BytesSent / 1024);

								NetworkStatistics[Inter.Name].ctrNetwork.UpdateValue(NetworkStatistics[Inter.Name].TrafficSentKBSec, NetworkStatistics[Inter.Name].TrafficReceivedKBSec);
							}
							else
							{
								NetworkStatistics.Add(Inter.Name, CreateNewNetwork(Inter));
								Update = true;
							}
						}
					}

					var itemsToDelete = NetworkStatistics.Where(x =>
						Interfaces.Find(Y => Y.Name == x.Value.Name) == null ||
						x.Value.InterFace.OperationalStatus != OperationalStatus.Up).ToList();

					foreach (var item in itemsToDelete)
					{
						Update = true;
						NetworkStatistics.Remove(item.Key);
					}

					if (Update)
					{
						UpdatePanel();
					}
				}
				catch (Exception ex)
				{
					TrentoGlobal.WriteToLogFile(ex.Message);
				}
				cancelSource.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(1));
			}
		}

		private delegate void UpdatePanelConsumer();
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
				foreach (var item in NetworkStatistics)
				{
					panelNetwork.Controls[0].Controls.Add(item.Value.ctrNetwork);
				}

				int FirstIndex = 0;
				panelNetwork.Controls[0].Controls[FirstIndex].Margin = new Padding(0);

				panelNetwork.ResumeLayout();
			}
		}

		private delegate void RemoveFromPanelConsumer(Network DriveToRemove);
		private void RemoveFromPanel(Network NetworkToRemove)
		{
			if (panelNetwork.InvokeRequired)
			{
				panelNetwork.Invoke(new RemoveFromPanelConsumer(RemoveFromPanel), NetworkToRemove);
			}
			else
			{
				panelNetwork.SuspendLayout();
				panelNetwork.Controls[0].Controls.Remove(NetworkToRemove.ctrNetwork);
				NetworkToRemove.ctrNetwork.Dispose();
				panelNetwork.ResumeLayout();
			}
		}

		public Panel getPanel()
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
			cancelSource.Cancel();

			Task.WaitAll(task);

			foreach (var item in task)
			{
				item.Dispose();
			}

			cancelSource.Dispose();
		}
	}

	public class Network
	{
		public string Name { get; set; }
		public int TrafficSentKBSec { get; set; }
		public int TrafficReceivedKBSec { get; set; }
		public int TrafficSentKB { get; set; }
		public int TrafficReceivedKB { get; set; }
		public NetworkInterface InterFace { get; set; }
		public CtrNetworkTraffic ctrNetwork { get; set; }
	}
}
