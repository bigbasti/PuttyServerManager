﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PuttyServerGUI2.Config {

    /// <summary>
    /// Führt eine Überprüfung des Installationsverzeichnisses 
    /// durch und legt die nötigen Ordner an
    /// </summary>
    public static class FolderSetup {

        public static bool SetupDirectory() {

            if (!Directory.Exists(ApplicationPaths.LocalRepositoryPath)) {
                Program.LogWriter.Log("! Local Repository Folder doesn't exist - creating the Folder");

                try {
                    Directory.CreateDirectory(ApplicationPaths.ApplicationPath + "\\.putty");
                    Directory.CreateDirectory(ApplicationPaths.LocalRepositoryPath);
                } catch (Exception ex) {
                    Program.LogWriter.Log("# Could not create Sessions Folder because {0}", ex.Message);
                    return false;
                }
            }

            return true;
        }
    }
}
