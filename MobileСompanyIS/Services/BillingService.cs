using MobileСompanyIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MobileСompanyIS.Services
{
    public class BillingService
    {
        private readonly DataService _dataService;

        public BillingService(DataService dataService)
        {
            _dataService = dataService;
        }

        public void MakeCall(Abonent caller, Abonent receiver, int durationSeconds)
        {
            var tariff = caller.Tariff;
            decimal callCost = tariff.CostPerMinute * (durationSeconds / 60); // примерный расчет

            if (caller.Balance >= callCost)
            {
                caller.Balance -= callCost;
                _dataService.SaveAbonents(new List<Abonent> { caller }); // обновляем баланс

                // Создаем новый счет 
                var bill = new Bill
                {
                    Abonent = caller,
                    Date = DateTime.Now,
                    Amount = callCost,
                    Description = $"Звонок абоненту {receiver.PhoneNumber}",
                    Status = "Не оплачен" // Можно добавить статус счета
                };

                // Сохраняем счет 
                var bills = _dataService.LoadBills();
                int id = bills.Count > 0 ? bills.Max(b => b.ID) + 1 : 1; // генерация ID
                bill.ID = id;
                bills.Add(bill);
                _dataService.SaveBills(bills);
            }
            else
            {
                // Обработка недостатка средств
                // Варианты:
                // 1. Выбросить исключение:
                // throw new InvalidOperationException("Недостаточно средств на балансе.");

                // 2. Вернуть bool значение (успех/неудача):
                // return false;

                // 3. Вывести сообщение пользователю:
                MessageBox.Show("Недостаточно средств на балансе для совершения звонка.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
