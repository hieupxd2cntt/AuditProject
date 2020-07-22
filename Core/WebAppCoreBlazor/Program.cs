using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebAppCoreBlazorWebAssembly.Service;
using Microsoft.Extensions.Configuration;

namespace WebAppCoreBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddSingleton<IModuleService, ModuleService>();
            builder.RootComponents.Add<App>("app");
            //await LoadAppSettingsAsync(builder);
            //builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //builder.Services.AddSingleton<IModuleService, ModuleService>();
            //builder.Services.AddSingleton<IUserService, UserService>();
            //builder.Services.AddSingleton<IMenuService, MenuService>();
            //builder.Services.AddSingleton<ILanguageService, LanguageService>();
            //builder.Services.AddSingleton<ILogService, LogService>();
            
            await builder.Build().RunAsync();
        }
        /// <summary>
        /// Load appsettings.json from the  wwwroot folder
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        //private static async Task LoadAppSettingsAsync(WebAssemblyHostBuilder builder)
        //{
        //    // read JSON file as a stream for configuration
        //    var client = new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
        //    // the appsettings file must be in 'wwwroot'
        //    using var response = await client.GetAsync("appsettings.json");
        //    using var stream = await response.Content.ReadAsStreamAsync();
        //    builder.Configuration.AddJsonStream(stream);
        //}
    }
}
