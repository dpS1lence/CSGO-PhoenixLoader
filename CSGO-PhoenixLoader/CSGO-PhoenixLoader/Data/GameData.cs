using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.Common;
using CSGO_PhoenixLoader.Helpers;

namespace CSGO_PhoenixLoader.Data
{
    public class GameData : ThreadedComponent
    {
        protected override string ThreadName => nameof(GameData);

        private GameProcess GameProcess { get; set; }

        public Player Player { get; set; }
        public Entity[] Entities { get; set; }

        public GameData(GameProcess gameProcess)
        {
            GameProcess = gameProcess;
            Player = new Player();
            Entities = Enumerable.Range(0, 64).Select(i => new Entity(i)).ToArray();
        }

        public override void Dispose()
        {
            base.Dispose();

            Entities = default;
            Player = default;
            GameProcess = default;
        }

        protected override void FrameAction()
        {
            ThreadFrameSleep = TimeSpan.FromMilliseconds(1);
            if (!GameProcess.IsValid)
            {
                return;
            }

            //Player.Update(GameProcess);
            var baseAddress = GameProcess.Process.Modules.Cast<ProcessModule>().SingleOrDefault(m =>
                string.Equals(m.ModuleName, "client.dll", StringComparison.OrdinalIgnoreCase)).BaseAddress;
            var glowManager = GameProcess.Process.Read<IntPtr>(baseAddress +
                                                               Offsets.dwGlowObjectManager);
           
            foreach (var entity in Entities)
            {
                //entity.Update(GameProcess);
                var index = entity.Index;
                var dwEntity = GameProcess.Process.Read<IntPtr>(baseAddress +
                                                                Offsets.dwEntityList + index * 0x10);
                var glowIndex = GameProcess.Process.Read<Int32>(dwEntity + Offsets.m_iGlowIndex);
                var health = GameProcess.Process.Read<int>(dwEntity + Offsets.m_iHealth);
                if (health < 1 || health > 100)
                {
                    continue;
                }

                var dormant = GameProcess.Process.Read<bool>(dwEntity + Offsets.m_bDormant);
                if (dormant)
                {
                    continue;
                }
                GlowSettingsStruct glowSettingsStruct = new GlowSettingsStruct() { renderOccluded = true, renderUnoccluded = false };


                GlowStruct struct1 = new GlowStruct()
                {
                    red = 0,
                    green = 0,
                    blue = 1,
                    alpha = 1,
                };
                float r = 255;
                float g = 251;
                float b = 0;
                float a = 1;
                GameProcess.Process.Write<GlowStruct>(glowManager + (glowIndex * 0x38) + 0x8, struct1);
                GameProcess.Process.Write<GlowSettingsStruct>(glowManager + (glowIndex * 0x38) + 0x28, glowSettingsStruct);


            }
            
        }

       
    }
    struct GlowStruct
    {
        public float red;
        public float green;
        public float blue;
        public float alpha;
    }
    public struct GlowSettingsStruct
    {
        public bool renderOccluded { get; set; }
        public bool renderUnoccluded { get; set; }
    }
}



