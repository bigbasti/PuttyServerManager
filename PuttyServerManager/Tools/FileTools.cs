using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using PuttyServerManager.Config;
using PuttyServerManager.Tools.Extensions;
using WeifenLuo.WinFormsUI.Docking;
using PuttyServerManager.WindowTools;
using PuttyServerManager.ToolWindows;
using System.Windows.Forms;
using PuttyServerManager.Persistence.Repository;

namespace PuttyServerManager.Tools {
    public class FileTools {

        /// <summary>
        /// Startet den PuttyAgent falls der Benutzer dies in den Einstellungen konfiguriert hat
        /// </summary>
        public static void StartPuttyAgentIfNeeded() {
            if (ApplicationSettings.UsePuttyAgent) {
                if (Process.GetProcessesByName("pageant").Length < 1) {
                    if (File.Exists(ApplicationSettings.PuttyAgentLocation)) {
                        ProcessStartInfo info = new ProcessStartInfo(ApplicationSettings.PuttyAgentLocation, ApplicationSettings.PuttyAgentParameters);
                        Process.Start(info);
                    }
                }
            }
        }

        /// <summary>
        /// Startet eine Putty Session in einem gedockten Fenster
        /// </summary>
        /// <param name="sessionName">Name der Session die gestartet werden soll</param>
        /// <param name="callForm">Das Formular auf dem die Action Invoked werden soll</param>
        /// <param name="containerForm">Das Formular in dem das neue Fenster angedockt werden soll</param>
        /// <param name="dockPanel">Das für das Docking verantwortliche DockPanel</param>
        /// <param name="dockstate">Angabe wie das neue Fenster angedockt werden soll</param>
        public static void StartPuttySession(string sessionName, Form callForm, Form containerForm, DockPanel dockPanel, DockState dockstate = DockState.Document) {

            StartPuttyAgentIfNeeded();

            twiPutty puttyWindow = null;

            ApplicationClosedCallback callback = delegate(bool closed) {
                if (puttyWindow != null) {

                    if (puttyWindow.InvokeRequired) {
                        callForm.BeginInvoke((MethodInvoker)delegate() {
                            puttyWindow.Close();
                        });
                    } else {
                        puttyWindow.Close();
                    }
                }
            };

            puttyWindow = new twiPutty(sessionName, callback, containerForm);
            puttyWindow.Show(dockPanel, dockstate);
        }

        /// <summary>
        /// Startet die angegebene Putty Session ein einem eigenen PuTTY Fenster. 
        /// Dieses Fenster kann nicht im MSM angedockt werden und bleibt selbstständig.
        /// </summary>
        /// <param name="sessionName">Name der Session die gestartet werden soll</param>
        public static void StartNativePuttySession(string sessionName) {
            //On demand: start putty agent
            StartPuttyAgentIfNeeded();

            ProcessStartInfo pi = new ProcessStartInfo(ApplicationSettings.PuttyLocation, "-load " + sessionName);
            Process.Start(pi);
        }

        /// <summary>
        /// Über trägt eine Teamsession in das lokale Repository und startet diese
        /// </summary>
        /// <param name="sessionName">Die Session die gestartet werden soll</param>
        /// <param name="repository">Das genutzte Repository</param>
        /// <param name="callForm">Das Formular auf dem die Action Invoked werden soll</param>
        /// <param name="containerForm">Das Formular in dem das neue Fenster angedockt werden soll</param>
        /// <param name="dockPanel">Das für das Docking verantwortliche DockPanel</param>
        public static void StartTeamSession(string sessionName, ISessionRepository repository, Form callForm, Form containerForm, DockPanel dockPanel) {
            string from = Path.Combine(ApplicationSettings.RemoteRepositoryPath, sessionName);

            TransferSessionFromTeamFolder(sessionName, from, repository);
            StartPuttySession(sessionName, callForm, containerForm, dockPanel);
        }

        /// <summary>
        /// Überträgt eine Session aus dem Teamverzeichnis und fügt die Benutzerspezifischen Werte ein
        /// </summary>
        /// <param name="sessionName">Name der Session</param>
        /// <param name="from">Pfad zu der Session im Team Repository</param>
        /// <param name="repository">Das genutzte Repository</param>
        public static void TransferSessionFromTeamFolder(string sessionName, string from, ISessionRepository repository) {
            FolderSetup.SetupDirectory();

            if (!repository.CheckSessionExists(sessionName)) {
                repository.AddSession(from);
            }

            SetUserSpecificConfiguration(sessionName);
        }

        /// <summary>
        /// Fügt die Benutzerspezifischen Werte in die Session ein
        /// </summary>
        /// <param name="sessionName">Die Session in die die einstellungen eingetragen werden sollen</param>
        public static void SetUserSpecificConfiguration(string sessionName) {
            //TODO: Needs refactory
            if (!string.IsNullOrEmpty(ApplicationSettings.TeamUsername)) {
                string[] newSession = File.ReadAllLines(Path.Combine(ApplicationSettings.LocalRepositoryPath, sessionName));

                for (int i = 0; i < newSession.Length; i++) {
                    if (newSession[i].Equals("UserName=")) {
                        newSession[i] = "UserName=" + ApplicationSettings.TeamUsername;
                    }
                    if (newSession[i].Contains("ssh  @")) {
                        newSession[i] = newSession[i].Replace("=ssh  @", "=ssh " + ApplicationSettings.TeamUsername + "@");
                    }
                }

                File.WriteAllLines(Path.Combine(ApplicationSettings.LocalRepositoryPath, sessionName), newSession);
            }
        }

        /// <summary>
        /// Startet eine Session mit modifizierten Fore- & Backgroundfarben
        /// </summary>
        /// <param name="sessionName">Session die gestartetw erden soll</param>
        /// <param name="bgColor">Gewünschte Hintergrundfarbe</param>
        /// <param name="foreColor">Gewünschte Textfarbe</param>
        /// <param name="callForm">Das Formular auf dem die Action Invoked werden soll</param>
        /// <param name="containerForm">Das Formular in dem das neue Fenster angedockt werden soll</param>
        /// <param name="dockPanel">Das für das Docking verantwortliche DockPanel</param>
        /// <param name="isRemote">Gibt an, ob eine Teamsession gestartet werden soll</param>
        public static void StartSessionInColor(string sessionName, string bgColor, string foreColor, Form callForm, Form containerForm, DockPanel dockPanel, bool isRemote = false) {
            string sessionFile = Path.Combine(ApplicationSettings.LocalRepositoryPath, sessionName);

            try {
                if (File.Exists(sessionFile)) {
                    string[] backup = File.ReadAllLines(sessionFile);
                    SetCustomColors(bgColor, foreColor, sessionFile);

                    StartPuttySession(sessionName, callForm, containerForm, dockPanel);

                    System.Threading.Thread.Sleep(500);

                    File.WriteAllLines(sessionFile, backup);
                }
            } catch (Exception ex) {
                Program.LogWriter.Log("Could not start Colored Session: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Ändert die Text- und Hintergrundfarbe einer Session
        /// </summary>
        /// <param name="bgColor">Gewünschte Hintergrundfarbe</param>
        /// <param name="foreColor">Gewünschte Textfarbe</param>
        /// <param name="sessionFile">Die Session die geändert werden soll</param>
        public static void SetCustomColors(string bgColor, string foreColor, string sessionFile) {
            string[] source = File.ReadAllLines(sessionFile);

            for (int i = 0; i < source.Length; i++) {
                if (source[i].StartsWith("Colour2=")) {
                    source[i] = bgColor;
                }
                if (source[i].StartsWith("Colour0=")) {
                    source[i] = foreColor;
                }
            }

            File.WriteAllLines(sessionFile, source);
        }

        /// <summary>
        /// Startet alle Sessions eines ordners
        /// </summary>
        /// <param name="folder">Ordner-TreeNode</param>
        /// <param name="callForm">Das Formular auf dem die Action Invoked werden soll</param>
        /// <param name="containerForm">Das Formular in dem das neue Fenster angedockt werden soll</param>
        /// <param name="dockPanel">Das für das Docking verantwortliche DockPanel</param>
        public static void StartAllSessionsInFolder(TreeNode folder, Form callForm, Form containerForm, DockPanel dockPanel) {
            foreach (TreeNode session in folder.Nodes) {
                if (session.ImageIndex == (int)NodeType.ServerNode) {
                    StartPuttySession(session.Text, callForm, containerForm, dockPanel);
                }
            }
        }

        /// <summary>
        /// Prüft die ausgewählten Dateinamen aus verbotene Zeichen und fügt sie zu der Sessionliste hinzu
        /// </summary>
        /// <param name="open">Referenz auf den Dialog der zum Öffnen der Dateien genutzt wurde</param>
        /// <param name="result">Das DialogResult der ShowDialog()-Methode</param>
        /// <param name="repository">Das zu verwendete Repository</param>
        /// <param name="treeView">Die TreeView die die Änderungen übernehmen soll</param>
        public static void GetSelectedSessionsFromFolder(OpenFileDialog open, DialogResult result, ISessionRepository repository, TreeView treeView) {
            if (result != DialogResult.Cancel && result != DialogResult.Abort) {

                bool forbiddenNameFound = false;
                foreach (string session in open.FileNames) {

                    if (Path.GetFileName(session).Contains(" ")) {
                        forbiddenNameFound = true;
                    } else {
                        if (treeView.DoesNodeExist(Path.GetFileName(session))) {
                            MessageBox.Show(string.Format("The Session {0} is already in your Session list and won't be added again!", Path.GetFileName(session)), "Session already in the list", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        } else {
                            repository.AddSession(session);

                            AddSessionAsTreeNode(session, treeView);
                        }
                    }
                }
                if (forbiddenNameFound) {
                    MessageBox.Show("One or more Sessions could not be added because they have forbidden characters in their name. Note: Whitespace is a forbidden character too!", "Can't add session", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Fügt eine neue Session in die TreeView ein
        /// </summary>
        /// <param name="session">Kompletter Pfad der Session</param>
        /// <param name="treeView">Die TreeView die die Änderungen übernehmen soll</param>
        public static void AddSessionAsTreeNode(string session, TreeView treeView) {
            TreeNode newNode = CreateNewServerNode(Path.GetFileName(session));

            treeView.SelectedNode.Nodes.Add(newNode);
        }

        /// <summary>
        /// Startet FTP Verbindung über FileZilla - Falls ein RemoreCommand gesetzt ist wird ein Tunnel über eine Putty Session aufgebaut
        /// </summary>
        /// <param name="session">Session zu der die FTP Verbindung aufgebaut werden soll</param>
        /// <param name="callForm">Das Formular auf dem die Action Invoked werden soll</param>
        /// <param name="containerForm">Das Formular in dem das neue Fenster angedockt werden soll</param>
        /// <param name="dockPanel">Das für das Docking verantwortliche DockPanel</param>
        public static void StartSessionInFileZilla(string session, string userPass, Form callForm, Form containerForm, DockPanel dockPanel) {

            try {
                string[] sessionData = File.ReadAllLines(Path.Combine(ApplicationSettings.LocalRepositoryPath, session));

                string filezillaString = "";
                string puttyTunnel = "";

                string userNameLine = "";
                string ipLine = "";
                string remoteCommandLine = "";

                foreach (string line in sessionData) {
                    if (line.StartsWith("UserName=")) { userNameLine = line; }
                    if (line.StartsWith("HostName=")) { ipLine = line; }
                    if (line.StartsWith("RemoteCommand=")) { remoteCommandLine = line; }
                }

                if (remoteCommandLine.Length > "RemoteCommand=".Length && remoteCommandLine.Contains("ssh")) {
                    string userName = "";
                    string serverName = "";
                    string serverPort = "";

                    //Es muss eine Getunnelte Verbindung aufgebaut werden
                    try {
                        userName = ExtractUserNameFromRemoteCommand(remoteCommandLine);
                        serverName = ExtractServerNameFromRemoteCommand(remoteCommandLine);
                    } catch (Exception ex) {
                        Program.LogWriter.Log("Could not extract username and server from RemoteCommand-Entry!");
                        serverName = GetValueFromInput("Could not extract the server ip, please edit it manually:", "Enter Server Name", remoteCommandLine);
                        userName = GetValueFromInput("Please Enter your username:", "Username", remoteCommandLine);
                    }
                    serverPort = "22";

                    string rndPort = new Random().Next(1025, 65000).ToString();

                    puttyTunnel = string.Format("PortForwardings=L{0}={1}:{2},", rndPort, serverName, serverPort);

                    sessionData = SetPortForwardingsString(sessionData, puttyTunnel);

                    SaveAndStartTempSession(session, sessionData, callForm, containerForm, dockPanel);

                    filezillaString = string.Format("sftp://{0}:{1}@localhost:{2}", userName, userPass, rndPort);
                    Program.LogWriter.Log("sftp://{0}:{1}@localhost:{2}", userName, "******", rndPort);

                    infWait waiter = new infWait(ApplicationSettings.PathToFileZilla, filezillaString);
                    waiter.Show();

                } else {
                    //kein tunnel nötig
                    string userName = "";
                    string serverName = "";
                    string serverPort = "22";

                    if (userNameLine.Length <= "UserName=".Length) {
                        userName = GetValueFromInput("Please Enter your username:", "Username", remoteCommandLine);

                        userNameLine = "UserName=" + userName;
                    }
                    userName = GetValueFromSetting(userNameLine);
                    serverName = GetValueFromSetting(ipLine);

                    filezillaString = string.Format("sftp://{0}:{1}@{2}:{3}", userName, userPass, serverName, serverPort);

                    Program.LogWriter.Log("sftp://{0}:{1}@{2}:{3}", userName, "******", serverName, serverPort);

                    ProcessStartInfo pi = new ProcessStartInfo(ApplicationSettings.PathToFileZilla, filezillaString);
                    pi.WorkingDirectory = ApplicationSettings.PathToFileZilla.Substring(0, ApplicationSettings.PathToFileZilla.LastIndexOf(Path.DirectorySeparatorChar));
                    Process.Start(pi);
                }
            } catch (Exception ex) {
                Program.LogWriter.Log("Could not start FileZilla: {0}", ex.Message);
            }

        }

        /// <summary>
        /// Startet FTP Verbindung über WinSCP - Falls ein RemoreCommand gesetzt ist wird ein Tunnel über eine Putty Session aufgebaut
        /// </summary>
        /// <param name="session">Session zu der die FTP Verbindung aufgebaut werden soll</param>
        /// <param name="callForm">Das Formular auf dem die Action Invoked werden soll</param>
        /// <param name="containerForm">Das Formular in dem das neue Fenster angedockt werden soll</param>
        /// <param name="dockPanel">Das für das Docking verantwortliche DockPanel</param>
        public static void StartSessionInWinSCP(string session, Form callForm, Form containerForm, DockPanel dockPanel) {

            try {
                string[] sessionData = File.ReadAllLines(Path.Combine(ApplicationSettings.LocalRepositoryPath, session));

                string winSCPString = "";
                string puttyTunnel = "";

                string userNameLine = "";
                string ipLine = "";
                string remoteCommandLine = "";

                foreach (string line in sessionData) {
                    if (line.StartsWith("UserName=")) { userNameLine = line; }
                    if (line.StartsWith("HostName=")) { ipLine = line; }
                    if (line.StartsWith("RemoteCommand=")) { remoteCommandLine = line; }
                }

                if (remoteCommandLine.Length > "RemoteCommand=".Length && remoteCommandLine.Contains("ssh")) {
                    string userName = "";
                    string serverName = "";
                    string serverPort = "";

                    //Es muss eine Getunnelte Verbindung aufgebaut werden
                    try {
                        userName = ExtractUserNameFromRemoteCommand(remoteCommandLine);
                        serverName = ExtractServerNameFromRemoteCommand(remoteCommandLine);
                    } catch (Exception ex) {
                        Program.LogWriter.Log("Could not extract username and server from RemoteCommand-Entry!");
                        serverName = GetValueFromInput("Could not extract the server ip, please edit it manually:", "Enter Server Name", remoteCommandLine);
                        userName = GetValueFromInput("Please Enter your username:", "Username", remoteCommandLine);
                    }

                    serverPort = "22";

                    string rndPort = new Random().Next(1025, 65000).ToString();

                    puttyTunnel = string.Format("PortForwardings=L{0}={1}:{2},", rndPort, serverName, serverPort);

                    sessionData = SetPortForwardingsString(sessionData, puttyTunnel);

                    SaveAndStartTempSession(session, sessionData, callForm, containerForm, dockPanel);

                    winSCPString = string.Format("sftp://{0}@localhost:{1}", userName, rndPort);
                    Program.LogWriter.Log("sftp://{0}@localhost:{1}", userName, rndPort);

                    infWait waiter = new infWait(ApplicationSettings.PathToWinSCP, winSCPString);
                    waiter.Show();

                } else {
                    //kein tunnel nötig
                    string userName = "";
                    string serverName = "";
                    string serverPort = "22";
                    if (userNameLine.Length <= "UserName=".Length) {
                        userName = GetValueFromInput("Please Enter your username:", "Username", remoteCommandLine);

                        userNameLine = "UserName=" + userName;
                    }
                    userName = GetValueFromSetting(userNameLine);
                    serverName = GetValueFromSetting(ipLine);

                    winSCPString = string.Format("sftp://{0}@{1}:{2}", userName, serverName, serverPort);

                    Program.LogWriter.Log("sftp://{0}@{1}:{2}", userName, serverName, serverPort);

                    ProcessStartInfo pi = new ProcessStartInfo(ApplicationSettings.PathToWinSCP, winSCPString);
                    pi.WorkingDirectory = ApplicationSettings.PathToWinSCP.Substring(0, ApplicationSettings.PathToWinSCP.LastIndexOf(Path.DirectorySeparatorChar));
                    Process.Start(pi);
                }
            } catch (Exception ex) {
                Program.LogWriter.Log("Could not start WinSCP: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Liest den Wert eines Settings aus das im Format Setting=Value vorliegt
        /// </summary>
        /// <param name="setting">Die komplette setting-Zeile</param>
        /// <returns>Der ausgelesene Wert</returns>
        public static string GetValueFromSetting(string setting) {
            string value = "";
            value = setting.Substring(setting.IndexOf("=") + 1, setting.Length - 1 - setting.IndexOf("="));
            return value;
        }

        /// <summary>
        /// Speichert die angegebenen Session-Informationen in einer neuen Datei mit "_tunnel" Ahang und führt diese aus
        /// </summary>
        /// <param name="session">Name der Sessions</param>
        /// <param name="sessionData">Inhalt der Session</param>
        /// <param name="callForm">Das Formular auf dem die Action Invoked werden soll</param>
        /// <param name="containerForm">Das Formular in dem das neue Fenster angedockt werden soll</param>
        /// <param name="dockPanel">Das für das Docking verantwortliche DockPanel</param>
        public static void SaveAndStartTempSession(string session, string[] sessionData, Form callForm, Form containerForm, DockPanel dockPanel) {
            //save temp session
            File.WriteAllLines(Path.Combine(ApplicationSettings.LocalRepositoryPath, session + "_tunnel"), sessionData);

            //start putty session
            StartPuttySession(session + "_tunnel", callForm, containerForm, dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Float);
        }

        /// <summary>
        /// Findet die passende Stelle in den Sessionsettings und setzt den neuen Wert für PortForwardings
        /// </summary>
        /// <param name="sessionData">Inhalt der Session als Array</param>
        /// <param name="puttyTunnel">Der neue Wert für die PortForwardings</param>
        /// <returns>Inhalt des Arrays mit den neuen Einstellungen</returns>
        public static string[] SetPortForwardingsString(string[] sessionData, string puttyTunnel) {
            for (int i = 0; i < sessionData.Length; i++) {
                if (sessionData[i].StartsWith("PortForwardings=")) { sessionData[i] = puttyTunnel; }
            }
            return sessionData;
        }

        /// <summary>
        /// Fordert den Benutzer mit einem Dialog dazu auf einen Benutzernamen einzugeben
        /// </summary>
        /// <param name="description">Beschreibung zur Anzeige im Dialog</param>
        /// <param name="title">Titel des Dialogs</param>
        /// <param name="defaultText">Text der dem benutzer als Eingabe vorgeschlagen wird</param>
        /// <returns>Den vom Benutzer eingegebenen String</returns>
        public static string GetValueFromInput(string description, string title, string defaultText) {
            string userName = "";
            userName = Microsoft.VisualBasic.Interaction.InputBox("Please Enter your username:", "Username", defaultText);
            if (string.IsNullOrEmpty(userName)) {
                return null;
            }
            return userName;
        }

        /// <summary>
        /// Liest den Servernamen aus dem RemoteCommand String heraus, der den Benutzernamen und den Server mit einem @ getrennt angibt
        /// </summary>
        /// <param name="remoteCommandLine">Die RemoteCommand Zeile</param>
        /// <returns>Der ausgelesene Wert</returns>
        public static string ExtractServerNameFromRemoteCommand(string remoteCommandLine) {
            string serverName = "";
            serverName = remoteCommandLine.Substring(remoteCommandLine.IndexOf("@") + 1, remoteCommandLine.Length - 1 - remoteCommandLine.IndexOf("@"));
            return serverName;
        }

        /// <summary>
        /// Liest den Benutzernamen aus dem RemoteCommand String heraus, der den Benutzernamen und den Server mit einem @ getrennt angibt
        /// </summary></summary>
        /// <param name="remoteCommandLine">Die RemoteCommand Zeile</param>
        /// <returns>Der ausgelesene Wert</returns>
        public static string ExtractUserNameFromRemoteCommand(string remoteCommandLine) {
            string userName = "";
            userName = remoteCommandLine.Substring(remoteCommandLine.IndexOf("ssh ") + 4, remoteCommandLine.IndexOf("@") - remoteCommandLine.IndexOf("ssh ") - 4);
            return userName;
        }

        /// <summary>
        /// Erstellt eine neue TreeNode vom Typ Server (entsprechendes Icon)
        /// </summary>
        /// <param name="sessionName">Name der Session - wird auch name des Node werden</param>
        /// <returns>Neuer TreeNode</returns>
        public static TreeNode CreateNewServerNode(string sessionName) {
            TreeNode newNode = new TreeNode(sessionName);
            newNode.ImageIndex = (int)NodeType.ServerNode;
            newNode.SelectedImageIndex = (int)NodeType.ServerNode;
            return newNode;
        }

        /// <summary>
        /// Erstellt eine neue TreeNode vom Typ Ordner (entsprechendes Icon)
        /// </summary>
        /// <param name="folderName">Name des Ordners</param>
        /// <returns>Neuer TreeNode</returns>
        public static TreeNode CreateNewFolderNode(string folderName) {
            TreeNode newNode = new TreeNode(folderName);
            newNode.ImageIndex = (int)NodeType.FolderNode;
            newNode.SelectedImageIndex = (int)NodeType.FolderNode;
            return newNode;
        }
    }
}
