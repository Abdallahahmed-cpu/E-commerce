using E_commers.Application.DTOS.Identity;
using E_commers.Application.Mapping;
using E_commers.Application.Service.Authantication;
using E_commers.Application.Service.Implementations;
using E_commers.Application.Service.Implementations.Authentication;
using E_commers.Application.Service.Interfaces;
using E_commers.Application.Validations.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.Extensions.DependencyInjection;

namespace E_commers.Application.DependencyInjection
{
    public static class ServiceCotainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingConfig));
            services.AddScoped<IProductService, ProductSevice>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            services.AddScoped(typeof(IValidationService), typeof(ValidationService<>));
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
