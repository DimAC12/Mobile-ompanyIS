using MobileСompanyIS.Models;
using MobileСompanyIS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileСompanyIS.ViewModels
{
    public class BillsViewModel : INotifyPropertyChanged
    {
        private DataService dataService;

        private Bill _selectedBill;

        public ObservableCollection<Bill> Bills { get; set; }

        public Bill SelectedBill
        {
            get => _selectedBill;
            set
            {
                _selectedBill = value;
                OnPropertyChanged(nameof(SelectedBill));
            }
        }

        public ICommand ClearCommand { get; }

        public BillsViewModel()
        {
            ClearCommand = new RelayCommand(Clear);

            dataService = new DataService();
            Bills = new ObservableCollection<Bill>(dataService.LoadBills());

            Bills.CollectionChanged += Bills_CollectionChanged;
        }

        private void Clear()
        {
            Bills.Clear();
            dataService.SaveBills(Bills.ToList());
        }

        private void Bills_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            dataService.SaveBills(new List<Bill>(Bills));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
