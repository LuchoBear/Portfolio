using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotificationCenter
{
    public partial class toDoListDialog : Form
    {
        public bool editing = false;
        public ToDoItem selectedToDoItem;

        public toDoListDialog()
        {
            InitializeComponent();
            this.dateTimePicker1.Value = DateTime.Today;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string itemName = textBox1.Text;
            string description = textBox2.Text;
            DateTime date = dateTimePicker1.Value;

            if (editing == false)
            {
                bool createNotification;
                if(this.checkBox1.Checked == true)
                {
                    createNotification = true;
                }
                else
                {
                    createNotification = false;
                }
                ToDoList.CreateNewToDoItem(itemName, description, false, date, createNotification);
            }
            else
            {
                ToDoList.ReplaceToDoItem(selectedToDoItem, itemName, description, selectedToDoItem.Complete, date);
            }

            ToDoList.UpdateListView();

            this.Close();
        }

        public void DialogClearInfo()
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
        }

        public void DialogLoadInfo()
        {
            DialogClearInfo();

            //Auto fill in information
            if (editing == true)
            {
                this.textBox1.Text = selectedToDoItem.ItemName;
                this.textBox2.Text = selectedToDoItem.Description;
            }
        }

        public ToDoItem SelectedToDoItem
        {
            get { return selectedToDoItem; }
        }

        public void SetSelectedNotification()
        {
            selectedToDoItem = ToDoList.SelectedItem;
        }
    }
}
