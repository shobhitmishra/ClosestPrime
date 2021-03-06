﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ClosestPrimeWebApi.Models
{
    public class NumberEntity
    {
        public NumberEntity(int num, int prime)
        {
            Number = num;
            MatchedPrime = prime;
            Id = num.ToString(CultureInfo.InvariantCulture);
            TimeStamps ??= new List<DateTime>() { DateTime.UtcNow };
            Count = 1;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
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
