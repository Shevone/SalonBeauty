﻿using System.Diagnostics.SymbolStore;
using SalonBeauty.Models;

namespace SalonBeauty;

class Program
{
    // Демонстрация ковариантности
    public static void CovDemo()
    {
        // Создаем нашего стилиста по волосам
        Stylist<Hairstyle> hairsyler = new Stylist<Hairstyle>("Вася");
        // Добавляем услугу
        hairsyler.Add(new Hairstyle("Стрижка", 50, true));
        // в данном слуае, работая с объектом болле конкретного типа из контрвариантного метода возращается наш кокнретный тип
        Hairstyle hairstyle1 = hairsyler.SomeCovarianceMethod("Стрижка");
        Console.WriteLine(hairstyle1?.DisplayInfo());
        // Присваем более обобщенно объекту от типа ICovarianceInterface<Service> объект боллее конкретного типа
        ICovarianceInterface<Service> hairst = hairsyler;
        // а работая с боллее оббщенным типом из ковариантного метода возвращается более обобщенный тип
        Service hairstyle = hairst.SomeCovarianceMethod("Стрижка");
        // Однако вызвав абстрактный метод, определенный в абстрактном класс, нам выведется информация имменно о Hairstyle
        Console.WriteLine(hairstyle?.DisplayInfo());

        Console.WriteLine("=================================");
        
        // То есть мы можем присвоить более общему типу ICovarianceInterface<Service>
        // объект более конкретного типа Stylist<Hairstyle> или ICovarianceInterface<Hairstyle>.
        ICovarianceInterface<Hairstyle> hairstyler2 = hairsyler;
        ICovarianceInterface<Service> hairstyler3 = hairstyler2;
        Service hairstyle2 = hairstyler2.SomeCovarianceMethod("Стрижка");
        Console.WriteLine(hairstyle2?.DisplayInfo());
        
        // Выведем все красиво и убуерм после считывания кнопки
        Console.WriteLine("=================================");
        Console.WriteLine("Демонстрация ковариантности завершена\nНажмите любую кнопку чтоб перейти к основной программе");
        Console.ReadKey();
        Console.Clear();
    }
// Коваринатный метод - возвращает объект с типом T
// Коваринтный интерфейс не может реализоваывать методы принимающие на вход тип Т

    private static Models.SalonBeauty Salon = new();

    public static void Main(string[] args)
    {
        // Вызов метода для заполнения салона
        FillTheSalon();
        // Вызов мтеода для демонстрации контрвариантности
        CovDemo();
        bool exitFlag = false;
        while (!exitFlag)
        {
            Console.Clear();
            Console.WriteLine("Введите цифру чтоб выбрать пункт меню\n" +
                              "1 - Создать услугу\n" +
                              "2 - Созадть заказ\n" +
                              "3 - Просмотр услгу(Мастеров)\n" +
                              "4 - Просмотр заказов\n" +
                              "5 - Выход");
            // Вопросики - если оставили пустю строку
            string index = Console.ReadLine() ?? "";
            switch (index)
            {
                default:
                    Console.WriteLine("Вы ничего не выбрали");
                    break;
                case "1":
                    // Пункт созданаие услуги
                    CreateService();
                    break;
                case "2":
                    // Пункт - создание заказа
                    CreateOrder();
                    break;
                case "3":
                    // Прсмотр списка услуг(Масттеров)
                    Console.WriteLine(Salon.StylistsSting);
                    break;
                case "4":
                    // Просмотр списка заказов
                    foreach (Order order in Salon.Orders)
                    {
                        Console.WriteLine(order);
                    }

                    Console.WriteLine($"Суммарная цена заказов : {Salon.OrderSumPrice}");
                    break;
                case "5":
                    // Выход
                    Console.WriteLine("Выбран выход");
                    exitFlag = true;
                    break;
            }

            Console.WriteLine("Чтобы продолжить нажмите любую кнопку");
            Console.ReadKey();
        }
    }

    private static void CreateService()
    {
        Console.Clear();
        Console.WriteLine("Введите цифру чтоб выбрать пункт меню\n" +
                          "1 - Создать услугу - работа с волосами\n" +
                          "2 - Созадть услугу - маникюр\n" +
                          "3 - Создать услугу - спа процедура\n" +
                          "4 - Создать услугу  - педикюр\n" +
                          "Для выхода введите люую строку\n");
        // Считываем название и цену
        string index = Console.ReadLine() ?? "";
        if (index != "1" && index != "2" && index != "3" && index != "4") return;
        Console.WriteLine("Введите название");
        string name = Console.ReadLine() ?? "";
        Console.WriteLine("Введите цену услуги");
        bool parseRes = Double.TryParse(Console.ReadLine() ?? "0", out double result);
        // Если что то ввели не правильно то заказничаваем создание
        if (name == "" || parseRes == false)
        {
            Console.WriteLine("^_^");
            return;
        }

        // в зависимости от того какой тип выбрали запрашаиваем доп данные
        // Затем создаем переменные - улсуг и добавляем в салон
        // Метод добавления в салон возвращает строк.
        // Выводим её на консоль
        string message;
        switch (index)
        {
            case "1":
                // Волосы
                Console.WriteLine("Нужнали мойка волос\n(введите 1 - если да)");
                string s = Console.ReadLine() ?? "";
                Hairstyle hairstyle = new Hairstyle(name, result, s == "1");
                message = Salon.AddService(hairstyle);
                break;
            case "2":
                // маникюр
                Console.WriteLine("Введите цвет маникюра");
                string color = Console.ReadLine() ?? "";
                Manicure manicure = new Manicure(name, result, color);
                message = Salon.AddService(manicure);
                break;
            case "3":
                // спа
                Console.WriteLine("Введите длительность процедуры");
                bool p = int.TryParse(Console.ReadLine() ?? "1", out int time);
                if (!p) time = 1;
                SpaProcedure spa = new SpaProcedure(name, result, time);
                message = Salon.AddService(spa);
                break;
            case "4":
                // Создать услуг педикюр
                Console.WriteLine("Введите тип педикюра");
                string pedicureType = Console.ReadLine() ?? "";
                Pedicure pedicure = new Pedicure(name, result, pedicureType);
                message = Salon.AddService(pedicure);
                break;
            default:
                message = "^_^";
                break;
        }

        Console.WriteLine(message);
    }

    private static void CreateOrder()
    {
        // Выводим список всех услуг
        Console.WriteLine("Введите id желаемых услуг черещ пробел");
        foreach (Service service in Salon.Services)
        {
            Console.WriteLine(service.DisplayInfo());
        }

        // Создаем список, для услуг которые выберут
        List<Service> services = new List<Service>();
        // Считываем строку
        string s = Console.ReadLine() ?? "0";
        // Записываем строку в массим, разделив через пробле
        // 1 2 3 превратится в ["1","2","3"]
        List<string> res = s.Split(" ").ToList();
        // проходимся по элементам пассива
        foreach (string item in res)
        {
            bool parseRes = int.TryParse(item, out int index);
            // пытаемся перевести в число( вдруг кто то ввел не число)
            if (parseRes)
            {
                // елс мы смогли перевести строку в число то ищем в ссписке услгу салона
                // такую услуг id которой равно нашему элементу массива
                // Это происзоди с помощьью функции FirstOrDefault, если элемент не найден то вернтся null
                Service? service = Salon.Services.FirstOrDefault(x => x.Id == index);
                if (service == null) continue;
                // и если нашлась усулга с таким id то добавляем её в заказ
                services.Add(service);
            }
        }

        // Елси наш заказ не пуст, то добаляем его в салон
        if (services.Count != 0)
        {
            Order order = new Order(services);
            Salon.AddNewOrder(order);
            Console.WriteLine("Заказ создан ");
            return;
        }

        Console.WriteLine("Ничего не выбрано");

    }

    private static void FillTheSalon()
    {
        // Метод для заполенения данными
        Salon.AddService(new Hairstyle("Стрижка", 50, true));
        Salon.AddService(new Hairstyle("Укладка", 30, true));
        Salon.AddService(new Hairstyle("Окрашивание", 70, false));
        Salon.AddService(new Hairstyle("Плетение кос", 40, true));

        Salon.AddService(new Manicure("Классический маникюр", 20, "Нюдовый"));
        Salon.AddService(new Manicure("Гель-лак", 25, "Нюдовый"));
        Salon.AddService(new Manicure("Маникюр с дизайном", 35, "Зелёненький"));
        Salon.AddService(new Manicure("Маникюр с френчем", 25, "Синий"));


        Salon.AddService(new SpaProcedure("Спа-процедура для волос", 60, 50));
        Salon.AddService(new SpaProcedure("Спа-процедура для ногтей", 45, 40));

        Salon.AddService(new Pedicure("Педикюр красный", 34, "Красивый"));
        Salon.AddService(new Pedicure("Педикюр нежный", 60, "Премиуим"));
    }
}
