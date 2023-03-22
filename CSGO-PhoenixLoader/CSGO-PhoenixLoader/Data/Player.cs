using CSGO_PhoenixLoader.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.Common;
using CSGO_PhoenixLoader.System.DataModels;

namespace CSGO_PhoenixLoader.Data
{
    public class Player
    {
        public void Update(GameProcess gameProcess)
        {
            var addressBase = gameProcess.ModuleClient.Read<IntPtr>(Offsets.dwLocalPlayer);
            if (addressBase == IntPtr.Zero)
            {
                return;
            }

            var origin = gameProcess.Process.Read<Vector3>(addressBase + Offsets.m_VecOrigin);
            var viewOffset = gameProcess.Process.Read<Vector3>(addressBase + Offsets.m_VecViewOffset);
            var eyeAngle = gameProcess.Process.Read<Vector3>(gameProcess.ModuleEngine.Read<IntPtr>(0x59F19C) + 0x4D90);
            var eyePosition = origin + viewOffset;

            Console.WriteLine($"{eyePosition.X:0.00} {eyePosition.Y:0.00} {eyePosition.Z:0.00} {eyeAngle.X:F2} {eyeAngle.Y:F2}");
        }
    }
}
