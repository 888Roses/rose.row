using rose.row.data;
using System;
using System.Runtime.InteropServices;

namespace rose.row.main
{
    public static class WinApplicationManager
    {
        public static void updateWindow()
        {
            WinApplicationManager.change(
                $"{Constants.basePath}/Textures/icons/small.ico",
                $"{Constants.basePath}/Textures/icons/big.ico",
                "Rise of War"
            );
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "LoadImage", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int message, int wParam, IntPtr lParam);

        private const int WM_SETICON = 0x80;
        private const int ICON_SMALL = 0;
        private const int ICON_BIG = 1;

        //Import the following.
        [DllImport("user32.dll", EntryPoint = "SetWindowText")]
        public static extern bool SetWindowText(IntPtr hwnd, string lpString);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string className, string windowName);

        public static void change(string smallIconPath, string bigIconPath, string applicationName)
        {
            IntPtr smallIconHandle = LoadImage(IntPtr.Zero, smallIconPath, 1, 16, 16, 0x00008010);
            IntPtr bigIcontHandle = LoadImage(IntPtr.Zero, bigIconPath, 1, 32, 32, 0x00008010);
            IntPtr handle = GetForegroundWindow();

            SendMessage(handle, WM_SETICON, ICON_SMALL, smallIconHandle);
            SendMessage(handle, WM_SETICON, ICON_BIG, bigIcontHandle);

            //Get the window handle.
            var windowPtr = FindWindow(null, "RavenfieldSteam");
            //Set the title text using the window handle.
            SetWindowText(windowPtr, applicationName);
        }
    }
}
