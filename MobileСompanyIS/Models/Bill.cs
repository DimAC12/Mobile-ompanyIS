namespace MobileСompanyIS.Models
{
    public class Bill
    {
        public int ID { get; set; }

        public Abonent Abonent { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        //private Abonent _abonent;
        //private DateTime _date;
        //private decimal _amount;
        //private string _status;

        //public Abonent Abonent 
        //{
        //    get => _abonent;
        //    set
        //    {
        //        _abonent = value;
        //        OnPropertyChanged(nameof(Abonent));
        //    }
        //}
        //public DateTime Date 
        //{ 
        //    get => _date; 
        //    set
        //    {
        //        _date = value;
        //        OnPropertyChanged(nameof(Date));
        //    }
        //}
        //public decimal Amount 
        //{
        //    get => _amount; 
        //    set
        //    {
        //        _amount = value;
        //        OnPropertyChanged(nameof(Amount));
        //    }
        //}
        //public string Status
        //{
        //    get => _status;
        //    set
        //    {
        //        _status = value;
        //        OnPropertyChanged(nameof(Status));
        //    }
        //}



        //public string FormattedDate
        //{
        //    get => _date.ToString("dd.MM.yyyy HH:mm:ss");
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}



    }
}
