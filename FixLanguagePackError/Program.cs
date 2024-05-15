// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;
using Microsoft.Win32;

if(!System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
    Console.WriteLine("Not run on windows");
    Console.ReadLine();
    return;
}


Console.WriteLine("Starting to write registry values");

try
{
    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate" , "DoNotConnectToWindowsUpdateInternetLocations", "0", RegistryValueKind.DWord);
    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU" , "UseWUServer", "0", RegistryValueKind.DWord);
}
catch (Exception e)
{
    Console.WriteLine("Failed to set registry values" + e);
    Console.ReadLine();
    return;
}

Console.WriteLine("Successfully set the registry key. Try to install the language pack now.");
Console.ReadLine();
