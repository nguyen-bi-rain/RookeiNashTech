using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day2.Interface
{
    public class FuelCar : Car,IFuelable
    {   
        public FuelCar(string make, string model, int year, DateTime registrationDate) 
            : base(make, model, year, registrationDate)
        {
        }

        public void Refuel(DateTime timeOfRefuel)
        {
            Console.WriteLine($"FuelCar {Make} {Model} refueled on {timeOfRefuel}");
        }
    }
}