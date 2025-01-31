using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.src.Services
{
    public interface IDpapiCacheManagerSvc
    {
        public Task<bool> RetriveOfflineToken();
    }
}
