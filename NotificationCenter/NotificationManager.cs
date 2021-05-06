using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NotificationCenter
{
    class NotificationManager
    {
        private static List<Notification> notifications = new List<Notification>();
        private static List<Notification> filteredNotifications = new List<Notification>();
        private static Notification selectedNotification;

        public static void UpdateNotificationViewer()
        {
            var form = Form1.Self;
            form.listView1.Clear();
            form.listView1.Groups.Clear();

            filteredNotifications = GetFilteredNotifications();

            foreach (Notification notification in filteredNotifications)
            {
                string days = "";
                string hourly;

                //Sets the days that will show up in the recurring days column
                if (notification.Recurring.Count != 7 && notification.Recurring.Count > 0)
                {
                    foreach (string day in notification.Recurring)
                    {
                        days += day.Remove(3) + ", ";
                    }

                    days = days.Remove(days.Length - 2);
                }
                else if(notification.Recurring.Count == 7)
                {
                    days = "Everyday";
                }
                else
                {
                    days = "";
                }
                
                //Sets what the hourly column will say
                if(notification.Hourly == true)
                {
                    hourly = "Yes";
                }
                else
                {
                    hourly = "No";
                }

                form.listView1.Items.Add(new ListViewItem(new string[] { notification.Name, notification.Message, notification.AlarmTime.ToString("MM/dd/yyyy hh:mm tt"), days, hourly}));
            }

            //adds the columns in the list
            form.listView1.Columns.Add("Name:", 100);
            form.listView1.Columns.Add("Message:", 340);
            form.listView1.Columns.Add("Alert Time:", 120);
            form.listView1.Columns.Add("Recurring Days", 155);
            form.listView1.Columns.Add("Hourly", 50);

            ResizeMessageColumn();
        }

        //Sorts the list by the selected filter option
        public static List<Notification> GetFilteredNotifications()
        {
            var form = Form1.Self;

            List<Notification> newNotifications = new List<Notification>();
            string selectedFilter = form.comboBox1.SelectedItem.ToString();

            switch(selectedFilter)
            {
                case "Filter By: All":
                    newNotifications = notifications;
                    break;

                case "Filter By: Today":
                    foreach(Notification notification in notifications)
                    {
                        if(notification.AlarmTime.Date == DateTime.Today.Date)
                        {
                            newNotifications.Add(notification);
                        }
                    }
                    break;

                case "Filter By: Date":
                    break;

                case "Filter By: Upcoming":
                    break;

                case "Filter By: Hourly":
                    foreach (Notification notification in notifications)
                    {
                        if (notification.Hourly == true)
                        {
                            newNotifications.Add(notification);
                        }
                    }
                    break;

                default:
                    newNotifications = notifications;
                    break;
            }

            return newNotifications;
        }

        //Resizes the description column so that the columns fill the list view based on the set widths of the other columns
        public static void ResizeMessageColumn()
        {
            var form = Form1.Self;
            int listWidth = form.listView1.Width;
            int columnTotal = 0;

            for (int i = 0; i < form.listView1.Columns.Count; i++)
            {
                if (i != 1)
                {
                    columnTotal += form.listView1.Columns[i].Width;
                }
            }

            try
            {
                form.listView1.Columns[1].Width = listWidth - columnTotal - 4 - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            }
            catch(ArgumentOutOfRangeException)
            {

            }
        }

        //Checks all notifications in the list to see if it is time to display one
        public static void CheckTimes()
        {
            //Console.WriteLine("Checking Times");

            for (int i = 0; i < notifications.Count; i++)
            {
                if (notifications[i].AlarmTime <= System.DateTime.Now && notifications[i].Sent == false)
                {
                    //Checks if it is a day that the recurring notification needs to be sent on
                    if(notifications[i].Recurring.Count != 0 && notifications[i].LastSent.Date != DateTime.Today)
                    {
                        if(notifications[i].Recurring.Contains(DateTime.Today.DayOfWeek.ToString()))
                        {
                            notifications[i].Display();
                        }
                    }
                    //Checks if it has been an hour since the last hourly notification has been sent
                    else if(notifications[i].Hourly == true && notifications[i].LastSent.AddHours(-1) < DateTime.Now)
                    {
                        notifications[i].Display();
                    }
                    else
                    {
                        notifications[i].Display();
                    }
                }
            }

            //Removes all notifications that have been sent and dont need to be sent again (recurring or hourly)
            for (int i = 0; i < notifications.Count; i++)
            {
                if (notifications[i].Sent == true && notifications[i].Recurring.Count == 0 && notifications[i].Hourly == false)
                {
                    RemoveNotification(notifications[i]);
                }
            }
        }

        //Creates new notifcation and adds it to the list
        public static void CreateNewNotification(string name, string message, DateTime alarmTime, List<string> recurring, bool hourly)
        {
            notifications.Add(new Notification(name, message, alarmTime, recurring, hourly));
        }

        //Removes a notification from the list
        public static void RemoveNotification(Notification notification)
        {
            notifications.Remove(notification);
            UpdateNotificationViewer();
        }

        //Allows editing of a notification by removing the old one and putting a new one in its place
        public static void ReplaceNotification(Notification replaceNotification, string name, string message, DateTime alarmTime, List<string> recurring, bool hourly)
        {
            int index;
            index = notifications.IndexOf(replaceNotification);

            notifications.RemoveAt(index);
            notifications.Insert(index, new Notification(name, message, alarmTime, recurring, hourly));

            UpdateNotificationViewer();
        }

        //Clears the list of notifications (Shouldnt be used outside of testing)
        public static void ClearNotifications()
        {
            notifications.Clear();
            UpdateNotificationViewer();
        }

        //Returns a notification at the given index in the list
        public static Notification FindNotificationByIndex(int index)
        {
            return notifications[index];
        }

        //Returns the list of notifications
        public static List<Notification> GetNotifications()
        {
            return notifications;
        }

        public static void SaveNotifications()
        {
            WriteToBinaryFile("notifications.bin", notifications);
            Console.WriteLine("Saving Notifications");
        }

        public static void LoadNotifications()
        {
            notifications = ReadFromBinaryFile<List<Notification>>("notifications.bin");
            Console.WriteLine("Loading Notifications");

            if(notifications == null)
            {
                notifications = new List<Notification>();
            }
        }

        private static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        private static T ReadFromBinaryFile<T>(string filePath)
        {
            try
            {
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(stream);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return (T)default;
            }
        }

        public static List<Notification> Notifications
        {
            get { return notifications; }
        }

        public static Notification SelectedNotification
        {
            get { return selectedNotification; }
            set { selectedNotification = value; }
        }
    }
}
