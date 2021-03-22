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


namespace ChangesSearch_DB
{
    public partial class Form_NewColName : Form
    {

        public Form_NewColName()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

      

        private void Apply_Click(object sender, EventArgs e)
        {
            if (textBox_NewColName.Text.Length !=0)
            {
                this.Close();
            }
           
        }

        public void textBox_NewColName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
