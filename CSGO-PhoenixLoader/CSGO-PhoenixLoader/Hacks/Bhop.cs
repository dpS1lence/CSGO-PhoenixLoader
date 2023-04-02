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
using CSGO_PhoenixLoader.System.DataModels;

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
            var clientState = ProcessCs.Read<IntPtr>((baseAddress + _offsets.Signatures.DwClientState));
            var viewAngles = clientState + _offsets.Signatures.DwClientStateViewAngles;
            var velocity = localPlayer + _offsets.Netvars.MVecVelocity;

            while (true)
            {
                var keys = GetAsyncKeyState((int)Keys.Space) & 0x8000;
                var forward = GetAsyncKeyState((int)Keys.W) & 0x8000;
                var backward = GetAsyncKeyState((int)Keys.S) & 0x8000;
                var left = GetAsyncKeyState((int)Keys.A) & 0x8000;
                var right = GetAsyncKeyState((int)Keys.D) & 0x8000;

                var currentAngles = ProcessCs.Read<Vector3>(viewAngles);

                var movementAngles = new Vector3();
                if (forward != 0 && backward == 0)
                {
                    movementAngles.X = currentAngles.X;
                }
                else if (backward != 0 && forward == 0)
                {
                    movementAngles.X = currentAngles.X + 180;
                }
                if (left != 0 && right == 0)
                {
                    movementAngles.Y = currentAngles.Y - 90;
                }
                else if (right != 0 && left == 0)
                {
                    movementAngles.Y = currentAngles.Y + 90;
                }

                var currentVelocity = ProcessCs.Read<Vector3>(velocity);
                var speed = Math.Sqrt(currentVelocity.X * currentVelocity.X + currentVelocity.Y * currentVelocity.Y);
                var angleRadians = (Math.PI / 180.0) * movementAngles.Y;
                var forwardMove = Math.Cos(angleRadians) * speed;
                var sideMove = Math.Sin(angleRadians) * speed;

                if ((ProcessCs.Read<int>(localPlayer + (_offsets.Netvars.MFFlags)) & 0x0001) == 1 && keys > 0)
                {
                    ProcessCs.Write<IntPtr>(baseAddress + _offsets.Signatures.DwForceJump, 6);
                    //ProcessCs.Write<Vector3>(localPlayer + _offsets.Netvars.MVecVelocity, new Vector3((float)sideMove, movementAngles.X, (float)forwardMove));
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        protected override void FrameAction() { }
    }
}

