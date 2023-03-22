using CSGO_PhoenixLoader.Data;
using System.Windows.Threading;
using CSGO_PhoenixLoader.System;
using System.Drawing.Printing;
using CSGO_PhoenixLoader.Helpers;
using System.Windows;
using Application = System.Windows.Application;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace CSGO_PhoenixLoader.Graphics
{
    public class WindowOverlay : ThreadedComponent
    {
        protected override TimeSpan ThreadFrameSleep { get; set; } = new TimeSpan(0, 0, 0, 0, 500);

        private GameProcess GameProcess { get; set; }

        public Form Window { get; private set; }

        public WindowOverlay(GameProcess gameProcess)
        {
            GameProcess = gameProcess;

            // create window
            Window = new Form
            {
                Name = "Overlay Window",
                Text = "Overlay Window",
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.None,
                TopMost = true,
                Width = 16,
                Height = 16,
                Left = -32000,
                Top = -32000,
                StartPosition = FormStartPosition.Manual,
            };

            Window.Load += (sender, args) =>
            {
                var exStyle = User32.GetWindowLong(Window.Handle, User32.GWL_EXSTYLE);
                exStyle |= User32.WS_EX_LAYERED;
                exStyle |= User32.WS_EX_TRANSPARENT;

                // make the window's border completely transparent
                User32.SetWindowLong(Window.Handle, User32.GWL_EXSTYLE, (IntPtr)exStyle);

                // set the alpha on the whole window to 255 (solid)
                User32.SetLayeredWindowAttributes(Window.Handle, 0, 255, User32.LWA_ALPHA);
            };
            Window.SizeChanged += (sender, args) => ExtendFrameIntoClientArea();
            Window.LocationChanged += (sender, args) => ExtendFrameIntoClientArea();
            Window.Closed += (sender, args) => Application.Current.Shutdown();

            // show window
            Window.Show();
        }

        public override void Dispose()
        {
            base.Dispose();

            Window.Close();
            Window.Dispose();
            Window = default;

            GameProcess = default;
        }

        private void ExtendFrameIntoClientArea()
        {
            var margins = new Margins
            {
                Left = 0,
                Right = 0,
                Top = 0,
                Bottom = 0,
            };
            Dwmapi.DwmExtendFrameIntoClientArea(Window.Handle, ref margins);
        }

        protected override void FrameAction()
        {
            Update(GameProcess.WindowRectangleClient);
        }

        private void Update(Rectangle windowRectangleClient)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Window.BackColor = Color.Blue; // TODO: temporary

                if (Window.Location != windowRectangleClient.Location || Window.Size != windowRectangleClient.Size)
                {
                    if (windowRectangleClient is {Width: > 0, Height: > 0})
                    {
                        // valid
                        Window.Location = windowRectangleClient.Location;
                        Window.Size = windowRectangleClient.Size;
                    }
                    else
                    {
                        // invalid
                        Window.Location = new Point(-32000, -32000);
                        Window.Size = new Size(16, 16);
                    }
                }
            }, DispatcherPriority.Normal);
        }
    }
}
