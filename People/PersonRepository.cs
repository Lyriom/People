using People.Models;
using SQLite;

namespace People;

public class PersonRepository
{
    string _dbPath;
    public string StatusMessage { get; set; }

    private SQLiteConnection conn;

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    private void Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<Person>();
    }
    
    public void AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            Init();
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            result = conn.Insert(new Person { Name = name });

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }

    public List<Person> GetAllPeople()
    {
        try
        {
            Init();
            StatusMessage = string.Format("Success");
            return conn.Table<Person>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<Person>();
    }

    public void DeletePerson(int Id)
    {
        int result = 0;

        try
        {
            Init();

            if (conn != null)
                result = conn.Delete<Person>(Id);
            StatusMessage = string.Format("{0} record(s) deleted (Id: {1})", result, Id);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to delete data. {0}", ex.Message);
        }
    }
}
