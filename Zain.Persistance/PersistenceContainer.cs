
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zain.Application.Contracts;
using Zain.Domain.Entities;
using Zain.Domain.TokenEntities;
using Zain.Persistance;
using Zain.Persistance.Repositories;
using Zain.Application.IUnit;
using Zain.Persistance.Unit;


namespace Zain.Persistance
{
    public static class PersistenceContainer
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("connectionString")));


            #region Email 

            //services.Configure<OutlookMailSettings>(configuration.GetSection("MailSetting"));
            //services.AddScoped<IEmailServices, EmailServices>();

            #endregion

            #region JWT 

            services.Configure<JWT>(configuration.GetSection("JWT"));
            //// Authorize for Bearer by Defualt without add to each controller

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            #endregion









         

            // DI

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //services.AddScoped<IHelper, HelperRepositories>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IForgetPassword, ForgetPassword>();

            // Repo



            // Json
            //services.AddControllers().AddJsonOptions(
            //    options =>
            //    {
            //        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //    });

            //services.AddControllers().AddNewtonsoftJson(
            //    options =>
            //    {
            //        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //    });


            return services;

        }
    }
}
