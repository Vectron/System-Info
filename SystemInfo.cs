using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Threading;
using Trento_Library;

namespace System_Info
{
    public delegate void ChangedEventHandler();

    static class SystemInfo
    {
        private static bool CancelThreads;
        private static Thread oThreadHdd;
        private static Thread oThreadNetwork;
        private static Thread oThreadCPU;
        private static Thread oThreadRam;

        public static void Createlist()
        {
            oThreadHdd = new Thread(new ThreadStart(CreateHddList));
            oThreadNetwork = new Thread(new ThreadStart(CreateNetworkList));
            oThreadCPU = new Thread(new ThreadStart(CreateCPUList));
            oThreadRam = new Thread(new ThreadStart(CreateMemoryList));

            oThreadCPU.Start();
            oThreadRam.Start();
            oThreadHdd.Start();
            oThreadNetwork.Start();

            oThreadHdd.Join();
            oThreadNetwork.Join();
            oThreadRam.Join();
            oThreadCPU.Join();

            oThreadHdd = new Thread(new ThreadStart(UpdateHddInfo));
            oThreadNetwork = new Thread(new ThreadStart(UpdateTraffic));
            oThreadCPU = new Thread(new ThreadStart(UpdateCPUInfo));
            oThreadRam = new Thread(new ThreadStart(UpdateMemory));

            CancelThreads = false;

            oThreadHdd.Start();
            oThreadNetwork.Start();
            oThreadCPU.Start();
            oThreadRam.Start();
        }

        #region Lists

        private static List<HDD> _ListHddInfo = new List<HDD>();
        public static List<HDD> ListHddInfo { get { return _ListHddInfo; } }
        private static List<Network> _ListNetworkStatistics = new List<Network>();
        public static List<Network> ListNetworkStatistics { get { return _ListNetworkStatistics; } }
        private static List<CPU> _ListCpuInfo = new List<CPU>();
        public static List<CPU> ListCpuInfo { get { return _ListCpuInfo; } }
        public static MemoryUse Ram = new MemoryUse();

        #endregion Lists

        #region CreateList Methods

        private static void CreateHddList()
        {
            _ListHddInfo.Clear();
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady == true)
                {
                    if (drive.DriveType == DriveType.Fixed || drive.DriveType == DriveType.Network)
                    {
                        HDD Drive = new HDD();
                        Drive.Name = drive.Name;
                        Drive.AvailableFreeSpace = drive.AvailableFreeSpace;
                        Drive.TotalSize = drive.TotalSize;
                        Drive.HddType = drive.DriveType;
                        _ListHddInfo.Add(Drive);
                    }
                }
            }
        }
        private static void CreateNetworkList()
        {
            try
            {
                _ListNetworkStatistics.Clear();
                foreach (NetworkInterface Inter in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (Inter.OperationalStatus == OperationalStatus.Up &&
                        (Inter.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                            Inter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                            Inter.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet))
                    {
                        Network Info = new Network();
                        Info.Name = Inter.Name;
                        Info.InterFace = Inter;
                        Info.TrafficSentKBSec = 0;
                        Info.TrafficReceivedKBSec = 0;
                        Info.TrafficReceivedKB = Convert.ToInt32(Inter.GetIPv4Statistics().BytesReceived / 1024);
                        Info.TrafficSentKB = Convert.ToInt32(Inter.GetIPv4Statistics().BytesSent / 1024);
                        _ListNetworkStatistics.Add(Info);
                    }
                }
                _ListNetworkStatistics.Sort(delegate(Network N1, Network N2) { return N1.Name.CompareTo(N2.Name); });
            }
            catch (Exception ex)
            {
                Global.WriteToLogFile(ex.Message);
            }

        }
        private static void CreateCPUList()
        {
            {
                _ListCpuInfo.Clear();
                CPU Info = new CPU();
                Info.CpuTotalUse = new PerformanceCounter();
                Info.CpuTotalUse.CategoryName = "Processor";
                Info.CpuTotalUse.CounterName = "% Processor Time";
                Info.CpuTotalUse.InstanceName = "_Total";
                Info.CpuTotalUse.ReadOnly = true;
                Info.NumberOfLogicalProcessors = Convert.ToInt16(Environment.ProcessorCount); //Convert.ToInt16(MO_CPU.Properties["NumberOfLogicalProcessors"].Value);
                Info.CpuCoreUse = new PerformanceCounter[(int)Info.NumberOfLogicalProcessors];
                Info.LoadPercentageCore = new short[(int)Info.NumberOfLogicalProcessors];

                for (int i = 0; i < Info.NumberOfLogicalProcessors; i++)
                {
                    Info.CpuCoreUse[i] = new PerformanceCounter();
                    Info.CpuCoreUse[i].CategoryName = "Processor";
                    Info.CpuCoreUse[i].CounterName = "% Processor Time";
                    Info.CpuCoreUse[i].InstanceName = i.ToString();
                    Info.CpuCoreUse[i].ReadOnly = true;
                    Info.LoadPercentageCore[i] = 0;
                }
                _ListCpuInfo.Add(Info);
                Info.Dispose();
            }

        }
        private static void CreateMemoryList()
        {
            using (ManagementObjectSearcher MOS_System = new ManagementObjectSearcher("select TotalVisibleMemorySize from Win32_OperatingSystem"))
            {
                foreach (ManagementObject MO_System in MOS_System.Get())
                {
                    Ram.PC_AvailableRam = new PerformanceCounter();
                    Ram.PC_AvailableRam.CategoryName = "Memory";
                    Ram.PC_AvailableRam.CounterName = "Available kBytes";
                    Ram.PC_AvailableRam.ReadOnly = true;
                    Ram.TotalVisibleMemorySize = Convert.ToUInt32(MO_System.Properties["TotalVisibleMemorySize"].Value);
                }
            }

        }

        #endregion CreateList Methods

        #region UpdateLists

        public static void UpdateHddInfo()
        {
            while (!CancelThreads)
            {
                foreach (HDD Disk in _ListHddInfo)
                {
                    DriveInfo drive = new DriveInfo(Disk.Name);
                    if (drive.IsReady)
                    {
                        Disk.Name = drive.Name;
                        Disk.AvailableFreeSpace = drive.AvailableFreeSpace;
                        Disk.TotalSize = drive.TotalSize;
                        Disk.HddType = drive.DriveType;
                    }
                }
                if (HDD_Changed != null)
                {
                    HDD_OnChanged();
                }
                Thread.Sleep(10000);
            }
        }
        public static void UpdateTraffic()
        {
            while (!CancelThreads)
            {
                try
                {
                    foreach (Network Item in _ListNetworkStatistics)
                    {
                        Item.TrafficSentKBSec = Convert.ToInt32(((Item.InterFace.GetIPv4Statistics().BytesSent / 1024) - Item.TrafficSentKB));
                        Item.TrafficReceivedKBSec = Convert.ToInt32(((Item.InterFace.GetIPv4Statistics().BytesReceived / 1024) - Item.TrafficReceivedKB));
                        Item.TrafficReceivedKB = Convert.ToInt32(Item.InterFace.GetIPv4Statistics().BytesReceived / 1024);
                        Item.TrafficSentKB = Convert.ToInt32(Item.InterFace.GetIPv4Statistics().BytesSent / 1024);
                    }
                    if (Network_Changed != null)
                    {
                        Network_OnChanged();
                    }
                }
                catch (Exception ex)
                {
                    Global.WriteToLogFile(ex.Message);
                }
                Thread.Sleep(1000);
            }
        }
        public static void UpdateCPUInfo()
        {
            while (!CancelThreads)
            {
                foreach (CPU Items in _ListCpuInfo)
                {
                    for (int i = 0; i < Items.NumberOfLogicalProcessors; i++)
                    {
                        Items.LoadPercentageCore[i] = Convert.ToInt16(Math.Truncate(Items.CpuCoreUse[i].NextValue() * 100) / 100);
                    }
                }
                if (CPU_Changed != null)
                {
                    CPU_Changed();
                }
                Thread.Sleep(1000);
            }
        }
        public static void UpdateMemory()
        {
            while (!CancelThreads)
            {
                if (Ram_Changed != null)
                {
                    Ram_OnChanged();
                }
                Thread.Sleep(1000);
            }
        }

        #endregion UpdateLists

        #region Eventhandlers

        // An event that clients can use to be notified whenever the
        // elements of the list change.
        public static event ChangedEventHandler HDD_Changed;
        public static event ChangedEventHandler Network_Changed;
        public static event ChangedEventHandler CPU_Changed;
        public static event ChangedEventHandler Ram_Changed;

        // Invoke the Changed event; called whenever list changes
        public static void HDD_OnChanged()
        {
            if (HDD_Changed != null)
                HDD_Changed();
        }
        public static void Network_OnChanged()
        {
            if (Network_Changed != null)
                Network_Changed();
        }
        public static void CPU_OnChanged()
        {
            if (CPU_Changed != null)
                CPU_Changed();
        }
        public static void Ram_OnChanged()
        {
            if (Ram_Changed != null)
                Ram_Changed();
        }
        #endregion Eventhandlers
    }

    #region DataLayout Classes

    public class Network
    {
        public string Name { get; set; }
        public int TrafficSentKBSec { get; set; }
        public int TrafficReceivedKBSec { get; set; }
        public int TrafficSentKB { get; set; }
        public int TrafficReceivedKB { get; set; }
        public NetworkInterface InterFace { get; set; }
    }
    public class CPU
    {
        public short NumberOfLogicalProcessors { get; set; }
        public short[] LoadPercentageCore { get; set; }
        public PerformanceCounter[] CpuCoreUse { get; set; }
        public PerformanceCounter CpuTotalUse = null;

        internal void Dispose()
        {
            CpuTotalUse.Dispose();
            for (int i = 0; i < CpuCoreUse.Length; i++) { CpuCoreUse[i].Dispose(); }
        }
    }
    public class MemoryUse
    {
        public uint AvailableRam { get { return (uint)PC_AvailableRam.NextValue(); } }
        public uint TotalVisibleMemorySize { get; set; }
        public PerformanceCounter PC_AvailableRam = null;

        ~MemoryUse() { PC_AvailableRam.Dispose(); }
    }
    public class HDD
    {
        public string Name { get; set; }
        public long AvailableFreeSpace { get; set; }
        public long TotalSize { get; set; }
        public DriveType HddType { get; set; }
    }

    #endregion DataLayout Classes

}
