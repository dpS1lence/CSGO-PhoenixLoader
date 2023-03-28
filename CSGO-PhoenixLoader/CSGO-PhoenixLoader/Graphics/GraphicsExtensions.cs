//using Microsoft.DirectX.Direct3D;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CSGO_PhoenixLoader.Helpers;
//using Microsoft.DirectX;

//namespace CSGO_PhoenixLoader.Graphics
//{
//    public static class GraphicsExtensions
//    {
//        public static void DrawPolyline(this Device device, Vector3[] vertices, Color color)
//        {
//            if (vertices.Length < 2 || vertices.Any(v => !v.IsValidScreen()))
//            {
//                return;
//            }

//            var vertexStreamZeroData = vertices.Select(v => new CustomVertex.TransformedColored(v.X, v.Y, v.Z, 0, color.ToArgb())).ToArray();
//            device.VertexFormat = VertexFormats.Diffuse | VertexFormats.Transformed;
//            device.DrawUserPrimitives(PrimitiveType.LineStrip, vertexStreamZeroData.Length - 1, vertexStreamZeroData);
//        }
//    }
//}
