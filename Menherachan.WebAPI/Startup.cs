using System;
using System.Reflection;
using System.Text;
using MediatR;
using Menherachan.Application;
using Menherachan.Application.Interfaces;
using Menherachan.Application.Interfaces.Repositories;
using Menherachan.Application.Interfaces.Services;
using Menherachan.Domain.Database;
using Menherachan.Infrastructure.Persistence.Repositories;
using Menherachan.Infrastructure.Shared.Mapping;
using Menherachan.Infrastructure.Shared.Services;
using Menherachan.WebAPI.Cookies;
using Menherachan.WebAPI.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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

            services.AddCors(o => o.AddPolicy("Policy", builder =>
            {
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            }));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,    
                        ValidateAudience = true,    
                        ValidateLifetime = true,    
                        ValidateIssuerSigningKey = true,    
                        ValidIssuer = Configuration["Jwt:Issuer"],    
                        ValidAudience = Configuration["Jwt:Audience"],    
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))    

                    };
                });

            services.AddDbContext<ApplicationDbContext>
            (options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MariaDbServerVersion(new Version(10, 3, 27)),
                opt => opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddMediatR(typeof(MediatREntryPoint).GetTypeInfo().Assembly);

            services.AddAutoMapper(m => m.AddProfile(new GeneralProfile()));
            
            services.AddTransient<IBoardRepository, BoardRepository>();
            services.AddTransient<IThreadRepository, ThreadRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ICookieService, CookieService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("Policy");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Menherachan.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseJwtInCookies();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}