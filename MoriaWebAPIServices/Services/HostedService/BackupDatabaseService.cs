using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MoriaWebAPIServices.Services.HostedService;

public class BackupDatabaseService : BackgroundService
{
    private readonly IConfiguration _configuration;

    readonly ILogger<BackupDatabaseService> _logger;
    private string backupFolderPath;
    private int intervalMinutes;
    private int maxBackups;
    private string pgDumpPath;
    private string connectionString;
    private string server;
    private string user;
    private string password;
    private string database;

    public BackupDatabaseService(IConfiguration configuration, ILogger<BackupDatabaseService> logger)
    {
        _logger = logger;
        _configuration = configuration;
        var backupSection = _configuration.GetSection("DatabaseBackup");

        backupFolderPath = backupSection["BackupFolderPath"];
        intervalMinutes = int.Parse(backupSection["IntervalMinutes"]);
        maxBackups = int.Parse(backupSection["MaxBackups"]);
        pgDumpPath = backupSection["PgDumpPath"];
        var config = _configuration.GetSection("ConnectionStrings");
        connectionString = config["MoriaDatabase"];

        var parameters = connectionString.Split(';')
                .Select(p => p.Split('='))
                .ToDictionary(p => p[0].Trim(), p => p[1].Trim());

        server = parameters["Server"];
        user = parameters["User Id"];
        password = parameters["Password"];
        database = parameters["Database"];
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                BackupDatabase(backupFolderPath);
                _logger.LogInformation("Backup wykonany: {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas wykonywania backupu");
            }

            await Task.Delay(TimeSpan.FromMinutes(intervalMinutes), stoppingToken); 
        }
    }

    public void BackupDatabase(string backupFolderPath)
    {   
        Directory.CreateDirectory(backupFolderPath);

        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string fileName = $"{timestamp}.backup";
        string outputFilePath = Path.Combine(backupFolderPath, fileName);

        // Przygotuj proces
        var startInfo = new ProcessStartInfo
        {
            FileName = pgDumpPath,
            Arguments = $"-h {server} -U {user} -F c -b -v -f \"{outputFilePath}\" {database}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        startInfo.EnvironmentVariables["PGPASSWORD"] = password;

        using var process = new Process { StartInfo = startInfo };

        process.OutputDataReceived += (sender, e) =>
        {
            //if (e.Data != null)
            //Console.WriteLine("[STDOUT] " + e.Data);
        };

        process.ErrorDataReceived += (sender, e) =>
        {
            //if (e.Data != null)
            //Console.WriteLine("[STDERR] " + e.Data);
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new Exception("Backup process failed.");
        }

        //Console.WriteLine($"Backup saved: {outputFilePath}");

        // 🧹 Przechowuj tylko maxBackups najnowszych plików
        var backupFiles = new DirectoryInfo(backupFolderPath)
            .GetFiles("*.backup")
            .OrderBy(f => f.CreationTimeUtc)
            .ToList();

        while (backupFiles.Count > maxBackups)
        {
            var oldest = backupFiles.First();
            try
            {
                //Console.WriteLine($"Deleting old backup: {oldest.FullName}");
                oldest.Delete();
                backupFiles.RemoveAt(0);
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Failed to delete old backup: {ex.Message}");
                break; // jeśli coś poszło nie tak, nie usuwaj więcej
            }
        }
    }
}

