using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tekton.Application.Interface.UseCases;
using Tekton.Application.UseCases.Products;
using Tekton.Application.Validator;

namespace Tekton.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IProductApplication, ProductApplication>();

            services.AddTransient<ProductDtoValidator>();

            return services;
        }
    }
}
