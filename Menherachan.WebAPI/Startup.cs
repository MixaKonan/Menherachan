using System;
using System.Collections.Generic;
using System.Reflection;
using MediatR;
using Menherachan.Application.CQRS.Handlers.BoardHandlers;
using Menherachan.Application.CQRS.Queries.BoardQueries;
using Menherachan.Application.Interfaces;
using Menherachan.Domain.Database;
using Menherachan.Domain.Entities.DBOs;
using Menherachan.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Menherachan.WebAPI
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
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Menherachan.WebAPI",
                    Version = "v1"
                });
            });

            
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(
                Configuration.GetConnectionString("DefaultConnection"),
                new MariaDbServerVersion(new Version(10, 3, 27))));

            services.AddMediatR(typeof(GetAllBoardsQuery).GetTypeInfo().Assembly);

            services.AddTransient<IBoardRepository, BoardRepository>();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Menherachan.WebAPI v1"));
            }

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