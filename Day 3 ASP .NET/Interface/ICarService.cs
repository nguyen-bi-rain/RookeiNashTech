using Day_3_ASP_.NET.Models;

namespace Day_3_ASP_.NET.Interface
{
    public interface ICarService
    {
        Car Get(int id);
        void Add(Car car);
        List<Car> GetAll();
        void Update(decimal id, DateTime LastMaintenance);
        void Delete(int id);
    }
}
