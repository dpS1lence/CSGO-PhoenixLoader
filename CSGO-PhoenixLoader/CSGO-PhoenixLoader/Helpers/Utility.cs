
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
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
    }
}
