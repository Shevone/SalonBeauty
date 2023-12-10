using System.Text;

namespace SalonBeauty.Models;

// Клас - заказ
public class Order
{
    private static int _nextTd = 1;
    public int Id { get; }
    private DateTime _dateTime; // Время когда оформлен заказ
    private Dictionary<Service,int> _services; // Список желаемых услуг

    // Свойство, которое будет возвращает сумму заказа
    public double SumPrice
    {
        get
        {
            double sum = 0;
            // Проходимся по элементно и суммирум цену всех услуг в заказе
            foreach (KeyValuePair<Service, int> keyValuePair in _services)
            {
                sum += keyValuePair.Key.Price * keyValuePair.Value;
            }
            return sum;
        }
    }

    public Order(List<Service> services)
    {
        // При создании заказа записываем туда текущее время
        Id = _nextTd;
        _nextTd++;
        _dateTime = DateTime.Now;
        _services = new Dictionary<Service, int>();
        // Записываем +1 в каждуйю услугу к заказу
        foreach (Service service in services)
        {
            if (!_services.ContainsKey(service))
            {
                _services.Add(service,0);
            }

            _services[service]++;
            service.OrderOneService();
        }
    }
    // Метод определяющий то как выгляит в виде строки
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder($"Заказ №{Id}. Время заказа {_dateTime}. Цена заказа {SumPrice}\n");
        foreach (KeyValuePair<Service, int> keyValuePair in _services)
        {
            sb.Append(keyValuePair.Key.DisplayInfo() + $" | В данном заказе : {keyValuePair.Value}\n");
        }

        return sb.ToString();
    }
}