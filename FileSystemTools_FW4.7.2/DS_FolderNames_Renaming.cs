using System;
using System.IO;
using System.Collections;
using DS_Forms;

namespace FilesRename
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

			string InputFilePath = newForm.DS_OpenFileDialogForm_txt().ToString();
			if (InputFilePath == "")
			{
				return;
			}

			string InputFolderPath = newForm.DS_OpenInputFolderDialogForm("Chose folder for renaming").ToString();
			if (InputFolderPath == "")
			{
				return;
			}

			//Name of input file
			//string textFile = "DS_Names.txt";

			if (File.Exists(InputFilePath))
			{
				DirectoryInfo Dir = new DirectoryInfo(InputFolderPath);
				DirectoryInfo[] DirArr = Dir.GetDirectories();
				//DirectoryInfo d = new DirectoryInfo(@Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));//Assuming Test is your Folder
				//FileInfo[] Files = d.GetFiles("*.*"); //Getting files
				ArrayList list = new ArrayList();
				int ai = 0;



				NewNamesList(DirArr, InputFilePath, list);

				int directoryCount = DirArr.Length;

				//Dirs renaming
				string[] lines = File.ReadAllLines(InputFilePath);
				try
				{
					for (int i = 0; i <= directoryCount - 1; i++)
					{
						if (DirArr[i].Name != InputFilePath)//Exceptions
						{
							Directory.Move(InputFolderPath + "/" + DirArr[i].Name, InputFolderPath + "/" + lines[i - ai]);
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
				Console.WriteLine("Existing folders:\n");

				list = new ArrayList();
				NewNamesList(DirArr, InputFilePath, list);
				foreach (string namelist in list)
				{
					Console.WriteLine(namelist);
				}

				Console.WriteLine("\n in the folder: '" + InputFolderPath + "'\n are renamed to: \n");

				//Output for renames files
				Dir = new DirectoryInfo(InputFolderPath);
				DirArr = Dir.GetDirectories();
				list = new ArrayList();
				NewNamesList(DirArr, InputFilePath, list);

				foreach (string namelist in list)
				{
					Console.WriteLine(namelist);
				}								
			}
			else
				Console.WriteLine("File '" + InputFilePath + "' is absent.");
			Console.ReadKey();
		}

		//New list names writing without exceptions
		private static void NewNamesList(DirectoryInfo[] DirArr, string textFile, ArrayList list)
		{
			foreach (DirectoryInfo d in DirArr)
			{
				if (d.Name != textFile)
				{					
					list.Add(d.Name);
				}
			}
		}
	}
}
