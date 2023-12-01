namespace SalonBeauty.Models;

// Интерфейс, реализующий ковариацию
public interface ICovarianceInterface <out T>
{
    // Ковариантный метод
    T SomeCovarianceMethod(string name);
}