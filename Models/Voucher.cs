using SQLite;
using System;

namespace TouristApp.Models;

public class Voucher
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int RouteId { get; set; }
    public int ClientId { get; set; }

    public DateTime DepartureDate { get; set; }

    public int Quantity { get; set; }

    public decimal DiscountPercent { get; set; }
}
