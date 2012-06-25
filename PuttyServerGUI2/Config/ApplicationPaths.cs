using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuttyServerGUI2.Properties;

namespace PuttyServerGUI2.Config {
    public static class ApplicationPaths {

        public static string ApplicationPath {
            get {
                return Application.StartupPath;
            }
        }

        public static string LocalRepositoryPath {
            get {
                return ApplicationPath + "\\.putty\\sessions";
            }
        }

        public static string RemoteRepositoryPath {
            get {
                return Settings.Default.TeamRepositoryPath;
            }
            set {
                Settings.Default.TeamRepositoryPath = value;
                Settings.Default.Save();
            }
        }

        public static string PuttyLocation {
            get {
                return ApplicationPath + "\\putty.exe";
            }
        }

        public static RepositoryType RepositoryType {
            get {
                return Settings.Default.LocalRepository == true ? RepositoryType.Local : RepositoryType.Remote;
            }
            set {
                Settings.Default.LocalRepository = value == RepositoryType.Local ? true : false;
            }
        }

        public static string SessionTemplatePath {
            get {
                return ApplicationPath + "\\sessiontemplate";
            }
        }

        public static string TeamSessionListPath {
            get {
                return Settings.Default.TeamSessionListPath;
            }
            set {
                Settings.Default.TeamSessionListPath = value;
            }
        }

        public static string LocalSessionListPath {
            get {
                return ApplicationPath + "\\sessionlist.xml";
            }
        }

        public static string RecentSessionListPath {
            get {
                return ApplicationPath + "\\recentSessionlist.xml";
            }
        }
    }
}
