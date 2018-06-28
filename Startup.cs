using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using University.Models;
using University.DI.Interfaces;
using University.DI.Implementation;
using University.DI.Middleware;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;

namespace University
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
            var connection = @"Server=EGOISTSYSTEM;Database=University;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<UniversityContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddTransient<IRequestRepository, RequestRepository>();
            services.AddTransient<MiddlewareRepository>();
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<UniversityContext>().AddDefaultTokenProviders();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseStaticFiles(new StaticFileOptions {
                OnPrepareResponse = ctx => {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public, max-age=600");
                }
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
                RequestPath = "/MyImages"
            });

            app.UseConventionalMiddleware();
            app.UseIMiddlewareMiddleware();           
            app.UseCookiePolicy();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
