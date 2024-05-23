namespace BillTimeAppLibrary.Databases;

public class SqliteDataAccess : ISqliteDataAccess
{
    private string Cnx { get; set; } = string.Empty;

    public SqliteDataAccess(IConfiguration config)
    {
        Cnx = config.GetConnectionString("Sqlite")!;
    }

    public List<T> Get<T, U>(
            string sql,
            U par)
    {
        using IDbConnection cnt = new SQLiteConnection(Cnx);
        var rows = cnt.Query<T>(sql, par);

        if (rows is null)
            return new();
        
        return rows.ToList();
    }

    public void Save<T>(
            string sql,
            T par)
    {
        using IDbConnection cnt = new SQLiteConnection(Cnx);
        cnt.Execute(sql, par);
    }
}
