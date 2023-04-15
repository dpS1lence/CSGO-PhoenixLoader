using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using CSGO_PhoenixLoader.Common.GlobalConstants;
using CSGO_PhoenixLoader.Helpers;
using CSGO_PhoenixLoader.System;

namespace CSGO_PhoenixLoader.Hacks
{
    public class ClanTagChanger
    {
        private static Process ProcessCs { get; set; } = null!;

        private static Offsets Offsets { get; set; } = null!;

        public ClanTagChanger(Process process, Offsets offsets)
        {
            ProcessCs = process;
            Offsets = offsets;
        }

        public void SetClantag(string tag)
        {
            var processHandle = Kernel32.OpenProcess(0x001F0FFF, false, ProcessCs.Id);
            
            var moduleBaseAddress = GetModuleBaseAddress(ProcessCs, "client.dll");
            
            var localPlayerAddress = IntPtr.Add(moduleBaseAddress, Offsets.Signatures.DwLocalPlayer);

            ReadProcessMemory(processHandle, localPlayerAddress, out var localPlayer, 4, IntPtr.Zero);
            
            var clantagBytes = Encoding.ASCII.GetBytes(tag + "\0");
            
            var clantagAddress = IntPtr.Add(new IntPtr(localPlayer), Offsets.Signatures.DwSetClanTag);

            WriteProcessMemory(processHandle, clantagAddress, clantagBytes, clantagBytes.Length, IntPtr.Zero);
            
            Kernel32.CloseHandle(processHandle);
        }

        static IntPtr GetModuleBaseAddress(Process process, string moduleName)
        {
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName == moduleName)
                {
                    return module.BaseAddress;
                }
            }
            return IntPtr.Zero;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, out int lpBuffer, int dwSize, IntPtr lpNumberOfBytesRead);


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, IntPtr lpNumberOfBytesWritten);

    }
}
