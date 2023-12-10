namespace SalonBeauty.Logging;

// Класс - логгер
public class Logger
{
    // event - куда записываютяс все обработчики
    private event Action<string> PrintMethods;

    // Метод - регистрации(добавления) обработчика 
    public void AddLogPrintMethod(Action<string> printMethod)
    {
        PrintMethods += printMethod;
        
        
    }

    // Метод - Вызывающийся для логирования информации
    public void LogInfo(string message)
    {
        PrintMethods?.Invoke(message);
    }
}