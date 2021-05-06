using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationCenter
{
    [Serializable]
    public class ToDoItem
    {
        private string itemName;
        private string description;
        private bool complete;
        private DateTime date;
        private string groupList;
        private Notification notification;

        public ToDoItem(string itemName, string description, bool complete, DateTime date, Notification notification)
        {
            this.itemName = itemName;
            this.description = description;
            this.complete = complete;
            this.date = date;
            this.notification = notification;
        }

        public ToDoItem(string itemName, string description, bool complete, DateTime date)
        {
            this.itemName = itemName;
            this.description = description;
            this.complete = complete;
            this.date = date;
        }

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public bool Complete
        {
            get { return complete; }
            set { complete = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string GroupList
        {
            get { return groupList; }
            set { groupList = value; }
        }

        public Notification Notification
        {
            get { return notification; }
        }
    }
}
