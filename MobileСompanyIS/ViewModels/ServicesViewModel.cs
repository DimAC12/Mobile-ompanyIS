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
    public class ServicesViewModel : INotifyPropertyChanged
    {
        private Service _selectedService;

        public ObservableCollection<Service> Services { get; set; }

        public Service SelectedService
        {
            get => _selectedService;
            set
            {
                _selectedService = value;
                OnPropertyChanged(nameof(SelectedService));
            }
        }

        public ServicesViewModel()
        {
            // Пример данных
            Services = new ObservableCollection<Service>
        {
            new Service { Name = "Голосовая связь", Type = "Голос", Cost = 10m },
            new Service { Name = "SMS", Type = "Сообщения", Cost = 1m },
            new Service { Name = "Интернет", Type = "Данные", Cost = 5m }
        };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
