using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PuttyServerManager.Tools.Extensions;
using PuttyServerManager.Persistence.Repository;
using PuttyServerManager.Config;
using System.IO;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using WindowTool;
using PuttyServerManager.WindowTools;
using PuttyServerManager.Tools;

namespace PuttyServerManager.ToolWindows {
    public partial class twiSessions : ToolWindow {

        private ISessionRepository localRepository;
        private ISessionRepository recentRepository;

        private DockPanel dockPanel;
        private Form containerForm;

        public TreeView TrvSessions {
            get {
                return trvSessions;
            }
        }

        public twiSessions(DockPanel dockPanel, Form container) {
            InitializeComponent();

            containerForm = container;

            this.dockPanel = dockPanel;
            localRepository = new LocalSessionRepository();
            recentRepository = new RecentSessionRepository();
        }

        /// <summary>
        /// Speichert alle Änderungen an der Session TreeView
        /// </summary>
        public void SaveChanges() {
            try {
                trvSessions.SerializeNode(trvSessions.Nodes[0], ApplicationSettings.LocalSessionListPath);
                trvRecentSessions.SerializeNode(trvRecentSessions.Nodes[0], ApplicationSettings.RecentSessionListPath);
            } catch (Exception ex) {
                //Fehler Kann auftreten beim laden des Formulars wenn die
                //recentSessions noch nicht geladen sind aber das ListAfterExpanded Event
                //schon aufgerufen wird -> kann ingnoriert werden
            }
        }

        /// <summary>
        /// Lädt alle nötigen Einstellungen
        /// </summary>
        private void LoadConfiguration() {

            LoadLocalSessionsList();
            trvSessions.Nodes[0].ImageIndex = (int)NodeType.RootNode;
            trvSessions.Nodes[0].SelectedImageIndex = (int)NodeType.RootNode;

            LoadRecentSessionsList();

            if (ApplicationSettings.RemoteSessionIsConfigured) {
                LoadTeamSessionsList();
                trvTeam.Sort();
                trvTeam.Nodes[0].Expand();
            }

            LoarRegestrySessionsList();

            trvSessions.Sort();
            trvRecentSessions.Sort();

            trvSessions.LabelEdit = localRepository.UserCanEditList();
            trvRecentSessions.LabelEdit = recentRepository.UserCanEditList();

        }

        private void LoadRecentSessionsList() {
            try {
                TreeNode node = trvRecentSessions.DeserializeNode(ApplicationSettings.RecentSessionListPath);
                trvRecentSessions.Nodes.Add(node);
            } catch (Exception ex) {
                trvRecentSessions.Nodes.Add("Recent Sessions");
            }
        }

        private void LoadLocalSessionsList() {
            try {
                TreeNode node = trvSessions.DeserializeNode(ApplicationSettings.LocalSessionListPath);
                trvSessions.Nodes.Add(node);
            } catch (Exception ex) {
                trvSessions.Nodes.Add("Saved Sessions");
            }
        }

        private void LoadTeamSessionsList() {
            try {
                TreeNode node = trvTeam.DeserializeTeamNode(ApplicationSettings.RemoteSessionListPath);
                trvTeam.Nodes.Add(node);
            } catch (Exception ex) {
                Program.LogWriter.Log("Could not load Team Sessions - {0}", ex.Message);
            }
        }

        private void LoarRegestrySessionsList() {
            try {
                TreeNode node = RegistryTools.ImportSessionsFromRegistry();
                trvRegistrySessions.Nodes.Add(node);
                node.Expand();
            } catch (Exception ex) {
                Program.LogWriter.Log("Could not load Team Sessions - {0}", ex.Message);
            }
        }

        /// <summary>
        /// Fügt der RecentSessionliste ein neues Element hinzu
        /// </summary>
        /// <param name="nodeName">Name des neuen Elements</param>
        public void AddSessionToRecentSessionList(string nodeName) {
            TreeNode newNode = CreateNewServerNode(nodeName);
            trvRecentSessions.Nodes[0].Nodes.Add(newNode);

            SaveChanges();
        }
        

        private void twiSessions_Load(object sender, EventArgs e) {

            LoadConfiguration();
        }

        private void trvSessions_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                //Auch bei einem Rechtsklick das gewählte Element markieren
                TreeNode clickedNode = trvSessions.GetNodeAt(e.Location);
                trvSessions.SelectedNode = clickedNode;

                if (clickedNode.SelectedImageIndex == (int)NodeType.ServerError) {
                    conMenuSessionMissing.Show(MousePosition);
                    return;
                }

                if (clickedNode.SelectedImageIndex == (int)NodeType.ServerNode) {
                    if (string.IsNullOrEmpty(ApplicationSettings.PathToFileZilla)) {
                        connectWithFileZillaToolStripMenuItem.Enabled = false;
                    } else { connectWithFileZillaToolStripMenuItem.Enabled = true; }

                    if (string.IsNullOrEmpty(ApplicationSettings.PathToWinSCP)) {
                        connectWithWinSCPToolStripMenuItem.Enabled = false;
                    } else { connectWithWinSCPToolStripMenuItem.Enabled = true; }

                    conMenuSession.Show(MousePosition);
                    return;
                }

                if (clickedNode.SelectedImageIndex == (int)NodeType.FolderNode) {
                    if (clickedNode.Nodes.Count == 0) {
                        startAllSessionsInFolderToolStripMenuItem.Enabled = false;
                    } else {
                        startAllSessionsInFolderToolStripMenuItem.Enabled = true;
                    }
                    conMenuFolder.Show(MousePosition);
                    return;
                }

                conMenuTreeView.Show(MousePosition);
            }
        }

        private void renameToolStripMenuItem1_Click(object sender, EventArgs e) {
            renameToolStripMenuItem_Click(sender, e);
        }

        private void addSubfolderToolStripMenuItem_Click(object sender, EventArgs e) {
            TreeNode newNode = CreateNewFolderNode("New Folder");

            trvSessions.SelectedNode.Nodes.Add(newNode);
            trvSessions.SelectedNode.Expand();
            newNode.BeginEdit();
        }

        private void addSubfolderToolStripMenuItem1_Click(object sender, EventArgs e) {
            addSubfolderToolStripMenuItem_Click(sender, e);
        }

        private void addSessionToolStripMenuItem1_Click(object sender, EventArgs e) {
            addSessionToolStripMenuItem_Click(sender, e);
        }

        private void sortOverviewToolStripMenuItem_Click(object sender, EventArgs e) {
            trvSessions.Sort();
        }

        private void expandAllFoldersToolStripMenuItem_Click(object sender, EventArgs e) {
            trvSessions.ExpandAll();
        }

        private void collapseAllFoldersToolStripMenuItem_Click(object sender, EventArgs e) {
            trvSessions.CollapseAll();
        }

        private void addSessionToolStripMenuItem_Click(object sender, EventArgs e) {

            if (FolderSetup.SetupDirectory()) {
                OpenFileDialog open = new OpenFileDialog();
                open.Title = "Select Session File(s)";
                open.Multiselect = true;

                DialogResult result = open.ShowDialog();
                GetSelectedSessionsFromFolder(open, result);
            }

            SaveChanges();
        }

        private void GetSelectedSessionsFromFolder(OpenFileDialog open, DialogResult result) {
            if (result != DialogResult.Cancel && result != DialogResult.Abort) {

                bool forbiddenNameFound = false;
                foreach (string session in open.FileNames) {

                    if (Path.GetFileName(session).Contains(" ")) {
                        forbiddenNameFound = true;
                    } else {
                        if (trvSessions.DoesNodeExist(Path.GetFileName(session))) {
                            MessageBox.Show(string.Format("The Session {0} is already in your Session list and won't be added again!", Path.GetFileName(session)), "Session already in the list", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        } else {
                            localRepository.AddSession(session);

                            AddSessionAsTreeNode(session);
                        }
                    }
                }
                if(forbiddenNameFound){
                    MessageBox.Show("One or more Sessions could not be added because they have forbidden characters in their name. Note: Whitespace is a forbidden character too!", "Can't add session", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Fügt eine neue Session in die TreeView ein
        /// </summary>
        /// <param name="session">Kompletter Pfad der Session</param>
        private void AddSessionAsTreeNode(string session) {
            TreeNode newNode = CreateNewServerNode(Path.GetFileName(session));

            trvSessions.SelectedNode.Nodes.Add(newNode);
        }

        private void trvSessions_AfterLabelEdit(object sender, NodeLabelEditEventArgs e) {
            if (e.Label == null || e.Label == "") {
                e.CancelEdit = true;
                return;
            }

            if (e.Label.IndexOfAny(Path.GetInvalidFileNameChars()) != -1 || e.Label.Contains(" ")) {
                MessageBox.Show("Your Filename contains forbidden characters!", "Forbidden Characters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.CancelEdit = true;
                return;
            }

            if (e.Node.ImageIndex == (int)NodeType.ServerNode && e.Label != null) {
                if (!localRepository.RenameSession(e.Node.Text, e.Label)) {
                    e.CancelEdit = true;
                    return;
                }
            }

            SaveChanges();
        }



        private void trvSessions_BeforeCheck(object sender, TreeViewCancelEventArgs e) {
            
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e) {
            if (localRepository.UserCanEditList()) {
                trvSessions.SelectedNode.BeginEdit();
            } else {
                MessageBox.Show("You are not allowed to change the Structure of a remote Repository!", "Not able to edit!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                renameToolStripMenuItem.Enabled = false;
                renameToolStripMenuItem1.Enabled = false;
                renameToolStripMenuItem2.Enabled = false;
            }
            SaveChanges();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            trvSessions.SelectedNode.Remove();
            SaveChanges();
        }

        private void editSessionToolStripMenuItem_Click(object sender, EventArgs e) {
            dlgSessionEdit edit = new dlgSessionEdit(trvSessions.SelectedNode.Text, localRepository);
            edit.ShowDialog();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e) {
            if (trvSessions.SelectedNode.Nodes.Count > 0) {
                DialogResult res = MessageBox.Show("This Folder contains further Data which will be deleted too if you proceed. Delete anyway?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == System.Windows.Forms.DialogResult.Yes) {
                    trvSessions.SelectedNode.Remove();
                }
            } else {
                trvSessions.SelectedNode.Remove();
            }
            SaveChanges();
        }

        private void renameToolStripMenuItem2_Click(object sender, EventArgs e) {
            if (localRepository.UserCanEditList()) {
                trvSessions.SelectedNode.BeginEdit();
            } else {
                MessageBox.Show("You are not allowed to change the Structure of a remote Repository!", "Not able to edit!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                renameToolStripMenuItem.Enabled = false;
                renameToolStripMenuItem1.Enabled = false;
                renameToolStripMenuItem2.Enabled = false;
            }
            SaveChanges();
        }

        private void trvSessions_DoubleClick(object sender, EventArgs e) {
            if (trvSessions.SelectedNode.ImageIndex == (int)NodeType.ServerNode) {         //Normale Session
                StartPuttySession(trvSessions.SelectedNode.Text);
            }
            if (trvSessions.SelectedNode.ImageIndex == (int)NodeType.ServerError) {   //Nicht gefundene Session
                RemoveMissingNode(trvSessions.SelectedNode);
            }
        }

        private void trvRecentSessions_DoubleClick(object sender, EventArgs e) {
            if (trvRecentSessions.SelectedNode.ImageIndex == (int)NodeType.ServerNode) {   //Normale Session
                StartPuttySession(trvRecentSessions.SelectedNode.Text);
            }
            if (trvRecentSessions.SelectedNode.ImageIndex == (int)NodeType.ServerError) {   //Nicht gefundene Session
                RemoveMissingNode(trvRecentSessions.SelectedNode);
            }
        }

        /// <summary>
        /// Startet eine Putty Session in einem gedockten Fenster
        /// </summary>
        /// <param name="sessionName">Name der Session die gestartet werden soll</param>
        /// <param name="dockstate">Angabe wie das neue Fenster angedockt werden soll</param>
        public void StartPuttySession(string sessionName, DockState dockstate = DockState.Document) {

            StartPuttyAgentIfNeeded();
            
            twiPutty puttyWindow = null;

            ApplicationClosedCallback callback = delegate(bool closed) {
                if (puttyWindow != null) {

                    if (puttyWindow.InvokeRequired) {
                        this.BeginInvoke((MethodInvoker)delegate() {
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
        /// Startet FTP Verbindung über FileZilla - Falls ein RemoreCommand gesetzt ist wird ein Tunnel über eine Putty Session aufgebaut
        /// </summary>
        /// <param name="session">Session zu der die FTP Verbindung aufgebaut werden soll</param>
        private void StartSessionInFileZilla(string session, string userPass) {

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
                    }catch (Exception ex) {
                        Program.LogWriter.Log("Could not extract username and server from RemoteCommand-Entry!");
                        serverName = GetValueFromInput("Could not extract the server ip, please edit it manually:", "Enter Server Name", remoteCommandLine);
                        userName = GetValueFromInput("Please Enter your username:", "Username", remoteCommandLine);
                    }
                    serverPort = "22";

                    string rndPort = new Random().Next(1025, 65000).ToString();

                    puttyTunnel = string.Format("PortForwardings=L{0}={1}:{2},", rndPort, serverName, serverPort);

                    sessionData = SetPortForwardingsString(sessionData, puttyTunnel);

                    SaveAndStartTempSession(session, sessionData);

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
        private void StartSessionInWinSCP(string session) {

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

                    SaveAndStartTempSession(session, sessionData);

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
        private static string GetValueFromSetting(string setting) {
            string value = "";
            value = setting.Substring(setting.IndexOf("=") + 1, setting.Length - 1 - setting.IndexOf("="));
            return value;
        }

        /// <summary>
        /// Speichert die angegebenen Session-Informationen in einer neuen Datei mit "_tunnel" Ahang und führt diese aus
        /// </summary>
        /// <param name="session">Name der Sessions</param>
        /// <param name="sessionData">Inhalt der Session</param>
        private void SaveAndStartTempSession(string session, string[] sessionData) {
            //save temp session
            File.WriteAllLines(Path.Combine(ApplicationSettings.LocalRepositoryPath, session + "_tunnel"), sessionData);

            //start putty session
            StartPuttySession(session + "_tunnel", WeifenLuo.WinFormsUI.Docking.DockState.DockBottom);
        }

        /// <summary>
        /// Findet die passende Stelle in den Sessionsettings und setzt den neuen Wert für PortForwardings
        /// </summary>
        /// <param name="sessionData">Inhalt der Session als Array</param>
        /// <param name="puttyTunnel">Der neue Wert für die PortForwardings</param>
        /// <returns>Inhalt des Arrays mit den neuen Einstellungen</returns>
        private static string[] SetPortForwardingsString(string[] sessionData, string puttyTunnel) {
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
        private static string GetValueFromInput(string description, string title, string defaultText) {
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
        private static string ExtractServerNameFromRemoteCommand(string remoteCommandLine) {
            string serverName = "";
            serverName = remoteCommandLine.Substring(remoteCommandLine.IndexOf("@") + 1, remoteCommandLine.Length - 1 - remoteCommandLine.IndexOf("@"));
            return serverName;
        }

        /// <summary>
        /// Liest den Benutzernamen aus dem RemoteCommand String heraus, der den Benutzernamen und den Server mit einem @ getrennt angibt
        /// </summary></summary>
        /// <param name="remoteCommandLine">Die RemoteCommand Zeile</param>
        /// <returns>Der ausgelesene Wert</returns>
        private static string ExtractUserNameFromRemoteCommand(string remoteCommandLine) {
            string userName = "";
            userName = remoteCommandLine.Substring(remoteCommandLine.IndexOf("ssh ") + 4, remoteCommandLine.IndexOf("@") - remoteCommandLine.IndexOf("ssh ") - 4);
            return userName;
        }

        /// <summary>
        /// Startet die angegebene Putty Session ein einem eigenen PuTTY Fenster. 
        /// Dieses Fenster kann nicht im MSM angedockt werden und bleibt selbstständig.
        /// </summary>
        /// <param name="sessionName">Name der Session die gestartet werden soll</param>
        private void StartNativePuttySession(string sessionName) {
            //On demand: start putty agent
            StartPuttyAgentIfNeeded();

            ProcessStartInfo pi = new ProcessStartInfo(ApplicationSettings.PuttyLocation, "-load " + sessionName);
            Process.Start(pi);
        }

        /// <summary>
        /// Startet den PuttyAgent falls der Benutzer dies in den Einstellungen konfiguriert hat
        /// </summary>
        private static void StartPuttyAgentIfNeeded() {
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
        /// Entfern die angegebene Node aus der TreeView und speichert die Übersicht
        /// </summary>
        /// <param name="node">Die Node die Entfernt werden soll</param>
        private void RemoveMissingNode(TreeNode node) {
            DialogResult res = MessageBox.Show("This session seems to be missing in your session folder. Do you want to remove this session from the list?", "Remove missing session?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == System.Windows.Forms.DialogResult.Yes) {
                node.Remove();
                SaveChanges();
            }
        }

        private void reloadOverviewToolStripMenuItem_Click(object sender, EventArgs e) {
            trvSessions.Nodes.Remove(trvSessions.Nodes[0]);
            LoadLocalSessionsList();

        }

        private void removeMissingSessionToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void removeMissingSessionFromListToolStripMenuItem_Click(object sender, EventArgs e) {
            trvSessions.SelectedNode.Remove();
            SaveChanges();
        }

        private void trvRecentSessions_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                //Auch bei einem Rechtsklick das gewählte Element markieren
                TreeNode clickedNode = trvRecentSessions.GetNodeAt(e.Location);
                trvRecentSessions.SelectedNode = clickedNode;

                if (clickedNode.SelectedImageIndex == (int)NodeType.ServerError) {
                    conMenuRemoveMissingRecent.Show(MousePosition);
                    return;
                }

                if (clickedNode.SelectedImageIndex == (int)NodeType.ServerNode) {
                    conMenuRecent.Show(MousePosition);
                    return;
                }
            }
        }

        private void transferSessionToPersonalSessionsToolStripMenuItem_Click(object sender, EventArgs e) {
            TreeNode node = CreateNewServerNode(trvRecentSessions.SelectedNode.Text);

            trvSessions.Nodes[0].Nodes.Add(node);
            trvRecentSessions.SelectedNode.Remove();

            SaveChanges();
        }

        private void removeSessionFromListToolStripMenuItem_Click(object sender, EventArgs e) {
            recentRepository.DeleteSession(trvRecentSessions.SelectedNode.Text);
            trvRecentSessions.SelectedNode.Remove();
            SaveChanges();
        }

        private void reloadSessionLisToolStripMenuItem_Click(object sender, EventArgs e) {
            trvRecentSessions.Nodes.Remove(trvRecentSessions.Nodes[0]);
            LoadRecentSessionsList();
        }

        private void reloadSessionListToolStripMenuItem_Click(object sender, EventArgs e) {
            reloadSessionLisToolStripMenuItem_Click(sender, e);
        }

        private void removeMissingSessionToolStripMenuItem1_Click(object sender, EventArgs e) {
            trvRecentSessions.SelectedNode.Remove();
            SaveChanges();
        }

        private void startSessionToolStripMenuItem1_Click(object sender, EventArgs e) {
            StartPuttySession(trvRecentSessions.SelectedNode.Text);
        }

        private void removeAllMissingSessionsToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (TreeNode n in trvRecentSessions.Nodes[0].Nodes) {
                if (n.SelectedImageIndex == (int)NodeType.ServerError) {
                    n.Remove();
                }
            }
            SaveChanges();
        }

        private void startSessionToolStripMenuItem_Click(object sender, EventArgs e) {
            StartPuttySession(trvSessions.SelectedNode.Text);
        }

        private void twiSessions_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
        }

        private void startSessionToolStripMenuItem2_Click(object sender, EventArgs e) {
            StartTeamSession(trvTeam.SelectedNode.Text);
        }

        /// <summary>
        /// Über trägt eine Teamsession in das lokale Repository und startet diese
        /// </summary>
        /// <param name="sessionName">Die Session die gestartet werden soll</param>
        private void StartTeamSession(string sessionName) {
            string from = Path.Combine(ApplicationSettings.RemoteRepositoryPath, sessionName);

            TransferSessionFromTeamFolder(sessionName, from);
            StartPuttySession(sessionName);
        }

        /// <summary>
        /// Überträgt eine Session aus dem Teamverzeichnis und fügt die Benutzerspezifischen Werte ein
        /// </summary>
        /// <param name="sessionName">Name der Session</param>
        /// <param name="from">Pfad zu der Session im Team Repository</param>
        private void TransferSessionFromTeamFolder(string sessionName, string from) {
            FolderSetup.SetupDirectory();

            if (!localRepository.CheckSessionExists(sessionName)) {
                localRepository.AddSession(from);
            }

            SetUserSpecificConfiguration(sessionName);
        }

        /// <summary>
        /// Fügt die Benutzerspezifischen Werte in die Session ein
        /// </summary>
        /// <param name="sessionName">Die Session in die die einstellungen eingetragen werden sollen</param>
        private static void SetUserSpecificConfiguration(string sessionName) {
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

        private void transferSessionToPersonalListToolStripMenuItem_Click(object sender, EventArgs e) {
            string from = Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text);

            if (trvSessions.DoesNodeExist(Path.GetFileName(from))) {
                MessageBox.Show(string.Format("The Session {0} is already in your Session list and won't be added again!", Path.GetFileName(from)), "Session already in the list", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else {

                TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, from);

                TreeNode newNode = CreateNewServerNode(Path.GetFileName(from));

                trvSessions.Nodes[0].Nodes.Add(newNode);
            }
            SaveChanges();
        }

        /// <summary>
        /// Erstellt eine neue TreeNode vom Typ Server (entsprechendes Icon)
        /// </summary>
        /// <param name="sessionName">Name der Session - wird auch name des Node werden</param>
        /// <returns>Neuer TreeNode</returns>
        private static TreeNode CreateNewServerNode(string sessionName) {
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
        private static TreeNode CreateNewFolderNode(string folderName) {
            TreeNode newNode = new TreeNode(folderName);
            newNode.ImageIndex = (int)NodeType.FolderNode;
            newNode.SelectedImageIndex = (int)NodeType.FolderNode;
            return newNode;
        }

        private void trvTeam_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                //Auch bei einem Rechtsklick das gewählte Element markieren
                TreeNode clickedNode = trvTeam.GetNodeAt(e.Location);
                trvTeam.SelectedNode = clickedNode;

                if (clickedNode.SelectedImageIndex == (int)NodeType.ServerError) {
                    conMenuSessionMissing.Show(MousePosition);
                    return;
                }

                if (clickedNode.SelectedImageIndex == (int)NodeType.ServerNode) {
                    conMenuTeamSession.Show(MousePosition);
                    return;
                }

                if (clickedNode.SelectedImageIndex == (int)NodeType.FolderNode) {
                    //conMenuFolder.Show(MousePosition);
                    return;
                }

            }
        }

        private void trvTeam_DoubleClick(object sender, EventArgs e) {
            if (trvTeam.SelectedNode.ImageIndex == (int)NodeType.ServerNode) {          //Normale Session
                StartTeamSession(trvTeam.SelectedNode.Text);
            }
            if (trvTeam.SelectedNode.ImageIndex == (int)NodeType.ServerError) {         //Nicht gefundene Session
                MessageBox.Show("This session seems to be missing in the Team folder. Please contact your Session Folder administrator!", "Missing session!", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
        }

        private void trvTeam_AfterSelect(object sender, TreeViewEventArgs e) {

        }

        private void trvSessions_DragDrop(object sender, DragEventArgs e) {
            TreeNode draggedNode;
            TreeNode destenationNode;

            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false)) {
                Point destenationPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));

                destenationNode = ((TreeView)sender).GetNodeAt(destenationPoint);
                draggedNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

                if (!destenationNode.FullPath.Contains(draggedNode.Text)) {
                    if (destenationNode.ImageIndex > (int)NodeType.FolderNode) {
                        destenationNode = destenationNode.Parent;
                    }

                    destenationNode.Nodes.Add(((TreeNode)draggedNode.Clone()));
                    destenationNode.Expand();

                    draggedNode.Remove();

                    SaveChanges();
                }
            }
        }

        private void trvSessions_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Move;
        }

        private void trvSessions_ItemDrag(object sender, ItemDragEventArgs e) {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void startInNativePuTTYWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            StartNativePuttySession(trvRecentSessions.SelectedNode.Text);
        }

        private void startSessionInNativePuTTYWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            StartNativePuttySession(trvSessions.SelectedNode.Text);
        }

        private void startInNativePuTTYWindowToolStripMenuItem1_Click(object sender, EventArgs e) {
            StartNativePuttySession(trvTeam.SelectedNode.Text);
        }

        private void trvRegistrySessions_AfterSelect(object sender, TreeViewEventArgs e) {
            
        }

        private void trvRegistrySessions_DoubleClick(object sender, EventArgs e) {
            StartPuttySession(trvRegistrySessions.SelectedNode.Text);
        }

        private void startSessionToolStripMenuItem3_Click(object sender, EventArgs e) {
            trvRegistrySessions_DoubleClick(sender, e);
        }

        private void startInNativeWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            StartNativePuttySession(trvRegistrySessions.SelectedNode.Text);
        }

        private void transferToPersonalListToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                FolderSetup.SetupDirectory();

                if (!trvSessions.DoesNodeExist(trvRegistrySessions.SelectedNode.Text)) {
                    TreeNode node = CreateNewServerNode(trvRegistrySessions.SelectedNode.Text);

                    trvSessions.Nodes[0].Nodes.Add(node);

                    SaveChanges();
                } else {
                    Program.LogWriter.Log("The Session you try to add is already in the list and won't be added again!");
                }
            } catch (Exception ex) {
                Program.LogWriter.Log("Could not transfer Session to personal List because {0}", ex.Message);
            }
        }

        private void trvRegistrySessions_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                //Auch bei einem Rechtsklick das gewählte Element markieren
                TreeNode clickedNode = SelectNodeAfterRightClick(trvRegistrySessions, e);

                if (clickedNode.SelectedImageIndex == (int)NodeType.ServerNode) {
                    conMenuRegistrySession.Show(MousePosition);
                    return;
                }
            }
        }

        /// <summary>
        /// Wählt die angeklickte Node aus
        /// </summary>
        /// <param name="trv">TreeView in der das Event ausgelößt wurde</param>
        /// <param name="e">Das Event zur Bestimmung der Mausposition</param>
        /// <returns>Das angeklickte TreeNode</returns>
        private static TreeNode SelectNodeAfterRightClick(TreeView trv, MouseEventArgs e) {
            TreeNode clickedNode = trv.GetNodeAt(e.Location);
            trv.SelectedNode = clickedNode;
            return clickedNode;
        }



        /// <summary>
        /// Startet eine Session mit modifizierten Fore- & Backgroundfarben
        /// </summary>
        /// <param name="sessionName">Session die gestartetw erden soll</param>
        /// <param name="bgColor">Gewünschte Hintergrundfarbe</param>
        /// <param name="foreColor">Gewünschte Textfarbe</param>
        /// <param name="isRemote">Gibt an, ob eine Teamsession gestartet werden soll</param>
        private void StartSessionInColor(string sessionName, string bgColor, string foreColor, bool isRemote=false) {
            string sessionFile = Path.Combine(ApplicationSettings.LocalRepositoryPath, sessionName);
            
            try {
                if (File.Exists(sessionFile)) {
                    string[] backup = File.ReadAllLines(sessionFile);
                    SetCustomColors(bgColor, foreColor, sessionFile);

                    StartPuttySession(sessionName);

                    System.Threading.Thread.Sleep(500);

                    File.WriteAllLines(sessionFile, backup);
                }
            }catch(Exception ex){
                Program.LogWriter.Log("Could not start Colored Session: {0}", ex.Message);
            }
        }

        /// <summary>
        /// Ändert die Text- und Hintergrundfarbe einer Session
        /// </summary>
        /// <param name="bgColor">Gewünschte Hintergrundfarbe</param>
        /// <param name="foreColor">Gewünschte Textfarbe</param>
        /// <param name="sessionFile">Die Session die geändert werden soll</param>
        private static void SetCustomColors(string bgColor, string foreColor, string sessionFile) {
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

        #region ColoredSessionEventHandlers

        //MySessions ---

        private void blackWhiteToolStripMenuItem_Click(object sender, EventArgs e) {
            StartSessionInColor(trvSessions.SelectedNode.Text, "Colour=0,0,0", "Colour0=255,255,255");
        }

        private void blackGreenToolStripMenuItem_Click(object sender, EventArgs e) {
            StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=255,255,255", "Colour0=0,0,0");
        }

        private void blackGreenToolStripMenuItem1_Click(object sender, EventArgs e) {
            StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=0,0,0", "Colour0=0,255,0");
        }

        private void yellowBlackToolStripMenuItem_Click(object sender, EventArgs e) {
            StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=227,255,104", "Colour0=0,0,0");
        }

        private void blueBlackToolStripMenuItem_Click(object sender, EventArgs e) {
            StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=119,255,239", "Colour0=0,0,0");
        }

        private void greenBlackToolStripMenuItem_Click(object sender, EventArgs e) {
            StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=174,255,145", "Colour0=0,0,0");
        }

        private void redBlackToolStripMenuItem_Click(object sender, EventArgs e) {
            StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=255,188,196", "Colour0=0,0,0");
        }

        private void greyBlackToolStripMenuItem_Click(object sender, EventArgs e) {
            StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=192,192,192", "Colour0=0,0,0");
        }

        //--- RecentSessions

        private void toolStripMenuItem13_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour=0,0,0", "Colour0=255,255,255");
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=255,255,255", "Colour0=0,0,0");
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=0,0,0", "Colour0=0,255,0");
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=227,255,104", "Colour0=0,0,0");
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=119,255,239", "Colour0=0,0,0");
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=174,255,145", "Colour0=0,0,0");
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=255,188,196", "Colour0=0,0,0");
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=192,192,192", "Colour0=0,0,0");
        }

        //Team Sessions ---

        private void toolStripMenuItem22_Click(object sender, EventArgs e) {
            TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text));
            StartSessionInColor(trvTeam.SelectedNode.Text, "Colour=0,0,0", "Colour0=255,255,255");
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e) {
            TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text));
            StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=255,255,255", "Colour0=0,0,0");
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e) {
            TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text));
            StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=0,0,0", "Colour0=0,255,0");
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e) {
            TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text));
            StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=227,255,104", "Colour0=0,0,0");
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e) {
            TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text));
            StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=119,255,239", "Colour0=0,0,0");
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e) {
            TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text));
            StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=174,255,145", "Colour0=0,0,0");
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e) {
            TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text));
            StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=255,188,196", "Colour0=0,0,0");
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e) {
            TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text));
            StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=192,192,192", "Colour0=0,0,0");
        }

        //Registry sessions ---
        private void toolStripMenuItem31_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour=0,0,0", "Colour0=255,255,255");
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=255,255,255", "Colour0=0,0,0");
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=0,0,0", "Colour0=0,255,0");
        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=227,255,104", "Colour0=0,0,0");
        }

        private void toolStripMenuItem35_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=119,255,239", "Colour0=0,0,0");
        }

        private void toolStripMenuItem36_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=174,255,145", "Colour0=0,0,0");
        }

        private void toolStripMenuItem37_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=255,188,196", "Colour0=0,0,0");
        }

        private void toolStripMenuItem38_Click(object sender, EventArgs e) {
            StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=192,192,192", "Colour0=0,0,0");
        }

        #endregion

        private void startAllSessionsInFolderToolStripMenuItem_Click(object sender, EventArgs e) {
            StartAllSessionsInFolder(trvSessions.SelectedNode);
        }

        /// <summary>
        /// Startet alle Sessions eines ordners
        /// </summary>
        /// <param name="folder">Ordner-TreeNode</param>
        private void StartAllSessionsInFolder(TreeNode folder) {
            foreach (TreeNode session in folder.Nodes) {
                if (session.ImageIndex == (int)NodeType.ServerNode) {
                    StartPuttySession(session.Text);
                }
            }
        }

        private void connectWithFileZillaToolStripMenuItem_Click(object sender, EventArgs e) {
            dlgPassword passDlg = new dlgPassword();
            DialogResult res = passDlg.ShowDialog();

            if (!string.IsNullOrEmpty(passDlg.EnteredPassword)) {
                StartSessionInFileZilla(trvSessions.SelectedNode.Text, passDlg.EnteredPassword);
            }
            
        }

        private void connectWithWinSCPToolStripMenuItem_Click(object sender, EventArgs e) {
            StartSessionInWinSCP(trvSessions.SelectedNode.Text);
        }



        private void convertToExistingSessionToolStripMenuItem_Click(object sender, EventArgs e) {
            trvSessions.SelectedNode.ImageIndex = (int)NodeType.ServerNode;
            trvSessions.SelectedNode.SelectedImageIndex = (int)NodeType.ServerNode;
        }

        private void trvSessions_AfterExpand(object sender, TreeViewEventArgs e) {
            SaveChanges();
        }

        private void trvSessions_AfterCollapse(object sender, TreeViewEventArgs e) {
            SaveChanges();
        }

        private void twiSessions_SizeChanged(object sender, EventArgs e) {
            ApplicationSettings.LastOverviewWindowSize = this.Size;
        }
    }
}
