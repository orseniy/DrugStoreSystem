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
        //private readonly DbAccess _Open;
        public FormUser()
        {
            InitializeComponent();
            _connection = new DbAccess();
            //_connection.Connection();
            _connection.OpenConnection();
        }
        public void LoadData()
        {
            _connection.DataLoad();
            dataGridView1.DataSource = _connection.Drugs;


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
            if (_connection != null && _connection.ConnectionState != ConnectionState.Closed)
            {
                _connection.CloseConnection();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void FormUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
    
}
