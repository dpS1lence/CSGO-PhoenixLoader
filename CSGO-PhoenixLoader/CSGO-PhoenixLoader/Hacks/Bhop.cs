using CSGO_PhoenixLoader.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.Helpers;

namespace CSGO_PhoenixLoader.Hacks
{
    public class Bhop
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Int32 vKey);

        private const string NAME_PROCESS = "csgo";

        private const string NAME_MODULE_CLIENT = "client.dll";

        private static VAMemory? Memory { get; set; }

        private static Module? Module { get; set; }

        private static Process? ProcessCs { get; set; }

        private static int _module = 0;

        public static void Start()
        {
            ProcessCs = Process.GetProcessesByName(NAME_PROCESS)
                .FirstOrDefault();

            Module = ProcessCs.GetModule(NAME_MODULE_CLIENT);

            _module = (int)Module.ProcessModule.BaseAddress;

            var bhopThread = new Thread(BhopScript);

            bhopThread.Start();
        }

        private static void BhopScript()
        {
            Memory = new VAMemory("csgo");

            var localPlayer = Memory.ReadInt32((IntPtr)(_module + Offsets.dwLocalPlayer));

            if (Memory != null)
            {
                while (true)
                {
                    int i = GetAsyncKeyState((int)Keys.Space);

                    if ((Memory.ReadInt32((IntPtr)localPlayer + Offsets.dwForceJump) == 0) && i is 1 or short.MinValue)
                    {
                        Memory.WriteInt32((IntPtr)_module + Offsets.dwForceJump, 6);
                    }
                }
            }
        }
    }
}

