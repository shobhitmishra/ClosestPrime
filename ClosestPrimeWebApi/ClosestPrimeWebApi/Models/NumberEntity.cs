using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ClosestPrimeWebApi.Models
{
    public class NumberEntity : TableEntity
    {
        public NumberEntity()
        {

        }
        public NumberEntity(int number, int mtchedPrime)
        {            
            PartitionKey = number.ToString(CultureInfo.InvariantCulture);
            RowKey = mtchedPrime.ToString(CultureInfo.InvariantCulture);
        }

        public int Count { get; set; }        
    }
}
