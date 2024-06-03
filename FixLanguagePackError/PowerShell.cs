using System.Diagnostics;

namespace FixLanguagePackError;

public class PowerShell
{
    public static int ExecuteCommand(string command)
    {
        var processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = "powershell.exe";
        processStartInfo.Arguments = $"-Command \"{command}\"";
        processStartInfo.UseShellExecute = false;
        processStartInfo.RedirectStandardOutput = true;
    
        using var process = new Process();
        process.StartInfo = processStartInfo;
        process.Start();
        process.WaitForExit();
        string output = process.StandardOutput.ReadToEnd();
        Console.WriteLine(output);
        return process.ExitCode;
    }
}