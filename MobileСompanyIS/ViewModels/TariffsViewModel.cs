using Bogus;
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
    public class TariffsViewModel : INotifyPropertyChanged
    {
        private DataService _dataService = new DataService();

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

        public ICommand AddTariffCommand { get; }
        public ICommand EditTariffCommand { get; }
        public ICommand DeleteTariffCommand { get; }
        public ICommand GenerateTariffsCommand { get; }
        public ICommand ClearCommand { get; }

        public TariffsViewModel()
        {
            Tariffs = new ObservableCollection<Tariff>(_dataService.LoadTariffs());

            AddTariffCommand = new RelayCommand(AddTariff);
            EditTariffCommand = new RelayCommand(EditTariff, CanEditTariff);
            DeleteTariffCommand = new RelayCommand(DeleteTariff, CanDeleteTariff);
            GenerateTariffsCommand = new RelayCommand(GenerateTariffs);
            ClearCommand = new RelayCommand(Clear);

            Tariffs.CollectionChanged += Tariffs_CollectionChanged;
        }

        private void Tariffs_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            _dataService.SaveTariffs(new List<Tariff>(Tariffs));
        }

        private void GenerateTariffs()
        {
            //GenerateRandomTariffs(10);
            foreach (var tariff in GenerateRandomTariffs(10))
            {
                Tariffs.Add(tariff);
            }
        }

        private List<Tariff> GenerateRandomTariffs(int count)
        {
            var id = 0;
            if (Tariffs.Count > 0)
            {
                id = Tariffs.Last().ID + 1;
            }

            var tariffFaker = new Faker<Tariff>("ru")
                .RuleFor(t => t.ID, f => id++)
                .RuleFor(t => t.Name, f => f.Random.Word()) // Случайное название тарифа
                .RuleFor(t => t.CostPerMinute, f => Math.Round(f.Random.Decimal(0.1m, 5.0m), 2)) // Округление до 2 знаков после запятой
                .RuleFor(t => t.CostPerSms, f => Math.Round(f.Random.Decimal(0.01m, 1.0m), 2)) // Округление до 2 знаков после запятой
                .RuleFor(t => t.CostPerMb, f => Math.Round(f.Random.Decimal(0.05m, 2.0m), 2));

            return tariffFaker.Generate(count);
        }

        private void Clear()
        {
            Tariffs.Clear();
            _dataService.SaveTariffs(Tariffs.ToList());
        }

        private void AddTariff()
        {
            var addWindow = new TariffEditWindow();
            var addViewModel = new TariffEditViewModel();

            addWindow.DataContext = addViewModel;

            if (addWindow.ShowDialog() == true)
            {
                int id = 0;
                if (Tariffs.Count > 0)
                {
                    id = Tariffs.Last().ID + 1;
                }
                addViewModel.Tariff.ID = id;

                Tariffs.Add(addViewModel.Tariff);
            }
        }

        private void EditTariff()
        {
            if (SelectedTariff != null)
            {
                var editWindow = new TariffEditWindow();
                var editViewModel = new TariffEditViewModel(SelectedTariff);

                editWindow.DataContext = editViewModel;

                if (editWindow.ShowDialog() == true)
                {
                    // Обновить данные в списке
                    SelectedTariff.Name = editViewModel.Tariff.Name;
                    SelectedTariff.CostPerMinute = editViewModel.Tariff.CostPerMinute;
                    SelectedTariff.CostPerSms = editViewModel.Tariff.CostPerSms;
                    SelectedTariff.CostPerMb = editViewModel.Tariff.CostPerSms;

                    _dataService.SaveTariffs(new List<Tariff>(Tariffs));
                }
            }
        }

        private bool CanEditTariff() => SelectedTariff != null;

        private void DeleteTariff()
        {
            if (SelectedTariff != null)
            {
                // Показать окно подтверждения удаления
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот тариф?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Tariffs.Remove(SelectedTariff); // Удаление абонента из коллекции
                    SelectedTariff = null; // Сброс выделенного абонента
                }
            }
        }

        private bool CanDeleteTariff() => SelectedTariff != null;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
