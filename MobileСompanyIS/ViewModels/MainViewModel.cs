using MobileСompanyIS.Models;
using MobileСompanyIS.Services;
using MobileСompanyIS.Views;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace MobileСompanyIS.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private BalanceReplenishmentSimulator balanceReplenishmentSimulator;
        private ExpenseSimulator expenseSimulator;

        private CancellationTokenSource _cancellationTokenSource;

        private AbonentsViewModel aboutViewModel;
        private PaymentsViewModel paymentsViewModel;
        private BillsViewModel billsViewModel;
        private TariffsViewModel tariffsViewModel;

        public ICommand OpenAbonentsWindowCommand { get; }
        public ICommand OpenTariffsWindowCommand { get; }
        public ICommand OpenBillsWindowCommand { get; }
        public ICommand OpenPaymentsWindowCommand { get; }
        public ICommand StartSimulationCommand { get; }
        public ICommand StopSimulationCommand { get; }

        public MainViewModel()
        {
            OpenAbonentsWindowCommand = new RelayCommand(OpenAbonentsWindow);
            OpenTariffsWindowCommand = new RelayCommand(OpenTariffsWindow);
            OpenBillsWindowCommand = new RelayCommand(OpenBillsWindow);
            OpenPaymentsWindowCommand = new RelayCommand(OpenPaymentsWindow);

            StartSimulationCommand = new RelayCommand(StartSimulation);
            StopSimulationCommand = new RelayCommand(StopSimulation);

            aboutViewModel = new AbonentsViewModel();
            tariffsViewModel = new TariffsViewModel();
            billsViewModel = new BillsViewModel();
            paymentsViewModel = new PaymentsViewModel();

        }

        private void OpenAbonentsWindow()
        {
            var window = new AbonentsWindow();
            window.DataContext = aboutViewModel;
            window.Show();
            
        }

        private void OpenTariffsWindow()
        {
            var window = new TariffsWindow();
            window.DataContext = tariffsViewModel;
            window.Show();
        }

        private void OpenBillsWindow()
        {
            var window = new BillsWindow();
            window.DataContext = billsViewModel;
            window.Show();
        }

        private void OpenPaymentsWindow()
        {
            var window = new PaymentsWindow();
            window.DataContext = paymentsViewModel;
            window.Show();
        }

        private void StartSimulation()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            balanceReplenishmentSimulator = new BalanceReplenishmentSimulator(
                aboutViewModel.Abonents.ToList(), 
                paymentsViewModel.Payments);

            expenseSimulator = new ExpenseSimulator(
                aboutViewModel.Abonents.ToList(), 
                tariffsViewModel.Tariffs.ToList(),
                billsViewModel.Bills);
            
            Task.Run(() => expenseSimulator.SimulateExpenses(_cancellationTokenSource.Token));
            Task.Run(() => balanceReplenishmentSimulator.SimulateReplenishment(_cancellationTokenSource.Token));
        }

        private void StopSimulation()
        {
            _cancellationTokenSource?.Cancel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
