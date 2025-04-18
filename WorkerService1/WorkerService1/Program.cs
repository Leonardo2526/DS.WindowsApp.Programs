// Program.cs
using CleanupService;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "File Cleanup Service";
    })
    .ConfigureServices((context, services) =>
    {
        // Регистрируем конфигурацию
        services.Configure<FileCleanupSettings>(context.Configuration.GetSection("FileCleanupSettings"));
        services.AddHostedService<FileCleanupService>();
    })
    .ConfigureLogging(logging =>
    {
        logging.AddEventLog(settings =>
        {
            settings.SourceName = "FileCleanupService";
        });
        logging.AddConsole(); // Для отладки
        logging.AddDebug();   // Для отладки в Visual Studio
    })
    .Build();

await host.RunAsync();