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
    public class AbonentsViewModel : INotifyPropertyChanged
    {
        private DataService _dataService = new DataService();

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

        public ICommand AddAbonentCommand { get; }
        public ICommand EditAbonentCommand { get; }
        public ICommand DeleteAbonentCommand { get; }

        public AbonentsViewModel()
        {
            Abonents = new ObservableCollection<Abonent>(_dataService.LoadAbonents());

            AddAbonentCommand = new RelayCommand(AddAbonent);
            EditAbonentCommand = new RelayCommand(EditAbonent, CanEditAbonent);
            DeleteAbonentCommand = new RelayCommand(DeleteAbonent, CanDeleteAbonent);

            Abonents.CollectionChanged += Abonents_CollectionChanged;
        }

        private void Abonents_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _dataService.SaveAbonents(new List<Abonent>(Abonents));
        }

        private void AddAbonent()
        {
            var addWindow = new AbonentEditWindow();
            var addViewModel = new AbonentEditViewModel();

            addWindow.DataContext = addViewModel;

            if (addWindow.ShowDialog() == true)
            {
                Abonents.Add(addViewModel.Abonent);
            }
        }

        private void EditAbonent()
        {
            if (SelectedAbonent != null)
            {
                var editWindow = new AbonentEditWindow();
                var editViewModel = new AbonentEditViewModel(SelectedAbonent);

                editWindow.DataContext = editViewModel;

                if (editWindow.ShowDialog() == true)
                {
                    // Обновить данные в списке
                    SelectedAbonent.FIO = editViewModel.Abonent.FIO;
                    SelectedAbonent.PhoneNumber = editViewModel.Abonent.PhoneNumber;
                    SelectedAbonent.Balance = editViewModel.Abonent.Balance;
                    SelectedAbonent.Status = editViewModel.Abonent.Status;
                }
            }
        }

        private bool CanEditAbonent() => SelectedAbonent != null;

        private void DeleteAbonent()
        {
            if (SelectedAbonent != null)
            {
                // Показать окно подтверждения удаления
                var result = MessageBox.Show("Вы уверены, что хотите удалить этого абонента?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Abonents.Remove(SelectedAbonent); // Удаление абонента из коллекции
                    SelectedAbonent = null; // Сброс выделенного абонента
                }
            }
        }

        private bool CanDeleteAbonent() => SelectedAbonent != null;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
