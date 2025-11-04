namespace RentCar.Application.Contracts.Repositories
{
    public interface IHealthRepository
    {
        Task<bool> IsHealthyDataBaseConnection();
    }
}
