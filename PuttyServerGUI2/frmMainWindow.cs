using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuttyServerGUI2.ToolWindows;
using WeifenLuo.WinFormsUI.Docking;
using WindowTool;
using PuttyServerGUI2.Persistence.Repository;
using PuttyServerGUI2.WindowTools;
using PuttyServerGUI2.Config;


namespace PuttyServerGUI2 {
    public partial class frmMainWindow : Form {

        ISessionRepository recentRepository = new RecentSessionRepository();



        /// <summary>
        /// Fenster mit der Treeview, die alle Sessions beinhaltet
        /// </summary>
        public twiSessions frmSessions;
        
        public frmMainWindow() {
            InitializeComponent();

            frmSessions = new twiSessions(ContentPanel, this);
            frmSessions.Show(ContentPanel, DockState.DockRight);

            
        }


        private void frmMainWindow_Load(object sender, EventArgs e) {
            cboServerProtocol.SelectedIndex = 0;

            this.Size = ApplicationPaths.LastWindowSize;
            this.Location = ApplicationPaths.LastWindowPosition;
            frmSessions.Size = ApplicationPaths.LastOverviewWindowSize;
            frmSessions.DockState = ApplicationPaths.SessionOverviewDockState;

            toolQuickConnect.Visible = ApplicationPaths.ShowQuickConnectionBar;
            showQuickConnectionBarToolStripMenuItem.Checked = ApplicationPaths.ShowQuickConnectionBar;

            this.GotFocus += ((object o, EventArgs ev) => {
                ContentPanel.ActivePane.Focus();
            });

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
            new frmSettings().ShowDialog();
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
