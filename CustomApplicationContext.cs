using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System_Info
{
	[ComVisible(false)]
	public class CustomApplicationContext : ApplicationContext
	{
		private const int GwlHwndParrent = -8;

		private SideBar formSideBar;

		private ToolStripMenuItem exit = new ToolStripMenuItem("&Exit");
		private ToolStripMenuItem lockPlacement = new ToolStripMenuItem("Lock Placement");
		private ToolStripMenuItem pinToDesktop = new ToolStripMenuItem("Pin To Desktop");
		private ToolStripMenuItem options = new ToolStripMenuItem("Options");

		// a list of components to dispose when the context is disposed
		private System.ComponentModel.IContainer components;
		private NotifyIcon notifyIcon;

		public CustomApplicationContext()
		{
			InitializeComponents();
			formSideBar = new SideBar();
			formSideBar.Show();
			Setparrent();
		}

		private delegate void CloseMethod(Form form);

		public void Setparrent()
		{
			if (Properties.Settings.Default.PinToDesktop)
			{
				NativeMethods.SetOnDesktop(formSideBar.Handle);
			}
			else
			{
				NativeMethods.SetParent(formSideBar.Handle, IntPtr.Zero);
			}
		}

		// disposing al the created components
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				exit?.Dispose();
				lockPlacement?.Dispose();
				pinToDesktop?.Dispose();
				notifyIcon?.Dispose();
				components?.Dispose();
				formSideBar?.Dispose();
			}
		}

		private static void CloseForm(Form form)
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

		private void InitializeComponents()
		{
			// creating a icon in the notification area of the taskbar and giving the icon a menu when clicked
			components = new System.ComponentModel.Container();
			notifyIcon = new NotifyIcon(components)
			{
				ContextMenuStrip = new ContextMenuStrip(),
				Icon = Properties.Resources.route,
				Text = "System Information",
				Visible = true
			};

			BuildContexMenu();
			notifyIcon.MouseUp += NotifyIcon_MouseUp;
		}

		private void BuildContexMenu()
		{
			pinToDesktop.Click += new EventHandler(PinToDesktop_Click);
			lockPlacement.Click += new EventHandler(LockPlacement_Click);
			exit.Click += Exit_Click;
			options.Click += new EventHandler(Options_Click);

			notifyIcon.ContextMenuStrip.Items.Add(options);
			notifyIcon.ContextMenuStrip.Items.Add(pinToDesktop);
			notifyIcon.ContextMenuStrip.Items.Add(lockPlacement);
			notifyIcon.ContextMenuStrip.Items.Add(exit);

			// putting checked markers infront of the menu item if this function is enabled
			lockPlacement.CheckState = Properties.Settings.Default.LockPlacement ? CheckState.Checked : CheckState.Unchecked;
			pinToDesktop.CheckState = Properties.Settings.Default.PinToDesktop ? CheckState.Checked : CheckState.Unchecked;
		}

		private void Options_Click(object sender, EventArgs e)
		{
			Options_Screen frmOptions = new Options_Screen();
			frmOptions.ShowDialog();
		}

		private void LockPlacement_Click(object sender, EventArgs e)
		{
			if (lockPlacement.CheckState != CheckState.Checked)
			{
				lockPlacement.CheckState = CheckState.Checked;
				Properties.Settings.Default.LockPlacement = true;
			}
			else
			{
				lockPlacement.CheckState = CheckState.Unchecked;
				Properties.Settings.Default.LockPlacement = false;
			}

			Properties.Settings.Default.Save();
		}

		private void PinToDesktop_Click(object sender, EventArgs e)
		{
			if (pinToDesktop.CheckState != CheckState.Checked)
			{
				pinToDesktop.CheckState = CheckState.Checked;
				Properties.Settings.Default.PinToDesktop = true;
			}
			else
			{
				pinToDesktop.CheckState = CheckState.Unchecked;
				Properties.Settings.Default.PinToDesktop = false;
			}

			Properties.Settings.Default.Save();
			Setparrent();
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

		private void NotifyIcon_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
				mi.Invoke(notifyIcon, null);
			}
		}
	}
}