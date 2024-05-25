namespace BillTimeAppLibrary.Models;

public class PaymentModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public ClientModel? Client { get; set; }
    public double Hours { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }

    public string DateOnly => Date.ToString("d");
    public string DisplayValue => $"{DateOnly} - ${Amount}";
}
