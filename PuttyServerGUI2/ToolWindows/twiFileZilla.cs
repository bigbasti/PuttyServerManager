using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuttyServerGUI2.WindowTools;
using System.Runtime.InteropServices;
using PuttyServerGUI2.Config;
using System.IO;

namespace PuttyServerGUI2.ToolWindows {
    public partial class twiFileZilla : ToolWindow {

        private ApplicationPanel applicationwrapper;
        private ApplicationClosedCallback m_ApplicationExit;

        private Form containerForm;

        public string sessionName;

        public twiFileZilla(string session, ApplicationClosedCallback callback, Form container) {
            InitializeComponent();

            m_ApplicationExit = callback;
            containerForm = container;
            sessionName = session;

            if (session == "") {
                this.Text = ApplicationPaths.PuttyLocation;
            } else {
                this.Text = session;
            }

            string parameters = PrepareFileZillaParametersFromSession(session);

            this.applicationwrapper = new ApplicationPanel(container);
            this.applicationwrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationwrapper.ApplicationName = ApplicationPaths.PathToFileZilla;
            if (session != "") {
                this.applicationwrapper.ApplicationParameters = "";
            }
            this.applicationwrapper.Location = new System.Drawing.Point(0, 0);
            this.applicationwrapper.Name = "applicationControl1";
            this.applicationwrapper.Size = new System.Drawing.Size(284, 264);
            this.applicationwrapper.TabIndex = 0;
            this.applicationwrapper.m_CloseCallback = this.m_ApplicationExit;
            this.Controls.Add(this.applicationwrapper);

            this.FormClosing += new FormClosingEventHandler(twiPutty_FormClosing);
            //this.TabPageContextMenuStrip = this.conMenuSessionTab;
        }

        private string PrepareFileZillaParametersFromSession(string session) {
            string retVal = "";

            //try {
                string[] sessionData = File.ReadAllLines(Path.Combine(ApplicationPaths.LocalRepositoryPath, session));

                string filezillaString = "";
                string puttyTunnel = "";

                string userNameLine = "";
                string ipLine = "";
                string remoteCommandLine = "";

                foreach(string line in sessionData){
                    if (line.StartsWith("UserName=")) { userNameLine = line; }
                    if (line.StartsWith("HostName=")) { ipLine = line; }
                    if (line.StartsWith("RemoteCommand=")) { remoteCommandLine = line; }
                }

                if (remoteCommandLine.Length > "RemoteCommand=".Length && remoteCommandLine.Contains("ssh")) {
                    string userName = "";
                    string userPass = "";
                    string serverName = "";
                    string serverPort = "";

                    //Es muss eine Getunnelte Verbindung aufgebaut werden
                    userName = remoteCommandLine.Substring(remoteCommandLine.IndexOf("ssh ") + 4, remoteCommandLine.IndexOf("@") - remoteCommandLine.IndexOf("ssh ") - 4);
                    serverName = remoteCommandLine.Substring(remoteCommandLine.IndexOf("@") + 1, remoteCommandLine.Length -1 - remoteCommandLine.IndexOf("@"));
                    serverPort = "22";

                    string rndPort = new Random().Next(1025, 65000).ToString();

                    puttyTunnel = string.Format("PortForwardings=L{0}={1}:{2},", rndPort, serverName, serverPort);

                    for (int i = 0; i < sessionData.Length; i++) {
                        if (sessionData[i].StartsWith("PortForwardings=")) { sessionData[i] = puttyTunnel; }
                    }

                    //save temp session
                    //start putty session

                    //get user pass
                    dlgPassword passDlg = new dlgPassword();
                    DialogResult res = passDlg.ShowDialog();

                    if (res == System.Windows.Forms.DialogResult.OK) {
                        userPass = passDlg.EnteredPassword;


                    } else {
                        return null;
                    }

                }
            //} catch (Exception ex) { }

            return retVal;
        }

        void twiPutty_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                var docs = ((frmMainWindow)containerForm).ContentPanel.Documents.ToArray();
                docs[docs.Length - 2].DockHandler.Activate();
            } catch (Exception ex) { /*ignore*/ }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public void FocusParentWindow() {
            SetForegroundWindow(this.Handle);
        }

    }
}
