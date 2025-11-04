using RentCar.Application.Contracts.Repositories;
using RentCar.Infrastructure.Repositories;

namespace RentCar.WebApi.Extensions
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration config)
        {

            return services;
        }

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddScoped<IHealthRepository, HealthRepository>(
                sd => new HealthRepository(config.GetConnectionString("DefaultConnection")!));

            return services;
        }
    }
}
