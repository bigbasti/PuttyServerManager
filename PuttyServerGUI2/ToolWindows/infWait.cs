using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace PuttyServerGUI2.ToolWindows {
    public partial class infWait : Form {
        
        private string program, param;

        public infWait(string program, string param) {
            InitializeComponent();

            this.program = program;
            this.param = param;
        }


        private void infWait_Load(object sender, EventArgs e) {

        }


        private void StartApplication(string program, string param) {
            ProcessStartInfo pi = new ProcessStartInfo(program, param);
            pi.WorkingDirectory = program.Substring(0, program.LastIndexOf(Path.DirectorySeparatorChar));
            Process.Start(pi);

            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e) {
            StartApplication(program, param);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
