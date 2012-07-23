using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PuttyServerManager.ToolWindows {
    public partial class dlgPassword : Form {

        public string EnteredPassword { get; private set; }

        public dlgPassword() {
            InitializeComponent();
        }

        private void dlgPassword_Load(object sender, EventArgs e) {
            txtPassword.Focus();
        }

        private void cmdOK_Click(object sender, EventArgs e) {
            EnteredPassword = txtPassword.Text;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
