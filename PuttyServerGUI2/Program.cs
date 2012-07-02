using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

using PuttyServerGUI2.Tools.Logging;
using PuttyServerGUI2.Config;
using System.Threading;
using PuttyServerGUI2.Tools;

namespace PuttyServerGUI2 {
    static class Program {

        public static ILogger LogWriter;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string [] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form applicationMainWindow = new frmMainWindow();
            ApplicationPaths.ApplicationMainForm = applicationMainWindow;

            if (Debugger.IsAttached) {
                LogWriter = new DebugLogger();
                LogWriter.Log("DebugLogger wurde aktiviert!");
            } else {
                LogWriter = new LoggerMock();
            }

            SingleInstanceHelper.RegisterRemotingService();

            if (ApplicationPaths.RuninSingleInstanceMode) {
                bool onlyInstance = false;
                Mutex mutex = new Mutex(true, "PuttyServerGUI2", out onlyInstance);

                LogWriter.Log("Running in single instance mode - is first instance: {0}", onlyInstance);
                if (!onlyInstance) {
                    SingleInstanceHelper.LaunchInExistingInstance(args);
                    Environment.Exit(0);
                }
            }




            
            Application.Run(applicationMainWindow);
            
        }
    }
}
