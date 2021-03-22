using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DS.ChangesSearch.MongoDB;
using DS_Space;


//Mongo
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChangesSearch_DB
{
    public partial class DB_Choose : Form
    {
        public string DB_CurrentName { get; set; }

        public static MongoClient client;

        public static string connectionString;



        public DB_Choose(MongoClient cl, string Constring)
        {
            InitializeComponent();
            client = cl;
            connectionString = Constring;
            GetMongoDB(client);
        }


        private void GetMongoDB(MongoClient client)
        {

            string[] DB_Array = MongoDBData.GetDatabaseNames(client).ToArray();

            comboBox_DBList.Items.AddRange(DB_Array);
            comboBox_DBList.Text = DB_Array[0];
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            MongoDBData.DB_Name = comboBox_DBList.SelectedItem.ToString();

            this.Hide();

            int index = connectionString.IndexOf("/", 10);
            if (index > 0)
                connectionString = connectionString.Substring(0, index + 1) + MongoDBData.DB_Name;

            client = new MongoClient(connectionString);
            DB_Start dB_Start = new DB_Start(client);
            dB_Start.ShowDialog();

        }

        private void comboBox_DBList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DB_Choose_Load(object sender, EventArgs e)
        {

        }
    }
}
