using CSGO_PhoenixLoader.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.Helpers;
using CSGO_PhoenixLoader.Common.GlobalConstants;
using CSGO_PhoenixLoader.Data;

namespace CSGO_PhoenixLoader.Hacks
{
    public class BHop : ThreadedComponent
    {
        private const string NAME_PROCESS = "csgo";

        public BHop(Offsets offsets)
        {
            _offsets = offsets;
        }

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private static Process ProcessCs { get; set; } = null!;

        private static Offsets _offsets = null!;

        private static IntPtr baseAddress;

        public void Start()
        {
            ProcessCs = Process.GetProcessesByName(NAME_PROCESS)
                .FirstOrDefault();

            baseAddress = ProcessCs.Modules.Cast<ProcessModule>().SingleOrDefault(m =>
                string.Equals(m.ModuleName, "client.dll", StringComparison.OrdinalIgnoreCase)).BaseAddress;

            var bHopThread = new Thread(BHopScript);

            bHopThread.Start();
        }

        private static void BHopScript()
        {
            var localPlayer = ProcessCs.Read<IntPtr>((baseAddress + _offsets.Signatures.DwLocalPlayer));

            while (true)
            {
                int i = GetAsyncKeyState((int)Keys.Space);

                if ((ProcessCs.Read<int>(localPlayer + (_offsets.Netvars.MFFlags)) & 0x0001) == 1 && (i & 0x8000) > 0)
                {
                    ProcessCs.Write<IntPtr>(baseAddress + _offsets.Signatures.DwForceJump, 6);
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        protected override void FrameAction() { }
    }
}

