namespace SalonBeauty.Models;

public class SpaProcedure : Service
{
    private int _procedureTimeMinutes; // Переменная в которой хранится значение
    
    // Свойство, через которое производим доступ к переменной
    // Это пример инкапсуляции, мы скрываем от внешнего источника, то как устанавлвиваем значение
    private int ProcedureTimeMinutes
    {
        get => _procedureTimeMinutes;
        // Этой штукой, мы ограничиваем то, чтоб сюда не было записано неодекватных значений
        set
        {
            // Елси время меньше единцы то ставим 1
            if (value <= 1)
            {
                _procedureTimeMinutes = 1;
                return;
            }
            // Елси время больше 600 ставим 600
            if (value > 600)
            {
                _procedureTimeMinutes = 600;
                return;
            }

            _procedureTimeMinutes = value;
        }
    }
    
    public SpaProcedure(string name, double price, int procedureTimeMinutes) : base(name, price)
    {
        ProcedureTimeMinutes = procedureTimeMinutes;
    }

    public override string DisplayInfo()
    {
        return $"id : {Id} | Спа процедура : {Name} | Цена : {Price} | Время работы : {ProcedureTimeMinutes}";
    }
}