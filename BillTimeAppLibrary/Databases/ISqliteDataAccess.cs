namespace BillTimeAppLibrary.Databases;

public interface ISqliteDataAccess
{
    List<T> Get<T, U>(string sql, U par);
    void Save<T>(string sql, T par);
}