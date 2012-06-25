using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowTools;
using WindowTool;
using PuttyServerGUI2.Config;

namespace PuttyServerGUI2.ToolWindows {
    public partial class twiPutty : ToolWindow {

        private ApplicationPanel applicationwrapper;
        private PuttyClosedCallback m_ApplicationExit;

        public twiPutty(string session, PuttyClosedCallback callback) {
            InitializeComponent();

            m_ApplicationExit = callback;

            if (session == "") {
                this.Text = ApplicationPaths.PuttyLocation;
            } else {
                this.Text = session;
            }

            this.applicationwrapper = new ApplicationPanel();
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

        }

        private void twiPutty_Load(object sender, EventArgs e) {

        }
    }
}
