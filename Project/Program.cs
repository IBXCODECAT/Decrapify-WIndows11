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
            public static bool allowChangesSystemSettings = false;
            public static bool allowChangesPrivacySettings = false;
            public static bool allowChangesUserSettings = false;

            public static bool allowRegistryEdits = false;
            public static bool allowUninstall = false;
        }

        static void Main(string[] args)
        {
            GetPermissions();
            Console.Read();
        }

        private static void GetPermissions()
        {
            bool unset = true;

            while (unset)
            {
                Console.Clear();
                Console.WriteLine("We would like your permission to do the following:\n");

                Console.Write("Allow 'Decrapify_Windows11' to change system-wide settings?");
                Settings.allowChangesSystemSettings = Ask();

                Console.Write("Allow 'Decrapify_Windows11' to change user specific settings?");
                Settings.allowChangesUserSettings = Ask();

                Console.Write("Allow 'Decrapify_Windows11' to change privacy settings?");
                Settings.allowChangesPrivacySettings = Ask();

                Console.Write("Allow 'Decrapify_Windows11' to make changes to the registry?");
                if (Ask())
                {
                    int allowRegistryEdits = Popup.CreatePopup("Allow Registry Editing?", "Registry editing will allow 'Decrapify_Windows11' to change advanced and unacessible system settings; However the edits to the registry could cause unexpected behaviour and are irreversable by this program.\n\nAre you sure you want to allow registry edits?", Popup.Options.yesNo, Popup.Icons.warn);
                    Settings.allowRegistryEdits = allowRegistryEdits == Popup.Responses.Yes;
                }
                else Settings.allowRegistryEdits = false;

                Console.Write("Allow 'Decrapify_Windows11' to uninstall applications on this device?");
                
                if (Ask())
                {
                    int allowUninstall = Popup.CreatePopup("Allow Uninstalling of Applications?", "Uninstalling applications will allow 'Decrapify_Windows11' to remove bloatware on your system; However once the programs are uninstalled you will have to download them again!.\n\nAre you sure you want to allow the uninstalling of applications?", Popup.Options.yesNo, Popup.Icons.warn);
                    Settings.allowUninstall = allowUninstall == Popup.Responses.Yes;
                }
                else Settings.allowUninstall = false;

                Console.Clear();

                Console.WriteLine("You have given 'Decrapify_Windows11' the following permissions:\n");
                Console.WriteLine("Allow changes to system wide settings   | " + Settings.allowChangesSystemSettings);
                Console.WriteLine("Allow changes to privacy settings       | " + Settings.allowChangesPrivacySettings);
                Console.WriteLine("Allow changes to user specific settings | " + Settings.allowChangesUserSettings);
                Console.WriteLine("Allow changes to the registry           | " + Settings.allowRegistryEdits);
                Console.WriteLine("Allow the uninstalling of programs      | " + Settings.allowUninstall);
                Console.Write("\nIs this correct: ");
                unset = !Ask();
            }

            Console.Clear();
        }

        /// <summary>
        /// Asks a (Y/N) question in the console
        /// </summary>
        /// <returns>A boolean representing the answer given</returns>
        private static bool Ask()
        {
            Console.Write("(Y/N): ");
            string ans = Console.ReadLine();

            return ans.ToLower().Equals("y");
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
