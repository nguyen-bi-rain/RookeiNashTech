using System.ComponentModel.DataAnnotations;
using System.Linq;
using Day2;
using Day2.Interface;

class Program
{
    static void Main()
    {
        AddCar();
    }
    public static void AddCar()
    {
        var carMake = ValidateStringInput("Enter Car make: ");
        var carModel = ValidateStringInput("Enter Car model: ");
        var carYear = ValidateYearInput("Enter Car year (e.g., 2020): ");
        var lastMaintenanceDate = ValidateTimeInput("Enter last maintenance date (yyyy-MM-dd): ");
        Car car;
        while (true)
        {
            Console.WriteLine("Is this a FuelCar or ElectricCar? (F/E): ");
            var carType = Console.ReadLine()?.Trim();
            if (carType == "F")
            {
                car = new FuelCar(carMake, carModel, carYear, lastMaintenanceDate);
                break;
            }
            else if (carType == "E")
            {
                car = new ElectricCar(carMake, carModel, carYear, lastMaintenanceDate);
                break;
            }
            else
            {
                Console.WriteLine("Invalid car type. Please enter F for FuelCar or E for ElectricCar.");
            }
        }
        car.DisplayDetail();
        ChargeCarOrRefuel(car);
    }

    public static void ChargeCarOrRefuel(Car car)
    {
        Console.WriteLine("Do you want to refuel/charge? (Y/N)");
        var response = Console.ReadLine();
        if (response == "Y")
        {
            var dateChargeOrRefule = ValidateTimeInput("Enter refuel date (yyyy-MM-dd hh:mm): ");
            try
            {
                if (car is FuelCar fuelCar)
                    fuelCar.Refuel(dateChargeOrRefule);
                else ((ElectricCar)car).Charge(dateChargeOrRefule);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"unable to cast object");
                return;
            }

        }
    }
    private static string ValidateStringInput(string message)
    {
        do
        {
            Console.Write(message);
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input?.Trim()))
            {
                Console.WriteLine("Invalid input. Please enter a valid string.");
                continue;
            }
            return input;
        } while (true);
    }
    private static DateTime ValidateTimeInput(string message)
    {
        do
        {
            // format of date time must be yyyy-mm-dd if wrong throw exception
            Console.Write(message);
            var input = Console.ReadLine();
            if (!DateTime.TryParseExact(input, new[] { "yyyy-MM-dd", "yyyy-MM-dd HH:mm" }, null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Invalid date and time format or invalid date. Please enter a valid date and time in yyyy-MM-dd HH:mm or yyyy-MM-dd format.");
                continue;
            }
            return date;

        } while (true);
    }

    private static int ValidateYearInput(string message)
    {
        do
        {
            Console.Write(message);
            var input = Console.ReadLine();
            var currentYear = DateTime.Now.Year;
            if (!int.TryParse(input, out int year) || year < 1886 || year > currentYear)
            {
                Console.WriteLine("Invalid year. Please enter a valid year between 1886 and the current year");
                continue;
            }
            return year;
        } while (true);
    }
}