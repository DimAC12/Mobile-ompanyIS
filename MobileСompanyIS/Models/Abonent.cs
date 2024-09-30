using System.ComponentModel;

namespace MobileСompanyIS.Models
{
    public class Abonent : INotifyPropertyChanged
    {
        public int ID { get; set; }
        private string _fullName;
        public string FullName 
        { 
            get => _fullName; 
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
        public string SubscriberType { get; set; }
        private string _phoneNumber;
        public string PhoneNumber 
        { 
            get => _phoneNumber; 
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        public int TariffID { get; set; }
        private decimal _balance;
        public decimal Balance 
        { 
            get => _balance; 
            set
            {
                _balance = value;
                OnPropertyChanged(nameof(Balance));
            }
        }
        public DateTime RegistrationDate { get; set; }
        private string _status;
        public string Status 
        { 
            get => _status; 
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        } 
        public string PassportDetails { get; set; }

        public Tariff _tariff { get; set; }
        public Tariff Tariff
        {
            get => _tariff;
            set
            {
                _tariff = value;
                OnPropertyChanged(nameof(Tariff));
            }
        }

        public string FormattedRegistrationDate
        {
            get => RegistrationDate.ToString("dd.MM.yyyy HH:mm:ss");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
