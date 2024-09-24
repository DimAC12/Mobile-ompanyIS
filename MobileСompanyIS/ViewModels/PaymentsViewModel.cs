using MobileСompanyIS.Models;
using MobileСompanyIS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileСompanyIS.ViewModels
{
    public class PaymentsViewModel : INotifyPropertyChanged
    {
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

        public ICommand AddPaymentCommand { get; }
        public ICommand DeletePaymentCommand { get; }

        public PaymentsViewModel()
        {
            Payments = new ObservableCollection<Payment>();
            AddPaymentCommand = new RelayCommand(AddPayment);
            DeletePaymentCommand = new RelayCommand(DeletePayment, CanDeletePayment);
        }

        private void AddPayment()
        {
            // Логика добавления платежа
        }

        private void DeletePayment()
        {
            if (SelectedPayment != null)
            {
                Payments.Remove(SelectedPayment);
                SelectedPayment = null;
            }
        }

        private bool CanDeletePayment() => SelectedPayment != null;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
