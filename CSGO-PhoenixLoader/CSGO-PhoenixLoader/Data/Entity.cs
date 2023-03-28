using CSGO_PhoenixLoader.Common;
using CSGO_PhoenixLoader.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.System.DataModels;

namespace CSGO_PhoenixLoader.Data
{
    public class Entity :
        EntityBase
    {
        public int Index { get; }

        public bool Dormant { get; private set; } = true;

        private IntPtr AddressStudioHdr { get; set; }

        /// <summary />
        public Entity(int index)
        {
            Index = index;
            
        }

        public override bool IsAlive()
        {
            return base.IsAlive() && !Dormant;
        }
        protected override IntPtr ReadAddressBase(GameProcess gameProcess)
        {
            return gameProcess.ModuleClient.Read<IntPtr>(Offsets.dwEntityList + Index * 0x10 /* size */);
        }

        public override bool Update(GameProcess gameProcess)
        {
            //if (!base.Update(gameProcess))
            //{
            //    return false;
            //}

            //Dormant = gameProcess.Process.Read<bool>(AddressBase + Offsets.m_bDormant);
            //Console.WriteLine($"{LifeState} {Health} {Team}");

            //if (!IsAlive())
            //{
            //    return true;
            //}
            return true;
        }
    }
}
