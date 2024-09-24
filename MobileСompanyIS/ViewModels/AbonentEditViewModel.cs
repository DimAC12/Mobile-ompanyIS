using MobileСompanyIS.Models;
using MobileСompanyIS.Services;
using MobileСompanyIS.Views;
using System;
using System.Collections.Generic;
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
        public Abonent Abonent { get; set; }
        public ICommand SaveCommand { get; }

        public AbonentEditViewModel()
        {
            Abonent = new Abonent();
            SaveCommand = new RelayCommand(Save);
        }

        public AbonentEditViewModel(Abonent abonent)
        {
            Abonent = abonent;
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
