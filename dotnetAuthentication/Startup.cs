using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace dotnetAuthentication
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
            services.AddControllersWithViews();
            services.AddDbContext<AuthContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            //Identity işlemlerini servise dahil ediyoruz.
            services.AddIdentity<IdentityUser, IdentityRole>(x =>
            {
                // x.Password.RequireNonAlphanumeric=false;
                // x.Password.RequireDigit=false;
                // x.Password.RequireLowercase=false;
                // x.Password.RequireUppercase=false;
            }).AddErrorDescriber<CustomPasswordValidation>().AddEntityFrameworkStores<AuthContext>();

            //Cookie
            services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = new PathString("/Home/Login");
                x.AccessDeniedPath = new PathString("/Home/Denied");
                x.Cookie = new CookieBuilder
                {
                    Name = "dotnetAuthCookie"
                };
                x.SlidingExpiration = true;
                x.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            });
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
                RequestPath = new PathString("/vendor")
            });



            app.UseRouting();


            app.UseAuthentication(); //kimlik yönetimi
            app.UseAuthorization(); //yetki yönetimi


            app.UseEndpoints(endpoints =>
            {
                //area
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                //default
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
