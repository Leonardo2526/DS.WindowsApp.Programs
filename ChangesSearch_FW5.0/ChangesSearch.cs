//User
using ChangesSearch_DB;
using DS_Space;
//Mongo
using MongoDB.Bson;
using MongoDB.Driver;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;


/*123
Данная программа выполняет dgdf
*/

namespace DS.ChangesSearch.MongoDB
{

    class MainProgram
    {
        //Get current date and time 
        public static string CurDate = DateTime.Now.ToString("yyMMdd");
        public static string CurDateTime = DateTime.Now.ToString("yyMMdd_HHmmss");

        private static string SourseFolderPath;

        static DS_Output dS_Output = new DS_Output();

        public static string ext { get; set; }

        [STAThread]
        public static void Main()
             
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConnectionString connectionString1 = new ConnectionString();
            connectionString1.ShowDialog();

            string connectionString = connectionString1.DB_String;

            if (connectionString == null)
            {
                return;
            }

            MongoClient client = new MongoClient(connectionString);

            DB_Choose dB_Choose = new DB_Choose(client, connectionString);
            dB_Choose.ShowDialog();

        }

        public void Program(MongoClient client)

        {

            DS_Form newForm = new DS_Form
            {
                TopLevel = true
            };
            newForm.Show();

            SourseFolderPath = newForm.DS_OpenFolderDialogForm("Chose folder for checking").ToString();
            if (SourseFolderPath == "")
            {
                return;

            }

            string DirName = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Desktop");
            dS_Output.DS_WritePath = DirName + "\\" + "Log_ReportStatusExport" + "\\" + CurDateTime + "_" + "Log" + ".txt";
            dS_Output.DS_PathNameCreate(CurDateTime, DirName + "\\" + "Log_ReportStatusExport");

            Form_ReportDays form_ReportDays = new Form_ReportDays
            {
                TopLevel = true
            };

            form_ReportDays.ShowDialog();

            int.TryParse(form_ReportDays.textBox_DaysNum.Text, out int DaysNumber);

            if (DaysNumber == 0)
            {
                Environment.Exit(1);
            }

            Uploading mongoDB_Test = new Uploading(client);


            //Iterating through lines in txt file
            CheckFiles(SourseFolderPath, DaysNumber, mongoDB_Test);
            CheckDir(SourseFolderPath, DaysNumber, mongoDB_Test);

            mongoDB_Test.SortPeople(client);

            MessageBox.Show("Log file has been saved to: " + dS_Output.DS_WritePath);

            UnloadOpt unloadOpt = new UnloadOpt
            {
                TopLevel = true
            };
            unloadOpt.ShowDialog();

            MessageBox.Show("Process complete!");

            Application.Exit();
        }

        public static void CheckDir(string CheckPath, int DaysNumber, Uploading mongoDB_Test)
        {
            try
            {
                //Check folders
                foreach (string d in Directory.GetDirectories(CheckPath))
                {

                    CheckFiles(d, DaysNumber, mongoDB_Test);
                    CheckDir(d, DaysNumber, mongoDB_Test);

                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public static void CheckFiles(string d, int DaysNumber, Uploading mongoDB_Test)
        {
            //Check files in folder
            // Make a reference to info of a directory.  
            DirectoryInfo di = new DirectoryInfo(d);

            // Get a reference to each file in that directory.

            FileInfo[] fiArr = di.GetFiles();

            //Extensions list
            var FileExt = new List<string> { ext };

            List<string> FileFullNames = Directory.EnumerateFiles(d, "*_*_*_*_*_*_*_*", SearchOption.TopDirectoryOnly).
            Where(s => FileExt.Contains(Path.GetExtension(s).TrimStart((char)46).ToLowerInvariant())).ToList();

            foreach (string fn in FileFullNames)
            {
                FileInfo f = new FileInfo(fn);

                List<string> field = new List<string>();

                if (f.LastWriteTime > DateTime.Now.AddDays(-DaysNumber))
                {
                    char[] charsToTrim = { ' ' };

                    string[] SplitString = f.LastWriteTime.ToString().Split(charsToTrim);

                    //LastWriteDate
                    field.Add(SplitString[0]);

                    //LastWriteTime
                    field.Add(SplitString[1]);

                    //Length
                    field.Add(f.Length.ToString());

                    //Directory name
                    field.Add(f.DirectoryName);

                    //File name parsing
                    string line = f.Name;
                    field.AddRange(FieldsGet(line));


                    if (Uploading.ColName == null)
                    {
                        Uploading.ColName = DateTime.Now.ToString("yyMMdd") + "_" + field[8] + "_" + field[10] + "_" + ext + "_" + DaysNumber;
                        mongoDB_Test.CheckCollectionsNames();

                        dS_Output.DS_StreamWriter("\n" + "Files has been found: ");
                    }

                    dS_Output.DS_StreamWriter(f.Name);

                    mongoDB_Test.InsertOneDoc(field);
                }
            }
        }

        public static List<string> FieldsGet(string line)
        {
            int i = 0;
            int indS = 0;
            int[] ind = new int[line.Length];

            List<string> field = new List<string>
            {
                line
            };

            //Through each symbol in file name iterating
            foreach (char sign in line)
            {
                indS += 1;

                if ((sign.ToString() == "_") && (i < 7))
                {
                    i += 1;

                    //Record the index of findings
                    ind[i] = indS;

                    //LIst record of fields
                    field.Add(line.Substring(ind[i - 1], ind[i] - ind[i - 1] - 1));
                }
            }
            return field;
        }

    }

    public class MongoDBData

    {
        //Set DB name
        public static string DB_Name;

        public static MongoClient client;

        public static IMongoDatabase GetDatabase(MongoClient client)
        {
            //Set database
            IMongoDatabase database = client.GetDatabase(DB_Name);
            return database;
        }

        public static List<string> GetDatabaseNames(MongoClient client)
        //Get all databases names from server
        {
            List<string> DBNames = new List<string>();

            using (var cursor = client.ListDatabases())
            {
                var databaseDocuments = cursor.ToList();
                foreach (var databaseDocument in databaseDocuments)
                {
                    DBNames.Add(databaseDocument["name"].AsString);
                }
            }
            return DBNames;
        }
    }

    class Uploading
    {

        public static string ColName { get; set; }

        public static MongoClient client;

        public Uploading(MongoClient cl)
        {
            client = cl;
        }

        public BsonDocument InsertOneDoc(List<string> list)
        {
            //Get collection
            IMongoCollection<BsonDocument> collection = MongoDBData.GetDatabase(client).GetCollection<BsonDocument>(ColName);

            var document = new BsonDocument
                {
                    {"Дата изменения", list[0]},
                    {"Время изменения",list[1]},
                    {"Шифр объекта",list[5]},
                    {"Номер объекта на ситуационном плане",list[6]},
                    {"Исполнитель", list[7]},
                    {"Проект", list[8]},
                    {"Объект", list[9]},
                    {"Стадия", list[10]},
                    {"Раздел", list[11]},
                    {"Имя файла", list[4]},
                    {"Размер файла", list[2]},
                    {"Ссылка на файл", list[3]}
                };

            collection.InsertOne(document);

            return document;
        }

        public void CheckCollectionsNames()
        //Get all collections names from all DBs
        {
            using var collCursor = MongoDBData.GetDatabase(client).ListCollectionNames();
            List<string> colls = collCursor.ToList();

            foreach (string col in colls)
            {
                if (col == ColName)
                {
                    string Mestext = "Default collection name '" + col + "' alredy exists. Do you like to replace it ?";

                    DialogResult dialogResult = MessageBox.Show(Mestext, "Warning message", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MongoDBData.GetDatabase(client).DropCollection(ColName);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        Form_NewColName form_NewColName = new Form_NewColName
                        {
                            TopLevel = true

                        };

                        form_NewColName.ShowDialog();

                        ColName = form_NewColName.textBox_NewColName.Text;

                        if (ColName == "")
                        {
                            Environment.Exit(1);
                        }

                    }

                }

            }

        }

        public void SortPeople(MongoClient client)
        {
            if (ColName != null)
            {
                //Get collection
                IMongoCollection<BsonDocument> collection = MongoDBData.GetDatabase(client).GetCollection<BsonDocument>(ColName);
                collection.Find(new BsonDocument()).Sort("{Объект:1}").ToListAsync();
            }
            else
                MessageBox.Show("Sorry:( \nNo files have been found by your request.");
        }

    }

    class Unloading
    {
        public static string ColNameUnload;

        public static MongoClient client;

        public Unloading(MongoClient cl)
        {
            client = cl;
        }

        public static void ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage();

            ExcelTools excel_Program = new ExcelTools(package, client);

            excel_Program.ExcelOutput();
            excel_Program.ExcelSave();

            Application.Exit();
        }

        public List<string> FindDocs(ExcelWorksheet newWorksheet)
        {
            var database = MongoDBData.GetDatabase(client);
            var collection = database.GetCollection<BsonDocument>(ColNameUnload);
            //var filter = Builders<BsonDocument>.Filter.Eq("Имя файла", "");
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");

            List<string> DocsList = new List<string>();

            var docs = collection.Find(new BsonDocument()).Project(projection).ToList();
            int row = 2;

            foreach (BsonDocument doc in docs)
            {
                int FieldOffset = 1;

                for (int col = 0; col < doc.Count(); col++)
                {


                    newWorksheet.Cells[row, col + FieldOffset].Value = doc[col];

                    newWorksheet.Cells[row, col + FieldOffset].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    newWorksheet.Cells[row, col + FieldOffset].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
                row += 1;
            }
            return DocsList;
        }
    }

    class ExcelTools
    {
        public static ExcelPackage package;

        public static MongoClient client;

        public ExcelTools(ExcelPackage pack, MongoClient cl)
        {
            package = pack;
            client = cl;
        }

        public void ExcelFileHeadings(ExcelWorksheet newWorksheet)
        {

            // Establish column headings in cells.
            List<string> listOfHeadings = new List<string>()
            {
                "Дата изменения",
                "Время изменения",
                "Шифр объекта",
                "Номер объекта на ситуационном плане",
                "Исполнитель",
                "Проект",
                "Объект",
                "Стадия",
                "Раздел",
                "Имя файла",
                "Размер файла",
                "Ссылка на файл",
            };

            int i = 0;
            foreach (string head in listOfHeadings)
            {
                i += 1;
                newWorksheet.Cells[1, i].Value = head;
                newWorksheet.Cells[1, i].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

        }

        public void ExcelOutput()
        {
            var sheets = package.Workbook.Worksheets;

            // Add a new worksheet to the empty workbook
            ExcelWorksheet newWorksheet = sheets.Add(Unloading.ColNameUnload);

            AllCellsToText(newWorksheet);


            Unloading dB_Unload = new Unloading(client);

            dB_Unload.FindDocs(newWorksheet);

            newWorksheet.Cells["A:L"].Sort(new[] { 4, 5, 2, 7, 3, 8 });

            newWorksheet.InsertRow(1, 1);

            ExcelFileHeadings(newWorksheet);
            ExcelSheetsFormat(newWorksheet);
            newWorksheet.Cells[newWorksheet.Dimension.Address].AutoFitColumns();


        }

        public void ExcelSheetsFormat(ExcelWorksheet newWorksheet)
        {

            //Excel sheet formating
            newWorksheet.Row(1).Style.Font.Bold = true;

            //Columns AutoFit
            newWorksheet.Cells[newWorksheet.Dimension.Address].AutoFitColumns();
        }

        public void ExcelSave()
        {
            DS_Form newForm = new DS_Form
            {
                TopLevel = true
            };
            newForm.Show();

            string DestinationPath = newForm.DS_OpenFolderDialogForm("Chose folder for Excel report").ToString();
            if (DestinationPath == "")
            {
                return;
            }

            string ExcelOutputFileName = "Bim-проекты_" + Unloading.ColNameUnload + "_изменения" + ".xlsx";

            string ExcelOutputDir = DestinationPath + ((char)92).ToString() + ExcelOutputFileName;

            FileInfo fileInfo = new FileInfo(ExcelOutputDir);

            try
            {
                package.SaveAs(fileInfo);
                MessageBox.Show("Excel file has been saved to \n" + ExcelOutputDir);
            }

            catch (Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }

        }

        public void AllCellsToText(ExcelWorksheet newWorksheet)
        {
            var range = newWorksheet.Cells;
            range.Style.Numberformat.Format = "@";
        }
    }
}






