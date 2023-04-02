using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.System.DataModels;
using MathSystem = global::System.Math;

namespace CSGO_PhoenixLoader.Common.Math
{
    public static class GfxMath
    {
        public static float Dot(this Vector3 left, Vector3 right)
        {
            return Vector3.Dot(left, right);
        }

        public static bool IsParallelTo(this Vector3 vector, Vector3 other, float tolerance = 1E-6f)
        {
            return MathSystem.Abs(1.0 - MathSystem.Abs(vector.Dot(other))) <= tolerance;
        }
    }
}
