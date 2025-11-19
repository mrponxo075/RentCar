using AutoMapper;
using FluentValidation;
using RentCar.Application.Contracts.Repositories;
using RentCar.Application.Contracts.Services;
using RentCar.Application.Dtos.Requests;
using RentCar.Application.Dtos.Responses;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services
{
    public class TaxService(
        ITaxRepository taxRepository,
        IValidator<TaxRequestDto> validator,
        IMapper mapper) : ITaxService
    {
        private readonly ITaxRepository _taxRepository = taxRepository;
        private readonly IValidator<TaxRequestDto> _validator = validator;
        private readonly IMapper _mapper = mapper;

        public async Task<TaxResponseDto> GetTaxByIdAsync(int taxId)
        {
            Tax? tax = await _taxRepository.GetTaxByIdAsync(taxId);

            if (tax == null)
            {
                return new(null, ResponseStatus.NotFound, "Tax not found.");
            }

            return new([_mapper.Map<TaxDto>(tax)], ResponseStatus.NotFound);
        }

        public async Task<TaxResponseDto> GetTaxesAsync()
        {
            IEnumerable<Tax> taxes = await _taxRepository.GetTaxesAsync();

            return new(_mapper.Map<IEnumerable<TaxDto>>(taxes), ResponseStatus.Success);
        }

        public async Task<TaxResponseDto> CreateTaxAsync(TaxRequestDto taxDto)
        {
            var result = await CreateValidationResult(taxDto);

            if (result.Status == ResponseStatus.ValidationError)
            {
                return result;
            }

            Tax tax = _mapper.Map<Tax>(taxDto);

            Tax newTax = await _taxRepository.AddTaxAsync(tax);
            
            return new(_mapper.Map<IEnumerable<TaxDto>>(new[] { newTax }), ResponseStatus.Success);
        }

        public async Task<TaxResponseDto> UpdateTaxAsync(int taxId, TaxRequestDto taxDto)
        {
            var result = await CreateValidationResult(taxDto);

            if (result.Status == ResponseStatus.ValidationError)
            {
                return result;
            }

            Tax tax = _mapper.Map<Tax>(taxDto);

            return new(null, await _taxRepository.UpdateTaxAsync(taxId, tax));
        }

        public async Task<TaxResponseDto> DeleteTaxAsync(int taxId)
        {
            ResponseStatus status = await _taxRepository.RemoveTaxAsync(taxId);
            
            return new(null, status);
        }

        private async Task<TaxResponseDto> CreateValidationResult(TaxRequestDto taxDto)
        {
            var validationResult = await _validator.ValidateAsync(taxDto);
            
            if (!validationResult.IsValid)
            {
                return new(null, ResponseStatus.ValidationError, string.Join(' ', validationResult.Errors));
            }
            
            return new(null, ResponseStatus.Success);
        }
    }
}
