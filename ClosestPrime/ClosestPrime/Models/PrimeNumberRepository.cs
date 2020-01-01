using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClosestPrime.Models
{
    public static class PrimeNumberRepository
    {
        public static HashSet<int> Primes { get; }

        static PrimeNumberRepository()
        {
            var primeNumberFilePath = @".\Resources\1e7_primes.txt";
            var allPrimes = System.IO.File.ReadAllLines(primeNumberFilePath);
            Primes = allPrimes.Select(x => int.Parse(x)).ToHashSet();
        }
    }
}
