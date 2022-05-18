using System;
using System.Runtime.InteropServices;

namespace IBX_Plugins.WindowsUtilities
{
    /// <summary>
    /// A class for creating a Windows Popup Dialog |
    /// For more info see: <see>https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-messagebox</see>
    /// </summary>
    public static class Popup
    {
        public struct Interupt
        {
            public static long app = 0x00000000L;
            public static long sys = 0x00001000L;
            public static long task = 0x00002000L;
            public static long help = 0x00004000L;
        }

        public struct Responses
        {
            public static int Ok = 1;
            public static int Cancel = 2;
            public static int Abort = 3;
            public static int Retry = 4;
            public static int Ignore = 5;
            public static int Yes = 6;
            public static int No = 7;
            public static int TryAgain = 10;
            public static int Continue = 11;
        }
        public struct Icons
        {
            public static long error = 0x00000010L;
            public static long querry = 0x00000020L;
            public static long warn = 0x00000030L;
            public static long info = 0x00000040L;
        }

        public struct Options
        {
            public static long ok = 0x00000000L;
            public static long okCancel = 0x00000001L;
            public static long abortRetryIgnore = 0x00000002L;
            public static long yesNoCancel = 0x00000003L;
            public static long yesNo = 0x00000004L;
            public static long retryCancel = 0x00000005L;
            public static long cancelRetryContinue = 0x00000006L;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        /// <summary>
        /// Get the memory pointer of the active window handle
        /// </summary>
        /// <returns>A 32-bit memory pointer to the active window handle</returns>
        public static IntPtr GetWindowHandle() { return GetActiveWindow(); }


        [DllImport("user32.dll", SetLastError = true)]
        static extern int MessageBox(IntPtr hwnd, String lpText, String lpCaption, uint uType);

        /// <summary>
        /// Create a Windows Dialog Box popup window. WARNING: This code is blocking and may temperarily suspend execution
        /// </summary>
        /// <param name="title">The text that is displayed in the title bar of the popup window</param>
        /// <param name="text">The text that is displayed within the popup window</param>
        /// <param name="options">The options that are availible in the options bar (long code, see docs)</param>
        /// <param name="icon">The icon shown to the left of the text in the popup window (long code, see docs)</param>
        /// <returns>An integer representing the option the user has chosen</returns>
        public static int CreatePopup(string title, string text, long options, long icon)
        {
            try
            {
                return MessageBox(GetWindowHandle(), text, title, (uint)(options | icon));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
    }
}
