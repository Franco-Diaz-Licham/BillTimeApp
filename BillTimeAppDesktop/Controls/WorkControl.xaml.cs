namespace BillTimeAppDesktop.Controls;

public partial class WorkControl : UserControl
{
    public WorkControl(
            ISqliteData data)
    {
        Data = data;
        InitializeComponent();
    }

    public ISqliteData Data { get; }

    protected override void OnInitialized(
            EventArgs e)
    {
        base.OnInitialized(e);
        SetFormVisibility(false);
        SetPaymentVisibility(false);
        clientDropDown.ItemsSource = Data.GetClients();
    }

    private void SetFormVisibility(
            bool visible)
    {
        if (visible is true)
        {
            titleStackPanel.Visibility = Visibility.Visible;
            descriptionStackPanel.Visibility = Visibility.Visible;
            paymentStackPanel.Visibility = Visibility.Visible;
            submitForm.Visibility = Visibility.Visible;
        }
        else
        {
            titleStackPanel.Visibility = Visibility.Hidden;
            descriptionStackPanel.Visibility = Visibility.Hidden;
            paymentStackPanel.Visibility = Visibility.Hidden;
            submitForm.Visibility = Visibility.Hidden;
        }

    }

    private void ClearFormData()
    {
        titleTextBox.Text = string.Empty;
        hoursTextBox.Text = string.Empty;
        descriptionTextBox.Text = string.Empty;
        paidCheckBox.IsChecked = false;
        dateDropDown.SelectedItem = null;
        dateDropDown.SelectedValue = null;

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
        if(paymentDropDown.SelectedValue is null)
            return(false, null);
        if(dateDropDown.SelectedValue is null)
            return (false, null);

        WorkModel model = new()
        {
            Id = (int)dateDropDown.SelectedValue,
            ClientId = client.Id,
            Hours = hours,
            Title = titleTextBox.Text,
            Description = descriptionTextBox.Text,
            Paid = paidCheckBox.IsChecked!.Value,
            PaymentId = (int)paymentDropDown.SelectedValue,
            DateEntered = DateTime.Today,
        };

        return (true, model);
    }

    private void SetFormData(
            WorkModel model)
    {
        titleTextBox.Text = model.Title;
        descriptionTextBox.Text = model.Description;
        hoursTextBox.Text = model.Hours.ToString();
        paidCheckBox.IsChecked = model.Paid;
        paymentDropDown.SelectedValue = model.PaymentId;
    }

    private void SetPaymentVisibility(
        bool visible)
    {
        if (visible is true)
            dateStackPanel.Visibility = Visibility.Visible;
        else
            dateStackPanel.Visibility = Visibility.Hidden;
    }

    private void clientDropDown_SelectionChanged(
            object sender, 
            SelectionChangedEventArgs e)
    {
        if (clientDropDown.SelectedItem is null)
            return;

        SetPaymentVisibility(true);
        SetFormVisibility(false);
        ClearFormData();

        var client = (ClientModel)clientDropDown.SelectedItem;
        dateDropDown.ItemsSource = Data.GetWork(client.Id);
        paymentDropDown.ItemsSource = Data.GetPayments(client.Id);
    }

    private void dateDropDown_SelectionChanged(
            object sender, 
            SelectionChangedEventArgs e)
    {
        if (dateDropDown.SelectedItem is null)
            return;

        SetFormVisibility(true);
        var workDate = (WorkModel)dateDropDown.SelectedItem;

        if (workDate is null)
            return;

        SetFormData(workDate);
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

        var client = (ClientModel)clientDropDown.SelectedItem;
        dateDropDown.ItemsSource = Data.GetWork(client.Id);
        dateDropDown.SelectedValue = model!.Id;
    }
}
