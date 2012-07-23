using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuttyServerManager.Persistence.Repository {
    public interface ISessionRepository {

        /// <summary>
        /// Gibt an, ob es dem Benutzer erlaubt ist die Liste zu bearbeiten
        /// </summary>
        bool UserCanEditList();

        /// <summary>
        /// Fügt eine neue Session zu der Sessionsammlung hinzu
        /// </summary>
        /// <param name="filename">Pfad zu der Sessiondatei</param>
        void AddSession(string filename);

        /// <summary>
        /// Erstellt eine neue Session unf speichert diese in der Sammlung
        /// </summary>
        /// <param name="server">IP Oder Servername</param>
        /// <param name="protocol">Protokoll</param>
        /// <param name="port">Port</param>
        /// <param name="name">Optional: Beschreibender Name</param>
        void AddSession(string server, string protocol, int port, string name = "");

        /// <summary>
        /// Prüfen, ob es die Session in der Sammlung gibt
        /// </summary>
        /// <param name="sessionName">Session die geprüft werden soll</param>
        /// <returns>True wenn die Session existiert</returns>
        bool CheckSessionExists(string sessionName);

        /// <summary>
        /// Benennt eine Session in der Sammlung um
        /// </summary>
        /// <param name="sessionName">Session die umbenannt werden soll</param>
        /// <param name="newName">Der neue Name für die Session</param>
        bool RenameSession(string sessionName, string newName);

        /// <summary>
        /// Löscht eine Session aus der Sammlung
        /// </summary>
        /// <param name="sessionName">Name der Session die gelöscht werden soll</param>
        bool DeleteSession(string sessionName);
    }
}
