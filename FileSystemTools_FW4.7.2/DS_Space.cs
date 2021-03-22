using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Security;
using System.Security.AccessControl;
using System.IO;

namespace DS_Space
{
   
        public class DS_Form : Form
        {
            private Button selectButton;
            private OpenFileDialog openFileDialog1;

            public string DS_OpenFileDialogForm_txt()
            {
                openFileDialog1 = new OpenFileDialog()
                {
                    FileName = "Select a text file",
                    Filter = "Text files (*.txt)|*.txt",
                    Title = "Open text file"
                };

                selectButton = new Button()
                {
                    Size = new Size(100, 20),
                    Location = new Point(15, 15),
                    Text = "Select file"
                };

                string filePath = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    SelectButton_Click(ref filePath);
                }
                return filePath;
            }

        public string DS_OpenFileDialogForm(string filename = "Select a file")
        {
                openFileDialog1 = new OpenFileDialog()
                {
                    FileName = filename,
                    Title = "Open file",
                    
                };

        selectButton = new Button()
            {
                Size = new Size(100, 20),
                Location = new Point(15, 15),
                Text = "Select file"
            };

            string filePath = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SelectButton_Click(ref filePath);
            }
            return filePath;
        }
        private string SelectButton_Click(ref string filePath)
            {
                try
                {
                    filePath = openFileDialog1.FileName.ToString();
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
                return filePath;
            }


        public string DS_OpenFolderDialogForm(string folderPath = "", string description = "Chose folder")
        {
            FolderBrowserDialog fbd;

            if (folderPath != "")
            {
                fbd = new FolderBrowserDialog
                {
                    Description = description,
                    SelectedPath = folderPath
                };
            }
            else
            {
                fbd = new FolderBrowserDialog
                {
                    Description = description
                };
            }

            // Show testDialog as a modal dialog
            DialogResult result = fbd.ShowDialog();
            string sfp = fbd.SelectedPath;

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                if (DS_HasWritePermissionOnDir(sfp) == true)
                {
                    return sfp;
                }
                else
                {
                    MessageBox.Show("Error access to path!");
                    return "";
                }
            }
            return "";
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
    
}
