using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaparaThirdWeek.Api.Extensions;
using PaparaThirdWeek.Api.Filters;
using PaparaThirdWeek.Api.Middlewares;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Data.Concretes;
using PaparaThirdWeek.Data.Context;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.Concretes;
using PaparaThirdWeek.Services.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Api
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

            services.AddControllers();
            //services.AddControllersWithViews(options => options.CacheProfiles.Add("Duration45", new CacheProfile
            //{
            //    Duration = 45,
            //    Location = ResponseCacheLocation.Client,

            //}));
            //services.AddResponseCaching();
            //services.AddControllers(
            //    options => options.Filters.Add(new HttpResponseExceptionFilter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaparaThirdWeek.Api", Version = "v1" });
            });
            services.AddDbContext<PaparaAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICompanyService, CompanyServices>();
            services.AddAutoMapper(typeof(MappingProfile)); //Sadece oluþturduðum MappingProfilini register eder.
            //services.AddAutoMapper(Assembly.GetExecutingAssembly()); //Farklý mapper profilleri eklenirse hepsi tarayýp bulup register eder.

            //Attribute olarak eklediðim actionda çalýþýr.
            services.AddScoped<ValidationFilterAttribute>(); // Actionlarda her defasýnda ModelState.IsValid yapan attribute.
            services.AddTransient<ICacheService, CacheService>();

            //Tüm actionlar için filter register yöntemi. Global olarak yapar.
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(typeof(ValidationFilterAttribute));
            //});

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaparaThirdWeek.Api v1"));
            }

            app.UseExceptionMiddleware();

            //app.ConfigureExceptionHandler(); // Lamda exp. ile Built-in ExceptionMiddleware extension olarak kullandýk. Exception Handle yaptýk.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseResponseCaching(); Response Cache aktif eden middleware
        }
    }
}
