using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheManager.src.Common;
using DpapiCache;
using VaultManager;
using static System.Net.Mime.MediaTypeNames;

namespace CacheManager.src.Services
{
    public class DpapiCacheManagerSvc : IDpapiCacheManagerSvc
    {
        public async Task<bool> RetriveOfflineToken()
        {
            VaultProxy.VaultUrl = Constants.VaultUrl;
            string secret = await VaultProxy.RetrieveSecretFromRemoteVaultAsync();
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            string cacheFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "offlineToken.dat");
            CacheHelper.CacheFilePath = cacheFilePath;
            CacheHelper.Encrypt(secret);
            return true;
        }
    }
}
