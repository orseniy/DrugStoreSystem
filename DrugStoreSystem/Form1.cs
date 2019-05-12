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

    public partial class Form1 : Form
    {
        private const string access = @"admin";
        private const string connaccess = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Арсений\source\repos\DrugStoreSystem\DrugStoreSystem\DrugStoreSystem.mdf;Integrated Security=Trueystem\DrugStoreSystem\DrugStoreSystem.mdf;Integrated Security=True";


        public Form1()
        {
            InitializeComponent();
        }




        private void button1_Click(object sender, EventArgs e)
        {
            string querylog = "Select * From Users Where Login = '" + textBoxLogin.Text + "' and Password = '" + textBoxPass.Text + "' ; ";
            try
            {
                using (SqlConnection sqlconn = new SqlConnection(connaccess))
                {
                    sqlconn.Open();
                    SqlCommand comm = new SqlCommand(querylog, sqlconn);
                    SqlDataReader reader = comm.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
                        if (reader.GetValue(3).ToString() == "admin")
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
                        {
                            MessageBox.Show("OK");
                            FormAdmin objFormAdmin = new FormAdmin();
                            this.Hide();
                            objFormAdmin.Show();
                        }

                        {
                            FormUser obj = new FormUser();
                            Hide();
                            obj.Show();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Неправильні логін та пароль");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка");
            }
            finally
            {

            }

        }

    }
}