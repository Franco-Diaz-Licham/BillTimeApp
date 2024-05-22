namespace BillTimeAppLibrary.Models;

public class WorkModel
{
    public int Id { get; set; }
    public double Hours { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DateEntered { get; set; }
    public bool Paid { get; set; }
    public int PaymentId { get; set; }
    public PaymentModel? Payment { get; set; }
}
