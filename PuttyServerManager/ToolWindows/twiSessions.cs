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
using PuttyServerManager.Tools;
using PuttyServerManager.Config;
using System.IO;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using WindowTool;
using PuttyServerManager.WindowTools;

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
        /// Startet eine Putty Session in einem gedockten Fenster
        /// </summary>
        /// <param name="sessionName">Name der Session die gestartet werden soll</param>
        /// <param name="dockstate">Angabe wie das neue Fenster angedockt werden soll</param>
        public void StartPuttySession(string sessionName, DockState dockstate = DockState.Document) {
            twiPutty puttyWindow = null;

            ApplicationClosedCallback callback = delegate(bool closed) {
                if (puttyWindow != null) {

                    if (puttyWindow.InvokeRequired) {
                        this.BeginInvoke((MethodInvoker)delegate {
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
            SetRootNodeIcon();

            LoadRecentSessionsList();

            if (ApplicationSettings.RemoteSessionIsConfigured) {
                LoadTeamSessionsList();
                trvTeam.Sort();
                trvTeam.Nodes[0].Expand();
            }

            LoadRegestrySessionsList();

            trvSessions.Sort();
            trvRecentSessions.Sort();

            trvSessions.LabelEdit = localRepository.UserCanEditList();
            trvRecentSessions.LabelEdit = recentRepository.UserCanEditList();

        }

        private void SetRootNodeIcon() {
            trvSessions.Nodes[0].ImageIndex = (int)NodeType.RootNode;
            trvSessions.Nodes[0].SelectedImageIndex = (int)NodeType.RootNode;
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

        private void LoadRegestrySessionsList() {
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
            TreeNode newNode = FileTools.CreateNewServerNode(nodeName);
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
            TreeNode newNode = FileTools.CreateNewFolderNode("New Folder");

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
                FileTools.GetSelectedSessionsFromFolder(open, result, localRepository, trvSessions);
            }

            SaveChanges();
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
                FileTools.StartPuttySession(trvSessions.SelectedNode.Text, this, containerForm, dockPanel);
            }
            if (trvSessions.SelectedNode.ImageIndex == (int)NodeType.ServerError) {   //Nicht gefundene Session
                RemoveMissingNode(trvSessions.SelectedNode);
            }
        }

        private void trvRecentSessions_DoubleClick(object sender, EventArgs e) {
            if (trvRecentSessions.SelectedNode.ImageIndex == (int)NodeType.ServerNode) {   //Normale Session
                FileTools.StartPuttySession(trvRecentSessions.SelectedNode.Text, this, containerForm, dockPanel);
            }
            if (trvRecentSessions.SelectedNode.ImageIndex == (int)NodeType.ServerError) {   //Nicht gefundene Session
                RemoveMissingNode(trvRecentSessions.SelectedNode);
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
            TreeNode node = FileTools.CreateNewServerNode(trvRecentSessions.SelectedNode.Text);

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
            FileTools.StartPuttySession(trvRecentSessions.SelectedNode.Text, this, containerForm, dockPanel);
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
            FileTools.StartPuttySession(trvSessions.SelectedNode.Text, this, containerForm, dockPanel);
        }

        private void twiSessions_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
        }

        private void startSessionToolStripMenuItem2_Click(object sender, EventArgs e) {
            FileTools.StartTeamSession(trvTeam.SelectedNode.Text, localRepository, this, containerForm, dockPanel);
        }

        private void transferSessionToPersonalListToolStripMenuItem_Click(object sender, EventArgs e) {
            string from = Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text);

            if (trvSessions.DoesNodeExist(Path.GetFileName(from))) {
                MessageBox.Show(string.Format("The Session {0} is already in your Session list and won't be added again!", Path.GetFileName(from)), "Session already in the list", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else {

                FileTools.TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, from, localRepository);

                TreeNode newNode = FileTools.CreateNewServerNode(Path.GetFileName(from));

                trvSessions.Nodes[0].Nodes.Add(newNode);
            }
            SaveChanges();
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
                FileTools.StartTeamSession(trvTeam.SelectedNode.Text, localRepository, this, containerForm, dockPanel);
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
            FileTools.StartNativePuttySession(trvRecentSessions.SelectedNode.Text);
        }

        private void startSessionInNativePuTTYWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            FileTools.StartNativePuttySession(trvSessions.SelectedNode.Text);
        }

        private void startInNativePuTTYWindowToolStripMenuItem1_Click(object sender, EventArgs e) {
            FileTools.StartNativePuttySession(trvTeam.SelectedNode.Text);
        }

        private void trvRegistrySessions_AfterSelect(object sender, TreeViewEventArgs e) {
            
        }

        private void trvRegistrySessions_DoubleClick(object sender, EventArgs e) {
            FileTools.StartPuttySession(trvRegistrySessions.SelectedNode.Text, this, containerForm, dockPanel);
        }

        private void startSessionToolStripMenuItem3_Click(object sender, EventArgs e) {
            trvRegistrySessions_DoubleClick(sender, e);
        }

        private void startInNativeWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            FileTools.StartNativePuttySession(trvRegistrySessions.SelectedNode.Text);
        }

        private void transferToPersonalListToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                FolderSetup.SetupDirectory();

                if (!trvSessions.DoesNodeExist(trvRegistrySessions.SelectedNode.Text)) {
                    TreeNode node = FileTools.CreateNewServerNode(trvRegistrySessions.SelectedNode.Text);

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

        #region ColoredSessionEventHandlers

        //MySessions ---

        private void blackWhiteToolStripMenuItem_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvSessions.SelectedNode.Text, "Colour=0,0,0", "Colour0=255,255,255", this, containerForm, dockPanel);
        }

        private void blackGreenToolStripMenuItem_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=255,255,255", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void blackGreenToolStripMenuItem1_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=0,0,0", "Colour0=0,255,0", this, containerForm, dockPanel);
        }

        private void yellowBlackToolStripMenuItem_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=227,255,104", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void blueBlackToolStripMenuItem_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=119,255,239", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void greenBlackToolStripMenuItem_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=174,255,145", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void redBlackToolStripMenuItem_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=255,188,196", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void greyBlackToolStripMenuItem_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvSessions.SelectedNode.Text, "Colour2=192,192,192", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        //--- RecentSessions

        private void toolStripMenuItem13_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour=0,0,0", "Colour0=255,255,255", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=255,255,255", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=0,0,0", "Colour0=0,255,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=227,255,104", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=119,255,239", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=174,255,145", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=255,188,196", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRecentSessions.SelectedNode.Text, "Colour2=192,192,192", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        //Team Sessions ---

        private void toolStripMenuItem22_Click(object sender, EventArgs e) {
            FileTools.TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text), localRepository);
            FileTools.StartSessionInColor(trvTeam.SelectedNode.Text, "Colour=0,0,0", "Colour0=255,255,255", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e) {
            FileTools.TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text), localRepository);
            FileTools.StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=255,255,255", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e) {
            FileTools.TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text), localRepository);
            FileTools.StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=0,0,0", "Colour0=0,255,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e) {
            FileTools.TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text), localRepository);
            FileTools.StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=227,255,104", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e) {
            FileTools.TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text), localRepository);
            FileTools.StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=119,255,239", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e) {
            FileTools.TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text), localRepository);
            FileTools.StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=174,255,145", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e) {
            FileTools.TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text), localRepository);
            FileTools.StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=255,188,196", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e) {
            FileTools.TransferSessionFromTeamFolder(trvTeam.SelectedNode.Text, Path.Combine(ApplicationSettings.RemoteRepositoryPath, trvTeam.SelectedNode.Text), localRepository);
            FileTools.StartSessionInColor(trvTeam.SelectedNode.Text, "Colour2=192,192,192", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        //Registry sessions ---
        private void toolStripMenuItem31_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour=0,0,0", "Colour0=255,255,255", this, containerForm, dockPanel); ;
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=255,255,255", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=0,0,0", "Colour0=0,255,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=227,255,104", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem35_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=119,255,239", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem36_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=174,255,145", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem37_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=255,188,196", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        private void toolStripMenuItem38_Click(object sender, EventArgs e) {
            FileTools.StartSessionInColor(trvRegistrySessions.SelectedNode.Text, "Colour2=192,192,192", "Colour0=0,0,0", this, containerForm, dockPanel);
        }

        #endregion

        private void startAllSessionsInFolderToolStripMenuItem_Click(object sender, EventArgs e) {
            FileTools.StartAllSessionsInFolder(trvSessions.SelectedNode, this, containerForm, dockPanel);
        }

        private void connectWithFileZillaToolStripMenuItem_Click(object sender, EventArgs e) {
            dlgPassword passDlg = new dlgPassword();
            DialogResult res = passDlg.ShowDialog();

            if (!string.IsNullOrEmpty(passDlg.EnteredPassword)) {
                FileTools.StartSessionInFileZilla(trvSessions.SelectedNode.Text, passDlg.EnteredPassword, this, containerForm, dockPanel);
            }
            
        }

        private void connectWithWinSCPToolStripMenuItem_Click(object sender, EventArgs e) {
            FileTools.StartSessionInWinSCP(trvSessions.SelectedNode.Text, this, containerForm, dockPanel);
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
