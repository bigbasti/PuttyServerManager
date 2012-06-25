using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuttyServerGUI2.Tools.Logging {
    public interface ILogger {

        /// <summary>
        /// Schreibt eine nachricht ins Log
        /// </summary>
        /// <param name="message">Die zu schreibende Nachricht</param>
        void Log(string message);

        /// <summary>
        /// Eine formatierte Ausgabe ins Log schreiben
        /// </summary>
        /// <param name="message">Nachricht mit Platzhaltern</param>
        /// <param name="args">Argumente für die Platzhalter</param>
        void Log(string message, params object[] args);
    }
}
