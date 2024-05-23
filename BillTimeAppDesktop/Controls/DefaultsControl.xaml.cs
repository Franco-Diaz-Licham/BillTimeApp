namespace BillTimeAppDesktop.Controls;

public partial class DefaultsControl : UserControl
{
    public ISqliteData Data { get; }

    public DefaultsControl(ISqliteData data)
    {
        Data = data;
        InitializeComponent();
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        LoadDefaultsFromDatabase();
    }

    private void LoadDefaultsFromDatabase()
    {
        var model = Data.GetDefaults();

        if(model is null)
        {
            hourlyRateTextBox.Text = "0";
            preBillCheckBox.IsChecked = true;
            hasCutOffCheckBox.IsChecked = false;
            cutOffTextBox.Text = "0";
            minimumHoursTextBox.Text = "0.25";
            billingIncrementTextBox.Text = "0.25";
            roundUpAfterXMinutesTextBox.Text = "0";
        }
        else
        {
            hourlyRateTextBox.Text = model.HourlyRate.ToString();
            preBillCheckBox.IsChecked = model.PreBill;
            hasCutOffCheckBox.IsChecked = model.HasCutOff;
            cutOffTextBox.Text = model.CutOff.ToString();
            minimumHoursTextBox.Text = model.MinimumHours.ToString();
            billingIncrementTextBox.Text = model.BillingIncrement.ToString();
            roundUpAfterXMinutesTextBox.Text = model.RoundUpAfterXMinutes.ToString();
        }
    }

    private void submitForm_Click(
            object sender, 
            RoutedEventArgs e)
    {
        var form = ValidateForm();

        if (form.isValid is false)
        {
            MessageBox.Show("Invalid form...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Data.SaveDefaults(form.model!);
        MessageBox.Show("Saved...", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private (bool isValid, DefaultsModel? model) ValidateForm()
    {
        var hourlyValid = double.TryParse(hourlyRateTextBox.Text, out double hourlyRate);
        var cutOffValid = int.TryParse(cutOffTextBox.Text, out int cutOff);
        var minHoursValid = double.TryParse(minimumHoursTextBox.Text, out double mininumHours);
        var billingValid = double.TryParse(billingIncrementTextBox.Text, out double billingIncrement);
        var roundValid = int.TryParse(roundUpAfterXMinutesTextBox.Text, out int roundUpAfterXMinutes);

        if(hourlyValid is false)
            return (false, null);
        if(cutOffValid is false) 
            return (false, null);
        if(minHoursValid is false) 
            return (false, null);
        if (billingValid is false) 
            return (false, null);
        if(roundValid is false) 
            return (false, null);

        DefaultsModel output = new()
        {
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
}
