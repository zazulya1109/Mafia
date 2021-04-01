using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Data.Interfaces;
using WebApplication1.Interfaces;
using WebApplication1.Mocks;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Startup
    {
        public static bool isLocal = false;
        public Startup(IConfiguration configuration)
        {
            //Database.SendQuery($"DELETE FROM Event");
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => {
                    o.LoginPath = "/Home/Login";
            });

            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();

                return factory.GetUrlHelper(actionContext);
            });
            services.AddTransient<IPlayer, PlayerMock>();
            services.AddTransient<IEvent, EventMock>();
            services.AddTransient<IGameData, GameDataMock>();
            services.AddTransient<EventsDataSource, EventsDataSourceImpl>();
            services.AddTransient<PlayersDataSource, PlayersDataSourceImpl>();
            services.AddTransient<IUser, UserMock>();
            services.AddTransient<IChat, ChatMock>();
            services.AddTransient<IAccount, AccountMock>();
            services.AddControllersWithViews();
            services.AddMvc().AddControllersAsServices();
            services.AddSignalR();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 5;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            services.AddRazorPages(options => {
                options.Conventions.AuthorizeFolder("/");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                isLocal = true;
                app.UseDeveloperExceptionPage();
            }
            else
            {
                isLocal = false;
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            } 
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
