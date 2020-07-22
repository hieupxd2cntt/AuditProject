using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebAppCoreNew.Service;
using WebAppCoreNew.Utils;
using WebCore.Entities;
using WebModelCore;
using WebModelCore.Menu;

namespace WebAppCoreNew.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;
        private readonly IMenuService _menuService;
        private readonly ILanguageService _languageService;
        public MenuViewComponent(IDistributedCache distributedCache, IConfiguration configuration, IMenuService menuService, ILanguageService languageService)
        {
            _distributedCache = distributedCache;
            _configuration = configuration;
            _menuService = menuService;
            _languageService = languageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = await LoadMenu();
            return View(menu);
        }
        private async Task<List<MenuModel>> LoadMenu()
        {
            var menus = new List<MenuModel>();
            int userId = int.Parse("0" + HttpContext.Session.GetString("UserId"));
            var dataMenu = await LoadDataMenu(userId);
            if (dataMenu!=null)
            {
                var leve0 = dataMenu.Where(x => x.OwnerMenuID == "000000");
                foreach (var item in leve0)
                {
                    var menuRoot = new MenuModel
                    {
                        Menu = item
                    };
                    var menuChild = dataMenu.Where(x => x.OwnerMenuID == item.MenuID);
                    if (menuChild.Any())
                    {
                        menuRoot.MenuChild.AddRange(menuChild.Select(x => new MenuModel { Menu = x }).ToList());
                    }
                    menus.Add(menuRoot);
                }
            }
            
            ViewBag.Languages = await LoadAllIcon();
            return menus;
        }

        private async Task<List<MenuItemInfo>> LoadDataMenu(int userId)
        {
            string key = ECacheKey.Menu.ToString();
            //var cachedData = _distributedCache.GetString(key);
            //if (cachedData != null)
            //{
            //    var menus = JsonConvert.DeserializeObject<List<MenuItemInfo>>(cachedData);
            //    return menus;
            //}
            //else
            //{
                var menus = await _menuService.GetAllMenu(userId);
               // RedisUtils.SetCacheData(_distributedCache, _configuration, menus, key);
                return menus;
           // }
        }

        private async Task<List<LanguageInfo>> LoadAllIcon()
        {
            string key = ECacheKey.AllIcon.ToString();
            var cachedData = _distributedCache.GetString(key);
            if (cachedData != null)
            {
                var menus = JsonConvert.DeserializeObject<List<LanguageInfo>>(cachedData);
                return menus;
            }
            else
            {
                var menus = await _languageService.GetAllIcon();
                RedisUtils.SetCacheData(_distributedCache, _configuration, menus, key);
                return menus;
            }
        }
    }
}
