using CSGO_PhoenixLoader.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.System;

namespace CSGO_PhoenixLoader.Data
{
    public class GameProcess : ThreadedComponent
    {
        private const string NAME_PROCESS = "csgo";

        private const string NAME_MODULE_CLIENT = "client.dll";

        private const string NAME_MODULE_ENGINE = "engine.dll";

        private const string NAME_WINDOW = "Counter-Strike: Global Offensive - Direct3D 9";

        public GameProcess()
        {
            ThreadFrameSleep = new TimeSpan(0, 0, 0, 0, 500);
        }

        protected override string ThreadName => nameof(GameProcess);

        protected override TimeSpan ThreadFrameSleep { get; set; }

        /// <summary>
        /// Game process.
        /// </summary>
        public Process Process { get; private set; }

        /// <summary>
        /// Client module.
        /// </summary>
        public Module ModuleClient { get; private set; }

        /// <summary>
        /// Engine module.
        /// </summary>
        public Module ModuleEngine { get; private set; }

        /// <summary>
        /// Game window handle.
        /// </summary>
        private IntPtr WindowHwnd { get; set; }

        /// <summary>
        /// Game window client rectangle.
        /// </summary>
        public Rectangle WindowRectangleClient { get; private set; }

        /// <summary>
        /// Whether game window is active.
        /// </summary>
        private bool WindowActive { get; set; }

        /// <summary>
        /// Is game process valid?
        /// </summary>
        public bool IsValid => WindowActive && !(Process is null) && !(ModuleClient is null) && !(ModuleEngine is null);

        public override void Dispose()
        {
            InvalidateWindow();
            InvalidateModules();

            base.Dispose();
        }

        protected override void FrameAction()
        {
            if (!EnsureProcessAndModules())
            {
                InvalidateModules();
            }

            if (!EnsureWindow())
            {
                InvalidateWindow();
            }

            Console.WriteLine(IsValid
                ? $"0x{(int)Process.Handle:X8} {WindowRectangleClient.X} {WindowRectangleClient.Y} {WindowRectangleClient.Width} {WindowRectangleClient.Height}"
                : "Game process invalid");
        }

        private void InvalidateModules()
        {
            ModuleEngine?.Dispose();
            ModuleEngine = default;

            ModuleClient?.Dispose();
            ModuleEngine = default;

            Process?.Dispose();
            Process = default;
        }

        private void InvalidateWindow()
        {
            WindowHwnd = IntPtr.Zero;
            WindowRectangleClient = Rectangle.Empty;
            WindowActive = false;
        }

        private bool EnsureProcessAndModules()
        {
            if (Process is null)
            {
                Process = Process.GetProcessesByName(NAME_PROCESS)
                    .FirstOrDefault();
            }
            if (Process is null || !Process.IsRunning())
            {
                return false;
            }

            if (ModuleClient is null)
            {
                ModuleClient = Process.GetModule(NAME_MODULE_CLIENT);
            }
            if (ModuleClient is null)
            {
                return false;
            }

            if (ModuleEngine is null)
            {
                ModuleEngine = Process.GetModule(NAME_MODULE_ENGINE);
            }
            if (ModuleEngine is null)
            {
                return false;
            }

            return true;
        }

        private bool EnsureWindow()
        {
            WindowHwnd = User32.FindWindow(null, NAME_WINDOW);

            if (WindowHwnd == IntPtr.Zero)
            {
                return false;
            }

            WindowRectangleClient = Utility.GetClientRect(WindowHwnd);
            if (WindowRectangleClient.Width <= 0 ||
                WindowRectangleClient.Height <= 0)
            {
                return false;
            }

            WindowActive = WindowHwnd == User32.GetForegroundWindow();

            return WindowActive;
        }
    }
}
