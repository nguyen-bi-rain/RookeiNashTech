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
        private string _make;
        private string _model;
        private int _year;
        private readonly FuelType _type;

        public string Make 
        { 
            get => _make;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Make cannot be empty or whitespace");
                _make = value;
            }
        }

        public string Model 
        { 
            get => _model;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Model cannot be empty or whitespace");
                _model = value;
            }
        }

        public int Year 
        { 
            get => _year;
            set => _year = value;
        }

        public FuelType Type => _type;

        public Car(string make, string model, int year, FuelType type)
        {
            if (string.IsNullOrWhiteSpace(make))
                throw new ArgumentException("Make cannot be empty", nameof(make));
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be empty", nameof(model));
            Make = make;
            Model = model;
            Year = year;
            _type = type;
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