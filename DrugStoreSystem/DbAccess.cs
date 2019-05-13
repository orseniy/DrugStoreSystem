using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using DrugStoreSystem.Models;

namespace DrugStoreSystem
{
    public class DbAccess

    {
        private SqlDataReader dataReader;
        private SqlCommand sqlCommand;
        private SqlConnection connection;
        private List<Drug> drugs = new List<Drug>();
        public ConnectionState ConnectionState => connection.State;

        public List<Drug> Drugs  => drugs;

        public DbAccess()
        {
            string adress = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|DrugStoreSystem.mdf;Integrated Security=True";
            connection = new SqlConnection(adress);
       }
        public void OpenConnection()
        {
            if(ConnectionState == ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (ConnectionState != ConnectionState.Closed)
                connection.Close();
        }


        public void DataLoad()
        {
            dataReader = null;
            OpenConnection();
            sqlCommand = new SqlCommand("SELECT * FROM [Drugs]", connection);

            try
            {
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    drugs.Add(new Drug
                    {
                        Id = Convert.ToInt32(dataReader["ID"]),
                        Name = Convert.ToString(dataReader["Назва"]),
                        Amount = Convert.ToInt32(dataReader["Кількість"]),
                        Code = Convert.ToString(dataReader["Штрихкод"]),
                        Manufacturer = Convert.ToString(dataReader["Виробник"]),
                        ATX = Convert.ToString(dataReader["АТХ група"]),
                        PharmGroup = Convert.ToString(dataReader["Фарм. Група"]),
                        Form = Convert.ToString(dataReader["Форма"]),
                        Storehouse =Convert.ToString(dataReader["Місце зберігання"]),
                    });
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
