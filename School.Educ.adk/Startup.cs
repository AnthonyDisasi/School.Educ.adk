using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Educ.adk.Areas.Admin.Data;
using School.Educ.adk.Areas.Ecole.DataContext;
using School.Educ.adk.Areas.Inspection.Data;
using School.Educ.adk.Areas.ProfeArea.Data;
using School.Educ.adk.Data;
using School.Educ.adk.Infrastructure;
using School.Educ.adk.Models;

namespace School.Educ.adk
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMemoryCache();
            services.AddSession();

            services.AddDbContext<DbEcole>(options => options.UseSqlServer(this.Configuration.GetConnectionString("EcoleDb_")));
            services.AddDbContext<ExamenDb>(options => options.UseSqlServer(this.Configuration.GetConnectionString("ExamenDb_")));
            services.AddDbContext<ProfeAreaDb>(options => options.UseSqlServer(this.Configuration.GetConnectionString("ProfesseurDb_")));
            services.AddDbContext<InspecteurDb>(options => options.UseSqlServer(this.Configuration.GetConnectionString("InspecteurDb_")));

            services.AddTransient<IPasswordValidator<ApplicationUser>, CustomPasswordValidator>();
            services.AddTransient<IUserValidator<ApplicationUser>, CustomUserValidator>();

            services.AddDbContext<UserAuthent>(options => options.UseSqlServer(this.Configuration.GetConnectionString("USerDb_")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<UserAuthent>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                           name: "areaRoute",
                           template: "{area:exists}/{controller=Home}/{action=Index}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
