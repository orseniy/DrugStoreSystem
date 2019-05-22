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
using DrugStoreSystem.Models;

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
            drugsGridView.AutoGenerateColumns = false;
            drugsGridView.DataSource = null;
            drugsGridView.DataSource = _connection.Drugs;
            drugsGridView.Update();
            drugsGridView.Refresh();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = "";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void FormUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


        private void FormUser_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (drugsGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = drugsGridView.SelectedRows[0];
                Drug selectedItem = (Drug)selectedRow.DataBoundItem;
                DrugForm InsertForm = new DrugForm(selectedItem);
                InsertForm.ShowDialog(this);
                LoadData();

            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            DrugForm InsertForm = new DrugForm();
            InsertForm.ShowDialog(this);
            LoadData();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (drugsGridView.SelectedRows.Count > 0)
            {
                if (MessageBox.Show(this, "Видалити рядок?", "Видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataGridViewRow selectedRow = drugsGridView.SelectedRows[0];
                    Drug selectedItem = (Drug)selectedRow.DataBoundItem;
                    if (selectedItem != null)
                    {
                        _connection.Delete(selectedItem.Id);
                        LoadData();
                    }
                }

            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            _connection.Filter(searchTextBox.Text);
            drugsGridView.DataSource = null;
            drugsGridView.DataSource = _connection.Drugs;
        }
    }
}
