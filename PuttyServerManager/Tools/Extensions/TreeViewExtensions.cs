using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PuttyServerManager.Persistence;
using System.IO;

namespace PuttyServerManager.Tools.Extensions {
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
            return SessionSerializer.DeserializeNode(filename);
        }

        /// <summary>
        /// Deserialiert ein TreeNode aus einer Datei
        /// </summary>
        /// <param name="trv">Automatischer Parameter (Gerade aktive TreeView)</param>
        /// <param name="filename">Datei aus der gelesen werden soll</param>
        /// <returns>Ausgelesene TreeNode oder null</returns>
        public static TreeNode DeserializeTeamNode(this TreeView trv, string filename) {
            return SessionSerializer.DeserializeTeamNode(filename);
        }

        /// <summary>
        /// Durchsucht rekursiv die gesamte Struktir nach einem Knoten
        /// </summary>
        /// <param name="trv">Automatischer Parameter (Gerade aktive TreeView)</param>
        /// <param name="nodeName">Name nach dem gesucht werden soll</param>
        /// <returns>True wenn der Name gefunden wurde</returns>
        public static bool DoesNodeExist(this TreeView trv, string nodeName) {
            bool found = false;
            foreach (TreeNode n in trv.Nodes) {
                found = SearchNextNode(n, nodeName);
            }
            return found;
        }

        private static bool SearchNextNode(TreeNode nextNode, string nodeName) {
            bool found = false;
            foreach (TreeNode n in nextNode.Nodes) {
                if (n.Text.Equals(nodeName)) { return true; }
                found = SearchNextNode(n, nodeName);
            }
            return found;
        }
    }
}
