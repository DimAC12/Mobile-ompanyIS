using MobileСompanyIS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileСompanyIS.Services
{
    public class ExpenseSimulator
    {
        private DataService dataService = new DataService();
        private readonly Random _random = new Random();
        private readonly List<Abonent> _subscribers;
        private readonly List<Tariff> _tariffs;
        private ObservableCollection<Bill> _bills;

        public ExpenseSimulator(List<Abonent> subscribers, List<Tariff> tariffs, ObservableCollection<Bill> bills)
        {
            _subscribers = subscribers;
            _tariffs = tariffs;
            _bills = bills;
        }

        public async Task SimulateExpenses(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_random.Next(1000, 5000)); // Пауза между расходами

                var subscriber = _subscribers[_random.Next(_subscribers.Count)];

                if (subscriber.Tariff != null)
                {
                    var tariff = _tariffs.FirstOrDefault(t => t.ID == subscriber.Tariff.ID);
                    if (tariff != null)
                    {
                        // Симуляция использования услуг
                        var minutesUsed = _random.Next(1, 10); // использовано минут
                        var smsUsed = _random.Next(1, 5); // использовано SMS
                        var mbUsed = _random.Next(1, 100); // использовано MB

                        var totalExpense = (minutesUsed * tariff.CostPerMinute) +
                                           (smsUsed * tariff.CostPerSms) +
                                           (mbUsed * tariff.CostPerMb);

                        if (subscriber.Balance >= totalExpense)
                        {
                            subscriber.Balance -= totalExpense;
                            dataService.SaveAbonents(_subscribers);


                            Bill bill = new Bill()
                            {
                                Abonent = subscriber,
                                Date = DateTime.Now,
                                Amount = totalExpense,
                            };

                            App.Current.Dispatcher.Invoke(() =>
                            {
                                _bills.Add(bill);
                            });

                            Debug.WriteLine($"Абонент {subscriber.FullName} использовал {minutesUsed} минут, {smsUsed} SMS, {mbUsed} MB, потрачено: {totalExpense} руб.");
                        }
                        else
                        {
                            Debug.WriteLine($"Недостаточно средств у абонента {subscriber.FullName} для расхода на {totalExpense} руб.");
                        }
                    }
                }
            }
        }
    }
}
