using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClosestPrimeWebApi.Models.StorageProvider
{
    public interface IStorageProvider
    {
        Task<NumberEntity> AddNumberEntityToTable(int number, int prime);
        Task<NumberEntity> GetNumberEntity(int number, int prime);
        Task<MaxDifferenceEntity> GetMaxDifferenceEntity();
        Task<List<NumberEntity>> GetAllNumberEntities();
    }
}
