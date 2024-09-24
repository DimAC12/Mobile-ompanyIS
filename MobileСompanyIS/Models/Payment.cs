using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileСompanyIS.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public Abonent Abonent { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // Наличный, Безналичный
    }
}
