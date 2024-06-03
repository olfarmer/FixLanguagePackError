using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace FixLanguagePackError
{
    public class FixLanguagePackError
    {
        public static void Main(string[] args)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("Not run on windows");
                Console.ReadLine();
                return;
            }


            Console.WriteLine("Starting to write registry values");

            try
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate",
                    "DoNotConnectToWindowsUpdateInternetLocations", "0", RegistryValueKind.DWord);
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU",
                    "UseWUServer", "0", RegistryValueKind.DWord);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to set registry values: " + e);
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Installing language pack...");


            try
            {
                int statusCode = PowerShell.ExecuteCommand("Install-Language -Language en-US; exit");

                if (statusCode != 0)
                {
                    Console.WriteLine("An error occurred. The installation was not successful.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.WriteLine("Revert registry keys...");

            try
            {
                DeleteKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate",
                    "DoNotConnectToWindowsUpdateInternetLocations");
                
                DeleteKey("SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU",
                    "UseWUServer");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to set registry values: " + e);
                Console.ReadLine();
                return;
            }

            System.Console.WriteLine("Installation successful! Please reboot your computer.");
            Console.ReadLine();


        }
        
        private static void DeleteKey(string keyName, string valueName)
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName, true))
            {
                if (key == null)
                {
                    Console.WriteLine("Registry key does not exit.");
                }
                else
                {
                    key.DeleteValue(valueName);
                }
            }

        }
    }
}