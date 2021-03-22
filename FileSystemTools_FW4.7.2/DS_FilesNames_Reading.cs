using System;
using System.IO;

namespace ReadFileNames
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo d = new DirectoryInfo(@Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.*"); //Getting files names
            
                string str = "";
                string FileNameOut = "DS_Names.txt";

                foreach (FileInfo file in Files)
                {
                    if (file.Extension != ".exe" && file.Name != FileNameOut)
                    {
                        str = str + file.Name + "\n";
                        Console.WriteLine(file.Name);
                        string writePath = d + "/" + FileNameOut;

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
                Console.WriteLine("\n Names of files are recorded to " + FileNameOut);

            Console.ReadLine();
        }
    }
}