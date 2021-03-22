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
    public partial class DB_Start : Form

    {
        public MongoClient client;


        public DB_Start(MongoClient cl)
        {
            InitializeComponent();

            label1.Text = MongoDBData.DB_Name;

            client = cl;

        }

        private void WriteDB_Click(object sender, EventArgs e)
        {

            this.Hide();
            MainProgram reportStatusExport = new MainProgram();
            reportStatusExport.Program(client);

        }

        private void label1_Click(object sender, EventArgs e)
        { 
        }


        private void DB_Start_Load(object sender, EventArgs e)
        {

        }

        private void UnloadDB_Click(object sender, EventArgs e)
        {
            CollectionsUnload collectionsUnload = new CollectionsUnload(client);
            collectionsUnload.ShowDialog();
        }
    }
}
