// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;
using FixLanguagePackError;
using Microsoft.Win32;

if(!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
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
    Console.WriteLine("Failed to set registry values: " + e);
    Console.ReadLine();
    return;
}

Console.WriteLine("Installing language pack...");


try
{
    int statusCode = PowerShell.ExecuteCommand("Install-Language -Language en-US");

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

try
{
    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate" , "DoNotConnectToWindowsUpdateInternetLocations", "1", RegistryValueKind.DWord);
    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU" , "UseWUServer", "1", RegistryValueKind.DWord);
}
catch (Exception e)
{
    Console.WriteLine("Failed to set registry values: " + e);
    Console.ReadLine();
    return;
}


Console.ReadLine();

