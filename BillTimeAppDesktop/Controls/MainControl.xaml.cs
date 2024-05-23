namespace BillTimeAppDesktop.Controls;

public partial class MainControl : UserControl
{
    public ISqliteData Data { get; }

    public MainControl(ISqliteData data)
    {
        Data = data;
        InitializeComponent();
    }

    protected override void OnInitialized(
            EventArgs e)
    {
        base.OnInitialized(e);
        clientDropDown.ItemsSource = Data.GetClients();
    }

    private void submitForm_Click(
            object sender, 
            RoutedEventArgs e)
    {

    }
}
