using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TouristApp.Models;
using TouristApp.Data;
using TouristApp.Helpers;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace TouristApp.ViewModels
{
    public class VouchersViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;
        public ObservableCollection<Voucher> Vouchers { get; }
        public ICommand LoadVouchersCommand { get; }
        public ICommand AddVoucherCommand { get; }
        public ICommand EditVoucherCommand { get; }
        public ICommand DeleteVoucherCommand { get; }

        public VouchersViewModel(DatabaseService db)
        {
            _db = db;
            Vouchers = new ObservableCollection<Voucher>();
            LoadVouchersCommand = new RelayCommand(async _ => await LoadVouchers());
            AddVoucherCommand = new RelayCommand(_ => Shell.Current.GoToAsync("/VoucherDetail"));
            EditVoucherCommand = new RelayCommand(async v => await Shell.Current.GoToAsync($"/VoucherDetail?voucherId={((Voucher)v).Id}"));
            DeleteVoucherCommand = new RelayCommand(async v => {
                await _db.DeleteVoucherAsync((Voucher)v);
                await LoadVouchers();
            });
        }

        public async Task LoadVouchers()
        {
            Vouchers.Clear();
            var list = await _db.GetVouchersAsync();
            foreach (var v in list)
                Vouchers.Add(v);
        }
    }
}
