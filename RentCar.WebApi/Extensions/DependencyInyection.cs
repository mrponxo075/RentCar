using RentCar.Application.Contracts.Repositories;
using RentCar.Application.Mappings;
using RentCar.Infrastructure.Repositories;
using FluentValidation;
using RentCar.Application.Validators;
using RentCar.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Services;
using RentCar.Application.Contracts.Services;

namespace RentCar.WebApi.Extensions
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration config)
        {
            // automapper
            services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile).Assembly);

            // validators
            services.AddScoped<IValidator<CustomerDto>, CustomerDtoValidator>();

            // services
            services.AddScoped<ITaxService, TaxService>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddScoped<IHealthRepository, HealthRepository>(
                sd => new HealthRepository(config.GetConnectionString("DefaultConnection")!));

            services.AddDbContext<RentCarContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITaxRepository, TaxRepository>();

            return services;
        }

        
    }
}
