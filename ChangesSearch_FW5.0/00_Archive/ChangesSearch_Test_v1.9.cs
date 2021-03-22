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


/*
Данная программа выполняет 
*/

namespace DS_ConsoleApp_ReportStatusExport
{

    class ReportStatusExport


    {
        //Get current date and time 
        public static string CurDate = DateTime.Now.ToString("yyMMdd");
        public static string CurDateTime = DateTime.Now.ToString("yyMMdd_HHmmss");

        private static string SourseFolderPath;

        static DS_Output dS_Output = new DS_Output();


        public static int FilesCount { get; set; }


        [STAThread]
        public static void Main()

        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new DB_Choose());

            MongoClient client = new MongoClient();

            DB_Choose dB_Choose = new DB_Choose(client);
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
            dS_Output.DS_WritePath = SourseFolderPath + "\\" + "Log_ReportStatusExport_" + CurDateTime + ".txt";

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

            //Files renaming
            string[,] ExcelArray = new string[1000, 15];
            int j = 0;

            DB_Write mongoDB_Test = new DB_Write(client);
            //mongoDB_Test.DBNamesOutput();


            //Iterating through lines in txt file
            CheckFiles(ref j, SourseFolderPath, DaysNumber, ref ExcelArray, mongoDB_Test, client);
            CheckDir(ref j, SourseFolderPath, DaysNumber, ref ExcelArray, mongoDB_Test, client);

            mongoDB_Test.SortPeople(client);

            MessageBox.Show("Done!");
            Application.Exit();
        }

        public static void CheckDir(ref int j, string CheckPath, int DaysNumber, ref string[,] ExcelArray, DB_Write mongoDB_Test, MongoClient client)
        {
            try
            {
                //Check folders
                foreach (string d in Directory.GetDirectories(CheckPath))
                {

                    CheckFiles(ref j, d, DaysNumber, ref ExcelArray, mongoDB_Test, client);
                    CheckDir(ref j, d, DaysNumber, ref ExcelArray, mongoDB_Test, client);

                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
        public static void CheckFiles(ref int j, string d, int DaysNumber, ref string[,] ExcelArray, DB_Write mongoDB_Test, MongoClient client)
        {
            //Check files in folder
            // Make a reference to info of a directory.  
            DirectoryInfo di = new DirectoryInfo(d);

            // Get a reference to each file in that directory.

            FileInfo[] fiArr = di.GetFiles();


            foreach (FileInfo f in fiArr)
            {
                List<string> field = new List<string>();

                if (f.Extension == ".rvt" && f.LastWriteTime > DateTime.Now.AddDays(-DaysNumber))
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


                    if (mongoDB_Test.ColName == null)
                    {
                        mongoDB_Test.ColName = DateTime.Now.ToString("yyMMdd") + "_" + field[8] + "_" + field[10];
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

    public class MongoDB_Data

    {
        //Set DB name
        public static string DB_Name;

        public static MongoClient client;

        public MongoDB_Data(MongoClient cl)
        {
            client = cl;
        }


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

    class DB_Write
    {

        public string ColName { get; set; }

        public static MongoClient client;

        public DB_Write(MongoClient cl)
        {
            client = cl;
        }

        public BsonDocument InsertOneDoc(List<string> list)
        {
            //Get collection
            IMongoCollection<BsonDocument> collection = MongoDB_Data.GetDatabase(client).GetCollection<BsonDocument>(ColName);

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
            using var collCursor = MongoDB_Data.GetDatabase(client).ListCollectionNames();
            List<string> colls = collCursor.ToList();

            foreach (string col in colls)
            {
                if (col == ColName)
                {
                    string Mestext = "Default collection name '" + col + "' alredy exists. Do you like to replace it ?";

                    DialogResult dialogResult = MessageBox.Show(Mestext, "Warning message", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MongoDB_Data.GetDatabase(client).DropCollection(ColName);
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
            //Get collection
            IMongoCollection<BsonDocument> collection = MongoDB_Data.GetDatabase(client).GetCollection<BsonDocument>(ColName);

            collection.Find(new BsonDocument()).Sort("{Объект:1}").ToListAsync();
        }
    }

    class DB_Unload
    {
        public static string ColNameUnload;

        public static MongoClient client;

        public DB_Unload(MongoClient cl)
        {
            client = cl;
        }

        public static void Unload()
        {

            DS_Form newForm = new DS_Form
            {
                TopLevel = true
            };
            newForm.Show();

            string FolderPathOutput = newForm.DS_OpenFolderDialogForm("Chose folder for checking").ToString();
            if (FolderPathOutput == "")
            {
                return;
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage();

            Excel_Program excel_Program = new Excel_Program(package, client);

            excel_Program.ExcelOutput();
            excel_Program.ExcelSave(FolderPathOutput);

            //DB_Reading();
            Application.Exit();
        }

        public void DB_Reading()
        {
            /*
            List<string> DocsList = FindDocs(client);

            string delimiter = "\n";
            string StringOutput = DocsList.Aggregate((i, j) => i + delimiter + j);
            MessageBox.Show(StringOutput);
            */
        }



        public List<string> FindDocs(ExcelWorksheet newWorksheet)
        {
            var database = MongoDB_Data.GetDatabase(client);
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

    class Excel_Program

    {

        public static ExcelPackage package;


        public static MongoClient client;

        public Excel_Program(ExcelPackage pack, MongoClient cl)
        {
            package = pack;
            client = cl;
        }

        public Excel_Program(ExcelPackage pack)
        {
            package = pack;
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
            ExcelWorksheet newWorksheet = sheets.Add(DB_Unload.ColNameUnload);

            AllCellsToText(newWorksheet);


            DB_Unload dB_Unload = new DB_Unload(client);

            dB_Unload.FindDocs(newWorksheet);


            newWorksheet.Cells["A:L"].Sort(4);
            newWorksheet.Cells["A:L"].Sort(5);
            newWorksheet.Cells["A:L"].Sort(2);
            newWorksheet.Cells["A:L"].Sort(7);
            newWorksheet.Cells["A:L"].Sort(3);
            newWorksheet.Cells["A:L"].Sort(8);

            newWorksheet.InsertRow(1, 1);

            ExcelFileHeadings(newWorksheet);
            ExcelSheetsFormat(newWorksheet);

        }


        public void ExcelSheetsFormat(ExcelWorksheet newWorksheet)
        {

            //Excel sheet formating
            newWorksheet.Row(1).Style.Font.Bold = true;

            //Columns AutoFit
            newWorksheet.Cells.AutoFitColumns();

        }

        public void ExcelSave(string FolderPathOutput)
        {

            string ExcelOutputFileName = "Bim-проекты_" + DB_Unload.ColNameUnload + "_изменения" + ".xlsx";

            string ExcelOutputDir = FolderPathOutput + ((char)92).ToString() + ExcelOutputFileName;

            FileInfo fileInfo = new FileInfo(ExcelOutputDir);

            package.SaveAs(fileInfo);

            try
            {
                package.SaveAs(fileInfo);

            }

            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }

        }

        public void AllCellsToText(ExcelWorksheet newWorksheet)
        {
            var range = newWorksheet.Cells;
            range.Style.Numberformat.Format = "@";
        }



    }


}






