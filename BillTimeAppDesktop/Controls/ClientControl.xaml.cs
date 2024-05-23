namespace BillTimeAppDesktop.Controls;

public partial class ClientControl : UserControl
{
    public ISqliteData Data { get; }
    private bool FormVisible { get; set; } = false;
    private bool NewClientEntry { get; set; } = false;

    public ClientControl(
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
        SetFormVisibility(false);
        clientDropDown.ItemsSource = Data.GetClients();
    }
    #endregion

    #region form logic
    private void SetFormVisibility(
            bool visible)
    {
        if (visible is true)
        {
            FormVisible = true;
            nameAndRateStackPanel.Visibility = Visibility.Visible;
            emailStackPanel.Visibility = Visibility.Visible;
            checkBoxesStackPanel.Visibility = Visibility.Visible;
            roundUpStackPanel.Visibility = Visibility.Visible;
            incrementsStackPanel.Visibility = Visibility.Visible;
            submitForm.Visibility = Visibility.Visible;
        }
        else
        {
            FormVisible = false;
            nameAndRateStackPanel.Visibility = Visibility.Hidden;
            emailStackPanel.Visibility = Visibility.Hidden;
            checkBoxesStackPanel.Visibility = Visibility.Hidden;
            roundUpStackPanel.Visibility = Visibility.Hidden;
            incrementsStackPanel.Visibility = Visibility.Hidden;
            submitForm.Visibility = Visibility.Hidden;
        }

    }

    private void ClearFormData()
    {
        nameTextBox.Text = string.Empty;
        emailTextBox.Text = string.Empty;
        hourlyRateTextBox.Text = string.Empty;
        preBillCheckBox.IsChecked = false;
        hasCutOffCheckBox.IsChecked = false;
        cutOffTextBox.Text = string.Empty;
        billingIncrementTextBox.Text = string.Empty;
        minimumHoursTextBox.Text = string.Empty;
        roundUpAfterXMinutesTextBox.Text = string.Empty;
    }

    private void SetFormData(
            ClientModel model)
    {
        nameTextBox.Text = model.Name;
        emailTextBox.Text = model.Email;
        hourlyRateTextBox.Text = model.HourlyRate.ToString();
        preBillCheckBox.IsChecked = model.PreBill;
        hasCutOffCheckBox.IsChecked = model.HasCutOff;
        cutOffTextBox.Text = model.CutOff.ToString();
        billingIncrementTextBox.Text = model.BillingIncrement.ToString();
        minimumHoursTextBox.Text = model.MinimumHours.ToString();
        roundUpAfterXMinutesTextBox.Text = model.RoundUpAfterXMinutes.ToString();
    }

    private (bool isValid, ClientModel? model) Validate()
    {
        var hourlyValid = double.TryParse(hourlyRateTextBox.Text, out double hourlyRate);
        var cutOffValid = int.TryParse(cutOffTextBox.Text, out int cutOff);
        var minHoursValid = double.TryParse(minimumHoursTextBox.Text, out double mininumHours);
        var billingValid = double.TryParse(billingIncrementTextBox.Text, out double billingIncrement);
        var roundValid = int.TryParse(roundUpAfterXMinutesTextBox.Text, out int roundUpAfterXMinutes);

        if (hourlyValid is false)
            return (false, null);
        if (cutOffValid is false)
            return (false, null);
        if (minHoursValid is false)
            return (false, null);
        if (billingValid is false)
            return (false, null);
        if (roundValid is false)
            return (false, null);
        if (string.IsNullOrEmpty(nameTextBox.Text) is true)
            return (false, null);
        if (string.IsNullOrEmpty(emailTextBox.Text) is true)
            return (false, null);

        ClientModel output = new()
        {
            Id = clientDropDown.SelectedValue is null ? 0 : (int)clientDropDown.SelectedValue,
            Name = nameTextBox.Text,
            Email = emailTextBox.Text,
            HourlyRate = hourlyRate,
            PreBill = preBillCheckBox.IsChecked!.Value,
            HasCutOff = hasCutOffCheckBox.IsChecked!.Value,
            CutOff = cutOff,
            MinimumHours = mininumHours,
            BillingIncrement = billingIncrement,
            RoundUpAfterXMinutes = roundUpAfterXMinutes
        };

        return (true, output);
    }
    #endregion

    #region component actions
    private void editButton_Click(
            object sender, 
            RoutedEventArgs e)
    {
        NewClientEntry = false;

        var model = (ClientModel)clientDropDown.SelectedItem;

        if (model is null)
        {
            MessageBox.Show("Select client first...", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        SetFormData(model);
        SetFormVisibility(true);
    }

    private void clientDropDown_SelectionChanged(
            object sender, 
            SelectionChangedEventArgs e)
    {
        if (clientDropDown.SelectedItem is null)
            return;
        if (FormVisible is false)
            return;

        editButton_Click(new(), new());
    }

    private void newButton_Click(
            object sender,
            RoutedEventArgs e)
    {
        NewClientEntry = true;
        ClearFormData();
        SetFormVisibility(true);
        clientDropDown.SelectedItem = null;
        var model = LoadDefaults();

        if (model is null)
            return;

        SetFormData(model);
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

        Data.SaveCient(model!);
        MessageBox.Show("Saved...", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

        clientDropDown.ItemsSource = Data.GetClients();

        if (NewClientEntry is false)
        {
            clientDropDown.SelectedValue = model!.Id;
            return;
        }

        SetFormVisibility(false);
        ClearFormData();
    }
    #endregion

    #region auxilliary logic
    private ClientModel? LoadDefaults()
    {
        var defaults = Data.GetDefaults();

        if (defaults is null)
            return null;

        ClientModel model = new()
        {
            Name = string.Empty,
            Email = string.Empty,
            HourlyRate = defaults.HourlyRate,
            PreBill = defaults.PreBill,
            HasCutOff = defaults.HasCutOff,
            CutOff = defaults.CutOff,
            MinimumHours = defaults.MinimumHours,
            BillingIncrement = defaults.BillingIncrement,
            RoundUpAfterXMinutes = defaults.RoundUpAfterXMinutes
        };

        return model;
    }
    #endregion
}
