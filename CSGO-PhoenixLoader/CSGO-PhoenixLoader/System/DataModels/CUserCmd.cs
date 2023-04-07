using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_PhoenixLoader.System.DataModels
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CUserCmd
    {
        public int commandNumber;
        public int tickCount;
        public Vector3 viewangles;
        public Vector3 move;
        public int buttons;
        public byte impulse;
        public int weaponselect;
        public int weaponsubtype;
        public int randomSeed;
        public short mousedx;
        public short mousedy;
        public bool hasBeenPredicted;
        public byte[] pad;
    }

}
