namespace GameLauncherV2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listView1 = new System.Windows.Forms.ListView();
            this.ExeIconList = new System.Windows.Forms.ImageList(this.components);
            this.LibraryViewer = new System.Windows.Forms.ImageList(this.components);
            this.NavigationMenu = new System.Windows.Forms.MenuStrip();
            this.LibraryMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.OverlayMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.LibraryViewSettings = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.steamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.originToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.epicGameStoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showHiddenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.favoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExeScanButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.exeRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NavigationMenu.SuspendLayout();
            this.LibraryViewSettings.SuspendLayout();
            this.exeRightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.HideSelection = false;
            this.listView1.Name = "listView1";
            this.listView1.StateImageList = this.ExeIconList;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.UseWaitCursor = true;
            this.listView1.View = System.Windows.Forms.View.SmallIcon;
            this.listView1.ItemActivate += new System.EventHandler(this.ListView1_ItemActivate);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView1_MouseClick);
            // 
            // ExeIconList
            // 
            this.ExeIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.ExeIconList, "ExeIconList");
            this.ExeIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // LibraryViewer
            // 
            this.LibraryViewer.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.LibraryViewer, "LibraryViewer");
            this.LibraryViewer.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // NavigationMenu
            // 
            resources.ApplyResources(this.NavigationMenu, "NavigationMenu");
            this.NavigationMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LibraryMenu,
            this.OverlayMenu,
            this.SettingsMenu});
            this.NavigationMenu.Name = "NavigationMenu";
            this.NavigationMenu.UseWaitCursor = true;
            // 
            // LibraryMenu
            // 
            this.LibraryMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            resources.ApplyResources(this.LibraryMenu, "LibraryMenu");
            this.LibraryMenu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LibraryMenu.Name = "LibraryMenu";
            // 
            // toolStripMenuItem2
            // 
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.ToolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            // 
            // toolStripMenuItem4
            // 
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            // 
            // OverlayMenu
            // 
            resources.ApplyResources(this.OverlayMenu, "OverlayMenu");
            this.OverlayMenu.Name = "OverlayMenu";
            // 
            // SettingsMenu
            // 
            resources.ApplyResources(this.SettingsMenu, "SettingsMenu");
            this.SettingsMenu.Name = "SettingsMenu";
            this.SettingsMenu.Click += new System.EventHandler(this.SettingsMenu_Click);
            // 
            // LibraryViewSettings
            // 
            resources.ApplyResources(this.LibraryViewSettings, "LibraryViewSettings");
            this.LibraryViewSettings.BackColor = System.Drawing.SystemColors.Control;
            this.LibraryViewSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem9,
            this.favoritesToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.LibraryViewSettings.Name = "LibraryViewSettings";
            this.LibraryViewSettings.UseWaitCursor = true;
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem6,
            this.toolStripMenuItem1,
            this.showHiddenToolStripMenuItem});
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            this.toolStripMenuItem5.Click += new System.EventHandler(this.ToolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.steamToolStripMenuItem,
            this.originToolStripMenuItem,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.epicGameStoreToolStripMenuItem});
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            resources.ApplyResources(this.toolStripMenuItem6, "toolStripMenuItem6");
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            resources.ApplyResources(this.allToolStripMenuItem, "allToolStripMenuItem");
            this.allToolStripMenuItem.Click += new System.EventHandler(this.AllToolStripMenuItem_Click);
            // 
            // steamToolStripMenuItem
            // 
            this.steamToolStripMenuItem.Name = "steamToolStripMenuItem";
            resources.ApplyResources(this.steamToolStripMenuItem, "steamToolStripMenuItem");
            this.steamToolStripMenuItem.Click += new System.EventHandler(this.SteamToolStripMenuItem_Click);
            // 
            // originToolStripMenuItem
            // 
            this.originToolStripMenuItem.Name = "originToolStripMenuItem";
            resources.ApplyResources(this.originToolStripMenuItem, "originToolStripMenuItem");
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
            this.toolStripMenuItem7.Click += new System.EventHandler(this.ToolStripMenuItem7_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
            this.toolStripMenuItem8.Click += new System.EventHandler(this.ToolStripMenuItem8_Click_1);
            // 
            // epicGameStoreToolStripMenuItem
            // 
            this.epicGameStoreToolStripMenuItem.Name = "epicGameStoreToolStripMenuItem";
            resources.ApplyResources(this.epicGameStoreToolStripMenuItem, "epicGameStoreToolStripMenuItem");
            this.epicGameStoreToolStripMenuItem.Click += new System.EventHandler(this.EpicGameStoreToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // showHiddenToolStripMenuItem
            // 
            this.showHiddenToolStripMenuItem.Name = "showHiddenToolStripMenuItem";
            resources.ApplyResources(this.showHiddenToolStripMenuItem, "showHiddenToolStripMenuItem");
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            resources.ApplyResources(this.toolStripMenuItem9, "toolStripMenuItem9");
            // 
            // favoritesToolStripMenuItem
            // 
            this.favoritesToolStripMenuItem.Name = "favoritesToolStripMenuItem";
            resources.ApplyResources(this.favoritesToolStripMenuItem, "favoritesToolStripMenuItem");
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // ExeScanButton
            // 
            resources.ApplyResources(this.ExeScanButton, "ExeScanButton");
            this.ExeScanButton.Name = "ExeScanButton";
            this.ExeScanButton.UseVisualStyleBackColor = true;
            this.ExeScanButton.UseWaitCursor = true;
            this.ExeScanButton.Click += new System.EventHandler(this.ExeScan_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.UseWaitCursor = true;
            // 
            // exeRightClickMenu
            // 
            this.exeRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchToolStripMenuItem,
            this.localFilesToolStripMenuItem});
            this.exeRightClickMenu.Name = "exeRightClickMenu";
            resources.ApplyResources(this.exeRightClickMenu, "exeRightClickMenu");
            // 
            // launchToolStripMenuItem
            // 
            this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            resources.ApplyResources(this.launchToolStripMenuItem, "launchToolStripMenuItem");
            this.launchToolStripMenuItem.Click += new System.EventHandler(this.LaunchToolStripMenuItem_Click);
            // 
            // localFilesToolStripMenuItem
            // 
            this.localFilesToolStripMenuItem.Name = "localFilesToolStripMenuItem";
            resources.ApplyResources(this.localFilesToolStripMenuItem, "localFilesToolStripMenuItem");
            this.localFilesToolStripMenuItem.Click += new System.EventHandler(this.LocalFilesToolStripMenuItem_Click);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.ExeScanButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.NavigationMenu);
            this.Controls.Add(this.LibraryViewSettings);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.MainMenuStrip = this.NavigationMenu;
            this.Name = "Form1";
            this.UseWaitCursor = true;
            this.NavigationMenu.ResumeLayout(false);
            this.NavigationMenu.PerformLayout();
            this.LibraryViewSettings.ResumeLayout(false);
            this.LibraryViewSettings.PerformLayout();
            this.exeRightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList LibraryViewer;
        private System.Windows.Forms.MenuStrip NavigationMenu;
        private System.Windows.Forms.ToolStripMenuItem LibraryMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.MenuStrip LibraryViewSettings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem steamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem originToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem favoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SettingsMenu;
        private System.Windows.Forms.ToolStripMenuItem OverlayMenu;
        private System.Windows.Forms.ToolStripMenuItem epicGameStoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button ExeScanButton;
        private System.Windows.Forms.ToolStripMenuItem showHiddenToolStripMenuItem;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        public System.Windows.Forms.ListView listView1;
        public System.Windows.Forms.ImageList ExeIconList;
        private System.Windows.Forms.ContextMenuStrip exeRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem launchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localFilesToolStripMenuItem;
    }
}

