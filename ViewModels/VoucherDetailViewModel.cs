using System;
using System.Threading.Tasks;
using TouristApp.Models;
using TouristApp.Data;
using TouristApp.Helpers;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace TouristApp.ViewModels
{

    [QueryProperty(nameof(VoucherId), "voucherId")]
    public class VoucherDetailViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;
        private int _voucherId;
        public int VoucherId
        {
            get => _voucherId;
            set
            {
                _voucherId = value;
                _ = LoadVoucher();
            }
        }

        private Voucher _voucher;
        public Voucher Voucher
        {
            get => _voucher;
            set => SetProperty(ref _voucher, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public VoucherDetailViewModel(DatabaseService db)
        {
            _db = db;
            Voucher = new Voucher();
            SaveCommand = new RelayCommand(async _ => await Save());
            CancelCommand = new RelayCommand(_ => Shell.Current.GoToAsync(".."));
        }

        private async Task LoadVoucher()
        {
            if (VoucherId == 0) return;
            Voucher = await _db.GetVoucherAsync(VoucherId) ?? new Voucher();
        }

        private async Task Save()
        {
            await _db.SaveVoucherAsync(Voucher);
            await Shell.Current.GoToAsync("//Vouchers");
        }
    }
}
