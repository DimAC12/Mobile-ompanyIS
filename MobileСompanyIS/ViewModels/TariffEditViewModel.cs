using MobileСompanyIS.Models;
using MobileСompanyIS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MobileСompanyIS.ViewModels
{
    public class TariffEditViewModel : INotifyPropertyChanged
    {
        public Tariff Tariff { get; set; }
        public ICommand SaveCommand { get; }

        public TariffEditViewModel()
        {
            Tariff = new Tariff();
            SaveCommand = new RelayCommand(Save);
        }

        public TariffEditViewModel(Tariff tariff)
        {
            Tariff = tariff;
            SaveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            // Закрыть окно и передать результат
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
