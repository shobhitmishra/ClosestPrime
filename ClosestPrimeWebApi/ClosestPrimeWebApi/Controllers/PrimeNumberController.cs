using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosestPrimeWebApi.Models;
using ClosestPrimeWebApi.Models.StorageProvider;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClosestPrimeWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrimeNumberController : Controller
    {
        IStorageProvider storageProvider;
        public PrimeNumberController(IStorageProvider _storageProvider)
        {
            storageProvider = _storageProvider;
        }

        private int GetClosestPrime(int number)
        {
            var primes = PrimeNumberRepository.Primes;
            if (primes.Contains(number))
                return number;
            int i = 1;
            while (true)
            {
                if (primes.Contains(number - i))
                    return number - i;
                if (primes.Contains(number + i))
                    return number + i;
                i++;
            }
        }

        // Leave this method to do local testing.
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        /// <summary>
        /// Takes an integer <= 179424691 and returns the closest (bigger or smaller) prime number. 
        /// </summary>
        /// <param name="number">Input number. Comes from the form</param>
        /// <returns>The prime number closest to the given number.</returns>
        [HttpPost]
        public async Task<NumberEntity> Post([FromBody]int number)
        {
            var closestPrime = GetClosestPrime(number);
            var numEntity = await storageProvider.AddNumberEntityToTable(number, closestPrime);
            return numEntity;
        }
        
        // GET api/<controller>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
