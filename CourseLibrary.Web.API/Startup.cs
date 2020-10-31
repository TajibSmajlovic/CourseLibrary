using CorseLibrary.Services;
using CourseLibrary.Common.Interfaces;
using CourseLibrary.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CourseLibrary.Web.API
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
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddHttpCacheHeaders(expirationModelOptionsAction =>
            {
                expirationModelOptionsAction.MaxAge = 60;
                expirationModelOptionsAction.CacheLocation = Marvin.Cache.Headers.CacheLocation.Private;
            },
            validationModelOptionsAction =>
            {
                validationModelOptionsAction.MustRevalidate = true;
            }
            );

            services.AddResponseCaching();

            services.AddControllers(setupActions =>
            {
                setupActions.ReturnHttpNotAcceptable = true;
                setupActions.CacheProfiles.Add("240SecondsCacheProfile",
                                                  new CacheProfile()
                                                  {
                                                      Duration = 240
                                                  });
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Course Library", Version = "v1" }));

            services.AddDbContext<CourseLibraryContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<ICourseLibraryContext, CourseLibraryContext>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICourseService, CourseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CourseLibrary");
                });
            }

            app.UseResponseCaching();

            app.UseHttpCacheHeaders();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}