﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using PuttyServerGUI2.Config;
using System.IO;

namespace PuttyServerGUI2.Tools {
    class RegistryExporter {

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
                            lst_n.Add(s.Replace("%20", " "));
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
    }
}
