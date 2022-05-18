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
        /// <param name="path">The full path to the process to invoke</param>
        /// <returns>The exit code provided by the executable if availible</returns>
        private static int StartProcess(string path)
        {
            if(File.Exists(path))
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
                    Popup.CreatePopup("Failed to Invoke Process", "The application failed to invoke the process `" + path + "` due to an exception.", Popup.Options.ok, Popup.Icons.error);
                }
            }
            else
            {
                Popup.CreatePopup("Failed to Invoke Process", "The application failed to invoke the process `" + path + "` because it does not exist.", Popup.Options.ok, Popup.Icons.error);
            }

            return -1;
        }
    }
}
