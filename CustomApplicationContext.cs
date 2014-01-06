using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;

namespace System_Info
{
    [ComVisible(false)]
    public class CustomApplicationContext : ApplicationContext
    {
        SideBar FormSideBar;
        public CustomApplicationContext()
        {
            InitializeComponents();
            System_Info.SystemInfo.Createlist();
            FormSideBar = new SideBar();
            FormSideBar.Show();
            setparrent();
            Options_Screen FrmOptions = new Options_Screen();
            FrmOptions.Show();
        }

        # region Constructor

        private System.ComponentModel.IContainer components;	// a list of components to dispose when the context is disposed
        public NotifyIcon notifyIcon;

        private void InitializeComponents()
        {
            /*
             * update the local settings file with net settings
             * if the version is different and the new version has the CallUpgrade variable set to true
             * the old settings will be copied to the new settingsfile
             */
            if (Properties.Settings.Default.CallUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Reload();
                Properties.Settings.Default.CallUpgrade = false;
                Properties.Settings.Default.Save();
            }

            /*
             * creating a icon in the notification area of the taskbar
             * and giving the icon a menu when clicked
            */
            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon("route.ico"),
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
            if (System_Info.Properties.Settings.Default.LockPlacement == true) { LockPlacement.CheckState = CheckState.Checked; }
            else { LockPlacement.CheckState = CheckState.Unchecked; }
            if (System_Info.Properties.Settings.Default.PinToDesktop == true) { PinToDesktop.CheckState = CheckState.Checked; }
            else { PinToDesktop.CheckState = CheckState.Unchecked; }
        }

        # endregion

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

        # region Menu Eventhandlers

        void Options_Click(object sender, EventArgs e)
        {
            Options_Screen FrmOptions = new Options_Screen();
            FrmOptions.Show();
        }
        void LockPlacement_Click(object sender, EventArgs e)
        {
            if (LockPlacement.CheckState != CheckState.Checked)
            {
                LockPlacement.CheckState = CheckState.Checked;
                System_Info.Properties.Settings.Default.LockPlacement = true;
            }
            else { LockPlacement.CheckState = CheckState.Unchecked; System_Info.Properties.Settings.Default.LockPlacement = false; }
            System_Info.Properties.Settings.Default.Save();
        }
        void PinToDesktop_Click(object sender, EventArgs e)
        {
            if (PinToDesktop.CheckState != CheckState.Checked)
            {
                PinToDesktop.CheckState = CheckState.Checked;
                System_Info.Properties.Settings.Default.PinToDesktop = true;
            }
            else
            {
                PinToDesktop.CheckState = CheckState.Unchecked;
                System_Info.Properties.Settings.Default.PinToDesktop = false;
            }
            System_Info.Properties.Settings.Default.Save();
            setparrent();
        }
        private void Exit_Click(object sender, EventArgs e)
        {
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

        # endregion

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
                if (hwndWorkerW == IntPtr.Zero) break;
                hShellDefView = FindWindowEx(hwndWorkerW, IntPtr.Zero, "SHELLDLL_DefView", null);
                if (hShellDefView == IntPtr.Zero) continue;
                hwndDesktop = FindWindowEx(hShellDefView, IntPtr.Zero, "SysListView32", null);
            }
            return hwndDesktop;
        }
        public void setparrent()
        {
            if (System_Info.Properties.Settings.Default.PinToDesktop) { SetWindowLong(FormSideBar.Handle, GWL_HWNDPARENT, getparrenthandle()); }
            else { SetWindowLong(FormSideBar.Handle, GWL_HWNDPARENT, IntPtr.Zero); }
        }
    }
}

