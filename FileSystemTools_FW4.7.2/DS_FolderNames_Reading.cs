using System;
using System.IO;
using DS_Forms;

namespace ReadFileNames
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            DS_Form newForm = new DS_Form
            {
                TopLevel = true
            };

            string InputFolderPath = newForm.DS_OpenInputFolderDialogForm("Chose folder for checking").ToString();
            if (InputFolderPath == "")
            {
                return;
            }

            string OutputFolderPath = newForm.DS_OpenOutputFolderDialogForm("Chose folder for result").ToString();
            if (OutputFolderPath == "")
            {
                return;
            }

            DirectoryInfo Dir = new DirectoryInfo(InputFolderPath);
            //DirectoryInfo[] Files = d.Get("*.*"); //Getting files names
            
                string str = "";
                string FileNameOut = "DS_Names.txt";
                string writePath = "";

                foreach (DirectoryInfo d in Dir.EnumerateDirectories())
                {
                    if (d.Name != FileNameOut)
                    {
                        str = str + d.Name + "\n";
                        Console.WriteLine(d.Name);
                        writePath = OutputFolderPath + "\\" + FileNameOut;

                        try
                        {
                            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.UTF8))
                            {
                                sw.WriteLine(str + "\n");
                            }
                        }
                        catch (Exception)
                        { }
                    }                   
                }
                if (str == "")
                    Console.WriteLine("No files in the folder.");
                else
                Console.WriteLine("\n Names of files are recorded to " + writePath);

            Console.ReadLine();
        }
    }
}