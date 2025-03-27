using Day_1;
using static Day_1.Car;

internal class Program
{
    private static void Main(string[] args)
    {
        CarManager carManager = new CarManager();
        Run(carManager);
    }
    public static void Menu()
    {
        Console.WriteLine("1. Add Car");
        Console.WriteLine("2. View All Cars");
        Console.WriteLine("3. Search Car By Make");
        Console.WriteLine("4. Filter Cars By Type");
        Console.WriteLine("5. Remove a car By Model");
        Console.WriteLine("6. Exit");
    }
    
    public static void Run(CarManager carManager)
    {
        while (true)
        {
            try
            {
                Menu();
                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                    throw new ArgumentException("Invalid input. Please enter a number.");

                switch (choice)
                {
                    case 1:
                        carManager.AddCar();
                        break;
                    case 2:
                        carManager.ViewAllCars();
                        break;
                    case 3:
                        Console.Write("Enter the make of the car: ");
                        string make = Console.ReadLine();
                        carManager.FindCarByMake(make);
                        break;
                    case 4:
                        carManager.FilterCarByType();
                        break;
                    case 5:
                        carManager.RemoveCarByModel();
                        break;
                    case 6:
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        throw new ArgumentException("Invalid choice. Please select a number between 1 and 6.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Input error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            Console.ReadLine();
            Console.Clear();
        }
    }
}