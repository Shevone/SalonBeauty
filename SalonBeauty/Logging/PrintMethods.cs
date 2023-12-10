namespace SalonBeauty.Logging;

// Класс - содержащий в себе методы печати лога
public class PrintMethods
{
    // Путь до файла
    private string _logFilePath;
    
    public PrintMethods(string logFilePath)
    {
        // Инициализируем путь до файла в конструкторе
        _logFilePath = logFilePath[1..];
        CheckFilePath();
    }

    private void CheckFilePath()
    {
        // проверям чтоб файл существовал
        string fullFilePath = Directory.GetCurrentDirectory()[..^16] + _logFilePath;
        if (Directory.Exists(fullFilePath))
        {
            // Если файл не сущствует, то создаем и закрываем 
            File.Create(fullFilePath).Close();
        }
        // Далее просто записываем в переменную 
        _logFilePath = fullFilePath;
    }
    
    // Метод печати в файл
    public void FileLog(string message)
    {
        string time = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        message = time + " | " + message + "\n";
        File.AppendAllText(_logFilePath,message);
        
    }
    
    // Метод печати в консоль
    public void ConsoleLog(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("\nНажмите любую клавишу чтобы продолжить");
        Console.ReadKey();
    }
    
}