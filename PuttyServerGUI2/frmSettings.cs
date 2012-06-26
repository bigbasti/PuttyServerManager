using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuttyServerGUI2.Config;
using System.IO;
using System.Diagnostics;

namespace PuttyServerGUI2 {
    public partial class frmSettings : Form {
        public frmSettings() {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e) {
            lblStoredSessions.Text = string.Format(lblStoredSessions.Text, Directory.GetFiles(ApplicationPaths.LocalRepositoryPath).Length);
            txtStoredSessionsPath.Text = ApplicationPaths.LocalRepositoryPath;

            lblVersion.Text = string.Format(lblVersion.Text, Application.ProductVersion);

            LoadSettings();
        }

        private void btnOpenStoredSessionsFolder_Click(object sender, EventArgs e) {
            try {
                Process.Start(ApplicationPaths.LocalRepositoryPath);
            } catch (Exception ex) {
                Program.LogWriter.Log("Error opening Sessions Folder - {0}", ex.Message);
            }
        }

        private void SaveSettings() {
            ApplicationPaths.StartWithWindows = chkStartWithWindows.Checked;
            ApplicationPaths.PuttyAgentParameters = txtPuttyAgentParameters.Text;
            ApplicationPaths.RemoteRepositoryPath = txtTeamSessionFolder.Text;
            ApplicationPaths.RemoteSessionListPath = txtTeamSessionList.Text;
            ApplicationPaths.TeamUsername = txtUsername.Text;
            ApplicationPaths.UsePuttyAgent = chkUsePuttyAgent.Checked;
        }

        private void LoadSettings() {
            chkStartWithWindows.Checked = ApplicationPaths.StartWithWindows;
            txtPuttyAgentParameters.Text = ApplicationPaths.PuttyAgentParameters;
            txtTeamSessionFolder.Text = ApplicationPaths.RemoteRepositoryPath;
            txtTeamSessionList.Text = ApplicationPaths.RemoteSessionListPath;
            txtUsername.Text = ApplicationPaths.TeamUsername;
            chkUsePuttyAgent.Checked = ApplicationPaths.UsePuttyAgent;
            txtPuttyAgentParameters.Enabled = chkUsePuttyAgent.Checked;
        }

        private void btnSaveClose_Click(object sender, EventArgs e) {
            SaveSettings();
            this.Close();
        }

        private void btnOpenTeamSessionList_Click(object sender, EventArgs e) {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            DialogResult res = file.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Abort && res != System.Windows.Forms.DialogResult.Cancel) {
                txtTeamSessionList.Text = file.FileName;
            }
        }

        private void btnOpenTeamSessionFolder_Click(object sender, EventArgs e) {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult res = folder.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Abort && res != System.Windows.Forms.DialogResult.Cancel) {
                txtTeamSessionFolder.Text = folder.SelectedPath;
            }
        }

        private void chkUsePuttyAgent_CheckedChanged(object sender, EventArgs e) {
            txtPuttyAgentParameters.Enabled = chkUsePuttyAgent.Checked;
        }
    }
}
