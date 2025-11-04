using Microsoft.Data.SqlClient;
using RentCar.Application.Contracts.Repositories;

namespace RentCar.Infrastructure.Repositories
{
    public class HealthRepository(string connectionString): IHealthRepository
    {
        private readonly string _connectionString = connectionString;

        public async Task<bool> IsHealthyDataBaseConnection()
        {
            try
            {
                using SqlConnection connection = new(_connectionString);
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT 1";

                return (int?)await command.ExecuteScalarAsync() == 1;
            }
            catch
            {
                return false;
            }
        }
    }
}
