using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace mailbox_desktop
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, Application.ProductName);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {

                General.configPath = Application.StartupPath + @"\config.xml";

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                //

                if (!File.Exists(General.configPath))
                {
                    frmSettings x = new frmSettings(true);

                    x.ShowDialog();

                    Application.Run(new Form1());
                }
                else
                {
                    General.cfg = XmlHelper.FromXmlFile<ConfigApp>(General.configPath);
                    Application.Run(new Form1());
                }
            }
            else
            {
                SingleExecution.SwitchToCurrentInstance();
            }


        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ErrorReporter reporter = new ErrorReporter(e.Exception);
            reporter.ShowDialog();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorReporter reporter = new ErrorReporter((Exception)e.ExceptionObject);
            reporter.ShowDialog();
        }
    }
}
