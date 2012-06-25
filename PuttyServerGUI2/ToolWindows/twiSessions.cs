using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowTools;

using PuttyServerGUI2.Tools.Extensions;
using PuttyServerGUI2.Persistence.Repository;
using PuttyServerGUI2.Config;
using System.IO;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;
using WindowTool;

namespace PuttyServerGUI2.ToolWindows {
    public partial class twiSessions : ToolWindow {

        private ISessionRepository localRepository;
        private ISessionRepository recentRepository;

        private DockPanel dockPanel;

        public twiSessions(DockPanel dockPanel) {
            InitializeComponent();

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
            try {
                TreeNode node = trvSessions.DeserializeNode(ApplicationPaths.LocalSessionListPath);
                trvSessions.Nodes.Add(node);
            } catch (Exception ex) { 
                trvSessions.Nodes.Add("Saved Sessions"); 
            }

            try {
                TreeNode node = trvRecentSessions.DeserializeNode(ApplicationPaths.RecentSessionListPath);
                trvRecentSessions.Nodes.Add(node);
            } catch (Exception ex) {
                trvRecentSessions.Nodes.Add("Recent Sessions"); 
            }

            trvSessions.LabelEdit = localRepository.UserCanEditList();
            trvRecentSessions.LabelEdit = recentRepository.UserCanEditList();

            //TODO: Ort der Sessions aus dem repository beziehen?
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
                    Process.Start(Path.Combine(ApplicationPaths.LocalRepositoryPath, trvSessions.SelectedNode.Text));
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
            if (trvSessions.SelectedNode.ImageIndex == 6) {

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

                puttyWindow = new twiPutty(trvSessions.SelectedNode.Text, callback);
                puttyWindow.Show(dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            }
        }

    }
}
