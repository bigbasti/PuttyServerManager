using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuttyServerManager.ToolWindows;
using WeifenLuo.WinFormsUI.Docking;
using WindowTool;
using PuttyServerManager.Persistence.Repository;
using PuttyServerManager.WindowTools;
using PuttyServerManager.Config;


namespace PuttyServerManager {
    public partial class frmMainWindow : Form {

        ISessionRepository recentRepository = new RecentSessionRepository();

        /// <summary>
        /// Fenster mit der Treeview, die alle Sessions beinhaltet
        /// </summary>
        public twiSessions frmSessions;
        
        public frmMainWindow() {
            InitializeComponent();

            frmSessions = new twiSessions(ContentPanel, this);
            frmSessions.Size = ApplicationPaths.LastOverviewWindowSize;

            RestoreLastSessionListWidth();

            frmSessions.Show(ContentPanel, ApplicationPaths.SessionOverviewDockState);
           
        }

        private void RestoreLastSessionListWidth() {
            if (ApplicationPaths.SessionOverviewDockState == DockState.DockLeft) {
                ContentPanel.DockLeftPortion = ApplicationPaths.LastOverviewWindowSize.Width;
            }else if(ApplicationPaths.SessionOverviewDockState == DockState.DockRight){
                ContentPanel.DockRightPortion = ApplicationPaths.LastOverviewWindowSize.Width;
            }
        }


        private void frmMainWindow_Load(object sender, EventArgs e) {
            cboServerProtocol.SelectedIndex = 0;

            this.Size = ApplicationPaths.LastWindowSize;
            this.Location = ApplicationPaths.LastWindowPosition;

            if (this.Location.X < 0 || this.Location.Y < 0) {
                Program.LogWriter.Log("Main Window is out of reach for the user, resetting window position");
                this.Location = new Point(0, 0);
            }

            toolQuickConnect.Visible = ApplicationPaths.ShowQuickConnectionBar;
            showQuickConnectionBarToolStripMenuItem.Checked = ApplicationPaths.ShowQuickConnectionBar;

            this.GotFocus += ((object o, EventArgs ev) => {
                ContentPanel.ActivePane.Focus();
            });

            CheckIfFirstStart();

        }

        private void CheckIfFirstStart() {
            if (ApplicationPaths.FirstStart) {
                ApplicationPaths.FirstStart = false;
                new frmWizard(frmSessions).ShowDialog();
            }
        }



        private void showQuickConnectionBarToolStripMenuItem_Click(object sender, EventArgs e) {
            toolQuickConnect.Visible = !showQuickConnectionBarToolStripMenuItem.Checked;
            showQuickConnectionBarToolStripMenuItem.Checked = !showQuickConnectionBarToolStripMenuItem.Checked;

            ApplicationPaths.ShowQuickConnectionBar = !showQuickConnectionBarToolStripMenuItem.Checked;
        }

        private void btnStartQuickConnection_Click(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(txtServerIP.Text) && !string.IsNullOrEmpty(txtServerPort.Text)) {
                try {
                    StartQuickSession(cboServerProtocol.Text, txtServerIP.Text, txtServerPort.Text);

                } catch (Exception ex) {
                    MessageBox.Show("Cold not create the Session with the Values you provided!", "Cann not create session", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.LogWriter.Log("# Cold not create the Session with the Values you provided! " + ex.Message);
                }
            }
        }

        public void StartQuickSession(string protocol, string host, string port) {
            string name = protocol + "-" + host + "-" + port;
            recentRepository.AddSession(host, protocol, Convert.ToInt32(port), name);

            frmSessions.AddSessionToRecentSessionList(name);

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

            puttyWindow = new twiPutty(name, callback, this);
            puttyWindow.Show(ContentPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
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

            puttyWindow = new twiPutty("", callback, this);
            puttyWindow.Show(ContentPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void txtServerIP_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                btnStartQuickConnection_Click(sender, e);
            }
        }

        private void pSGSettingsToolStripMenuItem_Click(object sender, EventArgs e) {
            new frmSettings(frmSessions).ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            frmSessions.SaveChanges();
            Environment.Exit(0);
        }

        private void frmMainWindow_FormClosing(object sender, FormClosingEventArgs e) {
            ApplicationPaths.LastWindowPosition = this.Location;
            ApplicationPaths.LastWindowSize = this.Size;
            ApplicationPaths.SessionOverviewDockState = frmSessions.DockState;
            ApplicationPaths.LastOverviewWindowSize = frmSessions.Size;
        }



    }
}
