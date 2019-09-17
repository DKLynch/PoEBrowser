using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PoEBrowser.Models;
using PoEBrowser.Services;

namespace PoEBrowser
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

            //Fetch DB settings from appsettings.json
            services.Configure<PoEDatabaseSettings>(Configuration.GetSection(nameof(PoEDatabaseSettings)));
            services.AddSingleton<IPoEDatabaseSettings>(sp => sp.GetRequiredService<IOptions<PoEDatabaseSettings>>().Value);

            //Create DBService Singleton
            services.AddSingleton<DBContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "BaseItems",
                    template: "BaseItems");

                routes.MapRoute(
                    name: "Currency",
                    template: "Currency");

                routes.MapRoute(
                    name: "Essences",
                    template: "Essences");

                routes.MapRoute(
                    name: "ActiveSkillGems",
                    template: "SkillGems/Active");

                routes.MapRoute(
                    name: "SupportSkillGems",
                    template: "SkillGems/Support");       
            });
        }
    }
}
