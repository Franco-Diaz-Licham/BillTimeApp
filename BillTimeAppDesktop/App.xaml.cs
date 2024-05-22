namespace BillTimeAppDesktop;

public partial class App : Application
{
    public static ServiceProvider Provider { get; private set; }

    protected override void OnStartup(
        StartupEventArgs e)
    {
        base.OnStartup(e);

        var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");

        IConfiguration config = builder.Build();

        var services = new ServiceCollection();
        services.AddTransient<MainWindow>();
        services.AddSingleton(config);

        Provider = services.BuildServiceProvider();
        var window = Provider.GetService<MainWindow>()!;
        window.Show();
    }
}
