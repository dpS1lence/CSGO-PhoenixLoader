using CSGO_PhoenixLoader.Data;
using CSGO_PhoenixLoader.Graphics;
using System;
using System.Windows;
using Application = System.Windows.Application;

namespace CSGO_PhoenixLoader
{
    public class Program : Application, IDisposable
    {
        public static void Main() => new Program().Run();

        private GameProcess GameProcess { get; set; }

        private GameData GameData { get; set; }

        private WindowOverlay WindowOverlay { get; set; }

        public Program()
        {
            Startup += (sender, args) => Ctor();
            Exit += (sender, args) => Dispose();
        }

        /// <summary />
        public void Ctor()
        {
            GameProcess = new GameProcess();
            GameData = new GameData(GameProcess);
            WindowOverlay = new WindowOverlay(GameProcess);


            GameProcess.Start();
            GameData.Start();
            WindowOverlay.Start();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            WindowOverlay.Dispose();
            WindowOverlay = default;

            GameData.Dispose();
            GameData = default;

            GameProcess.Dispose();
            GameProcess = default;
        }
    }
}