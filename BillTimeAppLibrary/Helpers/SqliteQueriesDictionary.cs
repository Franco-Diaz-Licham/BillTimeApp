namespace BillTimeAppLibrary.Helpers;

public static class SqliteQueriesDictionary
{
    public static Dictionary<ClientDataAccess, string> Client { get; set; } = new()
    {
        {
            ClientDataAccess.Create, 
            @"INSERT INTO Client(Name, HourlyRate, Email, PreBill, HasCutOff, 
                                CutOff, MinimumHours, BillingIncrement, RoundUpAfterXMinutes)
            VALUES(@Name, @HourlyRate, @Email, @PreBill, @HasCutOff, 
                    @CutOff, @MinimumHours, @BillingIncrement, @RoundUpAfterXMinutes)"
        },
        {
            ClientDataAccess.Update, 
            @"UPDATE Client
            SET Name = @Name, HourlyRate = @HourlyRate, Email = @Email, PreBill = @PreBill, 
                HasCutOff = @HasCutOff, CutOff = @CutOff, MinimumHours = @MinimumHours, 
                BillingIncrement = @BillingIncrement, RoundUpAfterXMinutes = @RoundUpAfterXMinutes
            WHERE Id = @Id"
        },
        {
            ClientDataAccess.Get, 
            @"SELECT* 
            FROM Client"
        }
    };

    public static Dictionary<DefaultsDataAccess, string> Defaults { get; set; } = new()
    {
        {
            DefaultsDataAccess.Get, 
            @"SELECT* 
            FROM Defaults
            LIMIT 1"
        },
        {
            DefaultsDataAccess.Create, 
            @"INSERT INTO Defaults(HourlyRate, PreBill, HasCutOff, CutOff, 
                                   MinimumHours, BillingIncrement, RoundUpAfterXMinutes)
            VALUES(@HourlyRate, @PreBill, @HasCutOff, @CutOff, 
                   @MinimumHours, @BillingIncrement, @RoundUpAfterXMinutes)"
        },
        {
            DefaultsDataAccess.Update, 
            @"UPDATE Defaults
            SET HourlyRate = @HourlyRate, PreBill = @PreBill, HasCutOff = @HasCutOff, 
                CutOff = @CutOff, MinimumHours = @MinimumHours, BillingIncrement = @BillingIncrement, 
                RoundUpAfterXMinutes = @RoundUpAfterXMinutes
            WHERE Id = @Id"
        }
    };

    public static Dictionary<PaymentDataAccess, string> Payment { get; set; } = new()
    {
        {
            PaymentDataAccess.Get, 
            @"SELECT* 
            FROM Payment"
        },
        {
            PaymentDataAccess.GetByClientId, 
            @"SELECT* 
            FROM Payment
            WHERE ClientId = @ClientId"
        },
        {
            PaymentDataAccess.Create, 
            @"INSERT INTO Payment(ClientId, Hours, Amount, Date)
            VALUES(@ClientId, @Hours, @Amount, @Date)"
        },
        {
            PaymentDataAccess.Update, 
            @"UPDATE Payment 
            SET ClientId = ClientId, Hours = @Hours, Amount = @Amount
            WHERE Id = @Id"
        }
    };
}
