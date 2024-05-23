namespace BillTimeAppLibrary.Models;

public class ClientModel
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public double? HourlyRate { get; set; }
    [Required]
    public string? Email { get; set; }
    public bool PreBill { get; set; }
    public bool HasCutOff { get; set; }
    public int CutOff { get; set; }
    public double MinimumHours { get; set; }
    public double BillingIncrement { get; set; }
    public int RoundUpAfterXMinutes { get; set; }
}
