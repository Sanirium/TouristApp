using SQLite;

namespace TouristApp.Models;

public class Client
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }

    public string Address { get; set; }
    public string Phone { get; set; }

    [SQLite.Ignore]
    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
}
