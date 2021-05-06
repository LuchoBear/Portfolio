using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace GameLauncherV2
{
    public class Exe
    {
        public int id; //List Index
        public string gameName; //Name of Program
        public string exeName; //Actual name of the exe
        public string filePath; //File Path to the location of the Exe
        public string rootFilePath; //File path to the folder taht contains the game files
        public string launcher; //Launcher that the game is associated with. Null if it has no launcher.
        public long fileSize; //Size of the file that the exe is in.
        public List<string> libraryCategories = new List<string>(); //Custom categories that the exe is sorted into. Uncategorized if none.
        public Image icon; //Icon pulled from the exe.
        private bool hidden = false; //Sets if the exe is hidden in the library. Set by the user.
        public bool removed = false; //Sets if the exe has been removed from the libray by being sorted out by the program not the user.
        public string drive; //Drive that the exe is located on

        public static Image[] exeIconsArray;
        private static List<Exe> Exes = new List<Exe>();

        public static void GetExesByRegistry()
        {
            string[] searchWords = { "steamapps\\common", "Origin Games", "Uplay", "Ubisoft Game Launcher\\games", "Epic Games", "GOG Galaxy\\Games", "Steam", "steam" };
            List<string> filepaths = new List<string>();

            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        Console.WriteLine(subkey.GetValue("DisplayName"));
                        //Console.WriteLine(subkey.GetValue("InstallLocation"));

                        //foreach(string searchWord in searchWords)
                        //{
                        //    try
                        //    {
                        //        if (subkey.GetValue("InstallLocation").ToString().Contains(searchWord))
                        //        {
                        //            filepaths.Add(subkey.GetValue("InstallLocation").ToString());
                        //            continue;
                        //        }
                        //    }
                        //    catch(NullReferenceException e)
                        //    {

                        //    }
                        //}
                    }
                }

                Console.WriteLine("Printing Filepaths");
                foreach (string filepath in filepaths)
                {
                    Console.WriteLine(filepath);
                }
            }
        }

        public static void FindExes()
        {
            Exes.Clear();

            GetSteamApps();
            GetGOGApps();
            GetPopularGames();
            GetEpicApps();
            GetOriginApps();
        }

        public static string steamFilepath;

        public static void GetSteamApps()
        {
            List<string> steamLibraryFilepaths = new List<string>();
            List<DirectoryInfo> gameFolderPaths = new List<DirectoryInfo>();
            Dictionary<string, string> manifestInfo = new Dictionary<string, string>();

            string registry_key = @"SOFTWARE\WOW6432Node\Valve\Steam";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                steamFilepath = key.GetValue("InstallPath").ToString();
            }

            manifestInfo = ReadSteamGameManifest();

            steamLibraryFilepaths.Add(steamFilepath);

            string libraryFileManifestPath = steamFilepath + "\\steamapps\\libraryfolders.vdf";

            StreamReader file = new StreamReader(@libraryFileManifestPath);
            string line;

            while((line = file.ReadLine()) != null)
            {
                if(line.Contains(":\\"))
                {
                    line = line.Substring(7, line.Length - 7);
                    line = line.TrimEnd('"');
                    steamLibraryFilepaths.Add(line);
                }
            }

            for(int i = 0; i < steamLibraryFilepaths.Count; i++)
            {
                steamLibraryFilepaths[i] = steamLibraryFilepaths[i] + "\\steamapps\\common";
                //Console.WriteLine(steamLibraryFilepaths[i]);
            }

            foreach(string filepath in steamLibraryFilepaths)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(@filepath);
                gameFolderPaths.AddRange(directoryInfo.GetDirectories());
            }

            foreach(DirectoryInfo gameFolder in gameFolderPaths)
            {
                //Console.WriteLine(gameFolder.Name);
                if(manifestInfo.ContainsKey(gameFolder.Name))
                {
                    Exe newExe = new Exe();
                    newExe.gameName = gameFolder.Name;
                    newExe.filePath = gameFolder.FullName + "\\" + manifestInfo[gameFolder.Name];
                    newExe.launcher = "Steam";
                    Console.WriteLine(newExe.filePath);
                    Exes.Add(newExe);
                }
            }
        }

        public static Dictionary<string, string> ReadSteamGameManifest()
        {
            Dictionary<string,string> manifestInfo = new Dictionary<string, string>();
            string appCacheManifestFilepath = steamFilepath + "\\appcache\\appinfo.vdf";

            StreamReader file = new StreamReader(@appCacheManifestFilepath);
            string line;

            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    int currentStartIndex = 0;

                    if (line.Contains("installdir"))
                    {
                        //FIXME: Missing 8 games for some reason
                        //FIXME: Some things in the appcache are labeled weird in the file like the banner saga showing up with the mac launch
                        //option first and the windows version second causing it not to show up right
                        while (line.IndexOf("installdir", currentStartIndex) > 0)
                        {
                            //Gets name of file in the steamapps\common folder
                            int installIndex = line.IndexOf("installdir", currentStartIndex);
                            string installDir = line.Substring(installIndex);

                            int endIndex = installDir.IndexOf('\0', 12);
                            installDir = installDir.Substring(11, endIndex - 11);

                            //Gets path from root folder(steamapps\common\[Game Name] folder) to the exe
                            int executableIndex = line.IndexOf("executable", currentStartIndex);
                            string exePath = line.Substring(executableIndex);

                            int exeEndIndex = exePath.IndexOf('\0', 12);
                            exePath = exePath.Substring(11, exeEndIndex - 11);

                            currentStartIndex = installIndex + 1000;

                            manifestInfo.Add(installDir, exePath);
                        }
                    }
                }
                catch(Exception e)
                {
                    //Console.WriteLine(e.Message);
                }
            }

            //foreach(KeyValuePair<string, string> entry in manifestInfo)
            //{
            //    Console.WriteLine(entry.Key + "||" + entry.Value);
            //}

            return manifestInfo;
        }

        public static void GetEpicApps()
        {
            string epicManifestFilepath;

            string registry_key = @"SOFTWARE\WOW6432Node\Epic Games\EpicGamesLauncher";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                epicManifestFilepath = key.GetValue("AppDataPath").ToString() + "\\Manifests";
                FileInfo[] manifests;

                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(@epicManifestFilepath);
                manifests = directoryInfo.GetFiles("*.item", SearchOption.TopDirectoryOnly);

                foreach (FileInfo manifest in manifests)
                {
                    StreamReader file = new StreamReader(manifest.FullName);
                    string line;

                    string gameName = "NULL";
                    string gameFilepath = "NULL";
                    string exeName = "NULL";

                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains("DisplayName"))
                        {
                            line = line.Substring(17, line.Length - 17);
                            line = line.TrimEnd(',');
                            line = line.TrimEnd('"');
                            gameName = line;
                        }
                        else if (line.Contains("InstallLocation"))
                        {
                            line = line.Substring(21, line.Length - 21);
                            line = line.TrimEnd(',');
                            line = line.TrimEnd('"');
                            gameFilepath = line;
                        }
                        else if (line.Contains("LaunchExecutable"))
                        {
                            line = line.Substring(22, line.Length - 22);
                            line = line.TrimEnd(',');
                            line = line.TrimEnd('"');
                            exeName = line;
                        }
                    }

                    gameFilepath = gameFilepath + "\\" + exeName;

                    Exe newExe = new Exe();
                    newExe.gameName = gameName;
                    newExe.filePath = gameFilepath;
                    newExe.launcher = "Epic Games";
                    Exes.Add(newExe);
                }
            }
        }

        public static void GetOriginApps()
        {
            List<string> gameFolderFilepaths = new List<string>();
            List<FileInfo> exeFilepaths = new List<FileInfo>();

            string localContentFilepath = "C:\\ProgramData\\Origin\\LocalContent";
            System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(@localContentFilepath);
            DirectoryInfo[] manifestFolders = directoryInfo.GetDirectories();

            foreach(DirectoryInfo manifestFolder in manifestFolders)
            {
                int count = 0;
                FileInfo[] manifests = manifestFolder.GetFiles("*.mfst", SearchOption.TopDirectoryOnly);

                string gameName = manifestFolder.Name;

                foreach (FileInfo manifest in manifests)
                {
                    StreamReader file = new StreamReader(manifest.FullName);
                    string line;

                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains("dipinstallpath"))
                        {
                            int pathIndex = line.IndexOf("dipinstallpath");
                            line = line.Substring(pathIndex);
                            int endIndex = line.IndexOf('&');
                            line = line.Substring(15, endIndex - 15);
                            line = line.Replace("%20", " ");
                            line = line.Replace("%3a", ":");
                            line = line.Replace("%5c", "\\");
                            line = line.TrimEnd('\\');

                            if (line.Contains(gameName) && count == 0)
                            {
                                gameFolderFilepaths.Add(line);
                                count++;
                            }
                        }
                    }
                }
            }
        }

        public static void GetGOGApps()
        {
            string GOGFilepath;

            string GOGRegistry_key = @"SOFTWARE\WOW6432Node\GOG.com\GalaxyClient\paths";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(GOGRegistry_key))
            {
                GOGFilepath = key.GetValue("client").ToString() + "\\GalaxyClient.exe";
            }

            Exe gogExe = new Exe();
            gogExe.gameName = "GOG Galaxy";
            gogExe.filePath = GOGFilepath;
            Exes.Add(gogExe);

            string registry_key = @"SOFTWARE\WOW6432Node\GOG.com\Games";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        Exe exe = new Exe();
                        exe.gameName = subkey.GetValue("GAMENAME").ToString();
                        exe.filePath = subkey.GetValue("LAUNCHCOMMAND").ToString();
                        exe.launcher = "GOG Galaxy";
                        Exes.Add(exe);
                    }
                }
            }
        }
        public static void GetPopularGames()
        {
            //League of Legends
            string leagueFilepath;

            string leagueRegistry_key = @"Software\Microsoft\Windows\CurrentVersion\Uninstall\Riot Game league_of_legends.live";
            using (Microsoft.Win32.RegistryKey key = Registry.CurrentUser.OpenSubKey(leagueRegistry_key))
            {
               leagueFilepath = key.GetValue("InstallLocation").ToString() + "\\LeagueClient.exe";

                Exe leagueExe = new Exe();
                leagueExe.gameName = "League of Legends";
                leagueExe.filePath = leagueFilepath;
                Exes.Add(leagueExe);
            }

            //Valorant
            string valorantFilepath;

            string valorantRegistry_key = @"Software\Microsoft\Windows\CurrentVersion\Uninstall\Riot Game valorant.live";
            using (Microsoft.Win32.RegistryKey key = Registry.CurrentUser.OpenSubKey(valorantRegistry_key))
            {
                valorantFilepath = key.GetValue("InstallLocation").ToString() + "\\VALORANT.exe";

                Exe valorantExe = new Exe();
                valorantExe.gameName = "Valorant";
                valorantExe.filePath = valorantFilepath;
                Exes.Add(valorantExe);
            }

            //Minecraft
            string minecraftFilepath;

            string minecraftRegistry_key = @"SOFTWARE\WOW6432Node\Mojang\InstalledProducts\Minecraft Launcher";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(minecraftRegistry_key))
            {
                minecraftFilepath = key.GetValue("InstallLocation").ToString() + "\\MinecraftLauncher.exe";

                Exe minecraftExe = new Exe();
                minecraftExe.gameName = "Minecraft";
                minecraftExe.filePath = minecraftFilepath;
                Exes.Add(minecraftExe);
            }
        }

        public static Exe[] GetExes()
        {
            return Exes.ToArray();
        }

        //Sets the information for the exes in a provided array
        public void GetInfo(Exe[] exes)
        {
            string filePath;
            FileInfo fileInfo;

            Console.WriteLine("Getting Info");

            if (exes != null)
            {
                for (int i = 0; i < exes.ToArray().Length; i++)
                {
                    filePath = exes[i].filePath;
                    Console.WriteLine(filePath);
                    exes[i].id = i;

                    //if (filePath.Contains("Steam"))
                    //{
                    //    exes[i].launcher = "Steam";
                    //}
                    //else if (filePath.Contains("Origin"))
                    //{
                    //    exes[i].launcher = "Origin";
                    //}
                    //else if (filePath.Contains("Uplay"))
                    //{
                    //    exes[i].launcher = "Uplay";
                    //}
                    //else if (filePath.Contains("GOG Galaxy"))
                    //{
                    //    exes[i].launcher = "GOG Galaxy";
                    //}
                    //else if (filePath.Contains("Epic Games"))
                    //{
                    //    exes[i].launcher = "Epic Games";
                    //}
                    //else
                    //{
                    //    exes[i].launcher = null;
                    //}
                    string rootFile = GetRootFile(filePath, this.launcher);
                    if (rootFile == "ERROR")
                    {
                        exes.ToList().RemoveAt(i);
                        continue;
                    }

                    if (exes[i].gameName == null)
                    {
                        exes[i].gameName = rootFile.Substring(rootFile.LastIndexOf("\\") + 1);
                    }
                    //exes[i].exeName = filePath.Substring(filePath.LastIndexOf("\\"));
                    //fileInfo = new FileInfo(rootFile);
                    //exes[i].fileSize = Exe.GetFileSize(fileInfo);
                    exes[i].libraryCategories.Add("Uncategorized");
                    exes[i].icon = Icon.ExtractAssociatedIcon(filePath).ToBitmap();
                    exes[i].drive = exes[i].filePath.Substring(0, 1);
                }
            }
        }

        //Sets the information for the exe that this is called for
        public void GetInfo()
        {
            string filePath;

            filePath = this.filePath;

            //if (filePath.Contains("Steam"))
            //{
            //    this.launcher = "Steam";
            //}
            //else if (filePath.Contains("Origin"))
            //{
            //    this.launcher = "Origin";
            //}
            //else if (filePath.Contains("Uplay"))
            //{
            //    this.launcher = "Uplay";
            //}
            //else if (filePath.Contains("GOG Galaxy"))
            //{
            //    this.launcher = "GOG Galaxy";
            //}
            //else if (filePath.Contains("Epic Games"))
            //{
            //    this.launcher = "Epic Games";
            //}
            //else
            //{
            //    this.launcher = null;
            //}

            try
            {
                string rootFile = GetRootFile(filePath, this.launcher);
                if (this.gameName == null)
                {
                    this.gameName = rootFile.Substring(rootFile.LastIndexOf("\\") + 1);
                }
                this.exeName = filePath.Substring(filePath.LastIndexOf("\\"));
                this.rootFilePath = rootFile;
                this.libraryCategories.Add("Uncategorized");
                this.icon = Icon.ExtractAssociatedIcon(filePath).ToBitmap();
                this.drive = this.filePath.Substring(0, 1);
            }
            catch(Exception e)
            {
                //Console.WriteLine(e.Message);
                this.gameName = null;
            }
        }

        //Gets the root file which is the folder with the name of the game which
        //includes all game files including the exe.
        private static string GetRootFile(string filePath, string launcher)
        {
            string keyWords;
            string rootFile = null;

            if (launcher != null && filePath != null)
            {
                //Sets the keyword(s) to search by.
                //Most launchers have the root file right after the name of the launcher.
                //However steam is a special case and has steamapps\\common\\ first.
                if (launcher == "Steam")
                {
                    keyWords = "steamapps\\common\\";
                }
                else
                {
                    keyWords = launcher + "\\";
                }

                //Getting a string from between two \'s turns out to be super complicated.
                //First it finds the length of the launcher file path by using the keywords from earlier.
                //Then, it finds the next \ using the launcher file path length as a starting point and
                //subtracts the launcherFPLength to get the name length.
                //Adding both of those together gives the final overall length which can be used to find
                //the actual root file and cut off everything after it.
                try
                {
                    int launcherFPLength = filePath.IndexOf(keyWords) + keyWords.Length;
                    int nameLength = filePath.IndexOf("\\", launcherFPLength) - launcherFPLength;
                    int index = launcherFPLength + nameLength;
                    rootFile = filePath.Substring(0, index);
                }
                catch(Exception e)
                {
                    //Console.WriteLine(filePath);
                    //Console.WriteLine(e.Message);
                    rootFile = null;
                }
            }
            else
            {
                rootFile = null;
            }

            return rootFile;
        }

        public static long GetFileSize(DirectoryInfo directoryInfo)
        {
            long fileSize = 0;
            DirectoryInfo rootInfo = new DirectoryInfo(directoryInfo.FullName);

            //Get file sizes
            FileInfo[] files = rootInfo.GetFiles();

            foreach(FileInfo file in files)
            {
                fileSize += file.Length;
            }

            //Get subdirectory size
            DirectoryInfo[] subdirectories = rootInfo.GetDirectories();

            foreach(DirectoryInfo newDirectoryInfo in subdirectories)
            {
                fileSize += GetFileSize(newDirectoryInfo);
            }

            return fileSize;
        }

        public long GetFileSize()
        {
            long fileSize = 0;
            DirectoryInfo rootInfo = new DirectoryInfo(this.rootFilePath);

            //Get file sizes
            FileInfo[] files = rootInfo.GetFiles();

            foreach (FileInfo file in files)
            {
                fileSize += file.Length;
            }

            //Get subdirectory size
            DirectoryInfo[] subdirectories = rootInfo.GetDirectories();

            foreach (DirectoryInfo newDirectoryInfo in subdirectories)
            {
                fileSize += GetFileSize(newDirectoryInfo);
            }

            return fileSize;
        }

        private static void ShortenFileSize(long fileSize)
        {
            bool shortened = false;
            string newFileSize = fileSize.ToString();

            while(shortened != true)
            {
                
            }
        }

        public void SetHidden (bool status)
        {
            hidden = status;
        }

        public bool GetHidden()
        {
            return hidden;
        }
    }
}
