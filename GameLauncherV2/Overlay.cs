using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace GameLauncherV2
{
    class Overlay
    {
        public static void OpenOverlay()
        {

        }
        public static void CloseOverlay()
        {

        }

        public static class MusicPlayer
        {

        }

        public static class Friends
        {

        }

        public static class Multiplayer
        {
            public static class DirectConnect
            {

            }
            public static class LANGroup
            {

            }
        }

        public static class ComputerAnalytics
        {
            public static void GetComputerAnalytics()
            {
                Thread t = new Thread(new ThreadStart(AnalyticsThread));
                t.Start();
            }

            public static void GetHardwareInformation()
            {
                GetCPUInformation();
                GetRAMInformation();
            }

            public static void GetCPUInformation()
            {
                string percentUsage;
                PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

                percentUsage = cpuCounter.NextValue() + "%";
                Console.WriteLine(percentUsage);
            }

            public static void GetGPUInformation()
            {
                string gpuPercentUsage;
                PerformanceCounter gpuCounter = new PerformanceCounter();

                gpuPercentUsage = gpuCounter.NextValue() + "%";
                Console.WriteLine(gpuPercentUsage);
            }

            public static void AnalyticsThread()
            {
                for(int i = 0; i < 100; i++)
                {
                    GetHardwareInformation();
                    Thread.Sleep(500);
                }
            }

            public static void GetRAMInformation()
            {
                string ramAvailable;
                PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");

                ramAvailable = ramCounter.NextValue() + "MB";
                Console.WriteLine(ramAvailable);
            }
        }
    }
}
