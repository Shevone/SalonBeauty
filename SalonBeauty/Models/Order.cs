using System.Text;

namespace SalonBeauty.Models;

// Клас - заказ
public class Order
{
    private static int _nextTd = 1;
    private int Id;
    private DateTime _dateTime; // Время когда оформлен заказ
    private List<Service> _services; // Список желаемых услуг
    
    // Свойство, которое будет возвращает сумму заказа
    public double SumPrice
    {
        get
        {
            double sum = 0;
            // Проходимся по элементно и суммирум цену всех услуг в заказе
            foreach (Service service in _services)
            {
                sum += service.Price;
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
        _services = services;
        // Записываем +1 в каждуйю услугу к заказу
        foreach (Service service in services)
        {
            service.OrderOneService();
        }
    }
    // Метод определяющий то как выгляит в виде строки
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder($"Заказ №{Id}. Время заказа {_dateTime}. Цена заказа {SumPrice}\n");
        foreach (Service service in _services)
        {
            sb.Append(service.DisplayInfo() + "\n");
        }

        return sb.ToString();
    }
}