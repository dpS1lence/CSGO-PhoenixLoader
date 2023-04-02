using CSGO_PhoenixLoader.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.System.DataModels;
using Microsoft.DirectX;
using CSGO_PhoenixLoader.Common.GlobalConstants;

namespace CSGO_PhoenixLoader.Data
{
    public class Player : EntityBase
    {
        public Vector3 ViewOffset { get; private set; }
        
        public Vector3 EyePosition { get; private set; }
        
        public Vector3 ViewAngles { get; private set; }
        
        public Vector3 AimPunchAngle { get; private set; }
        
        public Vector3 AimDirection { get; private set; }

        public int Fov { get; private set; }

        public Player(Offsets offsets) 
            : base(offsets)
        {
            _offsets = offsets;
        }

        private static Offsets _offsets { get; set; }

        protected override IntPtr ReadAddressBase(GameProcess gameProcess)
        {
            return gameProcess.ModuleClient.Read<IntPtr>(_offsets.Signatures.DwLocalPlayer);
        }

        public override  bool Update(GameProcess gameProcess)
        {
            if (!base.Update(gameProcess))
            {
                return false;
            }

            // read data
            ViewOffset = gameProcess.Process.Read<Vector3>(AddressBase + _offsets.Netvars.MVecViewOffset);
            EyePosition = Origin + ViewOffset;
            ViewAngles = gameProcess.Process.Read<Vector3>(gameProcess.ModuleEngine.Read<IntPtr>(_offsets.Signatures.DwClientState) + _offsets.Signatures.DwClientStateViewAngles);
            AimPunchAngle = gameProcess.Process.Read<Vector3>(AddressBase + _offsets.Netvars.MAimPunchAngle);
            Fov = gameProcess.Process.Read<int>(AddressBase + _offsets.Netvars.MIFOV);
            if (Fov == 0) Fov = 90; // correct for default

            // calc data
            AimDirection = GetAimDirection(ViewAngles, AimPunchAngle);

            return true;
        }

        private static Vector3 GetAimDirection(Vector3 viewAngles, Vector3 aimPunchAngle)
        {
            var phi = (viewAngles.X + aimPunchAngle.X * 2.0f).DegreeToRadian();
            var theta = (viewAngles.Y + aimPunchAngle.Y * 2.0f).DegreeToRadian();

            // https://en.wikipedia.org/wiki/Spherical_coordinate_system
            return new Vector3
            (
                (float) (Math.Cos(phi) * Math.Cos(theta)),
                (float) (Math.Cos(phi) * Math.Sin(theta)),
                (float) -Math.Sin(phi)
            );
        }

    }
}
