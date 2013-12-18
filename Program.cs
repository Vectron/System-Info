using System;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;

[assembly: AssemblyVersionAttribute("2.0.0.2")]
[assembly: AssemblyTitle("Displays System information")]
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
                mutex = new Mutex(true, "System Info 2");
                if (mutex.WaitOne(TimeSpan.Zero, true))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    app = new CustomApplicationContext();
                    Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
                    Application.Run(app);
                    mutex.ReleaseMutex();
                    mutex.Dispose();

                }
                else { MessageBox.Show("System Info is already running"); }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                if (((AbandonedMutexException)ex).Mutex != null) ((AbandonedMutexException)ex).Mutex.ReleaseMutex();
            }
        }
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                app.Dispose();
                mutex.ReleaseMutex();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
