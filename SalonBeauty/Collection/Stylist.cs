using System.Collections;
using System.Text;

namespace SalonBeauty.Models;

// Класс - мастер, переделали в стилиста
public class Stylist<T> : ICovarianceInterface<T>, IStylist<T> where T : Service
{
    // =============================================
    // Поля и свойства
    private string _name; // Имя стилиста

    private List<T> _serviceList; // Список услуг, которые может придоставить стилиист
    // Свойство для расчета зарплаты
    private double Salary
    {
        get
        {
            double sum = 0;
            foreach (T service in _serviceList)
            {
                sum += service.Price * service.OrderCount;
            }
            return sum;
        }
    }
    public int Count => _serviceList.Count; // Количесвто элементов(количесвто предоставляемых услуг)
    public bool IsReadOnly => false; // Просто  загулушка
    private string ServiceType => typeof(T).ToString()[19..]; // тип выполняемых услуг (прическа, маникюр, спа)
    
    // =============================================
    public Stylist(string name)
    {
        _serviceList = new List<T>();
        _name = name;
    }
    // =========================================================
    // Методы интерфеса ICollection(добавить, очистить все, содердит ли, копировать и удалить)
    // Из всех этих методов (add, remove, clear, copyto, contains) мы используем в нашей программе
    // только метод Add
    public void Add(T item)
    {
        // Добавить новую услугу
        _serviceList.Add(item);
    }
    
    public void Clear()
    {
        // Этот метод мы не используем в нашей программе,
        // Но обязаные его реализовать в классе из-за
        // Интерфеса ICollection
        
        // Полное очищение
        _serviceList.Clear();
    }

    public bool Contains(T item)
    {
        // Этот метод мы не используем в нашей программе,
        // Но обязаные его реализовать в классе из-за
        // Интерфеса ICollection
        
        // Содержит ли наша коллекция элемент item
        bool isContains = _serviceList.Contains(item);
        return isContains;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        // Этот метод мы не используем в нашей программе,
        // Но обязаные его реализовать в классе из-за
        // Интерфеса ICollection
        
        // Метод копирет наш список в масси, который передан в метод
        _serviceList.CopyTo(array,arrayIndex);
    }

    public bool Remove(T item)
    {
        // Этот метод мы не используем в нашей программе,
        // Но обязаные его реализовать в классе из-за
        // Интерфеса ICollection
        
        // Удаление
        // Возвращает true или false в зависмости от того, удалилось или нет
        bool removeRes = _serviceList.Remove(item);
        return removeRes;
    }
    // ============================================================================================
    // ниже 2 метода, которые мы обязаны реализовать из за интефрейса ICollection
    // Метод GetEnumerator возвращает объект для итерации, например чтоб засунуть его в констуркцию foreach
    public IEnumerator<T> GetEnumerator()
    {
        return _serviceList.GetEnumerator();
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    // ============================================================================================

    // Метод переопределяющий то, как будет выглядить в консоли
    public T SomeCovarianceMethod(string name)
    {
        // метод возращает услугу, название которой совпадает с переданной в методу строкой
        T service = _serviceList.Find(service => service.Name == name);
        return service;
    }
    // Определяем то как выглядит в строку
    public override string ToString()
    {
        return $"Стилист {_name} | Тип услуг : {ServiceType}";
    }
    // Метод для вывода информации
    public string DisplayInfo()
    {
        // Метод возвращающий подробную информацию, в строковом виде
        StringBuilder sb = new StringBuilder($"Стилист {_name} | Тип услуг : {ServiceType} | Количесвто услуг {Count} | Зарплата {Salary}\n");
        // Проходимся по предоставляемым услугам и записываем их в нашу строку
        foreach (T service in _serviceList)
        {
            // Это тоже полиморфный вызов - service.DisplayInfo()
            sb.Append(service.DisplayInfo() + "\n");
        }
        return sb.ToString();
    }
    // Метод сортировки
    // Принимает в качестве входного параметра - делегат Func
    // последний параметр в кавычках - тип, который возвращает наш делеагт
    // Реализовывает пузырьковую сортирвку
    // Сравнение элементов происходит функцией переданной в качестве входного параметра
    public void SortServices(Func<T, T, bool> orderFunc)
    {
        var len = Count;
        for (var i = 1; i < len; i++)
        {
            for (var j = 0; j < len - i; j++)
            {
                // Помещаем 2 параметра в функцию сравнения
                // Если первый больше второго, то меняем их местами
                T p1 = _serviceList[j];
                T p2 = _serviceList[j + 1];
                bool firstBiggerThanSecond = orderFunc(p1, p2); // Тут вызываем переданный делеагт сравнения
                // и если первый больше второго, то меняем элементы меставим
                if(firstBiggerThanSecond)
                {
                    (_serviceList[j], _serviceList[j + 1]) = (_serviceList[j + 1], _serviceList[j]);
                }
            }
        }
    }
}