using RentCar.Application.Dtos.Responses;
using RentCar.Domain.Entities;

namespace RentCar.Application.Contracts.Repositories
{
    public interface ITaxRepository
    {
        Task<Tax?> GetTaxByIdAsync(int taxId);

        Task<IEnumerable<Tax>> GetTaxesAsync();

        Task<Tax> AddTaxAsync(Tax tax);

        Task<ResponseStatus> UpdateTaxAsync(int taxId, Tax tax);

        Task<ResponseStatus> RemoveTaxAsync(int taxId);
    }
}
