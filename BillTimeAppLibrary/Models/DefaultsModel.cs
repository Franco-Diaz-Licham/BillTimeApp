namespace BillTimeAppLibrary.Models;

public class DefaultsModel
{
    public int Id { get; set; }
    public double? HourlyRate { get; set; }
    public bool PreBill { get; set; }
    public bool HasCutOff { get; set; }
    public bool CutOff { get; set; }
    public double MininumHours { get; set; }
    public double BillingIncrement { get; set; }
    public bool RoundUpAfterXMinutes { get; set; }
}
