using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowTool;
using PuttyServerGUI2.Config;
using System.Runtime.InteropServices;
using PuttyServerGUI2.WindowTools;


namespace PuttyServerGUI2.ToolWindows {
    public partial class twiPutty : ToolWindow {

        private ApplicationPanel applicationwrapper;
        private PuttyClosedCallback m_ApplicationExit;

        private Form containerForm;

        public string sessionName;

        public twiPutty(string session, PuttyClosedCallback callback, Form container) {
            InitializeComponent();

            m_ApplicationExit = callback;
            containerForm = container;
            sessionName = session;

            if (session == "") {
                this.Text = ApplicationPaths.PuttyLocation;
            } else {
                this.Text = session;
            }

            this.applicationwrapper = new ApplicationPanel(container);
            this.applicationwrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationwrapper.ApplicationName = ApplicationPaths.PuttyLocation;
            if (session != "") {
                this.applicationwrapper.ApplicationParameters = "-load " + session;
            }
            this.applicationwrapper.Location = new System.Drawing.Point(0, 0);
            this.applicationwrapper.Name = "applicationControl1";
            this.applicationwrapper.Size = new System.Drawing.Size(284, 264);
            this.applicationwrapper.TabIndex = 0;
            this.applicationwrapper.m_CloseCallback = this.m_ApplicationExit;
            this.Controls.Add(this.applicationwrapper);

            this.FormClosing += new FormClosingEventHandler(twiPutty_FormClosing);
            this.TabPageContextMenuStrip = this.conMenuSessionTab;
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

        private void twiPutty_Load(object sender, EventArgs e) {
            //this.GotFocus += ((object s, EventArgs ev) => {
            //    //this.applicationwrapper.Focus();
                
            //    applicationwrapper.ReFocusPuTTY();
            //});

            //this.LostFocus += ((object s, EventArgs ev) => {

            //});

            //this.applicationwrapper.GotFocus += ((object s, EventArgs ev) => {
            //    //FocusParentWindow();

            //    applicationwrapper.ReFocusPuTTY();
            //});
        }

        private void closeSessionToolStripMenuItem_Click(object sender, EventArgs e) {
            ((frmMainWindow)containerForm).ContentPanel.ActiveDocument.DockHandler.Close();
        }

        private void cloneSessionToolStripMenuItem_Click(object sender, EventArgs e) {
            ((frmMainWindow)containerForm).frmSessions.StartPuttySession(sessionName);
        }

        private void renameTabToolStripMenuItem_Click(object sender, EventArgs e) {
            string newName = Microsoft.VisualBasic.Interaction.InputBox("Please enter the new name", "enter new name", this.TabText);
            if (!string.IsNullOrEmpty(newName)) {
                this.TabText = newName;
            }
        }
    }
}
