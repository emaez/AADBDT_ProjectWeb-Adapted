using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NRAKOProjektWeb.Data;
using NRAKOProjektWeb.Interface.Repository;
using NRAKOProjektWeb.Models;
using NRAKOProjektWeb.Patterns.Facade;
using NRAKOProjektWeb.Patterns.MutationFactory;
using NRAKOProjektWeb.Patterns.Singleton;
using NRAKOProjektWeb.Patterns.StorageStrategyFactory;
using NRAKOProjektWeb.Repository;
using System.Security.Claims;

namespace NRAKOProjektWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("NRAKOProjektConnection")));
            services.AddDefaultIdentity<NRAKOUser>()
                .AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddAuthentication()
            //    .AddGoogle(googleOptions =>
            //    {
            //        googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //        googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //        googleOptions.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
            //        googleOptions.ClaimActions.Clear();
            //        googleOptions.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            //        googleOptions.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
            //        googleOptions.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
            //        googleOptions.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
            //        googleOptions.ClaimActions.MapJsonKey("urn:google:profile", "link");
            //        googleOptions.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
            //    })
            //    .AddGitHub(gitHubOptions =>
            //    {
            //        gitHubOptions.ClientId = Configuration["Authentication:Github:ClientId"];
            //        gitHubOptions.ClientSecret = Configuration["Authentication:Github:ClientSecret"];
            //    });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<INRAKOLogger, NRAKOLogger>();
            services.AddSingleton<AmazonS3Tools>();
            services.AddSingleton<IMutationActionFactorySelector, MutationActionFactorySelector>();
            services.AddSingleton<IStorageStrategyFactorySelector, StorageStrategyFactorySelector>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPhotoRepository, PhotoRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<NRAKOUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            ApplicationDbInitializer.SeedUsers(userManager);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
