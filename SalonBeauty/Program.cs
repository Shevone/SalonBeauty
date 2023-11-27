using SalonBeauty.Models;

namespace SalonBeauty;

class Program
{

    private static Models.SalonBeauty Salon = new Models.SalonBeauty();
    public static void Main(string[] args)
    {
        
    }

    public static void CovarioanceDemo()
    {
        
    }
    
    private static void FillTheSalon()
    {
        // Метод для заполенения данными
        Salon.AddService(new Hairstyle("Стрижка", 50, true));
        Salon.AddService(new Hairstyle("Укладка", 30, true));
        Salon.AddService(new Hairstyle("Окрашивание", 70,false));
        Salon.AddService(new Manicure("Классический маникюр", 20, "Нюдовый"));
        Salon.AddService(new Manicure("Гель-лак", 25, "Нюдовый"));
       
        Salon.AddService(new Manicure("Маникюр с френчем", 25,  "Синий"));
        Salon.AddService(new Hairstyle("Плетение кос", 40, true));
        Salon.AddService(new Manicure("Маникюр с дизайном", 35,  "Зелёненький"));
        Salon.AddService(new SpaProcedure("Спа-процедура для волос", 60,50));
        Salon.AddService(new SpaProcedure("Спа-процедура для ногтей", 45, 40));
    }
// Демонстрация ковариантности
    public static void CovDemo()
    {
        ICovarianceInterface<Service> hairsyler = new HairstyleMaster("Вася");
        Service hairstyle = hairsyler.CreateNewService("Стрижка модная", 300);
        Console.WriteLine(hairstyle.DisplayInfo());   
        
        // То есть мы можем присвоить более общему типу ICovarianceInterface<Service>
        // объект более конкретного типа HairstyleMaster или ICovarianceInterface<Hairstyle>.
        ICovarianceInterface<Hairstyle> hairstyler2 = new HairstyleMaster("Петя");
        ICovarianceInterface<Service> hairstyler3 = hairstyler2;
        Service hairstyle2 = hairstyler2.CreateNewService("Стрижка2",2);
        Console.WriteLine(hairstyle2.DisplayInfo());    
    }
}
// Коваринатный метод - возвращает объект с типом T
// Коваринтный интерфейс не может реализоваывать методы принимающие на вход тип Т
public interface ICovarianceInterface<out T>
{
    T CreateNewService(string name, int price); // Ковариантный мтеод
}

public class HairstyleMaster : ICovarianceInterface<Hairstyle>
{
    public string Name;
    private List<Hairstyle> _hairstyles = new List<Hairstyle>();

    public HairstyleMaster(string name)
    {
        Name = name;
    }
    // Реализация коваринтного метода
    public Hairstyle CreateNewService(string name, int price)
    {
        Hairstyle hairstyle = new Hairstyle(name, price, true);
        _hairstyles.Add(hairstyle);
        return hairstyle;
    }
}