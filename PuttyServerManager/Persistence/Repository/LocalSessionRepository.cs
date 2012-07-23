using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PuttyServerManager.Config;
using System.Windows.Forms;

namespace PuttyServerManager.Persistence.Repository {
    class LocalSessionRepository : ISessionRepository {

        public bool UserCanEditList() {
            return true;
        }

        public void AddSession(string filename) {
            try {
                Program.LogWriter.Log("Copying new Session to Repository: {0}", filename);

                File.Copy(filename, Path.Combine(ApplicationSettings.LocalRepositoryPath, Path.GetFileName(filename)), true);

            } catch (Exception ex) {
                Program.LogWriter.Log("# Cound not Copy the Sessionfile to Repostitory because {0}", ex.Message);
            }
        }

        public void AddSession(string server, string protocol, int port, string name = "") {
            throw new NotImplementedException();
        }

        public bool CheckSessionExists(string sessionName) {
            return File.Exists(Path.Combine(ApplicationSettings.LocalRepositoryPath, sessionName));
        }

        public bool RenameSession(string sessionName, string newName) {
            try {
                string oldFileName = Path.Combine(ApplicationSettings.LocalRepositoryPath, sessionName);
                string newFileName = Path.Combine(ApplicationSettings.LocalRepositoryPath, newName);
                File.Move(oldFileName, newFileName);
            } catch (Exception ex) {
                MessageBox.Show("Could not Rename the Session.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.LogWriter.Log("# Cound not Rename the Sessionfile in Repostitory because {0}", ex.Message);
                return false;
            }
            return true;
        }

        public bool DeleteSession(string sessionName) {
            try {
                File.Delete(Path.Combine(ApplicationSettings.LocalRepositoryPath, sessionName));
            } catch (Exception ex) {
                Program.LogWriter.Log("# Cound not delete the Sessionfile from Repostitory because {0}", ex.Message);
                return false;
            }
            return true;
        }




    }
}
