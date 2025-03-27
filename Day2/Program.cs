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
        var carMake = ValidateCarInforStringInput("Enter Car make: ");
        var carModel = ValidateCarInforStringInput("Enter Car model: ");
        var carYear = ValidateYearInput("Enter Car year (e.g., 2020): ");
        var lastMaintenanceDate = ValidateTimeInputForMaintenance("Enter last maintenance date (yyyy-MM-dd): ");
        Car car = new Car(carMake, carModel, carYear, lastMaintenanceDate);
        while (true)
        {
            Console.WriteLine("Is this a FuelCar or ElectricCar? (F/E): ");
            var carType = Console.ReadLine()?.Trim();

            if (carType == "F")
            {
                car = (FuelCar)car;
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
        Chargecar(car);
    }

    public static void Chargecar(Car car)
    {
        Console.WriteLine("Do you want to refuel/charge? (Y/N)");
        var response = Console.ReadLine();
        if (response == "Y")
        {
            if(car is FuelCar car1)
                car1.Refuel(ValidateTimeInputForCharge("Enter refuel date (yyyy-MM-dd hh:mm): "));
            else ((ElectricCar)car).Charge(ValidateTimeInputForCharge("Enter charge date (yyyy-MM-dd hh:mm): "));
        }
    }
    public static string ValidateCarInforStringInput(string message)
    {
        do
        {
            Console.Write(message);
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || input.Trim() == string.Empty)
            {
                Console.WriteLine("Make cannot be empty");
                continue;
            }
            return input;
        } while (true);
    }
    public static DateTime ValidateTimeInputForMaintenance(string message)
    {
        do
        {
            // format of date time must be yyyy-mm-dd if wrong throw exception
            Console.Write(message);
            var input = Console.ReadLine();
            if (!DateTime.TryParseExact(input, "yyyy-MM-dd" , null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Invalid date and time format or invalid date. Please enter a valid date and time in yyyy-MM-dd HH:mm or yyyy-MM-dd format.");
                continue;
            }
            return date;

        } while (true);
    }
    public static DateTime ValidateTimeInputForCharge(string message)
    {
        do
        {
            // format of date time must be yyyy-mm-dd if wrong throw exception
            Console.Write(message);
            var input = Console.ReadLine();
            if (!DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm" , null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Invalid date and time format or invalid date. Please enter a valid date and time in yyyy-MM-dd HH:mm or yyyy-MM-dd format.");
                continue;
            }
            return date;

        } while (true);
    }
    public static int ValidateYearInput(string message)
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