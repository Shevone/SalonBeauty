namespace SalonBeauty.Models;

public class Pedicure : Service
{
    private string _type; // Тип педикюра
    
    public Pedicure(string name, double price, string type) : base(name, price)
    {
        _type = type;
    }
    public override string DisplayInfo()
    {
        return $"id : {Id} |Педикюр : {Name} | Цена : {Price} | Тип педикюра : {_type}";
    }
}