using AMR_Server.Application;
using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Infrastructure;
using AMR_Server.Infrastructure.Persistence;
using AMR_Server.WebUI.Filters;
using AMR_Server.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Linq;
using AMR_Server.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using AMR_Server.Infrastructure.Configurations;
using Microsoft.Extensions.FileProviders;
using System.IO;
using DinkToPdf;
using DinkToPdf.Contracts;
using AMR_Server.WebUI.Utility;

namespace AMR_Server.WebUI
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
            //PDF
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            //>>>Cors Origin
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
            //>>>END Cors Origin


            // Replace the existing scoped IPasswordHasher<> implementation
            services.Replace(new ServiceDescriptor(
                serviceType: typeof(IPasswordHasher<IdentityUser>),
                implementationType: typeof(Md5PasswordHasher<IdentityUser>),
                ServiceLifetime.Scoped));


            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<AmrDbContext>();

            services.AddControllersWithViews(options =>
                options.Filters.Add(new ApiExceptionFilter()));

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "Clean_Architecture API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");//Allow Cors Orgign
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            //app.UseSwaggerUi3(settings =>
            //{
            //    settings.Path = "/api";
            //    settings.DocumentPath = "/api/specification.json";
            //});

            app.UseRouting();

            //app.UseAuthentication();
            app.UseIdentityServer();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });


            app.UseStaticFiles(); // For the wwwroot folder

            //Access Pdf Folder
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "PdfReports")),
                RequestPath = "/PdfReports"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "PdfReports")),
                RequestPath = "/PdfReports"
            });
            //>>>END Access Pdf Folder


        }
    }
}
