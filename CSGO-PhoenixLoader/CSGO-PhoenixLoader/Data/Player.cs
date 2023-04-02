using CSGO_PhoenixLoader.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.System.DataModels;
using Microsoft.DirectX;
using CSGO_PhoenixLoader.Common.GlobalConstants;

namespace CSGO_PhoenixLoader.Data
{
    public class Player
    {
        private readonly Offsets _offsets;
        public Player(Offsets offsets)
        {
            this._offsets = offsets;
        }
        public void Update(GameProcess gameProcess)
        {
            var addressBase = gameProcess.ModuleClient.Read<IntPtr>(_offsets.Signatures.DwLocalPlayer);
            if (addressBase == IntPtr.Zero)
            {
                return;
            }

            var origin = gameProcess.Process.Read<Vector3>(addressBase + _offsets.Netvars.MVecOrigin);
            var viewOffset = gameProcess.Process.Read<Vector3>(addressBase + _offsets.Netvars.MVecViewOffset);
            var eyeAngle = gameProcess.Process.Read<Vector3>(gameProcess.ModuleEngine.Read<IntPtr>(0x59F19C) + 0x4D90);
            var health = gameProcess.Process.Read<int>(addressBase + 0x100);
            var eyePosition = origin + viewOffset;

            Console.WriteLine($"{eyePosition.X:0.00} {eyePosition.Y:0.00} {eyePosition.Z:0.00} {eyeAngle.X:F2} {eyeAngle.Y:F2} {health}");
        }
    }
}
