using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rotativa.AspNetCore;
using StackExchange.Redis;
using WebAppCoreNew.Hub;
using WebAppCoreNew.Service;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebAppCoreNew {
    public class Startup {
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env/*, IServiceProvider serviceProvider*/)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //var mvcBuilder = serviceProvider.GetService<IMvcBuilder>();
            Configuration = builder.Build();
            RotativaConfiguration.Setup(env);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddHttpContextAccessor();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IModuleService, ModuleService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IMenuService, MenuService>();
            services.AddSingleton<ILanguageService, LanguageService>();
            services.AddSingleton<ILogService, LogService>();
            //set up login fb
            services.AddAuthentication(options => {
                options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;


            }).AddFacebook(options => {
                options.AppId = "255759719156728";
                options.AppSecret = "01c05b691cade47910cf328032217a34";
            }).AddCookie();

            services.AddDistributedRedisCache(options => // config redis cache server
            {
                options.Configuration = Configuration["ConfigApp:RedisConnection"];
            });
            //var redis = ConnectionMultiplexer.Connect("localhost");
            //var a = redis.TimeoutMilliseconds;

            string configString = "localhost";
            var options = ConfigurationOptions.Parse(configString);
            options.ConnectTimeout= 600000;
            options.ResponseTimeout = 600000;
            options.SyncTimeout = 600000;
            options.AllowAdmin = true;
            var conn = ConnectionMultiplexer.Connect(options);
            

            services.Configure<FormOptions>(x => {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
                x.MemoryBufferThreshold = int.MaxValue;
            });
            services.AddMvc().AddNewtonsoftJson();
            services.AddMvc(options =>{options.CacheProfiles.Add("Default",new CacheProfile(){Duration = 180});
                options.EnableEndpointRouting = false;
            }).AddRazorPagesOptions(options => {
                   options.Conventions.AuthorizeFolder("/Admin");
                   options.Conventions.AllowAnonymousToPage("/Admin/User/Login");
               });

            //services.AddMvc(options =>
            //       options.EnableEndpointRouting = false)
            //       .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
            //services.AddDistributedMemoryCache();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession(options => {
                options.IdleTimeout = System.TimeSpan.FromMinutes(30);//You can set Time
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });
            services.AddSignalR();
           
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
           
            app.UseAuthorization();
            app.UseMvc(routes => {
                routes.MapAreaRoute(
                   name: "MyAreaAdmin",
                   areaName: "Admin",
                   template: "Admin/{controller=HomeAdmin}/{action=Index}/{id?}");
                routes.MapRoute(
                  name: "default",
                  template: "{controller=HomePage}/{action=Index}/{id?}");

            });
            try
            {
               
                //app.UseEndpoints(endpoints => {
                //    endpoints.MapControllerRoute(
                //        name: "Admin",
                //        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                //    endpoints.MapControllerRoute(
                //        name: "default",
                //        pattern: "{controller=Home}/{action=Index}/{id?}");
                //    endpoints.MapRazorPages();
                //});

            }
            catch (Exception e)
            {


            }
            app.UseSignalR(routes => {
                routes.MapHub<MessageHub>("/Hub");
            });

        }
    }
}
