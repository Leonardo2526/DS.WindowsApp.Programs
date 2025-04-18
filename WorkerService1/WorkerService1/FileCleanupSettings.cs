namespace CleanupService
{
    public class FileCleanupSettings
    {
        public string DirectoryToClean { get; set; } = string.Empty;
        public int FileAgeThresholdDays { get; set; }
        public int CheckIntervalDays { get; set; }
    }
}
