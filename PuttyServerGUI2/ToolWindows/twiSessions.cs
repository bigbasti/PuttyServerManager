using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowTools;

using PuttyServerGUI2.Tools;

namespace PuttyServerGUI2.ToolWindows {
    public partial class twiSessions : ToolWindow {
        public twiSessions() {
            InitializeComponent();
        }

        

        private void twiSessions_Load(object sender, EventArgs e) {
            string path = Application.StartupPath + "\\sessionlist.xml";

            trvSessions.SerializeNode(trvSessions.Nodes[0], path);

            //trvSessions.Nodes.Clear();

            TreeNode node = trvSessions.DeserializeNode(path);

            trvSessions.Nodes.Add(node);
        }

        private void trvSessions_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                //Auch bei einem Rechtsklick das gewählte Element markieren
                trvSessions.SelectedNode = trvSessions.GetNodeAt(e.Location);

                if (trvSessions.SelectedNode.SelectedImageIndex == 6) {
                    conMenuSession.Show(MousePosition);
                }
            }
        }
    }
}
