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
    public class AbonentsViewModel : INotifyPropertyChanged
    {
        private Abonent _selectedAbonent;

        public ObservableCollection<Abonent> Abonents { get; set; }

        public Abonent SelectedAbonent
        {
            get => _selectedAbonent;
            set
            {
                _selectedAbonent = value;
                OnPropertyChanged(nameof(SelectedAbonent));
            }
        }

        public AbonentsViewModel()
        {
            // Пример данных
            Abonents = new ObservableCollection<Abonent>
        {
            new Abonent { FIO = "Иван Иванов", PhoneNumber = "1234567890", Balance = 100m, Status = "Активен" },
            new Abonent { FIO = "Мария Смирнова", PhoneNumber = "0987654321", Balance = 50m, Status = "Неактивен" }
        };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
