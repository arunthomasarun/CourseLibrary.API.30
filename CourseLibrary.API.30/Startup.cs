using AutoMapper;
using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CourseLibrary.API._30
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
           services.AddControllers(startupActions => {
               startupActions.ReturnHttpNotAcceptable = true; //This will throw error if a non supported header is requested. If set to false, the api will return the default return type
           }).AddXmlSerializerFormatters(); //This adds support for xml

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();

            services.AddDbContext<CourseLibraryContext>(options =>
            {
                options.UseSqlServer(
                    @"Server=DESKTOP-C8CNSOE\SQLEXPRESS;Database=CourseLibraryProject30;user id=sa;password=SqlExpress@9;Trusted_Connection=True;MultipleActiveResultSets=true;");
            });

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("CourseLibraryAPISpecification", 
                    new Microsoft.OpenApi.Models.OpenApiInfo { 
                        Title = "Course Library API",
                        Version = "1"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
