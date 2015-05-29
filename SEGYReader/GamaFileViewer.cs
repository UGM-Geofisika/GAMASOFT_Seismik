using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace SegyView
{
    public static class GamaFileViewer
    {
        public static string dirURL;
        public static string fileExt = "sgy";
        public static DirectoryInfo dirInfo;
        public static string fileURLNowOpened;
        public static TreeView _treeview;

        public static void Setup(TreeView treeview)
        {
            _treeview = treeview;
        }

        public static void LoadDirectory(string URL)
        {
            string pURL0 = URL.Substring(0,URL.LastIndexOf(@"\"));
            
            dirInfo = new System.IO.DirectoryInfo(@pURL0);
            dirURL = URL;
            
            if (dirInfo.Exists)
            {
                _treeview.Nodes.Clear(); 
                BuildTree(dirInfo, _treeview.Nodes);
            }
        }

        private static void BuildTree(DirectoryInfo dirInfo, TreeNodeCollection addInMe)
        {
            // add every file with specified files
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                string pfName = file.Extension.Replace(".","").ToLower();
                if (pfName == fileExt)
                {
                    addInMe.Add(file.FullName, file.Name, 1);
                }
            }

            // add subdirectory
            foreach (DirectoryInfo subdir in dirInfo.GetDirectories())
            {
                // check if subdirectory contains files with specified format
                bool fExtExist = false; 
                foreach (FileInfo file in subdir.GetFiles())
                {
                    string pfName = file.Extension.Replace(".","").ToLower();
                    if (pfName == fileExt)
                    {
                        fExtExist = true; break;
                    } 
                }

                // if files with specified format are exist, add the subdirectory
                if (fExtExist == true)
                {
                    TreeNode curNode = addInMe.Add(subdir.FullName, subdir.Name, 0);
                    BuildTree(subdir, curNode.Nodes);
                }
            }
        }


    }
}
