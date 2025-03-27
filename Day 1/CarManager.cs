using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Day_1.Car;

namespace Day_1
{
    public class CarManager
    {
        private List<Car> ListOfCars = new List<Car>(){
            new Car("Toyota","Corolla",2019,FuelType.Fuel),
            new Car("Toyota","Camry",2019,FuelType.Fuel),
            new Car("Toyota","Prius",2019,FuelType.Electric),
            new Car("Toyota","Highlander",2019,FuelType.Fuel),
            new Car("Toyota","Rav4",2019,FuelType.Fuel),
            new Car("Tesla","Model S",2019,FuelType.Electric),
            new Car("Tesla","Model 3",2019,FuelType.Electric),
        };

        #region Validation Methods

        private FuelType ValidateFuelType(string prompt = "Enter the car Type (Fuel/Electric):")
        {
            FuelType type;
            while (true)
            {
                Console.WriteLine(prompt);
                string typeInput = Console.ReadLine();
                if (Enum.TryParse<FuelType>(typeInput, true, out type))
                    break;
                Console.WriteLine("Invalid fuel type. Please enter 'Fuel' or 'Electric'.");
            }
            return type;
        }

        private string ValidateStringInput(string fieldName, string prompt = null)
        {
            string input;
            while (true)
            {
                Console.WriteLine(prompt ?? $"Enter the {fieldName}:");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    break;
                Console.WriteLine($"{fieldName} cannot be empty. Please try again.");
            }
            return input;
        }

        private int ValidateIntInput(string fieldName, string prompt = null)
        {
            int value;
            while (true)
            {
                Console.WriteLine(prompt ?? $"Enter the {fieldName}:");
                if (int.TryParse(Console.ReadLine(), out value))
                    break;
                Console.WriteLine($"Invalid {fieldName} format. Please enter a valid number.");
            }
            return value;
        }

        #endregion

        public void AddCar()
        {
            try
            {
                FuelType type = ValidateFuelType();
                string make = ValidateStringInput("make");
                string model = ValidateStringInput("model");
                int year = ValidateIntInput("year");
                Car car = new Car(make, model, year, type);
                ListOfCars.Add(car);
                Console.WriteLine("Car added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ViewAllCars()
        {
            try
            {
                if (ListOfCars.Count == 0 || ListOfCars == null)
                {
                    Console.WriteLine("No cars available in the list");
                    return;
                }

                DisplayCar(ListOfCars);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while viewing cars: {ex.Message}");
            }
        }

        public void FindCarByMake()
        {
            try
            {
                string make = ValidateStringInput("make", "Enter the make to search for:");
                var cars = ListOfCars?.FindAll(c => c.Make.Contains(make, StringComparison.OrdinalIgnoreCase));
                if (cars == null || !cars.Any())
                {
                    Console.WriteLine($"No cars found with make: {make}");
                    return;
                }

                Console.WriteLine($"Cars found with make '{make}':");
                DisplayCar(cars);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while searching: {ex.Message}");
            }
        }

        public void FilterCarByType()
        {
            try
            {
                FuelType type = ValidateFuelType("Enter the type to filter by (Fuel/Electric):");
                var cars = ListOfCars?.FindAll(c => c.Type == type);

                if (cars == null || !cars.Any())
                {
                    Console.WriteLine($"No cars found with type: {type}");
                    return;
                }
                Console.WriteLine($"Cars with type '{type}':");
                DisplayCar(cars);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while filtering: {ex.Message}");
            }
        }

        public void RemoveCarByModel()
        {
            try
            {
                string model = ValidateStringInput("model", "Enter the model of the car to remove:");

                var carToRemove = ListOfCars.FirstOrDefault(c => c.Model.Equals(model, StringComparison.OrdinalIgnoreCase));
                if (carToRemove == null)
                {
                    Console.WriteLine($"Car with model '{model}' not found");
                    return;
                }
                ListOfCars.Remove(carToRemove);
                Console.WriteLine($"Successfully removed car with model '{model}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing: {ex.Message}");
            }
        }

        public void DisplayCar(List<Car> cars)
        {
            cars.ForEach(car => Console.WriteLine(car.ToString()));
        }
    }
}