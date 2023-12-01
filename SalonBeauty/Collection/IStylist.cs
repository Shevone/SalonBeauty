namespace SalonBeauty.Models;

public interface IStylist<T> : ICollection<T> where T : Service
{
    public string DisplayInfo();

    public void SortServices(Func<T, T, bool> orderFunc);
}