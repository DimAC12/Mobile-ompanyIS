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
    public class TariffsViewModel : INotifyPropertyChanged
    {
        private Tariff _selectedTariff;

        public ObservableCollection<Tariff> Tariffs { get; set; }

        public Tariff SelectedTariff
        {
            get => _selectedTariff;
            set
            {
                _selectedTariff = value;
                OnPropertyChanged(nameof(SelectedTariff));
            }
        }

        public TariffsViewModel()
        {
            // Пример данных
            Tariffs = new ObservableCollection<Tariff>
        {
            new Tariff { Name = "Эконом", CostPerMinute = 1.5m, CostPerSms = 0.5m, CostPerMb = 2.0m },
            new Tariff { Name = "Бизнес", CostPerMinute = 2.0m, CostPerSms = 1.0m, CostPerMb = 1.5m }
        };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
