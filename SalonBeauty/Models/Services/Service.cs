namespace SalonBeauty.Models;

// Класс - услуга
public abstract class Service
{
    protected string Name; // Поле в котором хранится имя
    private double _price; // Поле в котором хранится цен

    public double Price // Свойство через которое мы получаем досутм к цене
    {
        get => _price;
        set
        {
            // Если поступающее значение меньше единицы то ставим 1
            if (value <= 1)
            {
                _price = 1;
            }
            else
            {
                _price = value;
            }
        }
    }
    
    private int _orderCount = 0;// количество раз, сколько заказали эту улсугу
    public int OrderCount => _orderCount; // Свойство, для доступа из вне(скорытие данных)
    
    // метод для заказа одной услуги
    // Вызывается, когда мы добавляем услугу в заказ
    public void OrderOneService()
    {
        _orderCount++;
    }
    // Модификатор доступа protected - значит что доступен только в классах наследниках
    protected Service(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public abstract string DisplayInfo();
    
}