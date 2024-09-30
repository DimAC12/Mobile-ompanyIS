using MobileСompanyIS.Services;
using MobileСompanyIS.Views;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace MobileСompanyIS.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand OpenAbonentsWindowCommand { get; }
        public ICommand OpenTariffsWindowCommand { get; }
        public ICommand OpenServicesWindowCommand { get; }
        public ICommand OpenBillsWindowCommand { get; }
        public ICommand OpenPaymentsWindowCommand { get; }

        public MainViewModel()
        {
            OpenAbonentsWindowCommand = new RelayCommand(OpenAbonentsWindow);
            OpenTariffsWindowCommand = new RelayCommand(OpenTariffsWindow);
            OpenServicesWindowCommand = new RelayCommand(OpenServicesWindow);
            OpenBillsWindowCommand = new RelayCommand(OpenBillsWindow);
            OpenPaymentsWindowCommand = new RelayCommand(OpenPaymentsWindow);
        }

        private void OpenAbonentsWindow()
        {
            var window = new AbonentsWindow();
            window.Show();
        }

        private void OpenTariffsWindow()
        {
            var window = new TariffsWindow();
            window.Show();
        }

        private void OpenServicesWindow()
        {
            var window = new ServicesWindow();
            window.Show();
        }

        private void OpenBillsWindow()
        {
            var window = new BillsWindow();
            window.Show();
        }

        private void OpenPaymentsWindow()
        {
            var window = new PaymentsWindow();
            window.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
