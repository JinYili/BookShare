using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShare.Web.Data;
using BookShare.Web.Models;
using BookShare.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BookShare.Web
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                })
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddMvc().
                SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 

            services.AddDbContext<BookContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                });

            services.AddScoped<IBookService, BookEfService>();
            services.AddScoped<ICommentService, CommentEfService>();
 
            services.AddMvc(options =>
            {
                options.RequireHttpsPermanent = true; // does not affect api requests
                options.RespectBrowserAcceptHeader = true; // false by default
            });

            //In memory Cache
            services.AddMemoryCache();

            // redis cache  port number on 6379
            /*
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost"; 
                options.InstanceName = "redis-for-book";

            });
            */
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // app.UseWelcomePage();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Static Files
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
