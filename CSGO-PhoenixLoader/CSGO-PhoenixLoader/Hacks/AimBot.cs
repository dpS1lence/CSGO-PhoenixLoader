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
using CSGO_PhoenixLoader.Common;
using CSGO_PhoenixLoader.Common.GlobalConstants;
using CSGO_PhoenixLoader.System;

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

            var currentEntity = GetClosestEntityToCrosshair(EntitiesCollection.EntitiesDynamic, player);

            if (currentEntity == null)
            {
                EntitiesCollection.ResetIgnoredEntities();

                return;
            }

            currentEntity.Update(GameProcess);

            var key = User32.GetAsyncKeyState((int)Keys.F16) & 0x8000;

            if (key <= 0)
            {
                return;
            }

            if (!currentEntity.IsAlive() || currentEntity.Dormant || currentEntity.Team == player.Team)
            {
                EntitiesCollection.IgnoredEntities.Add(currentEntity);
            }
            else if(Distance(player.EyePosition, currentEntity.BonesPos[8]) > 2000.0f)
            {
                return;
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

        private static Entity? GetClosestEntityToCrosshair(ICollection<Entity> entities, Player player)
        {
            Entity? closestEntity = null;

            foreach (var entity in entities.Where(a => a.Team is Team.CounterTerrorists or Team.Terrorists))
            {
                var aimRayWorld = new Line3D(player.EyePosition, player.EyePosition + player.AimDirection * 8192);

                var hitBoxId = AimHitBox(aimRayWorld, entity, 10.9f);
                if (hitBoxId >= 0)
                {
                    closestEntity = entity;
                    break;
                }
            }

            return closestEntity;
        }
        private static float Distance(Vector3 point1, Vector3 point2)
        {
            var deltaX = point2.X - point1.X;
            var deltaY = point2.Y - point1.Y;
            var deltaZ = point2.Z - point1.Z;

            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }

        private static int AimHitBox(Line3D aimRayWorld, Entity entity, float radius)
        {
            var lengthDistances = new Dictionary<float, int>();

            for (var hitBoxId = 0; hitBoxId < entity.StudioHitBoxSet.numhitboxes; hitBoxId++)
            {
                var hitBox = entity.StudioHitBoxes[hitBoxId];
                var boneId = hitBox.bone;
                if (boneId < 0 || boneId > 128 || hitBox.radius <= 0)
                {
                    continue;
                }

                // intersect capsule
                var bonePos = entity.BonesPos[boneId];

                var cPoint = aimRayWorld.ClosestPointOnLine(aimRayWorld.StartPoint, aimRayWorld.EndPoint, bonePos);
                var len = Distance(cPoint, bonePos);

                if (len < hitBox.radius * radius)
                {
                    lengthDistances.Add(len, hitBoxId);
                }
            }

            if (lengthDistances.Count > 0)
            {
                return lengthDistances.OrderBy(a => a.Key).FirstOrDefault().Value;
            }

            return -1;
        }
    }
}
