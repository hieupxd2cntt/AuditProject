using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAppCoreNew.Service;
using WebModelCore;

namespace WebGia.Common {
    public class RoleAuthorization : AuthorizeAttribute {
        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    var url = filterContext.HttpContext.Request.Url;
        //}
    }
    [AttributeUsage(AttributeTargets.Class)]
    [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
    public class CustomAuthorization : Attribute, IAuthorizationFilter {
        private readonly FormOptions _formOptions;

        public CustomAuthorization(int valueCountLimit)
        {
            _formOptions = new FormOptions() {
                ValueCountLimit = valueCountLimit
            };
        }
        protected IModuleService _moduleService;
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var features = filterContext.HttpContext.Features;
            var formFeature = features.Get<IFormFeature>();

            if (formFeature == null || formFeature.Form == null) {
                // Request form has not been read yet, so set the limits
                features.Set<IFormFeature>(new FormFeature(filterContext.HttpContext.Request, _formOptions));
            }
            try {
                _moduleService = (IModuleService)filterContext.HttpContext.RequestServices.GetService(typeof(IModuleService));
            }
            catch (Exception e) {


            }
            var controllerInfo = filterContext.ActionDescriptor as ControllerActionDescriptor;
            return;
            if (filterContext != null) {
                string controllerName = controllerInfo.ControllerName;
                if ((controllerInfo.ActionName == "Index" || controllerInfo.ActionName == "Login" || controllerInfo.ActionName == "GoToMod") && controllerName == "Home" || controllerInfo.ActionName == "Logout" || (filterContext.HttpContext.Request.Method.ToUpper()=="POST" && controllerInfo.ActionName.ToUpper() == "EDIT" && controllerName == "Home" )) {
                    return;
                }
                var modId = HttpUtility.ParseQueryString(filterContext.HttpContext.Request.QueryString.Value).Get("modId");
                if (string.IsNullOrEmpty(modId)) {
                    try {
                        modId = filterContext.HttpContext.Request.Form["modId"].ToString();
                    }
                    catch (Exception e) {

                    }

                }                
                if (modId.ToUpper() == ConstMod.ModViewPdf.ToUpper() || modId.ToUpper() == ConstMod.ModListHomo.ToUpper()) {
                    return;
                }
                int userId = int.Parse("0" + filterContext.HttpContext.Session.GetString("UserId"));
                if (userId == 0) {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                       {
                        "Controller",
                        "Home"
                       }, {
                        "Action",
                        "Login"
                       }
                    });
                }
                else {
                    if (string.IsNullOrEmpty(modId)) {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                           {
                            "Controller",
                            "Default"
                           }, {
                            "Action",
                            "ErrorPermission"
                           } });
                    }
                   
                      
                    var groupModUser = Task.Run(() => _moduleService.GetGroupModByUserId(userId));
                    if (groupModUser.Result == null || !groupModUser.Result.Any()) {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                           {
                            "Controller",
                            "Home"
                           }, {
                            "Action",
                            "Login"
                           }
                        });
                    }
                    else {
                        var checkMod = groupModUser.Result.Where(x => x.ModId == modId);
                        if (!checkMod.Any()) {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                           {
                            "Controller",
                            "Default"
                           }, {
                            "Action",
                            "ErrorPermission"
                           }
                        });
                        }

                    }
                }
            }
        }
    }
}
