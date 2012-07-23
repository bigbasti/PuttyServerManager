using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;

using PuttyServerManager;
using PuttyServerManager.Config;

namespace PuttyServerManager.Persistence {
    /// <summary>
    /// Se- und Deserialisiert Objekte
    /// </summary>
    static class SessionSerializer {

        /// <summary>
        /// Serialisert eine TreeNode in eine Datei
        /// </summary>
        /// <param name="node">Die TreeNode, das serialiert werden soll</param>
        /// <param name="file">Pfad zu der Datei in die das serialisierte Objekt gespeichert werden soll</param>
        /// <returns>True wenn Vorgang erfolgreich war</returns>
        public static bool SerializeNode(TreeNode node, string file) {

            XElement sessions = CreateXElementFromTreeNode(node);
            
            if (File.Exists(file)) {
                try {
                    File.Delete(file);
                } catch (Exception ex) {
                    Program.LogWriter.Log(string.Format("Konnte die Datei {0} nicht löschen. Grund: {1}", file, ex.Message));
                    return false;
                }
            }

            try {
                sessions.Save(file);
            } catch (Exception ex) {
                Program.LogWriter.Log(string.Format("Fehler : {0}", ex.Message));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Durchläuft rekursiv den angegebenen Knoten und speichert die Einträge in einem XElement
        /// </summary>
        /// <param name="nextNode">Node der umgewandelt werden soll</param>
        /// <returns>XElement mit der Struktur des TreeNode</returns>
        private static XElement CreateXElementFromTreeNode(TreeNode nextNode) {
            XElement retVal = new XElement(XmlConvert.EncodeName(nextNode.Text),
                        new XAttribute("ImageIndex", nextNode.ImageIndex),
                        new XAttribute("Expanded", nextNode.IsExpanded),
                        from TreeNode n in nextNode.Nodes
                        select
                            CreateXElementFromTreeNode(n)
                    );
            return retVal;
        }

        /// <summary>
        /// Deserialisiert eine TreeNode aus einer Datei
        /// </summary>
        /// <param name="filename">Pfad zu der Datei die deaserialisiert werden soll</param>
        /// <returns>Deserialisierte TreeNode oder null im Fehlerfall</returns>
        public static TreeNode DeserializeNode(string filename) {
            XmlDocument doc = new XmlDocument();
            TreeNode sessions = null;
            try {
                doc.LoadXml(File.ReadAllText(filename));

                //Es wird nur ein Node gesichert und wiederhergestellt!
                sessions = CreateTreeNodeFromXmlNode(doc.ChildNodes[1]);  //Mhh... müsste man mal schönder machen
            } catch (Exception ex) {
                Program.LogWriter.Log("# Could not load Sessionlist ({0}) - Just create some Folders and the List will be recreated!", ex.Message);
            }
            return sessions;
        }

        /// <summary>
        /// Durchläuft rekursiv den angegebenen Knoten und speichert die Einträge in einem TreeNode
        /// </summary>
        /// <param name="node">Der zu parsende XML-Knoten</param>
        /// <returns>TreeNode</returns>
        private static TreeNode CreateTreeNodeFromXmlNode(XmlNode node) {
            TreeNode retVal = new TreeNode(XmlConvert.DecodeName(node.Name));

            if (File.Exists(Path.Combine(ApplicationSettings.LocalRepositoryPath, XmlConvert.DecodeName(node.Name))) || node.HasChildNodes) {
                if (Convert.ToInt32(node.Attributes["ImageIndex"].Value) == (int)NodeType.ServerError) {
                    retVal.ImageIndex = (int)NodeType.ServerNode;
                    retVal.SelectedImageIndex = (int)NodeType.ServerNode;
                } else {
                    retVal.ImageIndex = Convert.ToInt32(node.Attributes["ImageIndex"].Value);
                    retVal.SelectedImageIndex = Convert.ToInt32(node.Attributes["ImageIndex"].Value);
                }
            } else {
                retVal.ImageIndex = (int)NodeType.ServerError;
                retVal.SelectedImageIndex = (int)NodeType.ServerError;
            }
            if (Convert.ToBoolean(node.Attributes["Expanded"].Value)) { retVal.Expand(); }

            foreach (XmlNode n in node.ChildNodes) {
                retVal.Nodes.Add(CreateTreeNodeFromXmlNode(n));
            }

            return retVal;
        }

        /// <summary>
        /// Deserialisiert eine TreeNode aus einer Datei
        /// </summary>
        /// <param name="filename">Pfad zu der Datei die deaserialisiert werden soll</param>
        /// <returns>Deserialisierte TreeNode oder null im Fehlerfall</returns>
        public static TreeNode DeserializeTeamNode(string filename) {
            XmlDocument doc = new XmlDocument();
            TreeNode sessions = null;
            try {
                doc.LoadXml(File.ReadAllText(filename));

                //Es wird nur ein Node gesichert und wiederhergestellt!
                sessions = CreateTreeNodeFromTeamXmlNode(doc.ChildNodes[1]);  //Mhh... müsste man mal schönder machen
            } catch (Exception ex) {
                Program.LogWriter.Log("# Could not load Sessionlist ({0}) - Just create some Folders and the List will be recreated!", ex.Message);
            }
            return sessions;
        }

        /// <summary>
        /// Durchläuft rekursiv den angegebenen Knoten und speichert die Einträge in einem TreeNode
        /// </summary>
        /// <param name="node">Der zu parsende XML-Knoten</param>
        /// <returns>TreeNode</returns>
        private static TreeNode CreateTreeNodeFromTeamXmlNode(XmlNode node) {
            TreeNode retVal = new TreeNode(XmlConvert.DecodeName(node.Name));

            if (File.Exists(Path.Combine(ApplicationSettings.RemoteRepositoryPath, node.Name)) || node.HasChildNodes) {
                //Abwärtskompatibilität gewährleisten
                try {
                    retVal.ImageIndex = Convert.ToInt32(node.Attributes["ImageIndex"].Value);
                    retVal.SelectedImageIndex = Convert.ToInt32(node.Attributes["ImageIndex"].Value);
                } catch (Exception ex) {
                    //Das alte Format kennt diese Attribute nicht
                    if (node.HasChildNodes) {
                        retVal.ImageIndex = (int)NodeType.FolderNode;
                        retVal.SelectedImageIndex = (int)NodeType.FolderNode;
                    } else {
                        retVal.ImageIndex = (int)NodeType.ServerNode;
                        retVal.SelectedImageIndex = (int)NodeType.ServerNode;
                    }
                }
            } else {
                retVal.ImageIndex = (int)NodeType.ServerError;
                retVal.SelectedImageIndex = (int)NodeType.ServerError;
            }

            try {
                if (Convert.ToBoolean(node.Attributes["Expanded"].Value)) { retVal.Expand(); }
            } catch (Exception ex) { }

            foreach (XmlNode n in node.ChildNodes) {
                retVal.Nodes.Add(CreateTreeNodeFromTeamXmlNode(n));
            }

            return retVal;
        }
    }
}
