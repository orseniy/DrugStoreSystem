using DrugStoreSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrugStoreSystem
{
    public partial class DrugForm : Form
    {
        private readonly DbAccess _connection;
        private Drug _item;
        public DrugForm(Drug item = null)
        {
            InitializeComponent();
            _connection = new DbAccess();
            _item = item;
            if (_item != null)
            {
                nameTextBox.Text=_item.Name ;
                priceTextBox.Text=_item.Price.ToString();
                amountUpDown.Value=_item.Amount;
                codeTextBox.Text=_item.Code  ;
                formTextBox.Text=_item.Form ;
                manufacturerTextBox.Text=_item.Manufacturer ;
                atxTextBox.Text=_item.ATX ;
                pharmGroupTextBox.Text=_item.PharmGroup ;
                storehouseTextBox.Text=_item.Storehouse ;

            }

        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            //TODO Need to validate input data
            if (_item == null)
            {
                Drug newItem = new Drug
                {
                    Name = nameTextBox.Text,
                    Amount = Convert.ToInt32(amountUpDown.Value),
                    Price = Convert.ToDecimal(priceTextBox.Text),
                    Code = codeTextBox.Text,
                    Form = formTextBox.Text,
                    Manufacturer = manufacturerTextBox.Text,
                    ATX = atxTextBox.Text,
                    PharmGroup = pharmGroupTextBox.Text,
                    Storehouse = storehouseTextBox.Text
                };
                Drug insertedItem = _connection.Insert(newItem);
            }
            else
            {
                _item.Name = nameTextBox.Text;
                _item.Price = Convert.ToDecimal(priceTextBox.Text);
                _item.Amount = Convert.ToInt32(amountUpDown.Value);
                _item.Code = codeTextBox.Text;
                _item.Form = formTextBox.Text;
                _item.Manufacturer = manufacturerTextBox.Text;
                _item.ATX = atxTextBox.Text;
                _item.PharmGroup = pharmGroupTextBox.Text;
                _item.Storehouse = storehouseTextBox.Text;
                _connection.Update(_item);
            }
            this.Close();
        }
    }
}
