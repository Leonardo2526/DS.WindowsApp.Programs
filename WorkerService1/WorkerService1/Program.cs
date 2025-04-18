// Program.cs
using CleanupService;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "File Cleanup Service";
    })
    .ConfigureServices((context, services) =>
    {
        // ������������ ������������
        services.Configure<FileCleanupSettings>(context.Configuration.GetSection("FileCleanupSettings"));
        services.AddHostedService<FileCleanupService>();
    })
    .ConfigureLogging(logging =>
    {
        logging.AddEventLog(settings =>
        {
            settings.SourceName = "FileCleanupService";
        });
        logging.AddConsole(); // ��� �������
        logging.AddDebug();   // ��� ������� � Visual Studio
    })
    .Build();

await host.RunAsync();