using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClosestPrime.Models;

namespace ClosestPrime.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checknumber(NumberInput number)
        {
            if (!ModelState.IsValid)
                ModelState.AddModelError(nameof(NumberInput),
                    "The input has to be a 'number' less than 179424692.");
            var closestPrime = GetClosestPrime(number.Input);
            var primeMapping = new ClosestPrimeMapping(number.Input, closestPrime);
            return View(primeMapping);
        }

        private int GetClosestPrime(int number)
        {
            var primes = PrimeNumberRepository.Primes;
            if (primes.Contains(number))
                return number;
            int i = 1;
            while(true)
            {
                if (primes.Contains(number - i))
                    return number - i;
                if (primes.Contains(number + i))
                    return number + i;
                i++;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
