namespace GameLauncherV2
{
    partial class exeSettingsDialog
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
            this.gameNameLabel = new System.Windows.Forms.Label();
            this.filePathLabel = new System.Windows.Forms.Label();
            this.exeNameLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.launcherLabel = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameNameLabel
            // 
            this.gameNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.gameNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.gameNameLabel.Location = new System.Drawing.Point(0, 0);
            this.gameNameLabel.Name = "gameNameLabel";
            this.gameNameLabel.Padding = new System.Windows.Forms.Padding(0, 9, 0, 0);
            this.gameNameLabel.Size = new System.Drawing.Size(484, 60);
            this.gameNameLabel.TabIndex = 0;
            this.gameNameLabel.Text = "GameName";
            this.gameNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // filePathLabel
            // 
            this.filePathLabel.AutoSize = true;
            this.filePathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.filePathLabel.Location = new System.Drawing.Point(34, 66);
            this.filePathLabel.Name = "filePathLabel";
            this.filePathLabel.Size = new System.Drawing.Size(123, 20);
            this.filePathLabel.TabIndex = 1;
            this.filePathLabel.Text = "Game File Path:";
            // 
            // exeNameLabel
            // 
            this.exeNameLabel.AutoSize = true;
            this.exeNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.exeNameLabel.Location = new System.Drawing.Point(34, 116);
            this.exeNameLabel.Name = "exeNameLabel";
            this.exeNameLabel.Size = new System.Drawing.Size(86, 20);
            this.exeNameLabel.TabIndex = 2;
            this.exeNameLabel.Text = "Exe Name:";
            this.exeNameLabel.Click += new System.EventHandler(this.Label3_Click);
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.sizeLabel.Location = new System.Drawing.Point(34, 146);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(106, 20);
            this.sizeLabel.TabIndex = 3;
            this.sizeLabel.Text = "Size on Drive:";
            // 
            // launcherLabel
            // 
            this.launcherLabel.AutoSize = true;
            this.launcherLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.launcherLabel.Location = new System.Drawing.Point(34, 176);
            this.launcherLabel.Name = "launcherLabel";
            this.launcherLabel.Size = new System.Drawing.Size(80, 20);
            this.launcherLabel.TabIndex = 4;
            this.launcherLabel.Text = "Launcher:";
            // 
            // browseButton
            // 
            this.browseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.browseButton.Location = new System.Drawing.Point(184, 256);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(131, 30);
            this.browseButton.TabIndex = 5;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // exeSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.launcherLabel);
            this.Controls.Add(this.sizeLabel);
            this.Controls.Add(this.exeNameLabel);
            this.Controls.Add(this.filePathLabel);
            this.Controls.Add(this.gameNameLabel);
            this.MaximumSize = new System.Drawing.Size(500, 500);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "exeSettingsDialog";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label filePathLabel;
        public System.Windows.Forms.Label exeNameLabel;
        public System.Windows.Forms.Label sizeLabel;
        public System.Windows.Forms.Label launcherLabel;
        public System.Windows.Forms.Button browseButton;
        public System.Windows.Forms.Label gameNameLabel;
    }
}