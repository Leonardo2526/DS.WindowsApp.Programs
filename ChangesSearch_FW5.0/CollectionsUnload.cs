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
    public partial class CollectionsUnload : Form
    {
        public MongoClient client;

        public CollectionsUnload(MongoClient cl)
        {
            InitializeComponent();

            client = cl;

            GetListCollectionsNames();

        }


        public void GetListCollectionsNames()
        //Get all collections names from all DBs
        {
            List<string> DB_collections;

            //Set database
            using var collCursor = MongoDBData.GetDatabase(client).ListCollectionNames();

            DB_collections = collCursor.ToList();
            string[] ColNames_Array = DB_collections.ToArray();

            comboBox_Collections.Items.AddRange(ColNames_Array);
            comboBox_Collections.Text = ColNames_Array[0];
        }



        private void comboBox_Collections_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Apply_Click(object sender, EventArgs e)
        {
            Unloading.ColNameUnload = comboBox_Collections.SelectedItem.ToString();
            Unloading.client = client;
            Unloading.ExportToExcel();

            this.Hide();
        }

        private void CollectionsUnload_Load(object sender, EventArgs e)
        {

        }
    }
}
