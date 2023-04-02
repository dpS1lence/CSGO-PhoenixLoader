using Microsoft.DirectX.PrivateImplementationDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_PhoenixLoader.System.DataModels
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        public float X, Y, Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public override int GetHashCode() => (int)this.Z ^ (int)this.Y ^ (int)this.X;

        public unsafe float Length()
        {
            fixed (Vector3 * vector3Ptr = &this)
            {
                var num1 = (float)((IntPtr)vector3Ptr + 8);

                var num2 = (float)((IntPtr)vector3Ptr + 4);

                var num3 = (float)(IntPtr)vector3Ptr;

                return (float)Math.Sqrt((double)num3 * (double)num3 + (double)num2 * (double)num2 + (double)num1 * (double)num1);
            }
        }

        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        public static Vector3 operator *(Vector3 vector, int scalar)
        {
            return new Vector3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3 operator /(Vector3 left, float right)
        {
            return new Vector3(left.X / right, left.Y / right, left.Z / right);
        }

        public static float Dot(Vector3 left, Vector3 right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        public static Vector3 operator *(float scalar, Vector3 vector)
        {
            return new Vector3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }
    }
}
