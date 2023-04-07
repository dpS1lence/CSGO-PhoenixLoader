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
using CSGO_PhoenixLoader.System;
using CSGO_PhoenixLoader.System.DataModels;

namespace CSGO_PhoenixLoader.Hacks
{
    public class BHop : ThreadedComponent
    {
        protected override string ThreadName => nameof(BHop);

        public BHop(Offsets offsets, Process process)
        {
            _offsets = offsets;
            ProcessCs = process;

            baseAddress = ProcessCs.Modules
                .Cast<ProcessModule>()
                .SingleOrDefault(m =>
                string.Equals(m.ModuleName, "client.dll", StringComparison.OrdinalIgnoreCase))?.BaseAddress
                          ?? throw new ArgumentException("Invalid Module!");
        }

        private static Process ProcessCs { get; set; } = null!;

        private static Offsets _offsets = null!;

        private static IntPtr baseAddress;

        public override void Dispose()
        {
            base.Dispose();

            ProcessCs.Dispose();
        }

        protected override void FrameAction() { }
        public void Start()
        {
            var bHopThread = new Thread(BHopCs);
            bHopThread.Start();
            //BHopCs();

            //RageStrafe();
        }

        private static void BHopCs()
        {
            while (true)
            {
                var localPlayer = ProcessCs.Read<IntPtr>((baseAddress + _offsets.Signatures.DwLocalPlayer));

                var keys = User32.GetAsyncKeyState((int)Keys.Space) & 0x8000;

                if ((ProcessCs.Read<int>(localPlayer + (_offsets.Netvars.MFFlags)) & 0x0001) == 1 && keys > 0)
                {
                    ProcessCs.Write<IntPtr>(baseAddress + _offsets.Signatures.DwForceJump, 6);
                }
            }
        }

        private static void RageStrafe()
        {
            while (true)
            {
                var localPlayer = ProcessCs.Read<IntPtr>(baseAddress + _offsets.Signatures.DwLocalPlayer);
                var clientState = ProcessCs.Read<IntPtr>(baseAddress + _offsets.Signatures.DwClientState);
                var viewAngles = clientState + _offsets.Signatures.DwClientStateViewAngles;
                var velocity = localPlayer + _offsets.Netvars.MVecVelocity;
                var flags = ProcessCs.Read<int>(localPlayer + _offsets.Netvars.MFFlags) & 0x0001;
                var moveDir = ProcessCs.Read<Vector3>(localPlayer + _offsets.Netvars.MVecViewOffset);

                Console.WriteLine(moveDir.X + " " + moveDir.Y + " " + moveDir.Z);

                var keysPressed =
                    (User32.GetAsyncKeyState((int)Keys.A) & 0x8000) > 0 ||
                    (User32.GetAsyncKeyState((int)Keys.W) & 0x8000) > 0 ||
                    (User32.GetAsyncKeyState((int)Keys.S) & 0x8000) > 0 ||
                    (User32.GetAsyncKeyState((int)Keys.D) & 0x8000) > 0;

                var spacePressed = (User32.GetAsyncKeyState((int)Keys.Space) & 0x8000) > 0;

                if (spacePressed && flags == 1 && keysPressed)
                {
                    var currentAngles =
                        ProcessCs.Read<Vector3>(viewAngles);
                    var moveAngles = new Vector3();

                    if (moveDir.Y > 0)
                    {
                        moveAngles.Y = currentAngles.Y - 90;
                    }
                    else if (moveDir.Y < 0)
                    {
                        moveAngles.Y = currentAngles.Y + 90;
                    }

                    var speed = Math.Sqrt(Math.Pow(ProcessCs.Read<float>(velocity + 0xC), 2) + Math.Pow(ProcessCs.Read<float>(velocity + 0x10), 2));
                    var angleRadians = (Math.PI / 180.0) * moveAngles.Y;
                    var forwardMove = Math.Cos(angleRadians) * speed;
                    var sideMove = Math.Sin(angleRadians) * speed;

                    if (Math.Abs(currentAngles.X) < 90)
                    {
                        if (ProcessCs.Read<float>(velocity + 0xC) != 0 || ProcessCs.Read<float>(velocity + 0x10) != 0)
                        {
                            if (ProcessCs.Read<float>(velocity + 0xC) < 0)
                            {
                                sideMove = -sideMove;
                            }
                            else if (ProcessCs.Read<float>(velocity + 0xC) == 0 && ProcessCs.Read<float>(velocity + 0x10) == 0)
                            {
                                forwardMove = 450f;
                            }
                        }
                    }
                    else
                    {
                        moveAngles.Y = currentAngles.Y + 180;
                    }

                    ProcessCs.Write<IntPtr>(baseAddress + _offsets.Signatures.DwForceJump, 6);
                    ProcessCs.Write<Vector3>(localPlayer + _offsets.Netvars.MVecVelocity, new Vector3((float)sideMove, moveAngles.X, (float)forwardMove));
                }
            }
        }
    }
}

