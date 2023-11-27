namespace SalonBeauty.Models;

public class Hairstyle : Service
{
    private bool _includesWash; // Поле содержащее параметр, включающий мойку волос

    public Hairstyle(string name, double price, bool includesWash) : base(name, price)
    {
        _includesWash = includesWash;
    }

    public override string DisplayInfo()
    {
        return $"Работа с волосами {Name} | Цена {Price} | Включает мойку волос : {_includesWash}";
    }
}