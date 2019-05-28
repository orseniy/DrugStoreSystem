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

    public partial class LoginForm : Form
    {
        private const string access = @"admin";
        private const string connaccess = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|DrugStoreSystem.mdf;Integrated Security=True";


        public LoginForm()
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
                        MessageBox.Show(reader.GetValue(3).ToString());

                        if (reader.GetValue(3).ToString() == "admin")
                        {
                            MessageBox.Show("OK");
                            /*FormAdmin objFormAdmin = new FormAdmin();
                            this.Hide();
                            objFormAdmin.Show();*/
                            FormUser btnhide = new FormUser();
                            Hide();
                            btnhide.button5.Visible = true;
                            btnhide.button6.Visible = true;
                            btnhide.Show();
                            MessageBox.Show("OK2");
                        }
                        else
                        {
                            FormUser obj = new FormUser();
                            Hide();
                            obj.Show();
                        }

                    }
                    else
                    {
                        MessageBox.Show(this, "Неправильні логін та пароль");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Помилка");
            }
            finally
            {

            }

        }

    }
}