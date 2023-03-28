﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CSGO_PhoenixLoader.Data;
//using System.Windows.Threading;
//using CSGO_PhoenixLoader.Helpers;
//using Microsoft.DirectX;
//using Microsoft.DirectX.Direct3D;
//using System.Windows;
//using Application = System.Windows.Application;
//using System.Drawing;
//using Font = System.Drawing.Font;
//using FontStyle = System.Drawing.FontStyle;

//namespace CSGO_PhoenixLoader.Graphics
//{
//    public class Graphics : ThreadedComponent
//    {
//        protected override string ThreadName => nameof(Graphics);

//        private WindowOverlay WindowOverlay { get; set; }

//        public GameProcess GameProcess { get; private set; }

//        public GameData GameData { get; private set; }

//        //private FpsCounter FpsCounter { get; set; }

//        public Device Device { get; private set; }

//        public Microsoft.DirectX.Direct3D.Font FontVerdana8 { get; private set; }

//        public Graphics(WindowOverlay windowOverlay, GameProcess gameProcess, GameData gameData)
//        {
//            WindowOverlay = windowOverlay;
//            GameProcess = gameProcess;
//            GameData = gameData;
//            //FpsCounter = new FpsCounter();

//            //InitDevice();
//            FontVerdana8 = new Microsoft.DirectX.Direct3D.Font(Device, new Font("Verdana", 8.0f, FontStyle.Regular));
//        }

//        public override void Dispose()
//        {
//            base.Dispose();

//            FontVerdana8.Dispose();
//            FontVerdana8 = default;
//            Device.Dispose();
//            Device = default;

//            //FpsCounter = default;
//            GameData = default;
//            GameProcess = default;
//            WindowOverlay = default;
//        }

//        private void InitDevice()
//        {
//            var parameters = new PresentParameters
//            {
//                Windowed = true,
//                SwapEffect = SwapEffect.Discard,
//                DeviceWindow = WindowOverlay.Window,
//                MultiSampleQuality = 0,
//                BackBufferFormat = Format.A8R8G8B8,
//                BackBufferWidth = WindowOverlay.Window.Width,
//                BackBufferHeight = WindowOverlay.Window.Height,
//                EnableAutoDepthStencil = true,
//                AutoDepthStencilFormat = DepthFormat.D16,
//                PresentationInterval = PresentInterval.Immediate, // turn off v-sync
//            };

//            Device.IsUsingEventHandlers = true;
//            Device = new Device(0, DeviceType.Hardware, WindowOverlay.Window, CreateFlags.HardwareVertexProcessing, parameters);
//        }

//        protected override void FrameAction()
//        {
//            if (!GameProcess.IsValid)
//            {
//                return;
//            }

//            //FpsCounter.Update();

//            Application.Current.Dispatcher.Invoke(() =>
//            {
//                // set render state
//                Device.RenderState.AlphaBlendEnable = true;
//                Device.RenderState.AlphaTestEnable = false;
//                Device.RenderState.SourceBlend = Blend.SourceAlpha;
//                Device.RenderState.DestinationBlend = Blend.InvSourceAlpha;
//                Device.RenderState.Lighting = false;
//                Device.RenderState.CullMode = Cull.None;
//                Device.RenderState.ZBufferEnable = true;
//                Device.RenderState.ZBufferFunction = Compare.Always;

//                // clear scene
//                Device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.FromArgb(0, 0, 0, 0), 1, 0);

//                // render scene
//                Device.BeginScene();
//                Render();
//                Device.EndScene();

//                // flush to screen
//                Device.Present();
//            }, DispatcherPriority.Normal);
//        }

//        private void Render()
//        {
//            DrawWindowBorder();
//            //DrawFps();
//        }

//        //private void DrawFps()
//        //{
//        //    FontVerdana8.DrawText(default, $"{FpsCounter.Fps:0} FPS", 5, 5, Color.Red);
//        //}

//        private void DrawWindowBorder()
//        {
//            Device.DrawPolyline(new[]
//            {
//                new Vector3(0, 0, 0),
//                new Vector3(GameProcess.WindowRectangleClient.Width - 1, 0, 0),
//                new Vector3(GameProcess.WindowRectangleClient.Width - 1, GameProcess.WindowRectangleClient.Height - 1, 0),
//                new Vector3(0, GameProcess.WindowRectangleClient.Height - 1, 0),
//                new Vector3(0, 0, 0),
//            }, Color.LawnGreen);
//        }

//    }
//}
