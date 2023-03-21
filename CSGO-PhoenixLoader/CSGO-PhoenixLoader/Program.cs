using System;
using System.Windows;

namespace CSGO_PhoenixLoader
{
    public class Program : Application, IDisposable
    {
        public static void Main() => new Program().Run();

        public Program()
        {
            Startup += (sender, args) => Ctor();
            Exit += (sender, args) => Dispose();
        }

        

        /// <summary />
        public void Ctor()
        {
            
        }

        /// <inheritdoc />
        public void Dispose()
        {
            
        }
       
    }
}