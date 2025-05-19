using SQLite;

namespace TouristApp.Models;

public class Route
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Country { get; set; }
    public string Climate { get; set; }

    public int DurationWeeks { get; set; }

    public string Hotel { get; set; }

    public decimal Price { get; set; }

    [Ignore]
    public string DisplayName => $"{Country}, {Hotel}, {DurationWeeks} нед.";
}
