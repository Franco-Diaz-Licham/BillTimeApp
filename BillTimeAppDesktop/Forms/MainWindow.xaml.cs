namespace BillTimeAppDesktop;

public partial class MainWindow : Window
{
    public MainWindow(ISqliteData data)
    {
        InitializeComponent();
        content.Content = App.Provider.GetRequiredService<MainControl>();
    }

    private void clientMenuItem_Click(
            object sender, 
            RoutedEventArgs e)
    {
        content.Content = App.Provider.GetRequiredService<ClientControl>();
    }

    private void paymentMenuItem_Click(
            object sender, 
            RoutedEventArgs e)
    {
        content.Content = App.Provider.GetRequiredService<PaymentsControl>();
    }

    private void workMenuItem_Click(
            object sender, 
            RoutedEventArgs e)
    {
        content.Content = App.Provider.GetRequiredService<WorkControl>();
    }

    private void defaultsMenuItem_Click(
            object sender, 
            RoutedEventArgs e)
    {
        content.Content = App.Provider.GetRequiredService<DefaultsControl>();
    }

    private void exitMenuItem_Click(
            object sender, 
            RoutedEventArgs e)
    {
        Close();
    }

    private void mainMenuItem_Click(
            object sender, 
            RoutedEventArgs e)
    {
        content.Content = App.Provider.GetRequiredService<MainControl>();
    }

    private void aboutMenuItem_Click(
            object sender, 
            RoutedEventArgs e)
    {
        content.Content = App.Provider.GetRequiredService<AboutControl>();
    }
}