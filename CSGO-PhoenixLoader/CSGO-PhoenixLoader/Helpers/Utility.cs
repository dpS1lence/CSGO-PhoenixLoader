
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using CSGO_PhoenixLoader.Common;
using CSGO_PhoenixLoader.System;
using CSGO_PhoenixLoader.System.DataModels;
namespace CSGO_PhoenixLoader.Helpers
{
    public static class Utility
    {
        public static Rectangle GetClientRect(IntPtr handle)
        {
            return User32.ClientToScreen(handle, out var point) && User32.GetClientRect(handle, out CSGO_PhoenixLoader.System.DataModels.Rect rect)
                ? new Rectangle(point.X, point.Y, rect.Right - rect.Left, rect.Bottom - rect.Top)
                : default;
        }

        public static Module? GetModule(this Process process, string moduleName)
        {
            var processModule = process.GetProcessModule(moduleName);

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            return processModule is null || processModule.BaseAddress == IntPtr.Zero
                ? default
                : new Module(process, processModule);
        }

        public static ProcessModule GetProcessModule(this Process process, string moduleName)
        {
            return process?.Modules.OfType<ProcessModule>()
                .FirstOrDefault(a => string.Equals(a.ModuleName.ToLower(), moduleName.ToLower()));
        }
        public static bool IsInfinityOrNaN(this float value)
        {
            return float.IsNaN(value) || float.IsInfinity(value);
        }
        public static bool IsValidScreen(this Vector3 value)
        {
            return !value.X.IsInfinityOrNaN() && !value.Y.IsInfinityOrNaN() && value.Z >= 0 && value.Z < 1;
        }
        public static bool IsRunning(this Process process)
        {
            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        public static T Read<T>(this Process process, IntPtr lpBaseAddress) where T : unmanaged
        {
            return Read<T>(process.Handle, lpBaseAddress);
        }

        public static T Read<T>(this Module module, int offset) where T : unmanaged
        {
            return Read<T>(module.Process.Handle, module.ProcessModule.BaseAddress + offset);
        }

        public static T Read<T>(IntPtr hProcess, IntPtr lpBaseAddress) where T : unmanaged
        {
            var size = Marshal.SizeOf<T>();
            var buffer = (object)default(T);
            Kernel32.ReadProcessMemory(hProcess, lpBaseAddress, buffer, size, out var lpNumberOfBytesRead);
            return lpNumberOfBytesRead == size ? (T)buffer : default;
        }

        public static void Write<T>(this Process process, IntPtr lpBaseAddress, object value)
        {
            Write<T>(process.Handle, lpBaseAddress, value);
        }

        public static void Write<T>(this Module module, int offset, object value)
        {
            Write<T>(module.Process.Handle, module.ProcessModule.BaseAddress + offset, value);
        }

        public static void Write<T>(IntPtr processHandle, IntPtr lpBaseAddress, object value)
        {
            var bufffer = StructureToByteArray(value);
            Kernel32.WriteProcessMemory(processHandle, lpBaseAddress, bufffer, bufffer.Length, out var lpNumberOfBytesWritten);
        }
        public static Team ToTeam(this int teamNum)
        {
            switch (teamNum)
            {
                case 1:
                    return Team.Spectator;
                case 2:
                    return Team.Terrorists;
                case 3:
                    return Team.CounterTerrorists;
                default:
                    return Team.Unknown;
            }
        }
        public static byte[] StructureToByteArray(object obj)
        {
            int len = Marshal.SizeOf(obj);

            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }
    }
}
