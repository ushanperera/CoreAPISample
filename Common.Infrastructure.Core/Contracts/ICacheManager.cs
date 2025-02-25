using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Base.Contracts
{
    public interface ICacheManager
    {
        Task<List<T>> GetCacheData<T>(string key);
        Task DeleteCacheData(string key);
        Task SetCacheData<T>(List<T> response, string key);
    }
}
