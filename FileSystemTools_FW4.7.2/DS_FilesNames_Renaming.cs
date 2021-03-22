using System;
using System.IO;
using System.Collections;

namespace FilesRename
{
    class Program
    {
		static void Main(string[] args)
		{
			//Name of input file
			string textFile = "DS_Names.txt";

			if (File.Exists(textFile))
			{
				DirectoryInfo d = new DirectoryInfo(@Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));//Assuming Test is your Folder
				FileInfo[] Files = d.GetFiles("*.*"); //Getting files
				ArrayList list = new ArrayList();
				int ai = 0;



				NewNamesList(Files, textFile, list);

				//Files renaming
				string[] lines = File.ReadAllLines(textFile);
				try
				{
					for (int i = 0; i <= Files.Length - 1; i++)
					{
						if (Files[i].Name != textFile && Files[i].Extension != ".exe")//Exceptions
						{
							File.Move(d + "/" + Files[i].Name, d + "/" + lines[i - ai]);
						}
						else
							ai += 1;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("An error occured: " + ex.Message);
				}

				//Output for existing files
				Console.WriteLine("Existing files:\n");

				list = new ArrayList();
				NewNamesList(Files, textFile, list);
				foreach (string namelist in list)
				{
					Console.WriteLine(namelist);
				}

				Console.WriteLine("\n in the folder: '" + d + "'\n are renamed to: \n");

				//Output for renames files
				Files = d.GetFiles("*.*"); //Getting files
				list = new ArrayList();
				NewNamesList(Files, textFile, list);

				foreach (string namelist in list)
				{
					Console.WriteLine(namelist);
				}								
			}
			else
				Console.WriteLine("File '" + textFile + "' is absent.");
			Console.ReadKey();
		}

		//New list names writing without exceptions
		private static void NewNamesList(FileInfo[] Files, string textFile, ArrayList list)
		{
			foreach (FileInfo file in Files)
			{
				if (file.Extension != ".exe" && file.Name != textFile)
				{					
					list.Add(file.Name);
				}
			}
		}
	}
}
