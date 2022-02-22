using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace LOL
{
    class Datify
    {
        static void Main(string[] args)
        {
            try
            {                
                string dir = args[0];
                string preString = String.Empty;
                try { preString = args[1]; }
                catch(Exception e) { Console.WriteLine("Warning: no pre string used, only date will be used in filenames!"); }

                DirectoryInfo dirRoot = new DirectoryInfo(Path.GetFullPath(dir));
                if (dirRoot.Exists)
                {
                    foreach (FileInfo fi in dirRoot.GetFiles("*", SearchOption.TopDirectoryOnly))
                    {
                        string newFileName;
                        FileInfo newFileInfo;
                        int i = 0;
                        bool exists = false;
                        while (!exists)
                        {
                            newFileName = fi.FullName.Replace(fi.Name, preString + "_" + fi.CreationTimeUtc.ToShortDateString() + "_" + i + "." + fi.Extension);
                            //newFileName = fi.FullName.Replace(fi.Name, preString + "_" + fi.LastWriteTimeUtc.ToShortDateString() + "_" + i + "." + fi.Extension);
                            newFileInfo = new FileInfo(newFileName);
                            if (!newFileInfo.Exists)
                            {
                                fi.CopyTo(newFileName);
                                exists = true;
                            }
                            i++;
                        }
                    }
                }
                else
                {
                    throw new Exception("No such directory: " + dir);
                }

            }
            catch(Exception e)
            {
                Console.WriteLine("[LOL.Datify.Main] Exception: " + e.Message);
                return;
            }
        }
    }
}
