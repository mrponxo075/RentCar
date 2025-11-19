using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Dtos.Requests
{
    public class TaxRequestDto
    {
        public string TaxName { get; set; } = string.Empty;

        public string? TaxDescription { get; set; }

        public decimal Rate { get; set; }
    }
}
