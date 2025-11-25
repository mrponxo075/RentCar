using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
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
        IMapper mapper,
        ILogger<TaxService> logger) : BaseService<TaxRequestDto>(validator, mapper), ITaxService
    {
        private readonly ITaxRepository _taxRepository = taxRepository;
        private readonly ILogger<TaxService> _logger = logger;

        public async Task<TaxResponseDto> GetTaxByIdAsync(int taxId)
        {
            Tax? tax = await _taxRepository.GetTaxByIdAsync(taxId);

            if (tax == null)
            {
                _logger.LogWarning("Tax with ID {TaxId} not found.", taxId);

                return new()
                {
                    Status = ResponseStatus.NotFound,
                    Message = $"Tax with ID {taxId} not found."
                };
            }

            return new() { Data = [_mapper.Map<TaxDto>(tax)], Status = ResponseStatus.Success };
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
                _logger.LogWarning("Validation failed for creating tax: {Message}", result.Message);

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
                _logger.LogWarning(
                    "Validation failed for updating tax with ID {TaxId}: {Message}",
                    taxId,
                    result.Message);

                return result;
            }

            Tax tax = _mapper.Map<Tax>(taxDto);
            var status = await _taxRepository.UpdateTaxAsync(taxId, tax);

            if (status == ResponseStatus.NotFound)
            {
                _logger.LogWarning("Tax with ID {TaxId} not found for update.", taxId);

                return new()
                {
                    Status = ResponseStatus.NotFound,
                    Message = $"Tax with ID {taxId} not found."
                };
            }

            return new() { Status = status };
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

            if (message != null)
            {
                _logger.LogWarning("Failed to delete tax with ID {TaxId}: {Message}", taxId, message);
            }

            return new() { Status = status, Message = message };
        }
    }
}
