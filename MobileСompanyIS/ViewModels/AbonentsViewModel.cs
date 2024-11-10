using Bogus;
using MobileСompanyIS.Models;
using MobileСompanyIS.Services;
using MobileСompanyIS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MobileСompanyIS.ViewModels
{
    public class AbonentsViewModel : INotifyPropertyChanged
    {
        private BalanceReplenishmentSimulator _replenishmentSimulator;
        private ExpenseSimulator _expenseSimulator;

        CancellationTokenSource _cancellationTokenSource;

        private DataService _dataService = new DataService();

        private Abonent _selectedAbonent;

        public ObservableCollection<Abonent> Abonents { get; set; }
        public ObservableCollection<Tariff> Tariffs { get; set; }

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
        public ICommand GenerateAbonentsCommand { get; }
        public ICommand ClearCommand { get; }

        public AbonentsViewModel()
        {
            Abonents = new ObservableCollection<Abonent>(_dataService.LoadAbonents());
            Tariffs = new ObservableCollection<Tariff>(_dataService.LoadTariffs());

            AddAbonentCommand = new RelayCommand(AddAbonent);
            EditAbonentCommand = new RelayCommand(EditAbonent, CanEditAbonent);
            DeleteAbonentCommand = new RelayCommand(DeleteAbonent, CanDeleteAbonent);
            GenerateAbonentsCommand = new RelayCommand(GenerateAbonents);
            ClearCommand = new RelayCommand(Clear);

            Abonents.CollectionChanged += Abonents_CollectionChanged;
        }

        private void Abonents_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _dataService.SaveAbonents(new List<Abonent>(Abonents));
        }

        private void GenerateAbonents()
        {
            foreach (var abonent in GenerateRandomAbonents(10))
            {
                Abonents.Add(abonent);
            }
        }

        private List<Abonent> GenerateRandomAbonents(int count)
        {
            var id = 0;
            if (Abonents.Count > 0)
            {
                id = Abonents.Last().ID + 1;
            }

            var abonentFaker = new Faker<Abonent>()
                .RuleFor(a => a.ID, f => id++) // ID по порядку
                .RuleFor(a => a.FullName, f => f.Name.FullName()) // Случайное имя абонента
                .RuleFor(a => a.PhoneNumber, f => f.Phone.PhoneNumber("+7 (###) ###-##-##")) // Случайный номер телефона
                .RuleFor(a => a.Balance, f => Math.Round(f.Random.Decimal(0m, 1000.0m), 2)) // Случайный баланс
                .RuleFor(a => a.RegistrationDate, f => f.Date.Past(5)) // Случайная дата регистрации за последние 5 лет
                .RuleFor(a => a.Status, f => f.PickRandom(new[] { "Активен", "Заблокирован"})) // Случайный статус
                .RuleFor(a => a.Tariff, f => f.PickRandom(_dataService.LoadTariffs())); // Случайный тариф из переданного списка

            return abonentFaker.Generate(count);
        }

        private void Clear()
        {
            Abonents.Clear();
            _dataService.SaveAbonents(Abonents.ToList());
        }

        private void AddAbonent()
        {
            var addWindow = new AbonentEditWindow();
            var addViewModel = new AbonentEditViewModel();

            addWindow.DataContext = addViewModel;

            if (addWindow.ShowDialog() == true)
            {
                int id = 0;
                if (Abonents.Count > 0) 
                {
                    id = Abonents.Last().ID + 1;
                }
                addViewModel.Abonent.ID = id;
                addViewModel.Abonent.RegistrationDate = DateTime.Now;
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
                    SelectedAbonent.FullName = editViewModel.Abonent.FullName;
                    SelectedAbonent.PhoneNumber = editViewModel.Abonent.PhoneNumber;
                    SelectedAbonent.Balance = editViewModel.Abonent.Balance;
                    SelectedAbonent.Status = editViewModel.Abonent.Status;
                    SelectedAbonent.Tariff = editViewModel.Abonent.Tariff;

                    _dataService.SaveAbonents(new List<Abonent>(Abonents));

                    //StopSimulation();
                    //StartSimulation();
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
