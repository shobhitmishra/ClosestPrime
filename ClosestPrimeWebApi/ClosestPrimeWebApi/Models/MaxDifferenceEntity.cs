using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClosestPrimeWebApi.Models
{
    public class MaxDifferenceEntity
    {
        public static string IdAndPartitionString = "MaxDifference";

        public MaxDifferenceEntity(int number, int prime)
        {
            Number = number;
            MatchedPrime = prime;
            TimeStamps ??= new List<DateTime>() { DateTime.UtcNow };
            Count = 1;
        }

        public string PartitionKey = IdAndPartitionString;

        [JsonProperty(PropertyName = "id")]
        public string Id = IdAndPartitionString;

        public int Difference
        {
            get
            {
                return Math.Abs(Number - MatchedPrime);
            }
        }
        public int Number { get; set; }
        public int MatchedPrime { get; set; }
        public List<DateTime> TimeStamps { get; set; }
        public int Count { get; set; }

        public void UpdateItem()
        {
            TimeStamps.Add(DateTime.UtcNow);
            Count += 1;
        }
    }
}

