using SQLite;
using TouristApp.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TouristApp.Data;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _db;

    public DatabaseService()
    {
        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "touristapp.db3");
        _db = new SQLiteAsyncConnection(dbPath);
    }

    public async Task InitializeAsync()
    {
        await _db.CreateTableAsync<Client>();
        await _db.CreateTableAsync<Route>();
        await _db.CreateTableAsync<Voucher>();
    }

    public Task<List<Client>> GetClientsAsync() =>
        _db.Table<Client>().ToListAsync();

    public Task<Client> GetClientAsync(int id) =>
        _db.Table<Client>().Where(c => c.Id == id).FirstOrDefaultAsync();

    public Task<int> SaveClientAsync(Client client) =>
        client.Id == 0 ? _db.InsertAsync(client) : _db.UpdateAsync(client);

    public Task<int> DeleteClientAsync(Client client) =>
        _db.DeleteAsync(client);

    public Task<List<Route>> GetRoutesAsync() =>
        _db.Table<Route>().ToListAsync();

    public Task<Route> GetRouteAsync(int id) =>
        _db.Table<Route>().Where(r => r.Id == id).FirstOrDefaultAsync();

    public Task<int> SaveRouteAsync(Route route) =>
        route.Id == 0 ? _db.InsertAsync(route) : _db.UpdateAsync(route);

    public Task<int> DeleteRouteAsync(Route route) =>
        _db.DeleteAsync(route);

    public Task<List<Voucher>> GetVouchersAsync() =>
        _db.Table<Voucher>().ToListAsync();

    public Task<Voucher> GetVoucherAsync(int id) =>
        _db.Table<Voucher>().Where(v => v.Id == id).FirstOrDefaultAsync();

    public Task<int> SaveVoucherAsync(Voucher vouvher) =>
        vouvher.Id == 0 ? _db.InsertAsync(vouvher) : _db.UpdateAsync(vouvher);

    public Task<int> DeleteVoucherAsync(Voucher voucher) =>
        _db.DeleteAsync(voucher);
}
