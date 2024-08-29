using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PredictiveApi.Interfaces;

namespace PredictiveApi.Services
{
    public class PredictiveService : IPythonService
    {

    private readonly string _pythonPath = @"D:\College\Programming\PythonAiProject\python.exe";
    private readonly string _scriptPath = @"D:\College\4th Year Prep\C# Apis\Analytics Api\PredictiveApi\Script\python.py";

    public async Task<string> PredictDemandAsync(int dayOfYear)
    {
        var arguments = $"\"{_scriptPath}\" {dayOfYear}";

        var psi = new ProcessStartInfo
        {
            FileName = _pythonPath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using (var process = Process.Start(psi))
            {
                if (process == null)
                {
                    throw new InvalidOperationException("Failed to start the Python process.");
                }

                string output = await process.StandardOutput.ReadToEndAsync();
                
                process.WaitForExit();

                

                return output.Trim();
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}", ex);
        }
    }
        
}

}