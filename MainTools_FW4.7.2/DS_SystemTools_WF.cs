using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;


namespace DS_SystemTools
{
    public class DS_Tools
    {
        public string DS_LogOutputPath { get; set; } = @"C:\Users\dnknt\OneDrive\Рабочий стол\Logs\";
        public string DS_LogName { get; set; } = @"Log.txt";

        public string DS_GetFullLogName()
        {
            string DS_FullLogName = DS_LogOutputPath + DS_LogName;
            return DS_FullLogName;
        }

        public void DS_StreamWriter(string OutputTxt, bool Add = true)
        {
            //Directory existing check
            if (Directory.Exists(DS_LogOutputPath) == false)
            {                
                Directory.CreateDirectory(DS_LogOutputPath);
            }

            //Permission check
            if (DS_HasWritePermissionOnDir(DS_LogOutputPath) == false)
            {
                MessageBox.Show("Error access to path!");
                return;
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(DS_LogOutputPath + DS_LogName, Add, Encoding.UTF8))
                {
                    if (OutputTxt != "" && OutputTxt != null)
                        sw.WriteLine(OutputTxt);
                    else
                        sw.WriteLine("No data");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            };
            return;

        }

        public void DS_FileExistMessage()
        {
            if (File.Exists(DS_GetFullLogName()))
            {
                MessageBox.Show("Output file has been saved to: " + DS_GetFullLogName());
            }
        }

        public string DS_PathNameCreate(string CurDateTime, string DirName = @"%USERPROFILE%\Desktop\Logs\", string FileName = "Log", string FileExt = ".txt")
        {
            string ExpDirName = Environment.ExpandEnvironmentVariables(DirName);

            if (Directory.Exists(ExpDirName) == false)
            {
                Directory.CreateDirectory(ExpDirName);
            }

            return Environment.ExpandEnvironmentVariables(DirName + CurDateTime + "_" + FileName + FileExt);
        }

        public string DS_ListToString(List<string> ListInput)
        //Return string from list
        {
            string delimiter = "\n";
            string StringOutput = ListInput.Aggregate((i, j) => i + delimiter + j);
            return StringOutput;
        }

        public bool DS_HasWritePermissionOnDir(string path)
        {
            //Check directory permissions
            var writeAllow = false;
            var writeDeny = false;
            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;

            //Check rules
            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    writeAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    writeDeny = true;
            }
            return writeAllow && !writeDeny;
        }

    }



    public class DS_DirTools

    {
        public List<string> DS_GetFileNamesList(string CheckedDir, string ext1 = "", string ext2 = "")
        //Check top directory for files existance. Without subdirectories.

        {
            List<string> FileFullNames = new List<string>();


            //Check files for extension
            if (ext1 != "" | ext2 != "")
            {
                // Get a reference to each file in that directory.
                FileFullNames = Directory.GetFiles(CheckedDir).ToList();

                //Extensions list
                var FileExt = new List<string> { ext1, ext2 };

                //Array forming from files with "ext" extension only
                FileFullNames = Directory.EnumerateFiles(CheckedDir, "*.*", SearchOption.TopDirectoryOnly).
                    Where(s => FileExt.Contains(Path.GetExtension(s).TrimStart((char)46).ToLowerInvariant())).ToList();

            }

            return FileFullNames;
        }
    }
}
