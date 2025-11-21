using AutoMapper;
using FluentValidation;
using RentCar.Application.Contracts.Repositories;
using RentCar.Application.Contracts.Services;
using RentCar.Application.Dtos.Requests;
using RentCar.Application.Dtos.Responses;
using RentCar.Domain.Entities;

namespace RentCar.Application.Services
{
    public class TaxService(
        ITaxRepository taxRepository,
        IValidator<TaxRequestDto> validator,
        IMapper mapper) : BaseService<TaxRequestDto>(validator, mapper), ITaxService
    {
        private readonly ITaxRepository _taxRepository = taxRepository;

        public async Task<TaxResponseDto> GetTaxByIdAsync(int taxId)
        {
            Tax? tax = await _taxRepository.GetTaxByIdAsync(taxId);

            if (tax == null)
            {
                return new()
                {
                    Status = ResponseStatus.NotFound,
                    Message = $"Tax with ID {taxId} not found."
                };
            }

            return new() { Data = [_mapper.Map<TaxDto>(tax)], Status = ResponseStatus.NotFound };
        }

        public async Task<TaxResponseDto> GetTaxesAsync()
        {
            IEnumerable<Tax> taxes = await _taxRepository.GetTaxesAsync();

            return new()
            {
                Data = _mapper.Map<IEnumerable<TaxDto>>(taxes),
                Status = ResponseStatus.Success
            };
        }

        public async Task<TaxResponseDto> CreateTaxAsync(TaxRequestDto taxDto)
        {
            var result = await CreateValidationResult<TaxDto, TaxResponseDto>(taxDto);

            if (result.Status == ResponseStatus.ValidationError)
            {
                return result;
            }

            Tax tax = _mapper.Map<Tax>(taxDto);
            Tax newTax = await _taxRepository.AddTaxAsync(tax);
            
            return new()
            { 
                Data = _mapper.Map<IEnumerable<TaxDto>>(new[] { newTax }),
                Status = ResponseStatus.Success
            };
        }

        public async Task<TaxResponseDto> UpdateTaxAsync(int taxId, TaxRequestDto taxDto)
        {
            var result = await CreateValidationResult<TaxDto, TaxResponseDto>(taxDto);

            if (result.Status == ResponseStatus.ValidationError)
            {
                return result;
            }

            Tax tax = _mapper.Map<Tax>(taxDto);
            await _taxRepository.UpdateTaxAsync(taxId, tax);

            return new() { Status = ResponseStatus.Success };
        }

        public async Task<TaxResponseDto> DeleteTaxAsync(int taxId)
        {
            ResponseStatus status = await _taxRepository.RemoveTaxAsync(taxId);

            string? message = null;

            if (status == ResponseStatus.NotAllowed)
            {
                message = $"Tax with ID {taxId} is not allowed to be deleted.";
            }
            else if (status == ResponseStatus.NotFound)
            {
                message = $"Tax with ID {taxId} not found.";
            }
            else if (status == ResponseStatus.Conflict)
            {
                message = $"Tax with ID {taxId} is associated with existing rentals and cannot be deleted.";
            }

            return new() { Status = status, Message = message };
        }
    }
}
