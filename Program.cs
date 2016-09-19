using System;
using System.Threading;
using System.Windows.Forms;

namespace System_Info
{
	public class Program
	{
		private static Mutex mutex;
		private static CustomApplicationContext app;

		public static void Main()
		{
			try
			{
				using (mutex = new Mutex(true, "System Info 2"))
				{
					if (mutex.WaitOne(TimeSpan.Zero, true))
					{
						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);
						app = new CustomApplicationContext();
						Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
						Application.Run(app);
						mutex.ReleaseMutex();
					}
					else
					{
						MessageBox.Show("System Info is already running");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
		private static void Application_ApplicationExit(object sender, EventArgs e)
		{
			try
			{
				app.Dispose();
				mutex.ReleaseMutex();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}
	}
}
