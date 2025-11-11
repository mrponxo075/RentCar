using RentCar.Application.Contracts.Repositories;
using RentCar.Application.Mappings;
using RentCar.Infrastructure.Repositories;
using FluentValidation;
using RentCar.Application.Validators;
using RentCar.Application.Dtos;
using Microsoft.EntityFrameworkCore;

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

            return services;
        }

        
    }
}
