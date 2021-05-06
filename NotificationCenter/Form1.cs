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
    public partial class Form1 : Form
    {
        private static Notification selectedNotification;
        private static ToDoItem selectedItem;
        private static bool showingNotifications = true;

        public static Form1 Self;
        Timer t = new Timer();

        public Form1()
        {
            InitializeComponent();
            Self = this;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new FormClosingEventHandler(this.Form1_Closing);
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            this.listView1.View = View.Details;
            this.listView1.DoubleBuffering(true);

            this.listView1.Columns.Add("Name:", 100);
            this.listView1.Columns.Add("Message:", 350);
            this.listView1.Columns.Add("Alert Time:", 100);
            this.listView1.Columns.Add("Recurring Days", 170);
            this.listView1.Columns.Add("Hourly", 50);

            this.listView1.FullRowSelect = true;

            this.listView1.BeginUpdate();
            foreach (Notification notification in NotificationManager.GetNotifications())
            {
                ListViewItem newItem = new ListViewItem(new string[2] { notification.Name, notification.Message });
                this.listView1.Items.Add(newItem);
            }
            this.listView1.EndUpdate();
        }

        public Notification SelectedNotification
        {
            get { return selectedNotification; }
        }

        private void LoadNotificationList()
        {
            this.button1.Text = "Create New Notification";
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.AddRange(new string[] { "Filter By: All", "Filter By: Today", "Filter By: Date", "Filter By: Upcoming", "Filter By: Hourly" });
            this.comboBox1.SelectedIndex = 0;
            NotificationManager.UpdateNotificationViewer();
            showingNotifications = true;
        }

        private void LoadToDoList()
        {
            this.button1.Text = "Create New To-Do List Item";
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.AddRange(new string[] { "Filter By: All", "Filter By: Today", "Filter By: Date", "Filter By: Incomplete", "Filter By: Complete" });
            this.comboBox1.SelectedIndex = 0;
            ToDoList.UpdateListView();
            showingNotifications = false;
        }

        //Opens dialog to create new notification or ToDoTask
        private void Button1_Click(object sender, EventArgs e)
        {
            if (showingNotifications == true)
            {
                noitificationDialog dialog1 = new noitificationDialog();
                dialog1.label1.Text = "Create Notification";
                dialog1.button1.Text = "Create";
                dialog1.ShowDialog();
            }
            else
            {
                toDoListDialog dialog1 = new toDoListDialog();
                dialog1.label1.Text = "Create Task";
                dialog1.button1.Text = "Create";
                dialog1.ShowDialog();
            }
        }

        //Opens options dialog
        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsDialog optionsDialog = new OptionsDialog();
            optionsDialog.Show();
        }

        //Creates a timer on the form loading
        private void Form1_Load(object sender, EventArgs e)
        {
            NotificationManager.LoadNotifications();
            LoadNotificationList();
            ToDoList.LoadToDoItem();

            t.Interval = 10;
            t.Tick += new System.EventHandler(this.t_tick);
            t.Start();
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            NotificationManager.SaveNotifications();
            ToDoList.SaveToDoItem();
        }

        //Every tick of the timer updates the time on the form
        private void t_tick(object sender, EventArgs e)
        {
            var time = System.DateTime.Now.ToShortTimeString();

            this.timeLabel.Text = time.ToString();

            NotificationManager.CheckTimes();
        }

        noitificationDialog dialog2 = new noitificationDialog();

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialog2.editing = true;
            dialog2.label1.Text = "Edit Notification";
            dialog2.button1.Text = "Edit";
            dialog2.SetSelectedNotification();
            dialog2.DialogLoadInfo();
            dialog2.ShowDialog();
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotificationManager.RemoveNotification(selectedNotification);
        }

        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                var hit = listView1.HitTest(e.X, e.Y);

                if (hit.Item != null || hit.SubItem != null)
                {
                    if (showingNotifications == true)
                    {
                        selectedNotification = NotificationManager.FindNotificationByIndex(hit.Item.Index);
                        NotificationManager.SelectedNotification = selectedNotification;
                        contextMenuStrip1.Show(Cursor.Position);
                    }
                    else
                    {
                        selectedItem = ToDoList.FindToDoItemByIndex(hit.Item.Index);
                        ToDoList.SelectedItem = selectedItem;
                        if(selectedItem.Complete == true)
                        {
                            contextMenuStrip2.Items[0].Text = "Mark Incomplete";
                        }
                        else
                        {
                            contextMenuStrip2.Items[0].Text = "Mark Complete";
                        }

                        contextMenuStrip2.Show(Cursor.Position);
                    }
                }
            }
        }

        private void NotificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadNotificationList();
        }

        private void ToDoListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadToDoList();
        }

        private void ListView1_Resize(object sender, EventArgs e)
        {
            NotificationManager.ResizeMessageColumn();
        }

        toDoListDialog dialog3 = new toDoListDialog();

        private void EditToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dialog3.editing = true;
            dialog3.label1.Text = "Edit Task";
            dialog3.button1.Text = "Edit";
            dialog3.SetSelectedNotification();
            dialog3.DialogLoadInfo();
            dialog3.ShowDialog();
        }

        private void RemoveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToDoList.RemoveToDoItem(selectedItem);
        }

        private void MarkCompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(selectedItem.Complete == true)
            {
                selectedItem.Complete = false;
            }
            else
            {
                selectedItem.Complete = true;
            }

            ToDoList.UpdateListView();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(showingNotifications == true)
            {
                NotificationManager.UpdateNotificationViewer();
            }
            else
            {
                ToDoList.UpdateListView();
            }
        }

        private void SendTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedNotification.Display();
        }

        private void ListView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = this.listView1.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }
    }
}
