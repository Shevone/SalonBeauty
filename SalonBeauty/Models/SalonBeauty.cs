﻿namespace SalonBeauty.Models;

public class SalonBeauty
{
    // Всего в салоне у нас 3 стиллиста
    private Stylist<Hairstyle> hairdresser = new Stylist<Hairstyle>("Иван"); // Парикмахер
    private Stylist<Manicure> manicurist = new Stylist<Manicure>("Анна"); // Маникюрщица
    private Stylist<SpaProcedure> spaExpert = new Stylist<SpaProcedure>("Аркадий"); // Эксперт по спа процедурам
    
    private  List<Service> _services; // Список предоставляемых услуг
    public List<Service> Services => new(_services); // Свойство для того чтобы из вне класса можно было обратиться
    // отправяем именно new(), чтобы работать уже с новым объектом
    
    private List<Order> _orders; // Список заказов
    public List<Order> Orders => new(_orders); // Свойство для того чтобы из вне класса можно было обратиться
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