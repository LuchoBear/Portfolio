using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NotificationCenter
{
    class ToDoList
    {
        private static List<ToDoItem> toDoList = new List<ToDoItem>();
        private static List<ToDoItem> filteredToDoList = new List<ToDoItem>();
        private static ToDoItem selectedItem;

        public static void UpdateListView()
        {
            var form = Form1.Self;
            form.listView1.Clear();

            string status;

            filteredToDoList = GetFilteredToDoList();

            foreach (ToDoItem toDoItem in filteredToDoList)
            {
                if(toDoItem.Complete == true)
                {
                    status = "Complete";
                }
                else
                {
                    status = "Incomplete";
                }

                string date;
                if(toDoItem.Date == DateTime.MinValue.Date)
                {
                    date = "";
                }
                else
                {
                    date = toDoItem.Date.ToShortDateString();
                }

                ListViewItem newItem = new ListViewItem(new string[] { toDoItem.ItemName, toDoItem.Description, date, status });

                if(toDoItem.Date !=  DateTime.MinValue)
                {
                    newItem.ToolTipText = "Name: " + toDoItem.ItemName + "\nDescription: " + toDoItem.Description + "\nDate: " + toDoItem.Date + "\nStatus: " + status;
                }
                else
                {
                    newItem.ToolTipText = "Name: " + toDoItem.ItemName + "\nDescription: " + toDoItem.Description + "\nDate: N/A" + "\nStatus: " + status;
                }

                toDoItem.GroupList = "test";

                //bool groupExists = false;

                //foreach(ListViewGroup group in form.listView1.Groups)
                //{
                //    if(group.Header == toDoItem.GroupList)
                //    {
                //        group.Items.Add(newItem);
                //        groupExists = true;
                //    }
                //}

                //if(groupExists == false)
                //{
                //    ListViewGroup newGroup = new ListViewGroup(toDoItem.GroupList);
                //    form.listView1.Groups.Add(newGroup);
                //    newGroup.Items.Add(newItem);
                //}

                form.listView1.Items.Add(newItem);
            }

            //adds the columns in the list
            form.listView1.Columns.Add("Task:", 100);
            form.listView1.Columns.Add("Description:", 400);
            form.listView1.Columns.Add("Date:", 100);
            form.listView1.Columns.Add("Status:", 100);

            NotificationManager.ResizeMessageColumn();
        }

        //Sorts the list by the selected filter option
        public static List<ToDoItem> GetFilteredToDoList()
        {
            var form = Form1.Self;

            List<ToDoItem> newToDoList = new List<ToDoItem>();
            string selectedFilter = form.comboBox1.SelectedItem.ToString();

            switch (selectedFilter)
            {
                case "Filter By: All":
                    newToDoList = toDoList;
                    break;

                case "Filter By: Today":
                    foreach (ToDoItem toDoItem in toDoList)
                    {
                        if (toDoItem.Date == DateTime.Today.Date)
                        {
                            newToDoList.Add(toDoItem);
                        }
                    }
                    break;

                case "Filter By: Date":
                    foreach (ToDoItem toDoItem in toDoList)
                    {
                        bool skip = false;
                        if (toDoItem.Date != DateTime.MinValue.Date)
                        {
                            if(newToDoList.Count != 0)
                            {
                                for(int i = 0; i < newToDoList.Count; i++)
                                {
                                    if(toDoItem.Date < newToDoList[i].Date)
                                    {
                                        newToDoList.Insert(i, toDoItem);
                                        skip = true;
                                        break;
                                    }
                                }
                                if (skip == false)
                                {
                                    newToDoList.Add(toDoItem);
                                }
                            }
                            else
                            {
                                newToDoList.Add(toDoItem);
                            }
                        }
                    }
                    break;

                case "Filter By: Incomplete":
                    foreach (ToDoItem toDoItem in toDoList)
                    {
                        if (toDoItem.Complete == false)
                        {
                            newToDoList.Add(toDoItem);
                        }
                    }
                    break;

                case "Filter By: Complete":
                    foreach (ToDoItem toDoItem in toDoList)
                    {
                        if (toDoItem.Complete == true)
                        {
                            newToDoList.Add(toDoItem);
                        }
                    }
                    break;

                default:
                    newToDoList = toDoList;
                    break;
            }

            return newToDoList;
        }

        public static void CreateNewToDoItem(string itemName, string description, bool complete, DateTime date, bool createNotification)
        {
            if (createNotification == true)
            {
                Notification toDoNotification = new Notification(itemName, description, date, new List<string>(), false);
                NotificationManager.Notifications.Add(toDoNotification);
                toDoList.Add(new ToDoItem(itemName, description, complete, date, toDoNotification));
            }
            else
            {
                toDoList.Add(new ToDoItem(itemName, description, complete, date));
            }
        }

        public static void RemoveToDoItem(ToDoItem toDoItem)
        {
            toDoList.Remove(toDoItem);
            UpdateListView();
        }

        public static void ReplaceToDoItem(ToDoItem replaceToDoItem, string itemName, string description, bool complete, DateTime date)
        {
            int index;
            index = toDoList.IndexOf(replaceToDoItem);

            toDoList.RemoveAt(index);
            toDoList.Insert(index, new ToDoItem(itemName, description, complete, date));

            UpdateListView();
        }

        public static void ClearToDoList()
        {
            toDoList.Clear();
            UpdateListView();
        }

        //Returns an item at the given index in the list
        public static ToDoItem FindToDoItemByIndex(int index)
        {
            return toDoList[index];
        }

        public static void SaveToDoItem()
        {
            WriteToBinaryFile("toDoList.bin", toDoList);
            Console.WriteLine("Saving ToDoList");
        }

        public static void LoadToDoItem()
        {
            toDoList = ReadFromBinaryFile<List<ToDoItem>>("toDoList.bin");
            Console.WriteLine("Loading ToDoList");

            if(toDoList == null)
            {
                toDoList = new List<ToDoItem>();
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return (T)default;
            }
        }

        public static ToDoItem SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }
    }
}
