using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileСompanyIS.Models
{
    public class Tariff
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal CostPerMinute { get; set; }
        public decimal CostPerSms { get; set; }
        public decimal CostPerMb { get; set; }
    }
}
