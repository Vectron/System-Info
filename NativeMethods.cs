using System;
using System.Runtime.InteropServices;

namespace System_Info
{
	internal class NativeMethods
	{
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, long dwNewLong);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern IntPtr FindWindow(string lpWindowClass, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

		public static void SetOnDesktop(IntPtr hWnd)
		{
			// IntPtr hWnd = new WindowInteropHelper(window).Handle;
			SetParent(hWnd, Getparrenthandle());
		}

		private static IntPtr Getparrenthandle()
		{
			IntPtr hwndWorkerW = IntPtr.Zero;
			IntPtr shellDefView = IntPtr.Zero;
			IntPtr hwndDesktop = IntPtr.Zero;
			IntPtr progMan = FindWindow("ProgMan", "Program Manager");

			if (progMan != IntPtr.Zero)
			{
				shellDefView = FindWindowEx(progMan, IntPtr.Zero, "SHELLDLL_DefView", null);
				if (shellDefView != IntPtr.Zero)
				{
					hwndDesktop = FindWindowEx(shellDefView, IntPtr.Zero, "SysListView32", null);
				}
			}

			while (hwndDesktop == IntPtr.Zero)
			{
				hwndWorkerW = FindWindowEx(IntPtr.Zero, hwndWorkerW, "WorkerW", null);
				if (hwndWorkerW == IntPtr.Zero)
				{
					break;
				}

				shellDefView = FindWindowEx(hwndWorkerW, IntPtr.Zero, "SHELLDLL_DefView", null);
				if (shellDefView == IntPtr.Zero)
				{
					continue;
				}

				hwndDesktop = FindWindowEx(shellDefView, IntPtr.Zero, "SysListView32", null);
			}

			return hwndDesktop;
		}
	}
}
