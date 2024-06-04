# Fix "Cannot install language pack. Error code: 0x80073D01" on Windows 10

On some devices on installing a new language pack the error 0x80073D01 is thrown.

This C# tool applies the solution from https://learn.microsoft.com/en-us/answers/questions/478741/cannot-install-language-pack-error-code-0x80073d01

The main focus is to allow non-technical users to change the language automatically (on a system in a language they may not understand).

It does following:
*  Changes the registry key DoNotConnectToWindowsUpdateInternetLocations in HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate to 0
*  Changes the registry key UseWUServer in HHKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU to 0
*  Installs language pack en-US via powershell scripts
*  Sets preferred language to en-US
*  Deletes the registry keys again

  # Requirements
  * Windows 11 22H2 or Windows 10 21H2+
  * Administrator privileges
  * .net 6.0
