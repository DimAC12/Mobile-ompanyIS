using MobileСompanyIS.Models;
using MobileСompanyIS.Services;
using MobileСompanyIS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MobileСompanyIS.ViewModels
{
    public class AbonentEditViewModel : INotifyPropertyChanged
    {
        private DataService _dataService = new DataService();

        public Abonent Abonent { get; set; }
        public ObservableCollection<Tariff> Tariffs { get; set; }
        private Tariff _selectedTariff;

        public Tariff SelectedTariff
        {
            get => _selectedTariff;
            set
            {
                _selectedTariff = value;
                OnPropertyChanged(nameof(SelectedTariff));
            }
        }

        public ICommand SaveCommand { get; }

        public AbonentEditViewModel()
        {
            Abonent = new Abonent();
            SaveCommand = new RelayCommand(Save);
            Tariffs = new ObservableCollection<Tariff>(_dataService.LoadTariffs());
        }

        public AbonentEditViewModel(Abonent abonent)
        {
            Abonent = abonent;
            SaveCommand = new RelayCommand(Save);
            Tariffs = new ObservableCollection<Tariff>(_dataService.LoadTariffs());

            if (abonent.Tariff != null)
            {
                Tariff foundTariff = Tariffs.FirstOrDefault(a => a.Name == abonent.Tariff.Name);
                SelectedTariff = foundTariff;
            }
        }

        private void Save()
        {
            Abonent.Tariff = SelectedTariff;

            var window = App.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            window.DialogResult = true;
            window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
