using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.Data.Raw;
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

        public static Vector3 Transform(this in Matrix3x4 matrix, Vector3 value)
        {
            var wInv = 1.0f / (matrix.m30 * value.X + matrix.m31 * value.Y + matrix.m32 * value.Z + 1.0f);
            return new Vector3
            (
                matrix.m00 * value.X + matrix.m01 * value.Y + matrix.m02 * value.Z,
                matrix.m10 * value.X + matrix.m11 * value.Y + matrix.m12 * value.Z,
                matrix.m20 * value.X + matrix.m21 * value.Y + matrix.m22 * value.Z
            ) * wInv;
        }

    }
}
