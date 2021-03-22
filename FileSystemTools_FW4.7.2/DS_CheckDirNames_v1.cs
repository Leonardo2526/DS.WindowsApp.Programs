using DS_Space;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp_Test
{
    class DS_CheckDirNames
    {
        [STAThread]
        public static void Main()
        {
            DS_Form newForm = new DS_Form
            {
                TopLevel = true
            };
            newForm.Show();

            string CheckPath = newForm.DS_OpenFolderDialogForm("", "Chose directory for check:").ToString();
            string CheckPathName = new DirectoryInfo(CheckPath).Name;
            if (CheckPath == "")
            {
                return;
            }

            string ReportPath = newForm.DS_OpenFolderDialogForm("", "Chose directory for report file:").ToString();
            if (ReportPath == "")
            {
                return;
            }

            //Name generation for output file
            string CurDate = DateTime.Now.ToString("yyMMdd");
            ReportPath = ReportPath + ((char)92).ToString() + CurDate + "_" + CheckPathName + ".txt";

            //Refresh existing file
            if (File.Exists(ReportPath))
            {
                List<string> EmptyList = new List<string>();
                File.WriteAllLines(ReportPath, EmptyList);
            }

            List<string> ForbiddenList = new List<string>()
            {
                "!", "£", "$", "%", "(", ")", "^", "&", "{", "}", "[", "]", "+",
                "-", "=", "@", "’", "~", "¬", "`", "‘", "/", "?", ":", "*", "<", ">"," ",
                ((char)34).ToString(), ((char)92).ToString(), ((char)124).ToString(),
            };

            CheckPathSearch(CheckPathName, CheckPath, ForbiddenList, ReportPath);
            CheckSubPathSearch(CheckPath, ForbiddenList, ReportPath);

            //No issues has been found
            FileInfo ReportPathInf = new FileInfo(ReportPath);
            if (File.Exists(ReportPath) == false| (File.Exists(ReportPath) == true && ReportPathInf.Length == 0))
            {
                StreamWriter(ReportPath, "Check complete!\nNo issues found in this folder!");
            }
           
            Process.Start("notepad.exe", ReportPath);
        }

        static void CheckPathSearch(string CheckPathName, string CheckPath, List<string> ForbiddenList, string ReportPath)
        {
            bool nCnt = false;

            //Check the main folder
            foreach (string item in ForbiddenList)
            {
                if (CheckPathName.Contains(item))
                {
                    if (nCnt == false)
                    {
                        StreamWriter(ReportPath, "Folder name: '" + CheckPathName + "'" + "\n");
                        StreamWriter(ReportPath, "in the direcory: " + Path.GetDirectoryName(CheckPath) + "\n");
                        StreamWriter(ReportPath, "contains forbidden symbols: ");
                    }
                    if (item != " ")
                        StreamWriter(ReportPath, " " + item);
                    else
                        StreamWriter(ReportPath, " 'space'");
                    nCnt = true;
                }
            }

            if (nCnt == true)
            {
                StreamWriterLine(ReportPath);
                StreamWriterLine(ReportPath);
            }

            CheckFiles(CheckPath, ForbiddenList, ReportPath);

        }

        static void CheckSubPathSearch(string CheckPath, List<string> ForbiddenList, string ReportPath)
        {
            //res2 = false;
            bool nCnt;
            string dirName;

            try
            {
                //Check folders
                foreach (string d in Directory.GetDirectories(CheckPath))
                {
                    dirName = new DirectoryInfo(d).Name;
                    nCnt = false;


                    foreach (string item in ForbiddenList)
                    {

                        if (dirName.Contains(item) == true)
                        {
                            if (nCnt == false)
                            {
                               
                                StreamWriter(ReportPath, "Folder name: '" + dirName + "'" + "\n");
                                StreamWriter(ReportPath, "in the direcory: " + Path.GetDirectoryName(d) + "\n");
                                StreamWriter(ReportPath, "contains forbidden symbols: ");
                            }

                            if (item != " ")
                            {
                                StreamWriter(ReportPath, " " + item);
                            }
                            else
                            {
                                StreamWriter(ReportPath, " 'space'");
                            }

                            nCnt = true;
                        }
                    }

                    if (nCnt == true)
                    {
                        StreamWriterLine(ReportPath);
                        StreamWriterLine(ReportPath);
                    }

                    CheckFiles(d, ForbiddenList, ReportPath);

                    CheckSubPathSearch(d, ForbiddenList, ReportPath);

                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        static void StreamWriter(string writePath, string str)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.UTF8))
                {
                    sw.Write(str);
                }
            }
            catch (Exception)
            { }
        }

        static void StreamWriterLine(string writePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine();
                }
            }
            catch (Exception)
            { }
        }

        static void CheckFiles(string d, List<string> ForbiddenList, string ReportPath)
        {
            //Check files in folder

            foreach (string f in Directory.GetFiles(d))
            {
                string dirName = new DirectoryInfo(f).Name;
                bool nCnt = false;

                foreach (string item in ForbiddenList)
                {

                    if (dirName.Contains(item) == true)
                    {
                        if (nCnt == false)
                        {
                            StreamWriter(ReportPath, "File name: '" + dirName + "'" + "\n");
                            StreamWriter(ReportPath, "in the direcory: " + Path.GetDirectoryName(f) + "\n");
                            StreamWriter(ReportPath, "contains forbidden symbols: ");
                        }

                        if (item != " ")
                            StreamWriter(ReportPath, " " + item);
                        else
                            StreamWriter(ReportPath, " 'space'");

                        nCnt = true;
                    }
                }
                if (nCnt == true)
                {
                    StreamWriterLine(ReportPath);
                    StreamWriterLine(ReportPath);
                }

            }
        }
    }

}
