using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSGO_PhoenixLoader.System
{
    public static class Dwmapi
    {
        /// <summary>
        /// Extends the window frame into the client area.
        /// </summary>
        [DllImport("dwmapi.dll", SetLastError = true)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMargins);
    }
}
