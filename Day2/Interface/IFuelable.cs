using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day2.Interface
{
    public interface IFuelable
    {
        void Refuel(DateTime timeOfRefuel);
    }
}