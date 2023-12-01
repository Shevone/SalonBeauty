namespace SalonBeauty.Models;

public class SalonBeauty
{
    // Всего в салоне у нас 3 стиллиста
    private IStylist<Hairstyle> hairdresser = new Stylist<Hairstyle>("Иван"); // Парикмахер
    private IStylist<Manicure> manicurist = new Stylist<Manicure>("Анна"); // Маникюрщица
    private IStylist<SpaProcedure> spaExpert = new Stylist<SpaProcedure>("Аркадий"); // Эксперт по спа процедурам
    private IStylist<Pedicure> pedicureExpert = new Stylist<Pedicure>("Ирина"); // Эксперт по педикюру

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
    // Метод который решает каким образом будет происходить сортировка
    public void SortServices(ServiceSortType serviceSortType)
    {
        // Объявляем переменную, которая будет содердать в себе - метод сортировки
        Func<Service, Service, bool> compareFunc;
        // Далее проходися по входному параметру
        // в зависимости от того по какому параметру выбрано сортировать данные - выбираем опрделенный метод сравнения
        // из наших статических методов, определенных в классе Service
        switch (serviceSortType)
        {
            case ServiceSortType.Name :
                compareFunc = Service.CompareByName;
                break;
            case ServiceSortType.Price:
                compareFunc = Service.CompareByPrice;
                break;
            case ServiceSortType.OrderCount:
                compareFunc = Service.CompareByOrderCount;
                break;
            default:
                // Елси что то не то передали то просто выходим
                return;
        }
        // Далее мы вызываем метод сортировки и всех специалистов
        hairdresser.SortServices(compareFunc);
        manicurist.SortServices(compareFunc);
        pedicureExpert.SortServices(compareFunc);
        spaExpert.SortServices(compareFunc);
        // Теперь услуги, записанные у специалистов отсортированны по выбраному параметру
        
    }
}
// Перечисление - тип сортировки
public enum ServiceSortType
{
    Name,
    Price,
    OrderCount,
}

