using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuttyServerManager.Config;
using System.IO;
using System.Diagnostics;
using PuttyServerManager.Tools;
using PuttyServerManager.ToolWindows;

namespace PuttyServerManager {
    public partial class frmSettings : Form {

        private twiSessions SessionsForm;

        public frmSettings(Form sessions) {
            InitializeComponent();

            SessionsForm = (twiSessions)sessions;
        }

        private void frmSettings_Load(object sender, EventArgs e) {

            FolderSetup.SetupDirectory();

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
            ApplicationPaths.RuninSingleInstanceMode = chkSingleWindow.Checked;
            ApplicationPaths.PathToFileZilla = txtFileZillaPath.Text;
            ApplicationPaths.PathToWinSCP = txtWinSCPPath.Text;
        }

        private void LoadSettings() {
            chkStartWithWindows.Checked = ApplicationPaths.StartWithWindows;
            txtPuttyAgentParameters.Text = ApplicationPaths.PuttyAgentParameters;
            txtTeamSessionFolder.Text = ApplicationPaths.RemoteRepositoryPath;
            txtTeamSessionList.Text = ApplicationPaths.RemoteSessionListPath;
            txtUsername.Text = ApplicationPaths.TeamUsername;
            chkUsePuttyAgent.Checked = ApplicationPaths.UsePuttyAgent;
            txtPuttyAgentParameters.Enabled = chkUsePuttyAgent.Checked;
            chkSingleWindow.Checked = ApplicationPaths.RuninSingleInstanceMode;
            txtFileZillaPath.Text = ApplicationPaths.PathToFileZilla;
            txtWinSCPPath.Text = ApplicationPaths.PathToWinSCP;
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

        private void btnFileZillaPath_Click(object sender, EventArgs e) {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            DialogResult res = file.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Abort && res != System.Windows.Forms.DialogResult.Cancel) {
                txtFileZillaPath.Text = file.FileName;
            }
        }

        private void btnWinSCPPath_Click(object sender, EventArgs e) {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            DialogResult res = file.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Abort && res != System.Windows.Forms.DialogResult.Cancel) {
                txtWinSCPPath.Text = file.FileName;
            }
        }

        private void chkStartWithWindows_CheckedChanged(object sender, EventArgs e) {
            chkStartWithWindows.Checked = !chkStartWithWindows.Checked;
            ApplicationPaths.StartWithWindows = !chkStartWithWindows.Checked;
            RegistryTools.RegisterInStartup(!chkStartWithWindows.Checked);
        }

        private void btnRunConfigurationWizard_Click(object sender, EventArgs e) {
            frmWizard wizard = new frmWizard(SessionsForm);
            wizard.ShowDialog();
        }
    }
}
