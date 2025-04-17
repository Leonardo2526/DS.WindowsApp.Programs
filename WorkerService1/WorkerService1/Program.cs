// Program.cs
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "File Cleanup Service";
    })
    .ConfigureServices(services =>
    {
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