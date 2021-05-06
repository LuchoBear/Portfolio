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
    public partial class noitificationDialog : Form
    {
        public bool editing = false;
        public Notification selectedNotification;

        public noitificationDialog()
        {
            InitializeComponent();
        }

        private void NoitificationDialog_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "hh:mm tt";
        }

        private void DateTimePicker1_MouseDown(object sender, MouseEventArgs e)
        {
            dateTimePicker1.CustomFormat = "hh:mm tt";
        }

        private void DateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Back)
            {
                dateTimePicker1.CustomFormat = "00:00 tt";
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;
            string message = this.textBox2.Text;
            DateTime alarmTime = this.dateTimePicker1.Value;
            alarmTime = alarmTime.AddSeconds(-alarmTime.Second);
            bool hourly = this.checkBox2.Checked;
            List<string> recurring = new List<string>();

            recurring.Clear();

            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if(this.checkedListBox1.GetItemChecked(i))
                {
                    recurring.Add(this.checkedListBox1.Items[i].ToString());
                }
            }

            if (editing == false)
            {
                NotificationManager.CreateNewNotification(name, message, alarmTime, recurring, hourly);
            }
            else
            {
                NotificationManager.ReplaceNotification(NotificationManager.SelectedNotification, name, message, alarmTime, recurring, hourly);
            }

            NotificationManager.UpdateNotificationViewer();

            this.Close();
        }

        //If the everyday checkbox is checked then it checks all of the others or vice versa
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
                {
                    this.checkedListBox1.SetItemChecked(i, false);
                }
            }
        }
        
        //Sets all of the information back to the default cleared state
        public void DialogClearInfo()
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.dateTimePicker1.Value = System.DateTime.Now;
            this.checkBox2.Checked = false;

            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, false);
            }
        }

        //Loads in all of the info into the editing dialog so you can change what you need with the resst staying the same
        public void DialogLoadInfo()
        {
            this.dateTimePicker1.CustomFormat = "hh:mm tt";

            DialogClearInfo();

            //Auto fill in information
            if (editing == true)
            {
                this.textBox1.Text = selectedNotification.Name;
                this.textBox2.Text = selectedNotification.Message;
                this.dateTimePicker1.Value = selectedNotification.AlarmTime;
                this.checkBox2.Checked = selectedNotification.Hourly;
                
                //Loops through each checkbox and finds out if it should be checked
                for(int i = 0; i < this.checkedListBox1.Items.Count; i++)
                {
                    //If it is everyday then it checks everyday and skips the list as all will be checked already
                    if(selectedNotification.Recurring.Count == 7)
                    {
                        this.checkBox1.Checked = true;
                        break;
                    }

                    if(selectedNotification.Recurring.Contains(this.checkedListBox1.Items[i]))
                    {
                        this.checkedListBox1.SetItemChecked(i, true);
                    }
                    else
                    {
                        this.checkedListBox1.SetItemChecked(i, false);
                    }
                }
            }
        }

        public Notification SelectedNotification
        {
            get { return selectedNotification; }
        }

        public void SetSelectedNotification()
        {
            selectedNotification = NotificationManager.SelectedNotification;
        }
    }
}
