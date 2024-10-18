using LMS.API.Business.Interfaces;
using LMS.API.Business;
using LMS.Repository.Business.Interfaces;
using LMS.Repository.Business;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text;
using APL.API.Extensions.Enums;
using LMS.API.IntEngine.ActionFilters;
using LMS.Repository.Authentication.Interfaces;
using LMS.Repository.Authentication;

namespace LMS.API.IntEngine
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<AsyncActionFilter>();
          
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddSingleton(Configuration);
            services.AddControllers();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
           // services.AddScoped<AsyncActionFilter>();
            var key = Configuration.GetSection("sffsdfasdfasdfasdfasdfsdfadfffffffffffffffffffffffffffadfasdfasdfasdfsdfasda").Value;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"], // Ensure this is not null
                    ValidAudience = Configuration["Jwt:Audience"], // Ensure this is not null
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]) // Ensure this is not null
                    )
                };
            });

            services.AddAuthorization();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAllOrigins");
            if (env.IsDevelopment())
            {
  
                app.UseDeveloperExceptionPage();
            }
            else
            {
     
                app.UseExceptionHandler("/Home/Error"); // Replace with your error handling logic
                app.UseHsts();
            }
            app.UseAuthentication();
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
