using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MUDhub.Prototype.Server.Hubs;
using MUDhub.Prototype.Server.Services;
using System.IO;

namespace MUDhub.Prototype.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _useProxy = Configuration.GetValue<bool>("spaAsStandalone");
            _spaDestiantion = Configuration["spaDestination"];
        }

        public IConfiguration Configuration { get; }
        private readonly bool _useProxy;
        private readonly string _spaDestiantion;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSpaStaticFiles(conf =>
            {
                conf.RootPath = _spaDestiantion;
            });
            services.AddSingleton<RoomManager>();
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite("myDatabase.db"));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseSpaStaticFiles();


            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();


            app.UseRouting();
            if (_useProxy)
            {
                app.UseCors(builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            }
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapHub<CommandHub>("/command");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                if (!_useProxy)
                {
                    spa.Options.SourcePath = _spaDestiantion;
                }
            });
        }
    }
}
