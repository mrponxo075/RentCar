using AutoMapper;
using FluentValidation;
using RentCar.Application.Dtos.Responses;

namespace RentCar.Application.Services
{
    public abstract class BaseService<TRequest>(
        IValidator<TRequest> validator,
        IMapper mapper) where TRequest : class
    {
        protected readonly IValidator<TRequest> _validator = validator;
        protected readonly IMapper _mapper = mapper;

        protected async Task<TResponse> CreateValidationResult<T, TResponse>(TRequest dto)
            where T : class
            where TResponse : Response<T>, new()

        {
            var validationResult = await _validator.ValidateAsync(dto);

            return new TResponse()
            {
                Status = validationResult.IsValid
                    ? ResponseStatus.Success
                    : ResponseStatus.ValidationError,
                
                Message = validationResult.IsValid
                    ? null
                    : string.Join(' ', validationResult.Errors)
            };
        }
    }
}
