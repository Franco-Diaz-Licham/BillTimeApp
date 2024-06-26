﻿namespace BillTimeAppDesktop;

public partial class App : Application
{
    public static ServiceProvider Provider { get; private set; }

    protected override void OnStartup(
        StartupEventArgs e)
    {
        base.OnStartup(e);

        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                .AddJsonFile("appsettings.json");

        IConfiguration config = builder.Build();

        // add DI
        var services = new ServiceCollection();

        //Windows and controls
        services.AddScoped<MainWindow>();
        services.AddScoped<AboutControl>();
        services.AddScoped<DefaultsControl>();
        services.AddTransient<MainControl>();
        services.AddTransient<ClientControl>();
        services.AddTransient<PaymentsControl>();
        services.AddTransient<WorkControl>();

        // data access
        services.AddScoped<ISqliteDataAccess, SqliteDataAccess>();
        services.AddScoped<ISqliteData, SqliteData>();

        services.AddSingleton(config);

        // display main form
        Provider = services.BuildServiceProvider();
        var window = Provider.GetService<MainWindow>()!;
        window.Show();
    }
}
