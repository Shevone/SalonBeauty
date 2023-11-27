using System.Collections;
using System.Text;

namespace SalonBeauty.Models;

// Класс - мастер, переделали в стилиста
public class Stylist<T> : ICollection<T> where T : Service
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
    public string ServiceType => typeof(T).ToString()[23..]; // тип выполняемых услуг (прическа, маникюр, спа)
    
    // =============================================
    public Stylist(string name)
    {
        _serviceList = new List<T>();
        _name = name;
    }
    // =============================================
    // Методы
    public void Add(T item)
    {
        // Добавить новую услугу
        _serviceList.Add(item);
    }

    public void Clear()
    {
        // Полное очищение
        _serviceList.Clear();
    }

    public bool Contains(T item)
    {
        // Содержит ли наша коллекция элемент item
        bool isContains = _serviceList.Contains(item);
        return isContains;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _serviceList.CopyTo(array,arrayIndex);
    }

    public bool Remove(T item)
    {
        // Удаление
        // Возвращает true или false в зависмости от того, удалилось или нет
        bool removeRes = _serviceList.Remove(item);
        return removeRes;
    }
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

    // Метод переопределяющий то, как будет выглядить в консоли
    public override string ToString()
    {
        return $"Стилист {_name} | Тип услуг : {ServiceType}";
    }

    public string DisplayInfo()
    {
        // Метод возвращающий подробную информацию, в строковом виде
        StringBuilder sb = new StringBuilder($"Стилист {_name} | Тип услуг : {ServiceType} | Количесвто услуг {Count}\n | Зарплата {Salary}");
        // Проходимся по предоставляемым услугам и записываем их в нашу строку
        foreach (T service in _serviceList)
        {
            // Это тоже полиморфный вызов - service.DisplayInfo()
            sb.Append(service.DisplayInfo() + "\n");
        }
        return sb.ToString();
    }
}