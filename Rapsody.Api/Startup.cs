using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rapsody.Api.DB;
using Rapsody.Api.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Rapsody.Api
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy => policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });

            services.AddMvc(opt =>
            {
                opt.Filters.Add(new ProducesAttribute("application/json"));
                opt.Filters.Add(new ConsumesAttribute("application/json"));
            });

            var sqlConnectionString = Configuration.GetConnectionString("RapsodyConnectionString");
            services.AddEntityFrameworkNpgsql();
            services.AddDbContext<RapsodyDbContext>(options => options.UseNpgsql(sqlConnectionString));

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IMagnitudeRepository, MagnitudeRepository>();
            services.AddScoped<ICsvService, CsvService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Rapsody Core",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact()
                    {
                        Name = "Akbar Shaikh",
                        Email = "akabarali.shaikh@socgen.com",
                        Url = "http://societegenerale.com"
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "My App");
            });
        }
    }
}
