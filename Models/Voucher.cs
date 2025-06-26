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

    [SQLite.Ignore]
    public string ClientFullName { get; set; }

    [SQLite.Ignore]
    public string RouteDisplayName { get; set; }

    [SQLite.Ignore]
    public decimal RoutePrice { get; set; }

    [SQLite.Ignore]
    public decimal TotalPrice => Math.Round(RoutePrice * Quantity * (1 - DiscountPercent), 2);

    [SQLite.Ignore]
    public int DiscountPercentDisplay => (int)(DiscountPercent * 100);
}
