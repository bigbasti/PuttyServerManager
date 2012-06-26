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

        public static string LocalSessionListPath {
            get {
                return ApplicationPath + "\\sessionlist.xml";
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

        public static string RemoteSessionListPath {
            get {
                return Settings.Default.TeamSessionListPath;
            }
            set {
                Settings.Default.TeamSessionListPath = value;
                Settings.Default.Save();
            }
        }

        public static bool RemoteSessionIsConfigured {
            get {
                return !string.IsNullOrEmpty(RemoteRepositoryPath);
            }
        }

        public static string PuttyLocation {
            get {
                return ApplicationPath + "\\putty.exe";
            }
        }

        public static string PuttyAgentLocation {
            get {
                return ApplicationPath + "\\pagent.exe";
            }
        }

        public static bool FirstStart {
            get {
                return Settings.Default.FirstStart;
            }
            set {
                Settings.Default.FirstStart = value;
            }
        }

        public static string SessionTemplatePath {
            get {
                return ApplicationPath + "\\sessiontemplate";
            }
        }

        public static string RecentSessionListPath {
            get {
                return ApplicationPath + "\\recentSessionlist.xml";
            }
        }

        public static bool UsePuttyAgent {
            get {
                return Settings.Default.UsePuttyAgent;
            }
            set {
                Settings.Default.UsePuttyAgent = value;
                Settings.Default.Save();
            }
        }

        public static string PuttyAgentParameters {
            get {
                return Settings.Default.PuttyAgentParameters;
            }
            set {
                Settings.Default.PuttyAgentParameters = value;
                Settings.Default.Save();
            }
        }

        public static string TeamUsername {
            get {
                return Settings.Default.TeamUsername;
            }
            set {
                Settings.Default.TeamUsername = value;
                Settings.Default.Save();
            }
        }

        public static bool StartWithWindows {
            get {
                return Settings.Default.StartAppWithWindows;
            }
            set {
                Settings.Default.StartAppWithWindows = value;
                Settings.Default.Save();
            }
        }

    }
}
