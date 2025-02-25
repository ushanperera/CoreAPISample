using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Base.Cache
{
    public class CacheSettings
    {
        public string Uri { get; set; }
        public string InstanceName { get; set; }
        public int SlidingExpirationInMin { get; set; }
        public int AbsoluteExpirationInMin { get; set; }
        public bool BypassCache { get; set; }
    }
}
