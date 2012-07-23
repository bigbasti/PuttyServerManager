using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PuttyServerManager.Config;

namespace PuttyServerManager.Persistence.Repository {
    class RecentSessionRepository : ISessionRepository{
        public bool UserCanEditList() {
            return false;
        }

        public void AddSession(string filename) {
            throw new NotImplementedException();
        }

        public void AddSession(string server, string protocol, int port, string name = "") {
            try {
                string defaultSession = File.ReadAllText(ApplicationPaths.SessionTemplatePath);

                defaultSession = defaultSession.Replace("HostName=", "HostName=" + server);
                defaultSession = defaultSession.Replace("Protocol=", "Protocol=" + protocol);
                defaultSession = defaultSession.Replace("PortNumber=", "PortNumber=" + port);
                defaultSession = defaultSession.Replace("WinTitle=", "WinTitle=" + name);

                File.WriteAllText(Path.Combine(ApplicationPaths.LocalRepositoryPath, name), defaultSession);
            } catch (Exception ex) {
                Program.LogWriter.Log("# Cound not create the Sessionfile in Repostitory because {0}", ex.Message);
            }
        }

        public bool CheckSessionExists(string sessionName) {
            throw new NotImplementedException();
        }

        public bool RenameSession(string sessionName, string newName) {
            throw new NotImplementedException();
        }

        public bool DeleteSession(string sessionName) {
            try {
                File.Delete(Path.Combine(ApplicationPaths.LocalRepositoryPath, sessionName));
            } catch (Exception ex) {
                Program.LogWriter.Log("# Cound not delete the Sessionfile from Repostitory because {0}", ex.Message);
                return false;
            }
            return true;
        }
    }
}
