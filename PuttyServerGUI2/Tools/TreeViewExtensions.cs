using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuttyServerGUI2.Persistence;

namespace PuttyServerGUI2.Tools {
    /// <summary>
    /// Erweiterungen für die TreeView
    /// </summary>
    static class TreeViewExtensions {

        /// <summary>
        /// Serialisiert eine angegebe TreeNode in eine Datei
        /// </summary>
        /// <param name="trv">Automatischer Parameter (Gerade aktive TreeView)</param>
        /// <param name="node">Die Node die serialisiert werden soll</param>
        /// <param name="filename">Datei in die geschrieben werden soll</param>
        public static void SerializeNode(this TreeView trv, TreeNode node, string filename){
            SessionSerializer.SerializeNode(node, filename);
        }

        /// <summary>
        /// Deserialiert ein TreeNode aus einer Datei
        /// </summary>
        /// <param name="trv">Automatischer Parameter (Gerade aktive TreeView)</param>
        /// <param name="filename">Datei aus der gelesen werden soll</param>
        /// <returns>Ausgelesene TreeNode oder null</returns>
        public static TreeNode DeserializeNode(this TreeView trv, string filename) {
            return (TreeNode)SessionSerializer.DeserializeNode(filename);
        }
    }
}
