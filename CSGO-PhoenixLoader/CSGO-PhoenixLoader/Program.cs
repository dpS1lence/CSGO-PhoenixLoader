using CSGO_PhoenixLoader.Data;
using System;
using System.Windows;

namespace CSGO_PhoenixLoader
{
    public class Program : Application, IDisposable
    {
        public static void Main() => new Program().Run();

        private GameProcess GameProcess { get; set; }

        public Program()
        {
            Startup += (sender, args) => Ctor();
            Exit += (sender, args) => Dispose();
        }

        /// <summary />
        public void Ctor()
        {
            GameProcess = new GameProcess();
            GameProcess.Start();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            GameProcess.Dispose();
            GameProcess = default;
        }
    }
}