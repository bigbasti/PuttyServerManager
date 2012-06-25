using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

using PuttyServerGUI2.Tools.Logging;

namespace PuttyServerGUI2 {
    static class Program {

        public static ILogger LogWriter;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Debugger.IsAttached) {
                LogWriter = new DebugLogger();
                LogWriter.Log("DebugLogger wurde aktiviert!");
            } else {
                LogWriter = new LoggerMock();
            }

            Application.Run(new frmMainWindow());
        }
    }
}
