// Worker.cs
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Timers;
using Timer = System.Timers.Timer;

public class FileCleanupService : BackgroundService
{
    private readonly ILogger<FileCleanupService> _logger;
    private Timer _timer;
    private readonly string _directoryToClean = @"C:\Temp\Cleanup";
    private readonly TimeSpan _fileAgeThreshold = TimeSpan.FromMinutes(5);
    //private readonly TimeSpan _fileAgeThreshold = TimeSpan.FromDays(14);
    private readonly int _checkIntervalInSeconds = 15;
    //private readonly int _checkIntervalInDays = 30;

    public FileCleanupService(ILogger<FileCleanupService> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(TimeSpan.FromSeconds(_checkIntervalInSeconds).TotalMilliseconds);
        //_timer = new Timer(TimeSpan.FromDays(_checkIntervalInDays).TotalMilliseconds);
        _timer.Elapsed += CleanupFiles;
        _timer.AutoReset = true;
        _timer.Enabled = true;

        // Выполнить сразу при старте
        CleanupFiles(null, null);

        return Task.CompletedTask;
    }

    private void CleanupFiles(object sender, ElapsedEventArgs e)
    {
        try
        {
            _logger.LogInformation("Starting file cleanup...");

            if (!Directory.Exists(_directoryToClean))
            {
                _logger.LogWarning($"Directory not found: {_directoryToClean}");
                return;
            }

            var cutoffDate = DateTime.Now - _fileAgeThreshold;
            var filesDeleted = 0;

            foreach (var file in Directory.GetFiles(_directoryToClean))
            {
                try
                {
                    var fileInfo = new FileInfo(file);
                    if (fileInfo.LastWriteTime < cutoffDate)
                    {
                        fileInfo.Delete();
                        filesDeleted++;
                        _logger.LogInformation($"Deleted file: {file}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error deleting file {file}");
                }
            }

            _logger.LogInformation($"File cleanup completed. Deleted {filesDeleted} files.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during file cleanup");
        }
    }

    public override void Dispose()
    {
        _timer?.Dispose();
        base.Dispose();
    }
}