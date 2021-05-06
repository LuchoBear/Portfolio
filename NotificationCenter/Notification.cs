using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NotificationCenter
{
    [Serializable]
    public class Notification
    {
        private string name;
        private string message;
        private DateTime alarmTime;
        private List<string> recurring;
        private bool hourly;
        private bool sent = false;
        private DateTime lastSent = DateTime.MinValue.AddHours(2);
        private ToDoItem toDoItem;

        public Notification(string name, string message, DateTime alarmTime, List<string> recurring, bool hourly, ToDoItem toDoItem)
        {
            this.name = name;
            this.message = message;
            this.alarmTime = alarmTime;
            this.recurring = recurring;
            this.hourly = hourly;
            this.toDoItem = toDoItem;
        }

        public Notification(string name, string message, DateTime alarmTime, List<string> recurring, bool hourly)
        {
            this.name = name;
            this.message = message;
            this.alarmTime = alarmTime;
            this.recurring = recurring;
            this.hourly = hourly;
        }

        public void Display()
        {
            SentNotificationPanel sentNotificationDialog = new SentNotificationPanel();
            var form = Form1.Self;

            this.sent = true;
            this.lastSent = DateTime.Now;
            sentNotificationDialog.label1.Text = name;
            sentNotificationDialog.textBox1.Text = message;
            sentNotificationDialog.label2.Text = alarmTime.ToString("hh: mm tt");
            //sentNotificationDialog.Location = new Point(Screen.PrimaryScreen.Bounds.X - form.Bounds.X, Screen.PrimaryScreen.Bounds.Y - form.Bounds.Y);
            sentNotificationDialog.Show();
            Console.WriteLine("Notification Sent");
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public DateTime AlarmTime
        {
            get { return alarmTime; }
            set { alarmTime = value; }
        }

        public List<string> Recurring
        {
            get { return recurring; }
            set { recurring = value; }
        }

        public bool Hourly
        {
            get { return hourly; }
            set { hourly = value; }
        }

        public bool Sent
        {
            get { return sent; }
        }

        public DateTime LastSent
        {
            get { return lastSent; }
        }

        public ToDoItem ToDoItem
        {
            get { return toDoItem; }
            set { toDoItem = value; }
        }
    }
}
