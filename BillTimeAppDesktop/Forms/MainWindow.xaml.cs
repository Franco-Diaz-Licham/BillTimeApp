namespace BillTimeAppDesktop;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        content.Content = new WorkControl();
    }
}