﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CSGO_PhoenixLoader.System.DataModels;
using Point = System.Drawing.Point;

namespace CSGO_PhoenixLoader.System
{
    public class User32
    {
        /// <summary>
        /// Sets a new extended window style
        /// </summary>
        public const int GWL_EXSTYLE = -20;

        /// <summary>
        /// Use bAlpha to determine the opacity of the layered window. 
        /// </summary>
        public const int LWA_ALPHA = 0x2;

        /// <summary>
        /// The window is a layered window.
        /// This style cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC.
        /// Windows 8: The WS_EX_LAYERED style is supported for top-level windows and child windows.
        /// Previous Windows versions support WS_EX_LAYERED only for top-level windows.
        /// </summary>
        public const int WS_EX_LAYERED = 0x80000;

        /// <summary>
        /// The window should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
        /// The window appears transparent because the bits of underlying sibling windows have already been painted.
        /// To achieve transparency without these restrictions, use the SetWindowRgn function.
        /// </summary>
        public const int WS_EX_TRANSPARENT = 0x20;

        /// <summary>
        /// The ClientToScreen function converts the client-area coordinates of a specified point to screen coordinates.
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ClientToScreen(IntPtr hWnd, out Point lpPoint);

        /// <summary>
        /// Retrieves the coordinates of a window's client area. The client coordinates
        /// specify the upper-left and lower-right corners of the client area.
        /// Because client coordinates are relative to the upper-left corner of
        /// a window's client area, the coordinates of the upper-left corner are (0,0).
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);

        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working).
        /// The system assigns a slightly higher priority to the thread that creates the foreground window than it does to other threads.
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Retrieves information about the specified window. The function also retrieves
        /// the 32-bit (DWORD) value at the specified offset into the extra window memory. 
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Retrieves a handle to the top-level window whose class name and window name match the specified strings.
        /// This function does not search child windows. This function does not perform a case-sensitive search.
        /// To search child windows, beginning with a specified child window, use the FindWindowEx function.
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Sets the opacity and transparency color key of a layered window.
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        /// <summary>
        /// Changes an attribute of the specified window.
        /// The function also sets the 32-bit (long) value at the specified offset into the extra window memory.
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
    }

}
