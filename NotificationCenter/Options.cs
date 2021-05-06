using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;

namespace NotificationCenter
{
    class Options
    {
        public static string fileLocation;
        public static bool openOnStartup = true;
        public static bool runInBackground = true;

        public static void SetBackgroundService()
        {

        }

        //Creates the windows service that allows the program to run in the background
        public static void CreateService()
        {

        }

        //Removes the windows service that allows the program to run in the background
        public static void DeleteService()
        {

        }

        public static void SetStartupKey()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (openOnStartup == true && rk.GetValue("NotificationCenter") == null)
            {
                rk.SetValue("NotificationCenter", Application.ExecutablePath);
            }
            else if(openOnStartup == false)
            {
                rk.DeleteValue("NotificationCenter", false);
            }

            rk.Close();
        }

        //Saves the settings in a file
        public static void SaveSettings()
        {

        }

        //Gets the settings from the save file
        public static void GetSettings()
        {

        }
    }
}
