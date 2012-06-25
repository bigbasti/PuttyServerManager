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

namespace PuttyServerGUI2 {
    public partial class frmMainWindow : Form {

        ISessionRepository recentRepository = new RecentSessionRepository();

        /// <summary>
        /// Fenster mit der Treeview, die alle Sessions beinhaltet
        /// </summary>
        private twiSessions frmSessions;
        
        public frmMainWindow() {
            InitializeComponent();

            frmSessions = new twiSessions(ContentPanel);
            frmSessions.Show(ContentPanel, DockState.DockRight);
        }


        private void frmMainWindow_Load(object sender, EventArgs e) {
            cboServerProtocol.SelectedIndex = 0;
        }

        private void showQuickConnectionBarToolStripMenuItem_Click(object sender, EventArgs e) {
            toolQuickConnect.Visible = !showQuickConnectionBarToolStripMenuItem.Checked;
            showQuickConnectionBarToolStripMenuItem.Checked = !showQuickConnectionBarToolStripMenuItem.Checked;
        }

        private void btnStartQuickConnection_Click(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(txtServerIP.Text) && !string.IsNullOrEmpty(txtServerPort.Text)) {
                try {
                    string name = cboServerProtocol.Text + "-" + txtServerIP.Text + "-" + txtServerPort.Text;
                    recentRepository.AddSession(txtServerIP.Text, cboServerProtocol.SelectedText, Convert.ToInt32(txtServerPort.Text), name);

                    frmSessions.AddSessionToRecentSessionList(name);

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

                    puttyWindow = new twiPutty(name, callback);
                    puttyWindow.Show(ContentPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);

                } catch (Exception ex) {
                    MessageBox.Show("Cold not create the Session with the Values you provided!", "Cann not create session", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Program.LogWriter.Log("# Cold not create the Session with the Values you provided! " + ex.Message);
                }
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
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

            puttyWindow = new twiPutty("", callback);
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



    }
}
