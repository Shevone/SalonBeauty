namespace SalonBeauty.Models;

public class SalonBeauty
{
    // Всего в салоне у нас 3 стиллиста
    private Stylist<Hairstyle> hairdresser = new Stylist<Hairstyle>("Иван"); // Парикмахер
    private Stylist<Manicure> manicurist = new Stylist<Manicure>("Анна"); // Маникюрщица
    private Stylist<SpaProcedure> spaExpert = new Stylist<SpaProcedure>("Аркадий"); // Эксперт по спа процедурам
    private Stylist<Pedicure> pedicureExpert = new Stylist<Pedicure>("Ирина"); // Эксперт по педикюру

    // свойство, которое переводит информацию об экспертах и услугах в строку и возвращает
    public string StylistsSting
    {
        get
        {
            string resString = $"Услуги парикмахера\n{hairdresser.DisplayInfo()}\n\n" +
                               $"Услуги маникюрщика\n{manicurist.DisplayInfo()} \n\n" +
                               $"Улуги эксперат по спа\n{spaExpert.DisplayInfo()}\n\n" +
                               $"Услуги эксперта по педиюру\n{pedicureExpert.DisplayInfo()}\n\n";
            return resString;
        }
    }
    // Возвращаем то,как выглядит список услуг
    
    // =====
    private  List<Service> _services; // Список предоставляемых услуг
    public List<Service> Services => new(_services); // Свойство для того чтобы из вне класса можно было обратиться
    // отправяем именно new(), чтобы работать уже с новым объектом
    
    private List<Order> _orders; // Список заказов
    public List<Order> Orders => new(_orders); // Свойство для того чтобы из вне класса можно было обратиться
    // Свойство, которое суммирует цены каждого заказа
    public double OrderSumPrice
    {
        get
        {
            double sum = 0;
            foreach (Order order in _orders)
            {
                sum += order.SumPrice;
            }

            return sum;
        }
    }
    // отправяем именно new(), чтобы работать уже с новым объектом

    public SalonBeauty()
    {
        _services = new List<Service>();
        _orders = new List<Order>();
    }
    // Метод по добавлению в салон новых услуг
    public string AddService(Service newService)
    {
        // Смотрим, каким типом является поступившая улуга, и в зависимости от этого
        // Добавляем эту услугу одному из наших спецаилистов
        switch (newService)
        {
            case Hairstyle hairstyle:
                hairdresser.Add(hairstyle);
                break;
            case Manicure manicure:
                manicurist.Add(manicure);
                break;
            case SpaProcedure spaProcedure:
                spaExpert.Add(spaProcedure);
                break;
            case Pedicure pedicure :
                pedicureExpert.Add(pedicure);
                break;
        }
        string message = "Добавлена новая услуга" + newService.DisplayInfo(); // Полиморфный вызов
        _services.Add(newService); // Добавляем в список улуг
        return message;
    }
    public void AddNewOrder(Order appointment)
    {
        _orders.Add(appointment);
    }
}