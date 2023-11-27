namespace SalonBeauty.Models;

public class Manicure : Service
{
    private string _color; // Цвет маникюра

    public Manicure(string name, double price, string color) : base(name, price)
    {
        _color = color;
    }

    public override string DisplayInfo()
    {
        return $"Маникюр : {Name} | Цена : {Price} | Цвет : {_color}";
    }
}