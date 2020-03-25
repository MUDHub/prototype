using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MUDhub.Prototype.Server.Configurations;
using MUDhub.Prototype.Server.Hubs;
using MUDhub.Prototype.Server.Services;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUDhub.Prototype.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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
            services.AddScoped<UserManager>();
            services.AddScoped<NavigationService>();
            services.AddSingleton<RoomManager>();
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite("Data Source=myDatabase.db"),
                    ServiceLifetime.Singleton);

            services.AddControllersWithViews();
            //services.AddRazorPages();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<UserSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<UserSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                //x.Events = new JwtBearerEvents()
                //{
                //    OnTokenValidated = context =>
                //    {
                //        //var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                //        var userId = int.Parse(context.Principal.Identity.Name);
                //        //var user = userService.GetById(userId);
                //        if (user == null)
                //        {
                //            // return unauthorized if user no longer exists
                //            context.Fail("Unauthorized");
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                // We have to hook the OnMessageReceived event in order to
                // allow the JWT authentication handler to read the access
                // token from the query string when a WebSocket or 
                // Server-Sent Events request comes in.

                // Sending the access token in the query string is required due to
                // a limitation in Browser APIs. We restrict it to only calls to the
                // SignalR hub in this code.
                // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
                // for more information about security considerations when using
                // the query string to transmit the access token.
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/hubs")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MUDhub API", Version = "v1" });
            });

        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSpaStaticFiles();
                app.Use(async (context, next) =>
                {

                    await next();


                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MUDhub API");
            });

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
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
                endpoints.MapHub<GameHub>("/hubs/game");
                endpoints.MapHub<ChatHub>("/hubs/chat");
            });

            if (!env.IsDevelopment())
            {
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
}
