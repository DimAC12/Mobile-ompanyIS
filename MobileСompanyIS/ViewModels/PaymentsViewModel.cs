using MobileСompanyIS.Models;
using MobileСompanyIS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileСompanyIS.ViewModels
{
    public class PaymentsViewModel : INotifyPropertyChanged
    {
        private DataService dataService;

        private Payment _selectedPayment;

        public ObservableCollection<Payment> Payments { get; set; }

        public Payment SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                _selectedPayment = value;
                OnPropertyChanged(nameof(SelectedPayment));
            }
        }

        public ICommand ClearCommand { get; }

        public PaymentsViewModel()
        {
            ClearCommand = new RelayCommand(Clear);

            dataService = new DataService();

            Payments = new ObservableCollection<Payment>(dataService.LoadPayments());

            Payments.CollectionChanged += Payments_CollectionChanged;
        }

        private void Clear()
        {
            Payments.Clear();
            dataService.SavePayments(Payments.ToList());
        }

        private void Payments_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            dataService.SavePayments(new List<Payment>(Payments));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
