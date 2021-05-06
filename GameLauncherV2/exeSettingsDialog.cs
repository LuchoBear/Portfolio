using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace GameLauncherV2
{
    public partial class exeSettingsDialog : Form
    {
        public Exe exe;

        public exeSettingsDialog()
        {
            InitializeComponent();
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            Process.Start(@exe.rootFilePath);
        }
    }
}
