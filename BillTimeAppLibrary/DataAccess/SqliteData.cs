namespace BillTimeAppLibrary.DataAccess;

public class SqliteData : ISqliteData
{
    private ISqliteDataAccess Db;

    public SqliteData(ISqliteDataAccess db)
    {
        Db = db;
    }

    public void SaveCient(
            ClientModel model)
    {
        string sql;
        object client = new
        {
            model.Id,
            model.Name,
            model.HourlyRate,
            model.Email,
            model.PreBill,
            model.HasCutOff,
            model.CutOff,
            model.MinimumHours,
            model.BillingIncrement,
            model.RoundUpAfterXMinutes,
        };

        if(model.Id is 0)
            SqliteQueriesDictionary.Client.TryGetValue(ClientDataAccess.Create, out sql!);
        else
            SqliteQueriesDictionary.Client.TryGetValue(ClientDataAccess.Update, out sql!);

        Db.Save(sql!, client);
    }

    public List<ClientModel> GetClients()
    {
        SqliteQueriesDictionary.Client.TryGetValue(ClientDataAccess.Get, out var sql);
        var rows = Db.Get<ClientModel, object>(sql!, new());
        return rows;
    }

    public DefaultsModel? GetDefaults()
    {
        SqliteQueriesDictionary.Defaults.TryGetValue(DefaultsDataAccess.Get, out var sql);
        var row = Db.Get<DefaultsModel, object>(sql!, new()).FirstOrDefault();
        return row;
    }

    public void SaveDefaults(
            DefaultsModel model)
    {
        SqliteQueriesDictionary.Defaults.TryGetValue(DefaultsDataAccess.Get, out var sql);
        var row = Db.Get<DefaultsModel, object>(sql!, new()).FirstOrDefault();

        object defaults = new
        {
            Id = row is not null ? row.Id : 0,
            model.HourlyRate,
            model.PreBill,
            model.HasCutOff,
            model.CutOff,
            model.MinimumHours,
            model.BillingIncrement,
            model.RoundUpAfterXMinutes,
        };
        
        if (row is null)
            SqliteQueriesDictionary.Defaults.TryGetValue(DefaultsDataAccess.Create, out sql);
        else
            SqliteQueriesDictionary.Defaults.TryGetValue(DefaultsDataAccess.Update, out sql);

        Db.Save(sql!, defaults);
    }

    public List<PaymentModel> GetPayments(
            int? clientId = null)
    {
        string sql;
        object p = new();

        if (clientId is null)
        {
            SqliteQueriesDictionary.Payment.TryGetValue(PaymentDataAccess.Get, out sql!);
        }
        else
        {
            p = new { ClientId = clientId };
            SqliteQueriesDictionary.Payment.TryGetValue(PaymentDataAccess.GetByClientId, out sql!);
        }

        var row = Db.Get<PaymentModel, object>(sql!, p).ToList();
        return row;
    }

    public void SavePayment(
            PaymentModel model)
    {
        string sql;
        object client = new
        {
            model.Id,
            model.ClientId,
            model.Hours,
            model.Amount,
            model.Date,
        };

        if (model.Id is 0)
            SqliteQueriesDictionary.Payment.TryGetValue(PaymentDataAccess.Create, out sql!);
        else
            SqliteQueriesDictionary.Payment.TryGetValue(PaymentDataAccess.Update, out sql!);

        Db.Save(sql!, client);
    }    
    
    public void SaveWork(
            WorkModel model)
    {
        string sql;
        object client = new
        {
            model.Id,
            model.ClientId,
            model.Hours,
            model.Title,
            model.Description,
            model.DateEntered,
            model.PaymentId,
            model.Paid
        };

        if (model.Id is 0)
            SqliteQueriesDictionary.Work.TryGetValue(WorkDataAccess.Create, out sql!);
        else
            SqliteQueriesDictionary.Work.TryGetValue(WorkDataAccess.Update, out sql!);

        Db.Save(sql!, client);
    }

    public List<WorkModel> GetWork(
            int? clientId = null)
    {
        string sql;
        object p = new();

        if (clientId is null)
        {
            SqliteQueriesDictionary.Work.TryGetValue(WorkDataAccess.Get, out sql!);
        }
        else
        {
            p = new { ClientId = clientId };
            SqliteQueriesDictionary.Work.TryGetValue(WorkDataAccess.GetByClientId, out sql!);
        }

        var row = Db.Get<WorkModel, object>(sql!, p).ToList();
        return row;
    }

}
