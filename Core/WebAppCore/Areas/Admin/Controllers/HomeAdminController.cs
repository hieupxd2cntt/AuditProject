using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using WebAppCoreNew.Service;

namespace WebAppCoreNew.Controllers
{
    [Area("Admin")]
    public class HomeAdminController : BaseController
    {
        private readonly IDistributedCache _distributedCache;
        protected IConfiguration _Configuration;
        protected IModuleService _moduleService;
        protected IUserService _userService;
        protected ILogService _logService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IHttpContextAccessor _accessor;
        public HomeAdminController(IConfiguration configuration, IModuleService moduleService, IUserService userService, ILogService logService, IDistributedCache distributedCache, IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor) : base(configuration)
        {
            _Configuration = configuration;
            _moduleService = moduleService;
            _distributedCache = distributedCache;
            _hostingEnvironment = hostingEnvironment;
            _userService = userService;
            _logService = logService;
            _accessor = accessor;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
