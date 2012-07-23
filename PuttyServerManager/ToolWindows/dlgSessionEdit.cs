using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuttyServerManager.Persistence.Repository;
using System.Diagnostics;
using System.IO;
using PuttyServerManager.Config;

namespace PuttyServerManager.ToolWindows {
    public partial class dlgSessionEdit : Form {

        ISessionRepository localRepository;
        string session;


        public dlgSessionEdit(string session, ISessionRepository repository) {
            InitializeComponent();

            this.localRepository = repository;
            this.session = session;


        }

        private string ReadSetting(string setting, string session) {
            try {
                string file = Path.Combine(ApplicationSettings.LocalRepositoryPath, session);
                string line = File.ReadAllLines(file).FirstOrDefault(m => m.StartsWith(setting));

                string value = line.Replace(setting + "=", "");

                return value;
            } catch (Exception ex) {
                Program.LogWriter.Log("Could not read settings from session because {0}", ex.Message);
            }
            return "";
        }

        private void WriteSetting(string setting, string data, string session) {
            try {
                string file = Path.Combine(ApplicationSettings.LocalRepositoryPath, session);
                string[] lines = File.ReadAllLines(file);

                for (int i = 0; i < lines.Length; i++) {
                    if (lines[i].StartsWith(setting)) {
                        lines[i] = setting + "=" + data;
                    }
                }

                File.WriteAllLines(file, lines);
            } catch (Exception ex) {
                Program.LogWriter.Log("Could not trite settings to session because {0}", ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            if (localRepository.CheckSessionExists(Path.Combine(ApplicationSettings.LocalRepositoryPath, session))) {
                try {
                    ProcessStartInfo info = new ProcessStartInfo("C:\\Windows\\System32\\notepad.exe", Path.Combine(ApplicationSettings.LocalRepositoryPath, session));
                    Process.Start(info);
                } catch (Exception ex) {
                    Program.LogWriter.Log("Could not start default editor, make sure there is an default editor for this type of file! - {0}", ex.Message);
                    MessageBox.Show("Could not start default editor, make sure there is an default editor for this type of file!");
                }
            }
        }

        private void LoadSettingsFromSession() {
            string hostName = ReadSetting("HostName", session);
            string userName = ReadSetting("UserName", session);
            string remoteCommant = ReadSetting("RemoteCommand", session);
            string bgcolor = ReadSetting("Colour2", session);
            string frcolor = ReadSetting("Colour0", session);

            txtHostname.Text = hostName;
            txtRemoteCommand.Text = remoteCommant;
            txtUserName.Text = userName;

            txtColorDemo.ForeColor = GetColorFromString(frcolor);
            txtColorDemo.BackColor = GetColorFromString(bgcolor);

        }

        private void SaveSettingsToSession() {
            WriteSetting("HostName", txtHostname.Text, session);
            WriteSetting("UserName", txtUserName.Text, session);
            WriteSetting("RemoteCommand", txtRemoteCommand.Text, session);
            WriteSetting("Colour0", txtColorDemo.ForeColor.R + "," +
                                    txtColorDemo.ForeColor.G + "," +
                                    txtColorDemo.ForeColor.B, session);
            WriteSetting("Colour2", txtColorDemo.BackColor.R + "," +
                                    txtColorDemo.BackColor.G + "," +
                                    txtColorDemo.BackColor.B, session);
        }

        private Color GetColorFromString(string colors) {
            string [] colorParts = colors.Split(',');

            try {
                return Color.FromArgb(Convert.ToInt32(colorParts[0]),
                                        Convert.ToInt32(colorParts[1]),
                                        Convert.ToInt32(colorParts[2]));
            } catch (Exception ex) {
                Program.LogWriter.Log("Cound not extract Color settings from given values: {0} because {1}", colors, ex.Message);
            }

            return Color.Black;
        }

        private void dlgSessionEdit_Load(object sender, EventArgs e) {
            LoadSettingsFromSession();

            lblCaption.Text = string.Format(lblCaption.Text, session);
        }

        private void btnSaveClose_Click(object sender, EventArgs e) {
            SaveSettingsToSession();
            this.Close();
        }

        private void btnTextColor_Click(object sender, EventArgs e) {
            ColorDialog cd = new ColorDialog();
            DialogResult res = cd.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Abort && res != System.Windows.Forms.DialogResult.Cancel) {
                txtColorDemo.ForeColor = cd.Color;
            }
        }

        private void btnBackColor_Click(object sender, EventArgs e) {
            ColorDialog cd = new ColorDialog();
            DialogResult res = cd.ShowDialog();

            if (res != System.Windows.Forms.DialogResult.Abort && res != System.Windows.Forms.DialogResult.Cancel) {
                txtColorDemo.BackColor = cd.Color;
            }
        }
    }
}
