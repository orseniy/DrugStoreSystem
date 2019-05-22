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
        private SqlConnection connection;
        private List<Drug> drugs = new List<Drug>();
        public ConnectionState ConnectionState => connection.State;

        public List<Drug> Drugs => drugs;

        public DbAccess()
        {
            string adress = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|DrugStoreSystem.mdf;Integrated Security=True";
            connection = new SqlConnection(adress);
        }
        public void OpenConnection()
        {
            if (ConnectionState == ConnectionState.Closed)
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
            drugs.Clear();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Drugs]", connection);

            try
            {
                dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    drugs.Add(new Drug
                    {
                        Id = Convert.ToInt32(dataReader["ID"]),
                        Name = Convert.ToString(dataReader["Name"]),
                        Price = Convert.ToDecimal(dataReader["Price"]),
                        Amount = Convert.ToInt32(dataReader["Amount"]),
                        Code = Convert.ToString(dataReader["Code"]),
                        Manufacturer = Convert.ToString(dataReader["Manufacturer"]),
                        ATX = Convert.ToString(dataReader["ATX"]),
                        PharmGroup = Convert.ToString(dataReader["PharmGroup"]),
                        Form = Convert.ToString(dataReader["Form"]),
                        Storehouse = Convert.ToString(dataReader["Storehouse"]),
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
                CloseConnection();
            }
        }

        public Drug GetById(int id)
        {
            OpenConnection();
            drugs.Clear();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Drugs] where id=@id", connection))
            {
                cmd.Parameters.AddWithValue("@id",id);
                try
                {
                    dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        Drug res = new Drug
                        {
                            Id = Convert.ToInt32(dataReader["ID"]),
                            Name = Convert.ToString(dataReader["Name"]),
                            Price = Convert.ToDecimal(dataReader["Price"]),
                            Amount = Convert.ToInt32(dataReader["Amount"]),
                            Code = Convert.ToString(dataReader["Code"]),
                            Manufacturer = Convert.ToString(dataReader["Manufacturer"]),
                            ATX = Convert.ToString(dataReader["ATX"]),
                            PharmGroup = Convert.ToString(dataReader["PharmGroup"]),
                            Form = Convert.ToString(dataReader["Form"]),
                            Storehouse = Convert.ToString(dataReader["Storehouse"]),
                        };
                        return res;
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
                    CloseConnection();
                }
            }
            //Not found
            return null;

        }


        /// <summary>
        /// Create new record
        /// </summary>
        /// <param name="item"></param>
        /// <returns>new record with generated id</returns>
        public Drug Insert(Drug item)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Drugs(Name,Price,Amount,Code,Manufacturer,[ATX],[PharmGroup],Form,[Storehouse]) output INSERTED.ID VALUES(@Name,@Price,@Amount,@Code,@Manufacturer,@ATX,@PharmGroup,@Form,@Storehouse)", connection))
            {
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@Amount", item.Amount);
                cmd.Parameters.AddWithValue("@Code", item.Code);
                cmd.Parameters.AddWithValue("@Manufacturer", item.Manufacturer);
                cmd.Parameters.AddWithValue("@ATX", item.ATX);
                cmd.Parameters.AddWithValue("@PharmGroup", item.PharmGroup);
                cmd.Parameters.AddWithValue("@Form", item.Form);
                cmd.Parameters.AddWithValue("@Storehouse", item.Storehouse);
                OpenConnection();

                int modified = (int)cmd.ExecuteScalar();
                item.Id = modified;
                CloseConnection();
                DataLoad();

                return item;
            }
        }

        /// <summary>
        /// Updates the existing item in the DB
        /// </summary>
        /// <param name="item">item to update</param>
        public void Update(Drug item)
        {
            using (SqlCommand cmd = new SqlCommand("Update Drugs set Name=@Name,Price=@Price,Amount=@Amount,Code=@Code,Manufacturer=@Manufacturer,[ATX]=@ATX,[PharmGroup]=@PharmGroup,Form=@Form,[Storehouse]=@Storehouse where id=@id", connection))
            {
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Amount", item.Amount);
                cmd.Parameters.AddWithValue("@Price", item.Price);
                cmd.Parameters.AddWithValue("@Code", item.Code);
                cmd.Parameters.AddWithValue("@Manufacturer", item.Manufacturer);
                cmd.Parameters.AddWithValue("@ATX", item.ATX);
                cmd.Parameters.AddWithValue("@PharmGroup", item.PharmGroup);
                cmd.Parameters.AddWithValue("@Form", item.Form);
                cmd.Parameters.AddWithValue("@Storehouse", item.Storehouse);
                cmd.Parameters.AddWithValue("@id", item.Id);
                OpenConnection();

                cmd.ExecuteScalar();
                CloseConnection();
                DataLoad();

            }

        }

        public void Delete(int id)
        {
            using (SqlCommand cmd = new SqlCommand("delete from Drugs where Id=@Id", connection))
            {
                cmd.Parameters.AddWithValue("@id", id);
                OpenConnection();

                cmd.ExecuteScalar();
                CloseConnection();
                DataLoad();

            }
        }

        public void Filter(string text)
        {
            drugs.Clear();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Drugs] where [Name] Like @filter OR [Code] Like @filter OR [Manufacturer] Like @filter", connection))
            {
                cmd.Parameters.AddWithValue("@filter", $"%{text}%");
                OpenConnection();
                try
                {
                    dataReader = cmd.ExecuteReader();
                    drugs.Clear();
                    while (dataReader.Read())
                    {
                        drugs.Add(new Drug
                        {
                            Id = Convert.ToInt32(dataReader["ID"]),
                            Name = Convert.ToString(dataReader["Name"]),
                            Price = Convert.ToDecimal(dataReader["Price"]),
                            Amount = Convert.ToInt32(dataReader["Amount"]),
                            Code = Convert.ToString(dataReader["Code"]),
                            Manufacturer = Convert.ToString(dataReader["Manufacturer"]),
                            ATX = Convert.ToString(dataReader["ATX"]),
                            PharmGroup = Convert.ToString(dataReader["PharmGroup"]),
                            Form = Convert.ToString(dataReader["Form"]),
                            Storehouse = Convert.ToString(dataReader["Storehouse"]),
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
                    CloseConnection();
                }
            }
        }
    }

}
