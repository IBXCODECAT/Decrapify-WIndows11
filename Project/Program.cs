using System; //For basic console behaviour
using System.Diagnostics; //For process stuff
using System.IO; //For checking if a file exists and handling the filesystem

using IBX_Plugins.WindowsUtilities; //For handling Popup boxes

namespace Decrapify_Windows11
{
    class Program
    {
        private struct Settings
        {
            public static bool allowRegistryEdits = false;
            public static bool allowChangesSystemSettings = false;
        }

        static void Main(string[] args)
        {
            GetPermissions();
            StartProcess("notepad.exe");
            Console.Read();
        }

        private static void GetPermissions()
        {
            Console.Write("Requesting Permission to Change System Settings...");
            int allowChangesSystemSettings = Popup.CreatePopup("Allow Changes to System Settings?", "Allowing changes to system settings will allow 'Decrapify_Windows11' to modify user level system settings.\n\nWould you like to allow changes to system settings?", Popup.Options.yesNo, Popup.Icons.querry);
            Settings.allowChangesSystemSettings = allowChangesSystemSettings == Popup.Responses.Yes;
            if (Settings.allowChangesSystemSettings) Console.WriteLine(" | Permission Granted!"); else Console.WriteLine(" | Permission Denied!");

            Console.Write("Requesting Permission to Edit Registry...");
            int allowRegistryEdits = Popup.CreatePopup("Allow Registry Editing?", "Registry editing will allow 'Decrapify_Windows11' to change advanced and unacessible system settings; However the edits to the registry could cause unexpected behaviour and are irreversable by this program.\n\nAre you sure you want to allow registry edits?", Popup.Options.yesNo, Popup.Icons.warn);
            Settings.allowRegistryEdits = allowRegistryEdits == Popup.Responses.Yes;
            if (Settings.allowRegistryEdits) Console.WriteLine(" | Permission Granted!"); else Console.WriteLine(" | Permission Denied!");

        }
        /// <summary>
        /// Starts the process at the path provided
        /// </summary>
        /// <param name="processName">The name of the process to invoke</param>
        /// <returns>The exit code provided by the executable if availible</returns>
        private static int StartProcess(string processName)
        {
            string searchPath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = searchPath + processName;

            Console.WriteLine(searchPath);

            if (File.Exists(fullPath))
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = fullPath;
                    process.EnableRaisingEvents = true;

                    process.Start(); //Invoke..

                    process.WaitForExit();

                    return process.ExitCode;
                }
                catch
                {
                    Popup.CreatePopup("Failed to Invoke Process", "The application failed to invoke the process `" + processName + "` due to an exception.", Popup.Options.ok, Popup.Icons.error);
                }
            }
            else
            {
                Popup.CreatePopup("Failed to Invoke Process", "The application failed to invoke the process `" + processName + "` because it's executor does not exist.", Popup.Options.ok, Popup.Icons.error);
            }

            Console.WriteLine("Failed to Invoke Process '" + processName + "' at '" + fullPath + "'. Verify that the required files are in the same directory as the executable.");
            return -1;
        }
    }
}
