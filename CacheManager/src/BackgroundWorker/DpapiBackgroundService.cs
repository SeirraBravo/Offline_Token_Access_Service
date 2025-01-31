using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacheManager.src.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CacheManager.src.BackgroundWorker
{
    public class DpapiBackgroundService : BackgroundService
    {
        private readonly ILogger<DpapiBackgroundService> _logger;   
        private readonly IDpapiCacheManagerSvc _dpapiCacheManagerSvc;
        public DpapiBackgroundService(ILogger<DpapiBackgroundService> logger, IDpapiCacheManagerSvc dpapiCacheManagerSvc)
        {
            _logger = logger;
            _dpapiCacheManagerSvc = dpapiCacheManagerSvc;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("DpapiBackgroundService is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("DpapiBackgroundService is running at :{time}.",DateTimeOffset.Now);
                await Task.Delay(3000, stoppingToken);
                await _dpapiCacheManagerSvc.RetriveOfflineToken();

            }
            stoppingToken.Register(() =>
                _logger.LogInformation("DpapiBackgroundService is stopping."));
            throw new NotImplementedException();
        }
    }
}
