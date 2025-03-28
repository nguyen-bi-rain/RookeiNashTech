using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day2
{
    public  class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public Car(string make, string model, int year, DateTime lastMaintenanceDate)
        {
            Make = make;
            Model = model;
            Year = year;
            LastMaintenanceDate = lastMaintenanceDate;
        }

        public Car()
        {
        }

        public DateTime ScheduleMaintenance(){
            return LastMaintenanceDate.AddMonths(6);
        }   
        public void DisplayDetail(){
            Console.WriteLine($"Car : {Make} {Model} ({Year})");
            Console.WriteLine($"Last Maintenance: {LastMaintenanceDate}");
            Console.WriteLine($"Next Maintenance: {ScheduleMaintenance()}");
        }
    }
}