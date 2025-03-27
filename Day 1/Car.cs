using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day_1
{
    public class Car
    {
        public enum FuelType{
            Fuel,
            Electric
        }

        public string Make {get ;set;}

        public string Model {get ;set;}

        public int Year {get ;set;}
        
        public FuelType Type {get ;set;}

        public Car(string make, string model, int year, FuelType _type)
        {
            Make = make;
            Model = model;
            Year = year;
            Type = _type;
        }
        public Car()
        {
        }
        public override string ToString()
        {
            return $"Make: {Make}, Model: {Model}, Year: {Year}, Type: {Type}";
        }
        

    }
}