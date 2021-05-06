using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace GameLauncherV2
{
    public partial class Form1 : Form
    {

        public static Form1 Self;
        public Form1()
        {
            InitializeComponent();
            Self = this;

            Overlay.ComputerAnalytics.GetComputerAnalytics();
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem8_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void SettingsMenu_Click(object sender, EventArgs e)
        {

        }

        private void ExeScan_Click(object sender, EventArgs e)
        {
            Exe.FindExes();
            Library.Populate(Exe.GetExes());

            listView1.View = View.LargeIcon;
            ExeIconList.ImageSize = new Size(32, 32);
            listView1.LargeImageList = ExeIconList;

            this.comboBox1.Items.Clear();

            this.comboBox1.Items.Add("All");
            this.comboBox1.Items.AddRange(System.Environment.GetLogicalDrives());
            this.comboBox1.SelectedItem = this.comboBox1.FindStringExact("All");
            this.comboBox1.Text = "All";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);

            ExeIconList.Images.Clear();
            listView1.Items.Clear();

            Library.DisplayExes();

            this.Controls.Add(listView1);
            Console.WriteLine(listView1.Items.Count);
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            List<Exe> newExes = new List<Exe>();
            newExes = Library.Sort.ByDrive();

            ExeIconList.Images.Clear();
            listView1.Items.Clear();

            for (int i = 0; i < newExes.Count; i++)
            {
                ExeIconList.Images.Add(newExes[i].icon);
            }

            for (int j = 0; j < ExeIconList.Images.Count; j++)
            {
                listView1.Items.Add(newExes[j].drive + " - " + newExes[j].gameName + " - " + newExes[j].exeName, j);
            }
        }

        private void SteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Library.Sort.ByLauncher("Steam");
        }

        private void EpicGameStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Library.Sort.ByLauncher("Epic Games");
        }

        private void ToolStripMenuItem8_Click_1(object sender, EventArgs e)
        {
            Library.Sort.ByLauncher("GOG Galaxy");
        }

        private void ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Library.Sort.ByLauncher("Uplay");
        }

        private void AllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Library.Sort.ByLauncher("All");
        }

        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            foreach(ListViewItem exe in this.listView1.SelectedItems)
            {
                Process.Start(@Library.GetExes()[exe.Index].filePath);
            }
        }

        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                foreach (ListViewItem exe in this.listView1.SelectedItems)
                {
                    this.exeRightClickMenu.Show(Cursor.Position);
                }
            }
        }

        private void LaunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem exe in this.listView1.SelectedItems)
            {
                Process.Start(@Library.GetExes()[exe.Index].filePath);
            }
        }

        private void LocalFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exe exeInfo = new Exe();
            exeSettingsDialog dialog1 = new exeSettingsDialog();

            foreach (ListViewItem exe in this.listView1.SelectedItems)
            {
                exeInfo = Library.GetExes()[exe.Index];
            }

            exeInfo.fileSize = exeInfo.GetFileSize();

            dialog1.gameNameLabel.Text = exeInfo.gameName;
            dialog1.filePathLabel.Text = "Game File Path:\n" + exeInfo.filePath;
            dialog1.exeNameLabel.Text = "Exe Name: " + exeInfo.exeName.TrimStart('\\');
            dialog1.sizeLabel.Text = "Size on Drive: " + exeInfo.fileSize.ToString();
            dialog1.launcherLabel.Text = "Launcher: " + exeInfo.launcher;

            dialog1.exe = exeInfo;

            dialog1.ShowDialog();
        }
    }
}
