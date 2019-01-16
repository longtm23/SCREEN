using SCREEN_MRW.ULTILITY;
using SCREEN_MRW.CONTROLLER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace SCREEN_MRW
{
    static class Program
    {
        public const uint ES_CONTINUOUS = 0x80000000;
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;
        public const uint ES_DISPLAY_REQUIRED = 0x00000002;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint SetThreadExecutionState([In] uint esFlags);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            killAppExist();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED);
            var config = MRW_Common.GetConfigByFileConfig();
            if (config != null)
            {
                var screen = new SCREEN(config);
                screen.StartPosition = FormStartPosition.Manual;
                screen.DataReceived += ReloadApp;
                Application.Run(screen);
            }
        }
        static void killAppExist()
        {
            var process = Process.GetProcessesByName("SCREEN_MRW");
            var pCurrent = Process.GetCurrentProcess();
            if (process != null && pCurrent != null)
            {
                foreach (var p in process.ToList())
                {
                    if (p.Id != pCurrent.Id)
                    {
                        p.Kill();
                    }
                }
            }
        }
       
        static void ReloadApp(EventScreen eventScreen)
        {
            SetThreadExecutionState(ES_CONTINUOUS);
            Thread.Sleep(100);
            Application.Restart();
        }
    }
}
