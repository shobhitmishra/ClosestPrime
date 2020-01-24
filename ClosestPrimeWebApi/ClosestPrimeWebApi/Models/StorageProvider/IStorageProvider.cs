using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClosestPrimeWebApi.Models.StorageProvider
{
    public interface IStorageProvider
    {
        public Task<NumberEntity> AddNumberEntityToTable(int number, int prime);
        public Task<NumberEntity> GetNumberEntity(int number, int prime);
    }
}
