using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day2.Interface
{
    public class ElectricCar : Car, IChargable
    {
        public ElectricCar(string make, string model, int year, DateTime lastMaintenanceDate) : base(make, model, year, lastMaintenanceDate)
        {
        }

        public void Charge(DateTime timeOfCharge)
        {
            Console.WriteLine($"ElectricCar {Make} {Model} Charge on {timeOfCharge}");
        }
    }
}