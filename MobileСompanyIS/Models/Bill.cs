using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileСompanyIS.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public Abonent Abonent { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } // Оплачен, Не оплачен
    }
}
