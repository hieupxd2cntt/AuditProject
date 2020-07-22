using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
//using WebAppCoreBlazorNew.Service;
using WebAppCoreBlazorWebAssembly.Service;

namespace WebAppCoreBlazorNew
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton<IModuleService, ModuleService>();
            builder.Services.AddSingleton<ILogService, LogService>();
            builder.Services.AddSingleton<IMenuService, MenuService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri("http://localhost:8013/api/") });
            builder.RootComponents.Add<App>("app");
            

            await builder.Build().RunAsync();
        }
    }
}
