using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebAppCoreNew.Controllers
{
    public class BaseController : Controller
    {
        protected string FaceBookAppId { get; set; }
        protected string WebSiteName { get; set; }

        protected IConfiguration Configuration;

        public BaseController(IConfiguration configuration)
        {
            //Configuration = configuration;
            //FaceBookAppId = Configuration["ConfigApp:FaceBookAppId"];
            //WebSiteName = Configuration["ConfigApp:WebSiteName"];
        }
        public ControllerContext ControllerContextBase {
            get { return this.ControllerContext; }
        }
        
    }
}