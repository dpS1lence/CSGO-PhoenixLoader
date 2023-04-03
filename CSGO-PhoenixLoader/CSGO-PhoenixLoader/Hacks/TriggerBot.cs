using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.Common.GlobalConstants;
using CSGO_PhoenixLoader.Common.Math;
using CSGO_PhoenixLoader.Data;
using CSGO_PhoenixLoader.Helpers;
using CSGO_PhoenixLoader.System.DataModels;

namespace CSGO_PhoenixLoader.Hacks
{
    public class TriggerBot : ThreadedComponent
    {
        private const string NAME_PROCESS = "csgo";

        protected override string ThreadName => nameof(TriggerBot);

        private GameProcess GameProcess { get; set; }

        private GameData GameData { get; set; }

        public TriggerBot(GameProcess gameProcess, GameData gameData)
        {
            GameProcess = gameProcess;
            GameData = gameData;
        }

        public override void Dispose()
        {
            base.Dispose();

            GameData = default;
            GameProcess = default;
        }

        protected override void FrameAction()
        {
            ThreadFrameSleep = TimeSpan.FromMilliseconds(1);
            if (!GameProcess.IsValid)
            {
                return;
            }

            // get aim ray in world
            var player = GameData.Player;
            if (player.AimDirection.Length() < 0.001)
            {
                return;
            }
            var aimRayWorld = new Line3D(player.EyePosition, player.EyePosition + player.AimDirection * 8192);

            // go through entities
            foreach (var entity in GameData.Entities)
            {
                if (!entity.IsAlive() || entity.AddressBase == player.AddressBase)
                {
                    continue;
                }

                // check if aim ray intersects any hitboxes of entity
                var hitBoxId = IntersectsHitBox(aimRayWorld, entity);
                if (hitBoxId >= 0)
                {
                    // shoot
                    Utility.MouseLeftDown();
                    Utility.MouseLeftUp();
                    Thread.Sleep(1);
                }
            }
        }

        public static int IntersectsHitBox(Line3D aimRayWorld, Entity entity)
        {
            //Console.WriteLine($"SkeletonCount {entity.SkeletonCount} Team {entity.Team} Health {entity.Health} X {entity.BonesPos[0].X} Y {entity.BonesPos[0].Y} Z {entity.BonesPos[0].Z}");
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
                
                if (len < hitBox.radius * 0.9f)
                {
                    // intersects
                    return hitBoxId;
                }
            }

            return -1;
        }

        public static float Distance(Vector3 point1, Vector3 point2)
        {
            float deltaX = point2.X - point1.X;
            float deltaY = point2.Y - point1.Y;
            float deltaZ = point2.Z - point1.Z;

            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }
    }
}
