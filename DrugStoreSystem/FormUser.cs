using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DrugStoreSystem
{
    public partial class FormUser : Form
    
    {

        private readonly DbAccess _connection;
        private readonly DbAccess _Open;
        public FormUser()
        {
            InitializeComponent();
            _connection = new DbAccess();
            _connection.Connection();
            _connection.Open();
        }
        public void LoadData()
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
    }
    
}
