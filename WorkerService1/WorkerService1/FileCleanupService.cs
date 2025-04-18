using Microsoft.Extensions.Options;
using System.Timers;
using Timer = System.Timers.Timer;

namespace CleanupService
{
    public class FileCleanupService : BackgroundService
    {
        private readonly ILogger<FileCleanupService> _logger;
        private readonly FileCleanupSettings _settings;
        private readonly Timer _timer;

        public FileCleanupService(ILogger<FileCleanupService> logger, IOptions<FileCleanupSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
            _timer = new Timer(TimeSpan.FromDays(_settings.CheckIntervalDays).TotalMilliseconds)
            {
                AutoReset = true,
                Enabled = true
            };
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer.Elapsed += CleanupFiles;

            // Выполнить сразу при старте
            CleanupFiles(this, null);

            return Task.CompletedTask;
        }

        private void CleanupFiles(object sender, ElapsedEventArgs e)
        {
            try
            {
                _logger.LogInformation("Starting file cleanup...");

                if (!Directory.Exists(_settings.DirectoryToClean))
                {
                    _logger.LogWarning($"Directory not found: {_settings.DirectoryToClean}");
                    return;
                }

                var cutoffDate = DateTime.Now - TimeSpan.FromDays(_settings.FileAgeThresholdDays);
                var filesDeleted = 0;

                foreach (var file in Directory.GetFiles(_settings.DirectoryToClean))
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
}