using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.Common;
using CSGO_PhoenixLoader.Common.GlobalConstants;
using CSGO_PhoenixLoader.Helpers;
using CSGO_PhoenixLoader.System.DataModels;
using Vector3 = CSGO_PhoenixLoader.System.DataModels.Vector3;

namespace CSGO_PhoenixLoader.Data
{
    public abstract class EntityBase
    {
        public IntPtr AddressBase { get; protected set; }

        /// <summary>
        /// Life state (true = dead, false = alive).
        /// </summary>
        public bool LifeState { get; protected set; }

        /// <summary>
        /// Health points.
        /// </summary>
        public int Health { get; protected set; }

        /// <inheritdoc cref="Team"/>
        public Team Team { get; protected set; }

        /// <summary>
        /// Model origin (in world).
        /// </summary>
        public Vector3 Origin { get; private set; }

        private Offsets Offsets { get; set; }

        public EntityBase(Offsets offsets)
        {
            Offsets = offsets;
        }

        public virtual bool IsAlive()
        {
            return AddressBase != IntPtr.Zero &&
                   !LifeState &&
                   Health > 0 &&
                   Team is Team.Terrorists or Team.CounterTerrorists;
        }

        protected abstract IntPtr ReadAddressBase(GameProcess gameProcess);

        public virtual bool Update(GameProcess gameProcess)
        {
            AddressBase = ReadAddressBase(gameProcess);
            if (AddressBase == IntPtr.Zero)
            {
                return false;
            }

            LifeState = gameProcess.Process.Read<bool>(AddressBase + Offsets.Netvars.MLifeState);
            Health = gameProcess.Process.Read<int>(AddressBase + Offsets.Netvars.MIHealth);
            Team = gameProcess.Process.Read<int>(AddressBase + Offsets.Netvars.MITeamNum).ToTeam();
            Origin = gameProcess.Process.Read<Vector3>(AddressBase + Offsets.Netvars.MVecOrigin);

            return true;
        }
    }
}
