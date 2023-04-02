using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.Common.GlobalConstants;
using CSGO_PhoenixLoader.Common.Math;
using CSGO_PhoenixLoader.Data;
using CSGO_PhoenixLoader.Helpers;

namespace CSGO_PhoenixLoader.Hacks
{
    public class TriggerBot : ThreadedComponent
    {
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
                    Thread.Sleep(5);
                }
            }
        }

        /*public static int IntersectsHitBox(Line3D aimRayWorld, Entity entity)
        {
            for (var hitBoxId = 0; hitBoxId < entity.StudioHitBoxSet.numhitboxes; hitBoxId++)
            {
                var hitBox = entity.StudioHitBoxes[hitBoxId];
                var boneId = hitBox.bone;
                if (boneId < 0 || boneId > Offsets.MAXSTUDIOBONES || hitBox.radius <= 0)
                {
                    continue;
                }

                // intersect capsule
                var matrixBoneModelToWorld = entity.BonesMatrices[boneId];
                var boneStartWorld = matrixBoneModelToWorld.Transform(hitBox.bbmin);
                var boneEndWorld = matrixBoneModelToWorld.Transform(hitBox.bbmax);
                var boneWorld = new Line3D(boneStartWorld, boneEndWorld);
                var (p0, p1) = aimRayWorld.ClosestPointsBetween(boneWorld, true);
                var distance = (p1 - p0).Length();
                if (distance < hitBox.radius * 0.9f /* trigger a little bit inside #1#)
                {
                    // intersects
                    return hitBoxId;
                }
            }

            return -1;
        }*/
    }
}
