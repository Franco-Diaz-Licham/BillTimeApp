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
    public bool CutOff { get; set; }
    public double MininumHours { get; set; }
    public double BillingIncrement { get; set; }
    public bool RoundUpAfterXMinutes { get; set; }
}
