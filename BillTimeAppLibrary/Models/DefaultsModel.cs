namespace BillTimeAppLibrary.Models;

public class DefaultsModel
{
    public int Id { get; set; }
    public double? HourlyRate { get; set; }
    public bool PreBill { get; set; }
    public bool HasCutOff { get; set; }
    public int CutOff { get; set; }
    public double MinimumHours { get; set; }
    public double BillingIncrement { get; set; }
    public int RoundUpAfterXMinutes { get; set; }
}
