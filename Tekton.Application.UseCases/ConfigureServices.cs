using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tekton.Application.UseCases.Common.Behaviors;
using Tekton.Application.UseCases.Products.Commands.CreateProductCommand;
using Tekton.Application.UseCases.Products.Commands.UpdateProductCommand;

namespace Tekton.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
            return services;
        }
    }
}
