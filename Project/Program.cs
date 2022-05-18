using System;
using System.Diagnostics;

using IBX_Plugins.WindowsUtilities;

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
        /// <param name="path">The full path to the process to invoke</param>
        /// <returns>The exit code provided by the executable if availible</returns>
        private static int StartProcess(string path)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = path;
                process.EnableRaisingEvents = true;

                process.Start(); //Invoke..

                process.WaitForExit();

                return process.ExitCode;
            }
            catch
            {
                Popup.CreatePopup("Failed to execute command", "The application failed to run the command `" + path + "` because the application 'cmd.exe' could not be found.", Popup.Options.ok, Popup.Icons.error);
            }

            return -1;
        }
    }
}
