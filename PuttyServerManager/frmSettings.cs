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

            lblStoredSessions.Text = string.Format(lblStoredSessions.Text, Directory.GetFiles(ApplicationSettings.LocalRepositoryPath).Length);
            txtStoredSessionsPath.Text = ApplicationSettings.LocalRepositoryPath;

            lblVersion.Text = string.Format(lblVersion.Text, Application.ProductVersion);
            lblVersion2.Text = string.Format(lblVersion2.Text, Application.ProductVersion);

            LoadSettings();
        }

        private void btnOpenStoredSessionsFolder_Click(object sender, EventArgs e) {
            try {
                Process.Start(ApplicationSettings.LocalRepositoryPath);
            } catch (Exception ex) {
                Program.LogWriter.Log("Error opening Sessions Folder - {0}", ex.Message);
            }
        }

        private void SaveSettings() {
            ApplicationSettings.StartWithWindows = chkStartWithWindows.Checked;
            ApplicationSettings.PuttyAgentParameters = txtPuttyAgentParameters.Text;
            ApplicationSettings.RemoteRepositoryPath = txtTeamSessionFolder.Text;
            ApplicationSettings.RemoteSessionListPath = txtTeamSessionList.Text;
            ApplicationSettings.TeamUsername = txtUsername.Text;
            ApplicationSettings.UsePuttyAgent = chkUsePuttyAgent.Checked;
            ApplicationSettings.RuninSingleInstanceMode = chkSingleWindow.Checked;
            ApplicationSettings.PathToFileZilla = txtFileZillaPath.Text;
            ApplicationSettings.PathToWinSCP = txtWinSCPPath.Text;
        }

        private void LoadSettings() {
            chkStartWithWindows.Checked = ApplicationSettings.StartWithWindows;
            txtPuttyAgentParameters.Text = ApplicationSettings.PuttyAgentParameters;
            txtTeamSessionFolder.Text = ApplicationSettings.RemoteRepositoryPath;
            txtTeamSessionList.Text = ApplicationSettings.RemoteSessionListPath;
            txtUsername.Text = ApplicationSettings.TeamUsername;
            chkUsePuttyAgent.Checked = ApplicationSettings.UsePuttyAgent;
            txtPuttyAgentParameters.Enabled = chkUsePuttyAgent.Checked;
            chkSingleWindow.Checked = ApplicationSettings.RuninSingleInstanceMode;
            txtFileZillaPath.Text = ApplicationSettings.PathToFileZilla;
            txtWinSCPPath.Text = ApplicationSettings.PathToWinSCP;
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
            ApplicationSettings.StartWithWindows = !chkStartWithWindows.Checked;
            RegistryTools.RegisterInStartup(!chkStartWithWindows.Checked);
        }

        private void btnRunConfigurationWizard_Click(object sender, EventArgs e) {
            frmWizard wizard = new frmWizard(SessionsForm);
            wizard.ShowDialog();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://sourceforge.net/projects/dockpanelsuite/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://www.famfamfam.com/lab/icons/silk/");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("http://code.google.com/p/superputty/");
        }
    }
}
