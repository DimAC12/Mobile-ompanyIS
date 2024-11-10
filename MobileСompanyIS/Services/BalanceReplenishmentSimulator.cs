using Bogus;
using MobileСompanyIS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MobileСompanyIS.Services
{
    public class BalanceReplenishmentSimulator
    {
        private DataService dataService = new DataService();

        private readonly Random _random = new Random();
        private readonly Faker _faker = new Faker("ru");
        private readonly List<Abonent> _subscribers;
        private ObservableCollection<Payment> _payments;

        public BalanceReplenishmentSimulator(List<Abonent> subscribers, ObservableCollection<Payment> payments)
        {
            _subscribers = subscribers;
            _payments = payments;
        }

        public async Task SimulateReplenishment(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(_random.Next(1000, 5000)); // Пауза между симуляциями

                var subscriber = _subscribers[_random.Next(_subscribers.Count)];
                var amount = _random.Next(100, 1000);

                // Создание объекта пополнения баланса
                var replenishment = new Payment
                {
                    Abonent = subscriber,
                    Amount = amount,
                    PaymentDate = DateTime.Now
                };

                App.Current.Dispatcher.Invoke(() =>
                {
                    _payments.Add(replenishment);
                });

                var abonents = dataService.LoadAbonents();

                abonents.First(a => a.ID == replenishment.Abonent.ID).Balance += amount;
                dataService.SaveAbonents(abonents);

                subscriber.Balance += amount;

                // Вывод информации о пополнении (для тестирования)
                //Debug.WriteLine($"Абонент {subscriber.FullName} пополнил баланс на {amount} руб.");
            }
        }
    }
}
