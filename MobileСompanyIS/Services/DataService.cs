using MobileСompanyIS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileСompanyIS.Services
{
    public class DataService
    {
        private string dataDirectory = "Data";  // Папка для хранения файлов

        public DataService()
        {
            // Создаем папку для хранения файлов, если её нет
            if (!Directory.Exists(dataDirectory))
                Directory.CreateDirectory(dataDirectory);
        }

        // Общий метод для сохранения данных
        private void SaveToFile<T>(string fileName, List<T> data)
        {
            string filePath = Path.Combine(dataDirectory, fileName);
            var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        // Общий метод для загрузки данных
        private List<T> LoadFromFile<T>(string fileName)
        {
            string filePath = Path.Combine(dataDirectory, fileName);
            if (!File.Exists(filePath))
                return new List<T>();

            var jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(jsonData);
        }

        // Автосохранение и автозагрузка для всех сущностей

        public void SaveAbonents(List<Abonent> abonents) => SaveToFile("abonents.json", abonents);
        public List<Abonent> LoadAbonents() => LoadFromFile<Abonent>("abonents.json");

        public void SaveBills(List<Bill> bills) => SaveToFile("bills.json", bills);
        public List<Bill> LoadBills() => LoadFromFile<Bill>("bills.json");

        public void SavePayments(List<Payment> payments) => SaveToFile("payments.json", payments);
        public List<Payment> LoadPayments() => LoadFromFile<Payment>("payments.json");

        public void SaveTariffs(List<Tariff> tariffs) => SaveToFile("tariffs.json", tariffs);
        public List<Tariff> LoadTariffs() => LoadFromFile<Tariff>("tariffs.json");
    }
}
