using DS_SystemTools;
using System.Drawing;
using System.Security;
using System.Windows.Forms;


namespace DS_Forms
{

    public class DS_Form : Form
    {
        private Button selectButton;
        private OpenFileDialog openFileDialog1;

        public string DS_OpenFileDialogForm_txt(string filename = "Select a text file", string title = "Open text file")
        {
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = filename,
                Filter = "Text files (*.txt)|*.txt",
                Title = title
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

        public string DS_OpenInputFolderDialogForm(string description = "Chose folder", string folderPath = "")
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

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return "";
            string sfp = fbd.SelectedPath;

            return sfp;
        }

        public string DS_OpenOutputFolderDialogForm(string description = "Chose folder", string folderPath = "")
        {
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


                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string sfp = fbd.SelectedPath;

                    DS_Tools dS_Tools = new DS_Tools();
                    if (dS_Tools.DS_HasWritePermissionOnDir(sfp) == true)
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

        }

        }

}

