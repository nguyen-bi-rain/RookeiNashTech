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
            Menu();
            Console.Write("Enter your choice: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
                Console.WriteLine("Invalid input. Please enter a number.");

            switch (choice)
            {
                case 1:
                    carManager.AddCar();
                    break;
                case 2:
                    carManager.ViewAllCars();
                    break;
                case 3:

                    carManager.FindCarByMake();
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
                    Console.WriteLine("Invalid choice. Please select a number between 1 and 6.");
                    continue;
            }
            Console.ReadLine();
            Console.Clear();
        }
    }
}