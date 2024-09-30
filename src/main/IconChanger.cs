using System;
using System.Runtime.InteropServices;

namespace rose.row.main
{
    public static class IconChanger
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "LoadImage", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int message, int wParam, IntPtr lParam);

        private const int WM_SETICON = 0x80;
        private const int ICON_SMALL = 0;
        private const int ICON_BIG = 1;

        public static void change(string pathSmall, string pathBig)
        {
            IntPtr smallIconHandle = LoadImage(IntPtr.Zero, pathSmall, 1, 16, 16, 0x00008010);
            IntPtr bigIcontHandle = LoadImage(IntPtr.Zero, pathBig, 1, 32, 32, 0x00008010);
            IntPtr handle = GetForegroundWindow();

            SendMessage(handle, WM_SETICON, ICON_SMALL, smallIconHandle);
            SendMessage(handle, WM_SETICON, ICON_BIG, bigIcontHandle);
        }
    }
}
