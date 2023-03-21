using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.DataModels;
using CSGO_PhoenixLoader.System;



namespace CSGO_PhoenixLoader.Helpers
{
    public static class Utillity
    {
        public static Rectangle GetClientRect(IntPtr handle)
        {
            return User32.ClientToScreen(handle, out var point) && User32.GetClientRect(handle, out Rect rect)
                ? new Rectangle(point.X, point.Y, rect.Right - rect.Left, rect.Bottom - rect.Top)
                : default;
        }
    }
}
