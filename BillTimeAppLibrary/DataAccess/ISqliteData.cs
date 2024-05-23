namespace BillTimeAppLibrary.DataAccess;

public interface ISqliteData
{
    List<ClientModel> GetClients();
    DefaultsModel? GetDefaults();
    List<PaymentModel> GetPayments(int? id = null);
    void SaveCient(ClientModel model);
    void SaveDefaults(DefaultsModel model);
    void SavePayment(PaymentModel model);
}