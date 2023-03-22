using CSGO_PhoenixLoader.Data;
using System;
using System.Windows;

namespace CSGO_PhoenixLoader
{
    public class Program : Application, IDisposable
    {
        public static void Main() => new Program().Run();

        private GameProcess GameProcess { get; set; }
        private GameData GameData { get; set; }

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
            GameProcess.Start();
            GameData.Start();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            GameData.Dispose();
            GameData = default;

            GameProcess.Dispose();
            GameProcess = default;
        }
    }
}