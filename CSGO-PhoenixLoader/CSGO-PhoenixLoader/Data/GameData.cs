using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.Helpers;

namespace CSGO_PhoenixLoader.Data
{
    public class GameData : ThreadedComponent
    {
        protected override string ThreadName => nameof(GameData);

        private GameProcess GameProcess { get; set; }

        public Player Player { get; set; }

        public GameData(GameProcess gameProcess)
        {
            GameProcess = gameProcess;
            Player = new Player();
        }

        public override void Dispose()
        {
            base.Dispose();

            Player = default;
            GameProcess = default;
        }

        protected override void FrameAction()
        {
            if (!GameProcess.IsValid)
            {
                return;
            }

            Player.Update(GameProcess);
        }
    }
}
