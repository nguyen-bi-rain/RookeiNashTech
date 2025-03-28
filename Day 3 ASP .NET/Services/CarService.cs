using Day_3_ASP_.NET.Interface;
using Day_3_ASP_.NET.Models;

namespace Day_3_ASP_.NET.Services
{
    public class CarService : ICarService
    {
        private readonly List<Car> cars = new List<Car>()
        {
            new Car() { Id = 1, Make = "Toyota", Model = "Corolla", LastMaintenance = new DateTime(2021, 1, 1) },
            new Car() { Id = 2, Make = "Honda", Model = "Civic", LastMaintenance = new DateTime(2021, 2, 1) },
            new Car() { Id = 3, Make = "Ford", Model = "Fiesta", LastMaintenance = new DateTime(2021, 3, 1) },
            new Car() { Id = 4, Make = "BMW", Model = "M3", LastMaintenance = new DateTime(2021, 4, 1) },
            new Car() { Id = 5, Make = "Mercedes", Model = "C-Class", LastMaintenance = new DateTime(2021, 5, 1) }
        };
        public void Add(Car entity)
        {
           var car = cars.FirstOrDefault(x => x.Id == entity.Id);
            if(car != null)
            {
                throw new Exception("car is exist");
            }
            cars.Add(entity);   
            
        }

        public void Delete(int id)
        {
            var car = cars.FirstOrDefault(x => x.Id == id);
            if(car == null)
            {
                throw new Exception("Car Not Found");
            }
            cars.Remove(car);
            
        }

        public Car Get(int id)
        {
            var car = cars.FirstOrDefault(x => x.Id == id);
            if(car == null)
            {
                throw new Exception("Car Not Found");
            }
            return car;
        }

        public List<Car> GetAll()
        {
            if (!cars.Any())
            {
                throw new Exception("does not have car ");
            }
            return cars;
        }

        public void Update(decimal id, DateTime LastMaintenance)
        {
            Car car = cars.FirstOrDefault(x => x.Id == id);
            if(car == null)
            {
                 throw new Exception("Car Not Found");
            }
            car.LastMaintenance = LastMaintenance;  
        }
    }
}
