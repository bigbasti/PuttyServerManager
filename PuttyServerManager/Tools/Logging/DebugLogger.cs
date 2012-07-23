using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace PuttyServerManager.Tools.Logging {
    public class DebugLogger : ILogger {

        public void Log(string message) {
            Debug.WriteLine(message);
        }

        public void Log(string message, params object[] args) {
            Debug.WriteLine(string.Format(message, args));
        }
    }
}
