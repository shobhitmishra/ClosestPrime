using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClosestPrime.Models
{
    public class ClosestPrimeMapping
    {
        public int Number { get; }
        public int ClosestPrime { get; }
        public ClosestPrimeMapping(int number, int closestPrime)
        {
            Number = number;
            ClosestPrime = closestPrime;
        }
    }
}
