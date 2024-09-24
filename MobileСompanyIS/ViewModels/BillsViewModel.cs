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
    public class BillsViewModel : INotifyPropertyChanged
    {
        private Bill _selectedBill;

        public ObservableCollection<Bill> Bills { get; set; }

        public Bill SelectedBill
        {
            get => _selectedBill;
            set
            {
                _selectedBill = value;
                OnPropertyChanged(nameof(SelectedBill));
            }
        }

        public BillsViewModel()
        {
            // Пример данных
            Bills = new ObservableCollection<Bill>
        {
            new Bill { Abonent = new Abonent { FIO = "Иван Иванов" }, Date = DateTime.Now, Amount = 100m, Status = "Оплачен" },
            new Bill { Abonent = new Abonent { FIO = "Мария Смирнова" }, Date = DateTime.Now, Amount = 50m, Status = "Не оплачен" }
        };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
