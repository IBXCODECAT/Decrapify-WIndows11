using System; //For basic console behaviour
using System.Diagnostics; //For process stuff
using System.IO; //For checking if a file exists and handling the filesystem

using IBX_Plugins.WindowsUtilities; //For handling Popup boxes

namespace Decrapify_Windows11
{
    class Program
    {
        static void Main(string[] args)
        {
            StartProcess("notepad.exe");
            Console.Read();
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
                Popup.CreatePopup("Failed to Invoke Process", "The application failed to invoke the process `" + processName + "` because it does not exist.", Popup.Options.ok, Popup.Icons.error);
            }

            Console.WriteLine("Failed to Invoke Process '" + processName + "' at '" + fullPath + "'. Verify that the required files are in the same directory as the executable.");
            return -1;
        }
    }
}
