using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Dtos.Responses
{
    public class TaxResponseDto : Response<TaxDto>
    {
        public TaxResponseDto(IEnumerable<TaxDto>? data, ResponseStatus status, string? message = null)
            : base(data, status, message)
        {
        }
    }
}
