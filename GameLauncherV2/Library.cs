using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace GameLauncherV2
{
    class Library 
    {

        private static List<Exe> libraryExes = new List<Exe>();
        private static List<Exe> sortedExes = new List<Exe>();
        private static List<Image> iconList = new List<Image>();

        //public static void LibraryThread()
        //{
        //    Populate(Exe.GetExes());
        //}

        public static void Populate(Exe[] exes)
        {
            List<Exe> tempList = new List<Exe>();
            Console.WriteLine("Populating");
            //Console.WriteLine(exes.Length);
            libraryExes.Clear();
            for (int i = 0; i < exes.Length; i++)
            {
                exes[i].GetInfo();
                if (exes[i].gameName == null)
                {
                    continue;
                }
                try
                {
                    libraryExes.Add(exes[i]);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            sortedExes.AddRange(libraryExes);
            //Library.Sort.ByIcon();
            //Library.Sort.BySize();
        }

        public static void DisplayExes()
        {
            var form = Form1.Self;

            iconList = GetIcons(sortedExes);
            form.ExeIconList.Images.Clear();
            form.listView1.Items.Clear();
            form.ExeIconList.Images.AddRange(iconList.ToArray());

            for (int j = 0; j < iconList.Count; j++)
            {
                if (sortedExes[j].removed == false) // <--- FIXME: When scanning a second time an OutOfRangeException is thrown
                {
                    form.listView1.Items.Add(sortedExes[j].drive + " - " + sortedExes[j].gameName + " - " + sortedExes[j].exeName, j);
                }
            }

        }

        public static List<Exe> GetExes()
        {
            return sortedExes;
        }

        public static List<Image> GetIcons(List<Exe> exes)
        {
            
            iconList.Clear();
            for (int i = 0; i < exes.ToArray().Length; i++)
            {
                
                iconList.Add(exes[i].icon);
            }

            return iconList;
        }

        public static class Sort
        {
            const int FILE_SORT_SIZE = 500000; 
            public static void BySize()
            {
                FileInfo fileInfo;
                //List<Exe> tempList = new List<Exe>();
                //List<Exe> newList = new List<Exe>();
                foreach (Exe exe in libraryExes)
                {
                    fileInfo = new FileInfo(exe.filePath);
                    if(fileInfo.Length < FILE_SORT_SIZE)
                    {
                        //tempList.Add(exe);
                        exe.removed = true;
                    }
                }
                //libraryExes = newList;
            }

            public static void ByLauncher(string launcher)
            {
                sortedExes.Clear();

                foreach(Exe exe in libraryExes)
                {
                    //Skip loop if you are sorting by all
                    if(launcher == "All")
                    {
                        sortedExes.AddRange(libraryExes);
                        break;
                    }

                    Console.WriteLine(exe.launcher);

                    if(exe.launcher == launcher)
                    {
                        sortedExes.Add(exe);
                    }
                }

                Console.WriteLine(sortedExes.Count);
                DisplayExes();
            }

            public static void ByIcon()
            {
                Console.WriteLine("Sorting");

                string[] defaultIconFiles = { "vrmonitor.exe", "OriginLegacyCLI.exe", "vrcompositor.exe"};
                List<Image> defaultIcons = new List<Image>();

                foreach (Exe exe in libraryExes)
                {
                    //Console.WriteLine(exe.name + ": " + exe.filePath);
                    for (int i = 0; i < defaultIconFiles.Length; i++)
                    {
                        if (exe.filePath.Contains(defaultIconFiles[i]))
                        {
                            defaultIcons.Add(exe.icon);
                        }
                    }
                }

                foreach (Exe exe in libraryExes)
                {
                    for (int i = 0; i < defaultIcons.ToArray().Length; i++)
                    {
                        if(IconCompare(exe.icon, defaultIcons[i]) == true)
                        {
                            exe.removed = true;
                        }
                    }
                }
            }

            public static List<Exe> ByDrive()
            {
                var form = Form.ActiveForm as Form1;
                List<Exe> returnExes = new List<Exe>();
                String drive = form.comboBox1.SelectedItem.ToString();

                if (drive == "All")
                {
                    foreach(Exe exe in libraryExes)
                    {
                        if(exe.removed == false)
                        {
                            returnExes.Add(exe);
                        }
                    }
                }
                else
                {
                    foreach(Exe exe in libraryExes)
                    {
                        if(exe.filePath.Contains(drive) && exe.removed == false)
                        {
                            returnExes.Add(exe);
                        }
                    }
                }

                return returnExes;
            }
        }

        private static bool IconCompare(Image firstIcon, Image secondIcon)
        {
            MemoryStream iconStream = new MemoryStream();
            firstIcon.Save(iconStream, System.Drawing.Imaging.ImageFormat.Bmp);
            String firstIconString = Convert.ToBase64String(iconStream.ToArray());
            iconStream.Position = 0;

            secondIcon.Save(iconStream, System.Drawing.Imaging.ImageFormat.Bmp);
            String secondIconString = Convert.ToBase64String(iconStream.ToArray());

            if(firstIconString == secondIconString)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    public static class Category
        {
            public static void AssignCategory(Exe exe)
            {

            }
            public static void UnassignCategory(Exe exe)
            {

            }
            public static void CreateCategory(string name)
            {

            }

            public static void RemoveCategory()
            {

            }
        }
    }
}
