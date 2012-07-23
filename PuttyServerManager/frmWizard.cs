using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuttyServerManager.Config;
using PuttyServerManager.Tools.Extensions;
using PuttyServerManager.ToolWindows;
using System.IO;

namespace PuttyServerManager {
    public partial class frmWizard : Form {

        private int SlideIndex = 0;

        private Panel[] Slides;
        private Panel CurrentSlide;

        private twiSessions SessionsForm;

        public frmWizard(Form sessions) {
            InitializeComponent();

            SessionsForm = (twiSessions)sessions;
        }

        private void btnSetSessionListPath_Click(object sender, EventArgs e) {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            DialogResult res = file.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Abort && res != System.Windows.Forms.DialogResult.Cancel) {
                txtTeamSessionListPath.Text = file.FileName;
            }
        }

        private void btnSetSessionFolderPath_Click(object sender, EventArgs e) {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult res = folder.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Abort && res != System.Windows.Forms.DialogResult.Cancel) {
                txtTeamSessionFolderPath.Text = folder.SelectedPath;
            }
        }

        private void btnFindFavourite_Click(object sender, EventArgs e) {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            DialogResult res = file.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Abort && res != System.Windows.Forms.DialogResult.Cancel) {
                try {
                    Program.LogWriter.Log("Importing favourive.xml");
                    TreeNode node = new TreeView().DeserializeTeamNode(file.FileName);
                    SessionsForm.TrvSessions.Nodes[0].Nodes.Add(node);
                    SessionsForm.SaveChanges();

                    Program.LogWriter.Log("Import was sucessful");
                    Program.LogWriter.Log("Copying old Sessions to new repository");

                    btnFindFavourite.Visible = false;
                    lblFavouriteStatus.Text = "Favourites successfully imported!";
                    chkImportFavourites.Enabled = false;

                    string pathToOldSessions = Path.Combine(Path.GetDirectoryName(file.FileName), ".putty\\sessions");
                    Program.LogWriter.Log("Copying from {0} to {1}", pathToOldSessions, ApplicationPaths.LocalRepositoryPath);
                    foreach (string f in Directory.GetFiles(pathToOldSessions)) {
                        string newFile = Path.Combine(ApplicationPaths.LocalRepositoryPath, f);
                        try {
                            Program.LogWriter.Log("Copying File to repository: {0}", f);
                            File.Copy(f, Path.Combine(ApplicationPaths.LocalRepositoryPath, Path.GetFileName(f)), false);
                        } catch (Exception ex) {
                            Program.LogWriter.Log("Skipping file {0} - because: {1}", f, ex.Message);
                        }
                    }
                } catch (Exception ex) {
                    Program.LogWriter.Log("Error While importing Sessions / Copying files - {0}", ex.Message);
                    lblFavouriteStatus.Text = "Error while importing list.";
                }
            }
        }

        private void frmWizard_Load(object sender, EventArgs e) {
            CurrentSlide = panel1;
            Slides = new Panel[] {panel1, panel2, panel3, panel4, panel5};

            txtFileZillaPath.Text = ApplicationPaths.PathToFileZilla;
            txtWinSCPPath.Text = ApplicationPaths.PathToWinSCP;
            txtTeamSessionFolderPath.Text = ApplicationPaths.RemoteRepositoryPath;
            txtTeamSessionListPath.Text = ApplicationPaths.RemoteSessionListPath;
            txtTeamUsername.Text = ApplicationPaths.TeamUsername;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (panel1.Visible) {
                if (rdTeamUser.Checked) {
                    SlideIndex += 1;
                } else {
                    SlideIndex += 2;
                }
            } else {
                SlideIndex += 1;
            }

            CurrentSlide.Visible = false;
            Slides[SlideIndex].Visible = true;
            CurrentSlide = Slides[SlideIndex];

            if (CurrentSlide == Slides[Slides.Length - 1]) {
                btnNext.Enabled = false;
                btnSkip.Enabled = false;
                btnFinish.Enabled = true;
            }

            btnBack.Enabled = true;
        }

        private void btnBack_Click(object sender, EventArgs e) {
            if (panel3.Visible) {
                if (rdTeamUser.Checked) {
                    SlideIndex -= 1;
                } else {
                    SlideIndex -= 2;
                }
            } else {
                SlideIndex -= 1;
            }

            CurrentSlide.Visible = false;
            Slides[SlideIndex].Visible = true;
            CurrentSlide = Slides[SlideIndex];

            if (CurrentSlide == Slides[0]) {
                btnBack.Enabled = false;
            }

            btnNext.Enabled = true;
            btnSkip.Enabled = true;
            btnFinish.Enabled = false;
        }

        private void btnOpenFilezillaPath_Click(object sender, EventArgs e) {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            DialogResult res = file.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Abort && res != System.Windows.Forms.DialogResult.Cancel) {
                txtFileZillaPath.Text = file.FileName;
            }
        }

        private void btnOpenWinSCPPath_Click(object sender, EventArgs e) {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            DialogResult res = file.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Abort && res != System.Windows.Forms.DialogResult.Cancel) {
                txtWinSCPPath.Text = file.FileName;
            }
        }

        private void chkImportFavourites_CheckedChanged(object sender, EventArgs e) {

        }

        private void chkImportFavourites_Click(object sender, EventArgs e) {
            chkImportFavourites.Checked = chkImportFavourites.Checked;
            panel6.Visible = chkImportFavourites.Checked;
        }

        private void btnSkip_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnFinish_Click(object sender, EventArgs e) {
             ApplicationPaths.PathToFileZilla = txtFileZillaPath.Text;
             ApplicationPaths.PathToWinSCP = txtWinSCPPath.Text;
             ApplicationPaths.RemoteRepositoryPath = txtTeamSessionFolderPath.Text;
             ApplicationPaths.RemoteSessionListPath = txtTeamSessionListPath.Text;
             ApplicationPaths.TeamUsername = txtTeamUsername.Text;

             MessageBox.Show("The new settings require a restart of the application. Putty Server Manager will close now", "Application restart required!", MessageBoxButtons.OK, MessageBoxIcon.Information);

             Environment.Exit(0);
        }


    }
}
