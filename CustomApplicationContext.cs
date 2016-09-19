using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System_Info
{
	[ComVisible(false)]
	public class CustomApplicationContext : ApplicationContext
	{
		private SideBar FormSideBar;
		public CustomApplicationContext()
		{
			InitializeComponents();
			FormSideBar = new SideBar();
			FormSideBar.Show();
			setparrent();
		}

		#region Constructor
		// a list of components to dispose when the context is disposed
		private System.ComponentModel.IContainer components;
		private NotifyIcon notifyIcon;

		private void InitializeComponents()
		{
			//creating a icon in the notification area of the taskbar and giving the icon a menu when clicked
			components = new System.ComponentModel.Container();
			notifyIcon = new NotifyIcon(components)
			{
				ContextMenuStrip = new ContextMenuStrip(),
				Icon = Properties.Resources.route,
				Text = "System Information",
				Visible = true
			};

			BuildContexMenu();
			notifyIcon.MouseUp += notifyIcon_MouseUp;
		}

		private ToolStripMenuItem Exit = new ToolStripMenuItem("&Exit");
		private ToolStripMenuItem LockPlacement = new ToolStripMenuItem("Lock Placement");
		private ToolStripMenuItem PinToDesktop = new ToolStripMenuItem("Pin To Desktop");
		private ToolStripMenuItem Options = new ToolStripMenuItem("Options");

		private void BuildContexMenu()
		{
			PinToDesktop.Click += new EventHandler(PinToDesktop_Click);
			LockPlacement.Click += new EventHandler(LockPlacement_Click);
			Exit.Click += Exit_Click;
			Options.Click += new EventHandler(Options_Click);

			notifyIcon.ContextMenuStrip.Items.Add(Options);
			notifyIcon.ContextMenuStrip.Items.Add(PinToDesktop);
			notifyIcon.ContextMenuStrip.Items.Add(LockPlacement);
			notifyIcon.ContextMenuStrip.Items.Add(Exit);

			//putting checked markers infront of the menu item if this function is enabled
			LockPlacement.CheckState = Properties.Settings.Default.LockPlacement ? CheckState.Checked : CheckState.Unchecked;
			PinToDesktop.CheckState = Properties.Settings.Default.PinToDesktop ? CheckState.Checked : CheckState.Unchecked;
		}
		#endregion

		#region Destructor
		delegate void CloseMethod(Form form);

		static private void CloseForm(Form form)
		{
			if (!form.IsDisposed)
			{
				if (form.InvokeRequired)
				{
					CloseMethod method = new CloseMethod(CloseForm);
					form.Invoke(method, new object[] { form });
				}
				else
				{
					form.Close();
				}
			}
		}

		//disposing al the created components
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				Exit.Dispose();
				LockPlacement.Dispose();
				PinToDesktop.Dispose();
				notifyIcon.Dispose();
				components.Dispose();
			}
		}
		#endregion Destructer

		#region Menu Eventhandlers
		void Options_Click(object sender, EventArgs e)
		{
			Options_Screen FrmOptions = new Options_Screen();
			FrmOptions.ShowDialog();
		}
		void LockPlacement_Click(object sender, EventArgs e)
		{
			if (LockPlacement.CheckState != CheckState.Checked)
			{
				LockPlacement.CheckState = CheckState.Checked;
				Properties.Settings.Default.LockPlacement = true;
			}
			else
			{
				LockPlacement.CheckState = CheckState.Unchecked;
				Properties.Settings.Default.LockPlacement = false;
			}
			Properties.Settings.Default.Save();
		}
		void PinToDesktop_Click(object sender, EventArgs e)
		{
			if (PinToDesktop.CheckState != CheckState.Checked)
			{
				PinToDesktop.CheckState = CheckState.Checked;
				Properties.Settings.Default.PinToDesktop = true;
			}
			else
			{
				PinToDesktop.CheckState = CheckState.Unchecked;
				Properties.Settings.Default.PinToDesktop = false;
			}
			Properties.Settings.Default.Save();
			setparrent();
		}
		private async void Exit_Click(object sender, EventArgs e)
		{
			await SystemInfo.Exit();
			FormCollection forms = Application.OpenForms;
			for (int i = 0; i < forms.Count; i++)
			{
				CloseForm(forms[i]);
			}
			ExitThread();
			notifyIcon.Visible = false; // should remove lingering tray icon
		}
		private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
				mi.Invoke(notifyIcon, null);
			}
		}
		#endregion

		#region DllInports
		[DllImport("user32.dll", SetLastError = true)]
		static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindow(string lpWindowClass, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
		const int GWL_HWNDPARENT = -8;
		#endregion dllInports

		private IntPtr getparrenthandle()
		{
			IntPtr hwndWorkerW = IntPtr.Zero;
			IntPtr hShellDefView = IntPtr.Zero;
			IntPtr hwndDesktop = IntPtr.Zero;
			IntPtr hProgMan = FindWindow("ProgMan", null);
			if (hProgMan != IntPtr.Zero)
			{
				hShellDefView = FindWindowEx(hProgMan, IntPtr.Zero, "SHELLDLL_DefView", null);
				if (hShellDefView != IntPtr.Zero)
				{
					hwndDesktop = FindWindowEx(hShellDefView, IntPtr.Zero, "SysListView32", null);
				}
			}
			while (hwndDesktop == IntPtr.Zero)
			{
				hwndWorkerW = FindWindowEx(IntPtr.Zero, hwndWorkerW, "WorkerW", null);
				if (hwndWorkerW == IntPtr.Zero)
					break;
				hShellDefView = FindWindowEx(hwndWorkerW, IntPtr.Zero, "SHELLDLL_DefView", null);
				if (hShellDefView == IntPtr.Zero)
					continue;
				hwndDesktop = FindWindowEx(hShellDefView, IntPtr.Zero, "SysListView32", null);
			}
			return hwndDesktop;
		}
		public void setparrent()
		{
			if (Properties.Settings.Default.PinToDesktop)
			{
				SetWindowLong(FormSideBar.Handle, GWL_HWNDPARENT, getparrenthandle());
			}
			else
			{
				SetWindowLong(FormSideBar.Handle, GWL_HWNDPARENT, IntPtr.Zero);
			}
		}
	}
}

