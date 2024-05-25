namespace BillTimeAppDesktop.Controls;

public partial class MainControl : UserControl
{
    public ISqliteData Data { get; }
    public Stopwatch Timer { get; set; } = new();
    public bool Active { get; set; }

    public MainControl(ISqliteData data)
    {
        Data = data;
        InitializeComponent();
        SetFormVisibility(false);
    }

    protected override void OnInitialized(
            EventArgs e)
    {
        base.OnInitialized(e);
        clientDropDown.ItemsSource = Data.GetClients();
        stopTimeButton.Visibility = Visibility.Collapsed;
    }

    private void SetFormVisibility(
            bool visible)
    {
        if (visible is true)
        {
            timerStackPanel.Visibility = Visibility.Visible;
            titleStackPanel.Visibility = Visibility.Visible;
            descriptionStackPanel.Visibility = Visibility.Visible;
            submitForm.Visibility = Visibility.Visible;
        }
        else
        {
            timerStackPanel.Visibility = Visibility.Hidden;
            titleStackPanel.Visibility = Visibility.Hidden;
            descriptionStackPanel.Visibility = Visibility.Hidden;
            submitForm.Visibility = Visibility.Hidden;
        }
    }

    private void ClearFormData()
    {
        titleTextBox.Text = string.Empty;
        hoursTextBox.Text = string.Empty;
        descriptionTextBox.Text = string.Empty;
        resetTimeButton_Click(new(), new());
    }

    private (bool isValid, WorkModel? model) Validate()
    {
        var hoursValid = double.TryParse(hoursTextBox.Text, out double hours);
        var client = (ClientModel)clientDropDown.SelectedItem;

        if (hoursValid is false)
            return (false, null);
        if (client is null)
            return (false, null);
        if (string.IsNullOrEmpty(titleTextBox.Text))
            return (false, null);        
        if (string.IsNullOrEmpty(descriptionTextBox.Text))
            return (false, null);

        WorkModel model = new()
        {
            Id = 0,
            ClientId = client.Id,
            Hours = hours,
            Title = titleTextBox.Text,
            Description = descriptionTextBox.Text,
            Paid = false,
            PaymentId = null,
            DateEntered = DateTime.Today,
        };

        return (true, model);
    }

    private void submitForm_Click(
            object sender, 
            RoutedEventArgs e)
    {
        var (isValid, model) = Validate();

        if (isValid is false)
        {
            MessageBox.Show("Invalid form...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Data.SaveWork(model!);
        MessageBox.Show("Saved...", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        ClearFormData();
    }

    private async void startTimeButton_Click(
            object sender, 
            RoutedEventArgs e)
    {
        startTimeButton.Visibility = Visibility.Collapsed;
        stopTimeButton.Visibility = Visibility.Visible;
        resetTimeButton.Visibility = Visibility.Collapsed;

        Timer.Start();

        while (Timer.IsRunning is true)
        {
            timerTextBlock.Text = Timer.Elapsed.ToString(@"hh\:mm\:ss");
            await Task.Delay(1000);
        }
    }

    private double CalculateHours(
            double minutes)
    {
        double total = 0;
        double tmpMins = minutes/60;

        var client = (ClientModel)clientDropDown.SelectedItem;
        double billingMins = client.BillingIncrement;
        double minMins = client.MinimumHours;
        double roundUp = client.RoundUpAfterXMinutes / 60;

        if (tmpMins < minMins)
            return minMins;

        while (tmpMins >= billingMins)
        {
            total += billingMins;
            tmpMins -= billingMins;
        }

        if(tmpMins >= roundUp)
            total += billingMins;

        return total;
    }

    private void stopTimeButton_Click(
            object sender, 
            RoutedEventArgs e)
    {
        Timer.Stop();
        hoursTextBox.Text = CalculateHours(Timer.Elapsed.TotalMinutes).ToString("0.00");

        startTimeButton.Visibility = Visibility.Visible;
        stopTimeButton.Visibility = Visibility.Collapsed;
        resetTimeButton.Visibility= Visibility.Visible;
    }

    private void resetTimeButton_Click(object sender, RoutedEventArgs e)
    {
        Timer.Reset();
        timerTextBlock.Text = Timer.Elapsed.ToString(@"hh\:mm\:ss");
        hoursTextBox.Text = string.Empty;

        startTimeButton.Visibility = Visibility.Visible;
        stopTimeButton.Visibility = Visibility.Collapsed;
        resetTimeButton.Visibility = Visibility.Visible;
    }

    private void clientDropDown_SelectionChanged(
            object sender, 
            SelectionChangedEventArgs e)
    {
        if (clientDropDown.SelectedItem is null)
            return;

        SetFormVisibility(true);
        ClearFormData();
    }
}
