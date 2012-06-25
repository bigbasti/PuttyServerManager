using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using PuttyServerGUI2.Config;

namespace PuttyServerGUI2.Persistence.Repository {
    class TeamSessionRepository : ISessionRepository {

        public bool UserCanEditList() {
            return false;
        }

        public void AddSession(string filename) {
            try {
                Program.LogWriter.Log("Copying new Session to Repository: {0}", filename);

                File.Copy(filename, Path.Combine(ApplicationPaths.LocalRepositoryPath, Path.GetFileName(filename)), true);

            } catch (Exception ex) {
                Program.LogWriter.Log("# Cound not Copy the Sessionfile to Repostitory because {0}", ex.Message);
            }
        }

        public void AddSession(string server, string protocol, int port, string name = "") {
            throw new NotImplementedException();
        }

        public bool CheckSessionExists(string sessionName) {
            return File.Exists(Path.Combine(ApplicationPaths.LocalRepositoryPath, sessionName));
        }

        public bool RenameSession(string sessionName, string newName) {
            //Nicht möglich bei Teamsammlungen
            throw new NotImplementedException();

        }

        public bool DeleteSession(string sessionName) {
            //Nicht möglich bei Teamsammlungen
            throw new NotImplementedException();
        }
    }
}
