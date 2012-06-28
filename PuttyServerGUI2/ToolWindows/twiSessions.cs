using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PuttyServerGUI2.Tools.Extensions;
using PuttyServerGUI2.Persistence.Repository;
using PuttyServerGUI2.Config;
using System.IO;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using WindowTool;
using PuttyServerGUI2.WindowTools;

namespace PuttyServerGUI2.ToolWindows {
    public partial class twiSessions : ToolWindow {

        private ISessionRepository localRepository;
        private ISessionRepository recentRepository;

        private DockPanel dockPanel;
        private Form containerForm;

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
        private void SaveChanges() {
            trvSessions.SerializeNode(trvSessions.Nodes[0], ApplicationPaths.LocalSessionListPath);
            trvRecentSessions.SerializeNode(trvRecentSessions.Nodes[0], ApplicationPaths.RecentSessionListPath);
        }

        /// <summary>
        /// Lädt alle nötigen Einstellungen
        /// </summary>
        private void LoadConfiguration() {
            LoadLocalSessionsList();
            LoadRecentSessionsList();

            if (ApplicationPaths.RemoteSessionIsConfigured) {
                LoadTeamSessionsList();
                trvTeam.Sort();
                trvTeam.Nodes[0].Expand();
            }

            trvSessions.Sort();
            trvRecentSessions.Sort();

            trvSessions.LabelEdit = localRepository.UserCanEditList();
            trvRecentSessions.LabelEdit = recentRepository.UserCanEditList();

        }

        private void LoadRecentSessionsList() {
            try {
                TreeNode node = trvRecentSessions.DeserializeNode(ApplicationPaths.RecentSessionListPath);
                trvRecentSessions.Nodes.Add(node);
            } catch (Exception ex) {
                trvRecentSessions.Nodes.Add("Recent Sessions");
            }
        }

        private void LoadLocalSessionsList() {
            try {
                TreeNode node = trvSessions.DeserializeNode(ApplicationPaths.LocalSessionListPath);
                trvSessions.Nodes.Add(node);
            } catch (Exception ex) {
                trvSessions.Nodes.Add("Saved Sessions");
            }
        }

        private void LoadTeamSessionsList() {
            try {
                TreeNode node = trvTeam.DeserializeTeamNode(ApplicationPaths.RemoteSessionListPath);
                trvTeam.Nodes.Add(node);
            } catch (Exception ex) {
                Program.LogWriter.Log("Could not load Team Sessions - {0}", ex.Message);
            }
        }

        /// <summary>
        /// Fügt der RecentSessionliste ein neues Element hinzu
        /// </summary>
        /// <param name="nodeName">Name des neuen Elements</param>
        public void AddSessionToRecentSessionList(string nodeName) {
            TreeNode newNode = new TreeNode(nodeName);
            newNode.ImageIndex = 6;
            newNode.SelectedImageIndex = 6;
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

                if (clickedNode.SelectedImageIndex == 9) {
                    conMenuSessionMissing.Show(MousePosition);
                    return;
                }

                if (clickedNode.SelectedImageIndex == 6) {
                    conMenuSession.Show(MousePosition);
                    return;
                }

                if (clickedNode.SelectedImageIndex == 1) {
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
            TreeNode newNode = new TreeNode("New Folder");
            newNode.SelectedImageIndex = 1;
            newNode.ImageIndex = 1;

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
                foreach (string session in open.FileNames) {

                    if (trvSessions.DoesNodeExist(Path.GetFileName(session))) {
                        MessageBox.Show(string.Format("The Session {0} is already in your Session list and won't be added again!", Path.GetFileName(session)), "Session already in the list", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    } else {
                        localRepository.AddSession(session);

                        AddSessionAsTreeNode(session);
                    }
                }
            }
        }

        /// <summary>
        /// Fügt eine neue Session in die TreeView ein
        /// </summary>
        /// <param name="session">Kompletter Pfad der Session</param>
        private void AddSessionAsTreeNode(string session) {
            TreeNode newNode = new TreeNode(Path.GetFileName(session));
            newNode.ImageIndex = 6;
            newNode.SelectedImageIndex = 6;

            trvSessions.SelectedNode.Nodes.Add(newNode);
        }

        private void trvSessions_AfterLabelEdit(object sender, NodeLabelEditEventArgs e) {
            if (e.Label == null || e.Label == "") {
                e.CancelEdit = true;
                return;
            }
            if (e.Node.ImageIndex == 6 && e.Label != null) {
                if (!localRepository.RenameSession(e.Node.Text, e.Label)) {
                    MessageBox.Show("Could not Rename the Session. Look into Logfile for further Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (localRepository.CheckSessionExists(Path.Combine(ApplicationPaths.LocalRepositoryPath, trvSessions.SelectedNode.Text))) {
                try {
                    ProcessStartInfo info = new ProcessStartInfo("C:\\Windows\\System32\\notepad.exe", Path.Combine(ApplicationPaths.LocalRepositoryPath, trvSessions.SelectedNode.Text));
                    Process.Start(info);
                } catch (Exception ex) {
                    Program.LogWriter.Log("Could not start default editor, make sure there is an default editor for this type of file! - {0}", ex.Message);
                    MessageBox.Show("Could not start default editor, make sure there is an default editor for this type of file!");
                }
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e) {
            if (trvSessions.SelectedNode.Nodes.Count > 0) {
                DialogResult res = MessageBox.Show("This Folder contains further Data which will be deleted too if you proceed. Delete anyway?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == System.Windows.Forms.DialogResult.Yes) {
                    trvSessions.SelectedNode.Remove();
                }
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
            if (trvSessions.SelectedNode.ImageIndex == 6) {         //Normale Session
                StartPuttySession(trvSessions.SelectedNode.Text);
            }
            if (trvSessions.SelectedNode.ImageIndex == 9) {   //Nicht gefundene Session
                RemoveMissingNode(trvSessions.SelectedNode);
            }
        }

        private void trvRecentSessions_DoubleClick(object sender, EventArgs e) {
            if (trvRecentSessions.SelectedNode.ImageIndex == 6) {   //Normale Session
                StartPuttySession(trvRecentSessions.SelectedNode.Text);
            }
            if (trvRecentSessions.SelectedNode.ImageIndex == 9) {   //Nicht gefundene Session
                RemoveMissingNode(trvRecentSessions.SelectedNode);
            }
        }

        private void StartPuttySession(string sessionName) {

            //On demand: start putty agent
            if (ApplicationPaths.UsePuttyAgent) {
                if (Process.GetProcessesByName("pageant").Length < 1) {
                    if (File.Exists(ApplicationPaths.PuttyAgentLocation)) {
                        ProcessStartInfo info = new ProcessStartInfo(ApplicationPaths.PuttyAgentLocation, ApplicationPaths.PuttyAgentParameters);
                        Process.Start(info);
                    }
                }
            }
            
            twiPutty puttyWindow = null;

            PuttyClosedCallback callback = delegate(bool closed) {
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
            puttyWindow.Show(dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

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

                if (clickedNode.SelectedImageIndex == 9) {
                    conMenuRemoveMissingRecent.Show(MousePosition);
                    return;
                }

                if (clickedNode.SelectedImageIndex == 6) {
                    conMenuRecent.Show(MousePosition);
                    return;
                }
            }
        }

        private void transferSessionToPersonalSessionsToolStripMenuItem_Click(object sender, EventArgs e) {
            TreeNode node = new TreeNode(trvRecentSessions.SelectedNode.Text);
            node.ImageIndex = 6;
            node.SelectedImageIndex = 6;

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
                if (n.SelectedImageIndex == 9) {
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

        private void StartTeamSession(string sessionName) {
            string from = Path.Combine(ApplicationPaths.RemoteRepositoryPath, sessionName);

            TransferSessionFromTeamFolder(sessionName, from);

            StartPuttySession(sessionName);
        }

        private void TransferSessionFromTeamFolder(string sessionName, string from) {
            if (!localRepository.CheckSessionExists(sessionName)) {
                localRepository.AddSession(from);
            }

            //TODO: Needs refactory
            if (!string.IsNullOrEmpty(ApplicationPaths.TeamUsername)) {
                string[] newSession = File.ReadAllLines(Path.Combine(ApplicationPaths.LocalRepositoryPath, sessionName));

                for (int i = 0; i < newSession.Length; i++) {
                    if (newSession[i].Equals("UserName=")) {
                        newSession[i] = "UserName=" + ApplicationPaths.TeamUsername;
                    }
                    if (newSession[i].Contains("ssh  @")) {
                        newSession[i] = newSession[i].Replace("=ssh  @", "=ssh " + ApplicationPaths.TeamUsername + "@");
                    }
                }

                File.WriteAllLines(Path.Combine(ApplicationPaths.LocalRepositoryPath, sessionName), newSession);
            }
        }

        private void transferSessionToPersonalListToolStripMenuItem_Click(object sender, EventArgs e) {
            string from = Path.Combine(ApplicationPaths.RemoteRepositoryPath, trvTeam.SelectedNode.Text);

            if (trvSessions.DoesNodeExist(Path.GetFileName(from))) {
                MessageBox.Show(string.Format("The Session {0} is already in your Session list and won't be added again!", Path.GetFileName(from)), "Session already in the list", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else {

                TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, from);

                TreeNode newNode = new TreeNode(Path.GetFileName(from));
                newNode.ImageIndex = 6;
                newNode.SelectedImageIndex = 6;

                trvSessions.Nodes[0].Nodes.Add(newNode);
            }
            SaveChanges();
        }

        private void trvTeam_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                //Auch bei einem Rechtsklick das gewählte Element markieren
                TreeNode clickedNode = trvTeam.GetNodeAt(e.Location);
                trvTeam.SelectedNode = clickedNode;

                if (clickedNode.SelectedImageIndex == 9) {
                    conMenuSessionMissing.Show(MousePosition);
                    return;
                }

                if (clickedNode.SelectedImageIndex == 6) {
                    conMenuTeamSession.Show(MousePosition);
                    return;
                }

                if (clickedNode.SelectedImageIndex == 1) {
                    //conMenuFolder.Show(MousePosition);
                    return;
                }

            }
        }

        private void trvTeam_DoubleClick(object sender, EventArgs e) {
            if (trvTeam.SelectedNode.ImageIndex == 6) {         //Normale Session
                StartTeamSession(trvTeam.SelectedNode.Text);
            }
            if (trvTeam.SelectedNode.ImageIndex == 9) {   //Nicht gefundene Session
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
                    if (destenationNode.ImageIndex > 1) {
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

    }
}
