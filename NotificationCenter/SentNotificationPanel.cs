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
    public partial class SentNotificationPanel : Form
    {

        Timer t = new Timer();

        public SentNotificationPanel()
        {
            InitializeComponent();

            var form = Form1.Self;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - this.Size.Width, Screen.PrimaryScreen.WorkingArea.Bottom - this.Size.Height);
            Console.WriteLine(Screen.PrimaryScreen.WorkingArea.Top);
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void SentNotificationPanel_LocationChanged(object sender, EventArgs e)
        {

        }

        private void SentNotificationPanel_Load(object sender, EventArgs e)
        {
            t.Interval = 5000;
            t.Tick += new System.EventHandler(this.t_tick);
            t.Start();
        }

        private void t_tick(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }
    }
}
