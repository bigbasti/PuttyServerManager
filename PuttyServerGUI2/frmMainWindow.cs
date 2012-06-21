using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuttyServerGUI2.ToolWindows;
using WeifenLuo.WinFormsUI.Docking;

namespace PuttyServerGUI2 {
    public partial class frmMainWindow : Form {

        /// <summary>
        /// Fenster mit der Treeview, die alle Sessions beinhaltet
        /// </summary>
        private twiSessions sessions;
        
        public frmMainWindow() {
            InitializeComponent();

            sessions = new twiSessions();
            sessions.Show(ContentPanel, DockState.DockRight);
        }


        private void frmMainWindow_Load(object sender, EventArgs e) {

        }
    }
}
