using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosestPrimeWebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClosestPrimeWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrimeNumberController : Controller
    {
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

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        

        // POST api/<controller>
        [HttpPost]
        public int Post([FromBody]int number)
        {
            var closestPrime = GetClosestPrime(number);            
            return closestPrime;
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
