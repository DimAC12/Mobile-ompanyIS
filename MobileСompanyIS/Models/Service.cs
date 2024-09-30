using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileСompanyIS.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Голосовая связь, SMS, Интернет и т.д.
        public decimal Cost { get; set; }
    }
}
