using Common.Infrastructure.Base.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Base.Cache
{
    public class CacheManager: ICacheManager
    {
        private readonly IDistributedCache Cache;
        private readonly CacheSettings Settings;
        public CacheManager(IDistributedCache cache, IOptions<CacheSettings> settings)
        {
            Cache = cache;
            Settings = settings.Value;
        }
        public async Task<List<T>> GetCacheData<T>(string key)
        {
            List<T> response = default(List<T>);
            if (Settings.BypassCache) return response;
            var cachedResponse = await Cache.GetAsync(key);
            if (cachedResponse != null)
            {
                response = JsonConvert.DeserializeObject<List<T>>(Encoding.Default.GetString(cachedResponse));
            }
            return response;
        }

        public async Task SetCacheData<T>(List<T> response, string key)
        {
            if (!Settings.BypassCache)
            {
                var slidingExpiration = TimeSpan.FromMinutes(Settings.SlidingExpirationInMin);
                var absoluteExpiration = TimeSpan.FromMinutes(Settings.AbsoluteExpirationInMin);
                var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration, AbsoluteExpirationRelativeToNow = absoluteExpiration };
                var serializedData = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));
                await Cache.SetAsync(key, serializedData, options);
            }
        }

        public async Task DeleteCacheData(string key)
        {
            if (!Settings.BypassCache)
            {
                await Cache.RemoveAsync(key);
            }
        }
    }
}
