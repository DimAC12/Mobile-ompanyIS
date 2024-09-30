using MobileСompanyIS.Models;
using MobileСompanyIS.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MobileСompanyIS.ViewModels
{
    public class ServiceEditViewModel : INotifyPropertyChanged
    {
        public Service Service { get; set; }
        public ICommand SaveCommand { get; }

        public ServiceEditViewModel()
        {
            Service = new Service();
            SaveCommand = new RelayCommand(Save);
        }

        public ServiceEditViewModel(Service service)
        {
            Service = service;
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
