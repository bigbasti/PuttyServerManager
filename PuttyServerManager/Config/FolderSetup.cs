using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PuttyServerManager.Config {

    /// <summary>
    /// Führt eine Überprüfung des Installationsverzeichnisses 
    /// durch und legt die nötigen Ordner an
    /// </summary>
    public static class FolderSetup {

        public static bool SetupDirectory() {

            if (!Directory.Exists(ApplicationSettings.LocalRepositoryPath)) {
                Program.LogWriter.Log("! Local Repository Folder doesn't exist - creating the Folder");

                try {
                    Directory.CreateDirectory(ApplicationSettings.ApplicationPath + "\\.putty");
                    Directory.CreateDirectory(ApplicationSettings.LocalRepositoryPath);
                } catch (Exception ex) {
                    Program.LogWriter.Log("# Could not create Sessions Folder because {0}", ex.Message);
                    return false;
                }
            }

            return true;
        }
    }
}
