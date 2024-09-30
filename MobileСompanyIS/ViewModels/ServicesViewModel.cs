using MobileСompanyIS.Models;
using MobileСompanyIS.Services;
using MobileСompanyIS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MobileСompanyIS.ViewModels
{
    public class ServicesViewModel : INotifyPropertyChanged
    {
        private DataService _dataService = new DataService();

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

        public ICommand AddServiceCommand { get; }
        public ICommand EditServiceCommand { get; }
        public ICommand DeleteServiceCommand { get; }

        public ServicesViewModel()
        {
            Services = new ObservableCollection<Service>(_dataService.LoadServices());

            AddServiceCommand = new RelayCommand(AddService);
            EditServiceCommand = new RelayCommand(EditService, CanEditService);
            DeleteServiceCommand = new RelayCommand(DeleteService, CanDeleteService);

            Services.CollectionChanged += Services_CollectionChanged;
        }

        private void AddService()
        {
            var addWindow = new ServiceEditWindow();
            var addViewModel = new ServiceEditViewModel();

            addWindow.DataContext = addViewModel;

            if (addWindow.ShowDialog() == true)
            {
                int id = 0;
                if (Services.Count > 0)
                {
                    id = Services.Last().ID + 1;
                }
                addViewModel.Service.ID = id;

                Services.Add(addViewModel.Service);
            }
        }

        private void EditService()
        {
            if (SelectedService != null)
            {
                var editWindow = new ServiceEditWindow();
                var editViewModel = new ServiceEditViewModel(SelectedService);

                editWindow.DataContext = editViewModel;

                if (editWindow.ShowDialog() == true)
                {
                    // Обновить данные в списке
                    SelectedService.Name = editViewModel.Service.Name;
                    SelectedService.Type = editViewModel.Service.Type;
                    SelectedService.Cost = editViewModel.Service.Cost;

                    _dataService.SaveServices(new List<Service>(Services));
                }
            }
        }

        private bool CanEditService() => SelectedService != null;

        private void DeleteService()
        {
            if (SelectedService != null)
            {
                // Показать окно подтверждения удаления
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот Сервис?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Services.Remove(SelectedService);
                    SelectedService = null;
                }
            }
        }

        private bool CanDeleteService() => SelectedService != null;

        private void Services_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _dataService.SaveServices(new List<Service>(Services));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
