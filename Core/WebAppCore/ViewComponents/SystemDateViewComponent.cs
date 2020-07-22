using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace WebAppCoreNew.ViewComponents
{
    public class SystemDateViewComponent : ViewComponent
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;
        public SystemDateViewComponent(IDistributedCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache;
            _configuration = configuration;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
