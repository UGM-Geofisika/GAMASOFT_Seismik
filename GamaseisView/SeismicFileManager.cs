using System;
using System.IO;
using System.Windows.Forms;

namespace Gamaseis
{
    public static class SeismicFileManager
    {
        public static string FileExt = "sgy";
        private static DirectoryInfo _dirInfo;
        public static string FileUrlNowOpened;
        public static TreeView Treeview;

        public static void Setup(TreeView treeview)
        {
            Treeview = treeview;
        }

        public static void LoadDirectory(string url)
        {
            var pUrl0 = url.Substring(0, url.LastIndexOf(@"\", StringComparison.Ordinal));

            _dirInfo = new DirectoryInfo(pUrl0);

            if (_dirInfo.Exists)
            {
                Treeview.Nodes.Clear();
                BuildTree(_dirInfo, Treeview.Nodes);
            }
        }

        private static void BuildTree(DirectoryInfo dirInfo, TreeNodeCollection addInMe)
        {
            // add every file with specified files
            foreach (var file in dirInfo.GetFiles())
            {
                var pfName = file.Extension.Replace(".", "").ToLower();
                if (pfName == FileExt)
                {
                    addInMe.Add(file.FullName, file.Name, 1);
                }
            }

            // add subdirectory
            foreach (var subdir in dirInfo.GetDirectories())
            {
                // check if subdirectory contains files with specified format
                var fExtExist = false;
                foreach (var file in subdir.GetFiles())
                {
                    var pfName = file.Extension.Replace(".", "").ToLower();
                    if (pfName == FileExt)
                    {
                        fExtExist = true;
                        break;
                    }
                }

                // if files with specified format are exist, add the subdirectory
                if (fExtExist)
                {
                    var curNode = addInMe.Add(subdir.FullName, subdir.Name, 0);
                    BuildTree(subdir, curNode.Nodes);
                }
            }
        }
    }
}