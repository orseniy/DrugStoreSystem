using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DrugStoreSystem
{
    public class DbAccess

    {
        


        public DbAccess()
        {
        }
        SqlDataReader dataReader;
        SqlCommand sqlCommand;
        SqlConnection connection;
        private object listBox1 = null;
        public void Open()
        {
            connection.Open();
        }

        public void Connection()

        {
            SqlConnection connection;
            string adress = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Арсений\source\repos\DrugStoreSystem\DrugStoreSystem\DrugStoreSystem.mdf;Integrated Security=Trueystem\DrugStoreSystem\DrugStoreSystem.mdf;Integrated Security=True";
            connection = new SqlConnection(adress);
            connection.Open();


        }

        

        public void DataLoad()
        {
            dataReader = null;
            Connection();
            sqlCommand = new SqlCommand("SELECT * FROM [Drugs]", connection);

            try
            {
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    listBox1.Items.Add(Convert.ToString(dataReader["ID"]) + "   " + Convert.ToString(dataReader["Назва"]) + "   " + Convert.ToString(dataReader["Кількість"]) + "   " + Convert.ToString(dataReader["Штрихкод"]) + "   " + Convert.ToString(dataReader["Виробник"]) + "   " + Convert.ToString(dataReader["АТХ група"]) + "   " + Convert.ToString(dataReader["Фарм. Група"]) + "   " + Convert.ToString(dataReader["Форма"]) + "   " + Convert.ToString(dataReader["Місце зберігання"]));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dataReader != null)
                    dataReader.Close();
            }
        }
        ///<summary>

        ///</summary>
        /// <returns> LOGS </returns>
       // public List <DbItem> GetAll()
        

        
    }

}
