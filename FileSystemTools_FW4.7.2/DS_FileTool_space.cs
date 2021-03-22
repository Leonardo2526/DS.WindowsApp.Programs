using DS_Space;
using System;
using System.IO;

namespace DS_FileTool_space
{
    class DS_FileCopyToFolder
    {
        [STAThread]
        public static void Main1()

        {
            DS_Form newForm = new DS_Form();
            string SourceFilePath = newForm.DS_OpenFileDialogForm("Chose the base file:").ToString();
            string ext = new DirectoryInfo(SourceFilePath).Extension;

            string FolderPath = newForm.DS_OpenFolderDialogForm("", "Chose directory with folders:").ToString();
            string SubFolderPath = newForm.DS_OpenFolderDialogForm(FolderPath, "Chose a type of folder for the base file:").ToString();
            SubFolderPath = SubFolderPath.Replace(FolderPath, "");

            string[] DirPathes = Directory.GetDirectories(FolderPath);
            string dirName;

            //Subfolder path Getting
            foreach (string dirPath in DirPathes)
            {
                dirName = new DirectoryInfo(dirPath).Name;

                SubFolderPath = SubFolderPath.Replace(dirName, "");
                SubFolderPath = SubFolderPath.TrimStart((char)92);
            }

            //File copying
            foreach (string dirPath in DirPathes)
            {
                dirName = new DirectoryInfo(dirPath).Name;

                File.Copy(SourceFilePath, dirPath + @"\" + SubFolderPath + @"\" + dirName + ext, true);
            }

            Console.WriteLine("Done!" + "\n" + "File " + "\n" + "'" + SourceFilePath + "'" +
                "\n" + "has been copied to each folder: " + "\n" + "'" + SubFolderPath + "'" +
                "\n" + " in the directory: " + "\n" + "'" + FolderPath + "'");
            Console.ReadLine();
        }

    }

    class DS_FolderGenerator
    {
        [STAThread]
        public static void Main()
        {
            DS_Form newForm = new DS_Form();
            string SourcePath = newForm.DS_OpenFolderDialogForm("", "Chose source folder:").ToString();
            string BasedirName = new DirectoryInfo(SourcePath).Name;
            string TopFolderPath = SourcePath.Replace(BasedirName, "");

            string TextFilePath = newForm.DS_OpenFileDialogForm("Chose the txt file with list of names:").ToString();
            string[] lines = File.ReadAllLines(TextFilePath);

            //Copying and renaming directories
            try
            {
                foreach (string line in lines)
                {
                    if (line != BasedirName)
                    {
                        string DestinationPath = TopFolderPath + line;
                        DirCopy(SourcePath, DestinationPath);
                        DirRename(DestinationPath, line, BasedirName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
            }

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        static void DirSearch(string sDir, string newName)
        {
            string dirName;
            string BasedirName = new DirectoryInfo(sDir).Name;
            BasedirName.Replace(BasedirName, newName);

            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    dirName = new DirectoryInfo(d).Name;

                    if (dirName == BasedirName)
                    {
                        Directory.Move(d, newName);
                        dirName.Replace(dirName, newName);
                    }

                    foreach (string f in Directory.GetFiles(d))
                    {
                        dirName = new DirectoryInfo(f).Name;
                        if (dirName == BasedirName)
                        {
                            File.Move(f, newName);
                        }
                        Console.WriteLine(dirName);
                    }
                    DirSearch(d, newName);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        static void DirRename(string DestinationPath, string newName, string BasedirName)
        {
            string dirName;
            string newPath;

            try
            {
                foreach (string d in Directory.GetDirectories(DestinationPath))
                {
                    //All folders with BasedirName renaming
                    dirName = new DirectoryInfo(d).Name;
                    if (dirName == BasedirName)
                    {
                        newPath = d.Replace(BasedirName, newName);
                        Directory.Move(d, newPath);
                    }

                    //All files with BasedirName renaming
                    foreach (string f in Directory.GetFiles(d))
                    {
                        dirName = Path.GetFileNameWithoutExtension(f);
                        if (dirName == BasedirName)
                        {
                            newPath = f.Replace(BasedirName, newName);
                            File.Move(f, newPath);
                        }
                    }
                    DirRename(d, newName, BasedirName);
                }
            }
            catch (System.Exception excpt)
            {
                //Console.WriteLine(excpt.Message);
            }
        }

        static void DirCopy(string SourcePath, string DestinationPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
            SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
        }

    }
}
