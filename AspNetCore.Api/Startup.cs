using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspNetCore.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using AspNetCore.Api.DB;
using AspNetCore.Api.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace AspNetCore.Api
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                         new TokenValidationParameters
                         {
                             ValidateIssuer = true,
                             ValidateAudience = true,
                             ValidateLifetime = true,
                             ValidateIssuerSigningKey = true,

                             ValidIssuer = "AspNetCore.Security.Bearer",
                             ValidAudience = "AspNetCore.Security.Bearer",
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("aspnetcore-secret-key"))
                         };

                    #region TokenValidateEvents
                    //options.Events = new JwtBearerEvents
                    //{
                    //    OnAuthenticationFailed = context =>
                    //    {
                    //        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                    //        return Task.CompletedTask;
                    //    },
                    //    OnTokenValidated = context =>
                    //    {
                    //        Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                    //        return Task.CompletedTask;
                    //    }
                    //};
                    #endregion
                });

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

            var sqlConnectionString = Configuration.GetConnectionString("AspNetCoreConnectionString");
            services.AddEntityFrameworkNpgsql();
            services.AddDbContext<EFCoreDbContext>(options =>
            {
                options.UseNpgsql(sqlConnectionString);
            });

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICurrencyCodeRepository, CurrencyCodeRepository>();
            services.AddScoped<IMagnitudeRepository, MagnitudeRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<ICsvService, CsvService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ASP.NET Core 2.0",
                    Contact = new Contact()
                    {
                        Name = "Akbar Shaikh",
                        Email = "aashaikh55@gmail.com",
                    }
                });

                swagger.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                swagger.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() } });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            app.UseCors("AllowAll");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET CORE API");
            });
        }
    }
}
