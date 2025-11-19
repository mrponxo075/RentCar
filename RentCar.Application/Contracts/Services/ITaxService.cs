using RentCar.Application.Dtos.Requests;
using RentCar.Application.Dtos.Responses;

namespace RentCar.Application.Contracts.Services
{
    public interface ITaxService
    {
        Task<TaxResponseDto> GetTaxByIdAsync(int taxId);

        Task<TaxResponseDto> GetTaxesAsync();
        
        Task<TaxResponseDto> CreateTaxAsync(TaxRequestDto taxDto);
        
        Task<TaxResponseDto> UpdateTaxAsync(int taxId, TaxRequestDto taxDto);
        
        Task<TaxResponseDto> DeleteTaxAsync(int taxId);
    }
}
