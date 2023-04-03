using CSGO_PhoenixLoader.Data;
using CSGO_PhoenixLoader.Graphics;
using System;
using System.Windows;
using CSGO_PhoenixLoader.Common;
using CSGO_PhoenixLoader.Common.GlobalConstants;
using CSGO_PhoenixLoader.Hacks;
using Application = System.Windows.Application;

namespace CSGO_PhoenixLoader
{
    public class Program : Application, IDisposable
    {
        public static void Main() => new Program().Run();

        private GameProcess GameProcess { get; set; }

        private GameData GameData { get; set; }

        private WindowOverlay WindowOverlay { get; set; }

        private BHop BHop { get; set; }

        private Offsets Offsets { get; set; }

        private TriggerBot TriggerBot { get; set; }

        //private Graphics.Graphics Graphics { get; set; }

        public Program()
        {
            Startup += (sender, args) => Ctor();
            Exit += (sender, args) => Dispose();
        }

        /// <summary />
        public void Ctor()
        {
            Offsets = OffsetsService.GetAllOffsetsJson("https://raw.githubusercontent.com/frk1/hazedumper/master/csgo.json")
                      ?? throw new ArgumentException("");

            GameProcess = new GameProcess();
            GameData = new GameData(GameProcess, Offsets);
            BHop = new BHop(Offsets);
            TriggerBot = new TriggerBot(GameProcess, GameData);

            BHop.Start();
            GameProcess.Start();
            GameData.Start();
            TriggerBot.Start(); 
        }

        /// <inheritdoc />
        public void Dispose()
        {
            //Graphics.Dispose();
            //Graphics = default;

            WindowOverlay.Dispose();
            WindowOverlay = default;

            GameData.Dispose();
            GameData = default;

            GameProcess.Dispose();
            GameProcess = default;
        }
    }
}