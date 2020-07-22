using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using WebAppCoreNew.Service;
using WebModelCore.LoginModel;
using Microsoft.AspNetCore.Authentication;

namespace WebAppCoreNew.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IDistributedCache _distributedCache;
        protected IConfiguration _Configuration;
        protected IModuleService _moduleService;
        protected IUserService _userService;
        protected ILogService _logService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IHttpContextAccessor _accessor;
        public LoginController(IConfiguration configuration, IModuleService moduleService, IUserService userService, ILogService logService, IDistributedCache distributedCache, IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor) : base(configuration)
        {
            _Configuration = configuration;
            _moduleService = moduleService;
            _distributedCache = distributedCache;
            _hostingEnvironment = hostingEnvironment;
            _userService = userService;
            _logService = logService;
            _accessor = accessor;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userService.GetUserByUserNamePassword(model.UserName, model.Password);
            if (user == null)
            {
                model.Err = "Username hoặc Password không chính xác";
                return View(model);
            }
            HttpContext.Session.SetString("DisplayName", user.DisplayName);
            HttpContext.Session.SetString("UserName", user.Username);
            HttpContext.Session.SetString("UserId", user.UserID.ToString());
            return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("DisplayName", string.Empty);
            HttpContext.Session.SetString("UserName", string.Empty);
            HttpContext.Session.SetString("UserId", string.Empty);
            return RedirectToAction("Login");
        }
        public ActionResult Loginfb()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" });
        }

    }
}
