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

        private BHop BHop { get; set; }

        private Offsets Offsets { get; set; }

        private TriggerBot TriggerBot { get; set; }

        private AimBot AimBot { get; set; }

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
            GameProcess.Start();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            GameData = new GameData(GameProcess, Offsets);
            GameData.Start();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            BHop = new BHop(Offsets);
            BHop.Start();

            TriggerBot = new TriggerBot(GameProcess, GameData);
            TriggerBot.Start();

            AimBot = new AimBot(GameProcess, GameData, Offsets);
            AimBot.Start();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            GameData.Dispose();
            GameData = default;

            GameProcess.Dispose();
            GameProcess = default;

            BHop.Dispose();
            BHop = default;

            TriggerBot.Dispose();
            TriggerBot = default;

            AimBot.Dispose();
            AimBot = default;
        }
    }
}