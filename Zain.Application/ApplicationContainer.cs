
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zain.Application.IUnit;
using Zain.Application.Servcies.Interface;
using Zain.Application.Servcies.Repository;

namespace Zain.Application
{

    public static class ApplicationContainer
    {

        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IProductServices, ProductServices>();

            return services;
        }

    }
}
