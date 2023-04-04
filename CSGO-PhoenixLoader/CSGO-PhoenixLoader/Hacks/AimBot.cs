using CSGO_PhoenixLoader.Common.Math;
using CSGO_PhoenixLoader.Data;
using CSGO_PhoenixLoader.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.System.DataModels;
using System.Diagnostics;
using CSGO_PhoenixLoader.Common.GlobalConstants;

namespace CSGO_PhoenixLoader.Hacks
{
    internal class AimBot : ThreadedComponent
    {
        private const string NAME_PROCESS = "csgo";

        private const string NAME_MODULE_CLIENT = "client.dll";

        private const string NAME_MODULE_ENGINE = "engine.dll";

        protected override string ThreadName => nameof(AimBot);

        private GameProcess GameProcess { get; set; }

        private Process Process { get; }

        private Module ModuleClient { get; }

        private Module ModuleEngine { get; }

        private GameData GameData { get; set; }

        private static Offsets _offsets = null!;

        public AimBot(GameProcess gameProcess, GameData gameData, Offsets offsets)
        {
            GameProcess = gameProcess;
            GameData = gameData;

            Process = Process.GetProcessesByName(NAME_PROCESS)
                .FirstOrDefault();

            ModuleClient = Process.GetModule(NAME_MODULE_CLIENT);

            ModuleEngine = Process.GetModule(NAME_MODULE_ENGINE);

            _offsets = offsets;
        }

        public override void Dispose()
        {
            base.Dispose();

            GameData = default;
            GameProcess = default;
        }

        protected override void FrameAction()
        {
            ThreadFrameSleep = TimeSpan.FromMilliseconds(10);

            if (!GameProcess.IsValid)
            {
                return;
            }

            var player = GameData.Player;
            if (player.AimDirection.Length() < 0.001)
            {
                return;
            }

            var eCol = EntitiesCollection.Entities;

            var currentEntity = EntitiesCollection.EntitiesDynamic.FirstOrDefault();

            if (currentEntity == null)
            {
                EntitiesCollection.ResetIgnoredEntities();

                return;
            }

            currentEntity.Update(GameProcess);

            if (!currentEntity.IsAlive() || currentEntity.Dormant || currentEntity.Team == player.Team)
            {
                EntitiesCollection.IgnoredEntities.Add(currentEntity);
            }
            else if (!(currentEntity.AddressBase == player.AddressBase))
            {
                var correctedViewAngles = CalculateCorrectedViewAngles(player.EyePosition, currentEntity.BonesPos[8]);

                var viewAnglesAddress = ModuleEngine
                    .Read<IntPtr>(_offsets.Signatures.DwClientState) + _offsets.Signatures.DwClientStateViewAngles;

                Process.Write<Vector3>(viewAnglesAddress, correctedViewAngles);
            }
            else
            {
                EntitiesCollection.IgnoredEntities.Add(currentEntity);
            }
        }

        private static Vector3 CalculateCorrectedViewAngles(Vector3 eyePos, Vector3 bonePos)
        {
            var delta = bonePos - eyePos;
            var hyp = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y); // horizontal distance
            var pitch = (float)Math.Atan2(-delta.Z, hyp) * 180 / MathF.PI; // pitch angle
            var yaw = (float)Math.Atan2(delta.Y, delta.X) * 180 / MathF.PI; // yaw angle

            return new Vector3(pitch, yaw, 0);
        }

        public static Vector3 Distance(Vector3 point1, Vector3 point2)
        {
            var deltaX = point2.X - point1.X;
            var deltaY = point2.Y - point1.Y;
            var deltaZ = point2.Z - point1.Z;

            return new Vector3(deltaX, deltaY, deltaZ);
        }
    }
}
