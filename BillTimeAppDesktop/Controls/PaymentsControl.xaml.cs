namespace BillTimeAppDesktop.Controls;

public partial class PaymentsControl : UserControl
{
    public ISqliteData Data { get; }
    private bool NewDateEntry { get; set; } = true;

    public PaymentsControl(
            ISqliteData data)
    {
        Data = data;
        InitializeComponent();
    }

    #region lifecycle methods
    protected override void OnInitialized(
            EventArgs e)
    {
        base.OnInitialized(e);
        clientDropDown.ItemsSource = Data.GetClients();
        SetFormVisibility(false);
    }
    #endregion

    #region form logic
    private void ClearFormData()
    {
        amountTextBox.Text = string.Empty;
        hoursTextBox.Text = string.Empty;
        dateDropDown.ItemsSource = null;
        dateDropDown.SelectedValue = null;
    }

    private void ResetForm()
    {
        ClearFormData();
        dateDropDown.ItemsSource = null;
        dateDropDown.SelectedValue = null;
    }

    private void SetFormData(
            PaymentModel model)
    {
        amountTextBox.Text = model.Amount.ToString();
        hoursTextBox.Text = model.Hours.ToString();
    }

    private void SetFormVisibility(
        bool visible)
    {
        if (visible is true)
        {
            hoursStackPanel.Visibility = Visibility.Visible;
            amountStackPanel.Visibility = Visibility.Visible;
            dateStackPanel.Visibility = Visibility.Visible;
            submitForm.Visibility = Visibility.Visible;
        }
        else
        {
            hoursStackPanel.Visibility = Visibility.Hidden;
            amountStackPanel.Visibility = Visibility.Hidden;
            dateStackPanel.Visibility = Visibility.Hidden;
            submitForm.Visibility = Visibility.Hidden;
        }
    }

    private (bool isValid, PaymentModel? model) Validate()
    {
        var hoursValid = double.TryParse(hoursTextBox.Text, out double hours);
        var amountValid = double.TryParse(amountTextBox.Text, out double amount);
        var client = (ClientModel)clientDropDown.SelectedItem;

        if (hoursValid is false)
            return (false, null);
        if(amountValid is false) 
            return (false,null);
        if(client is null)
            return (false,null);

        PaymentModel model = new()
        {
            Id = dateDropDown.SelectedValue is null ? 0 : (int)dateDropDown.SelectedValue,
            ClientId = client.Id,
            Hours = hours,
            Amount = amount,
            Date = DateTime.Today,
        };

        return (true, model);
    }
    #endregion

    #region component actions
    private void dateDropDown_SelectionChanged(
            object sender, 
            SelectionChangedEventArgs e)
    {
        if (dateDropDown.SelectedItem is null)
            return;

        NewDateEntry = false;
        var payment = (PaymentModel)dateDropDown.SelectedItem;

        if (payment is null)
            return;

        SetFormData(payment);
    }

    private void newButton_Click(
            object sender,
            RoutedEventArgs e)
    {
        NewDateEntry = true;
        ResetForm();
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

        Data.SavePayment(model!);
        MessageBox.Show("Saved...", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

        dateDropDown.ItemsSource = Data.GetPayments(model!.ClientId);

        if (NewDateEntry is false)
        {
            clientDropDown.SelectedValue = model!.Id;
            return;
        }

        ResetForm();
    }

    private void clientDropDown_Selected(
            object sender,
            RoutedEventArgs e)
    {
        if(clientDropDown.SelectedItem is null)
            return;

        SetFormVisibility(true);
        ResetForm();
        NewDateEntry = false;

        var client = (ClientModel)clientDropDown.SelectedItem;
        var payments = Data.GetPayments(client.Id);

        if (payments.Any() is false)
            return;

        dateDropDown.ItemsSource = payments;
    }
    #endregion
}
