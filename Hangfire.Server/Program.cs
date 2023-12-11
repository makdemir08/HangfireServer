using Hangfire;
using Hangfire.Server;

var host = Host.CreateDefaultBuilder(args);
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

host.ConfigureServices(services =>
    {
        services.AddHangfire(opt =>
        {
            opt.UseSqlServerStorage(configuration.GetConnectionString("DbConnectionString"))
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();
        });

        services.AddHangfireServer();
    });

host.Build().Run();
