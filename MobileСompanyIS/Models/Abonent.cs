using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileСompanyIS.Models
{
    public class Abonent
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string PhoneNumber { get; set; }
        public Tariff TariffPlan { get; set; }
        public decimal Balance { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Status { get; set; } // "Активен", "Заблокирован"
    }
}
