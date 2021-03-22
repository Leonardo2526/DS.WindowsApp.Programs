using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangesSearch_DB
{
    public partial class ConnectionString : Form
    {
        public string DB_String { get; set; }

        public ConnectionString()
        {
            InitializeComponent();
        }

        private void ConnectionString_Load(object sender, EventArgs e)
        {

        }

        private void Apply_Click(object sender, EventArgs e)
        {
            DB_String = textBox_ConnectionString.Text;
            this.Hide();
        }

        private void textBox_ConnectionString_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
