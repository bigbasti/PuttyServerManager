using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using PuttyServerManager.Config;
using System.IO;

namespace PuttyServerManager.Tools {
    class RegistryTools {

        /// <summary>
        /// Liest alle in der Registry gespeicherten Sessions aus
        /// </summary>
        /// <returns>Treenode mit allen gefundenen Sessions</returns>
        public static TreeNode ImportSessionsFromRegistry() {

            TreeNode retVal = new TreeNode("PuTTY Sessions");
            retVal.ImageIndex = -1;
            retVal.SelectedImageIndex = - 1;

            try {

                Microsoft.Win32.RegistryKey f = default(Microsoft.Win32.RegistryKey);
                List<string> lst = new List<string>();
                //Inhalt der Sessions
                List<string> lst_n = new List<string>();
                //Namen der Sessions

                try {
                    f = Registry.CurrentUser.OpenSubKey("Software\\SimonTatham\\PuTTY\\Sessions", false);

                    foreach (string s in f.GetSubKeyNames()) {

                        try {
                            string session = "";
                            Microsoft.Win32.RegistryKey nf = default(Microsoft.Win32.RegistryKey);
                            nf = Registry.CurrentUser.OpenSubKey("Software\\SimonTatham\\PuTTY\\Sessions\\" + s);
                            lst_n.Add(s.Replace("%20", " ").Replace("%2F", "/"));
                            foreach (string a in nf.GetValueNames()) {
                                string o = Registry.GetValue("HKEY_CURRENT_USER\\Software\\SimonTatham\\PuTTY\\Sessions\\" + s, a, null).ToString();
                                session = (a + "=" + o) + Environment.NewLine + session;
                            }

                            lst.Add(session);

                        } catch (Exception ex) {
                        }

                    }

                } catch (Exception ex) {
                }



                for (int i = 0; i <= lst_n.Count - 1; i++) {

                    if (lst_n[i].IndexOfAny(Path.GetInvalidFileNameChars()) != -1 || lst_n[i].Contains(" ")) {
                        foreach (char c in Path.GetInvalidFileNameChars()) {
                            lst_n[i] = lst_n[i].Replace(c.ToString(), "_");
                        }
                        lst_n[i] = lst_n[i].Replace(" ", "_");
                    }

                    string filepath = Path.Combine(ApplicationPaths.LocalRepositoryPath, lst_n[i].ToString());
                    if(File.Exists(filepath)){
                        File.Delete(filepath);
                    }
                    File.WriteAllText(filepath, lst[i].ToString());

                    TreeNode nN = new TreeNode(lst_n[i].ToString());
                    nN.ImageIndex = 6;
                    nN.StateImageIndex = 6;
                    nN.SelectedImageIndex = 6;

                    retVal.Nodes.Add(nN);
                }
            } catch (Exception ex) { }

            return retVal;

        }

        public static void RegisterInStartup(bool isChecked) {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isChecked) {
                registryKey.SetValue("PuttyServerManager", Application.ExecutablePath);
            } else {
                registryKey.DeleteValue("PuttyServerManager");
            }
        }
    }
}
