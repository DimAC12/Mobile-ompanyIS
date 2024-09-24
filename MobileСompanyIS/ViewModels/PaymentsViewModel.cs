using MobileСompanyIS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public PaymentsViewModel()
        {
            // Пример данных
            Payments = new ObservableCollection<Payment>
        {
            new Payment { Abonent = new Abonent { FIO = "Иван Иванов" }, PaymentDate = DateTime.Now, Amount = 100m, PaymentMethod = "Безналичный" },
            new Payment { Abonent = new Abonent { FIO = "Мария Смирнова" }, PaymentDate = DateTime.Now, Amount = 50m, PaymentMethod = "Наличный" }
        };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
