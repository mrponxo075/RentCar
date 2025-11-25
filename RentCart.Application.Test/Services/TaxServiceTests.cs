using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using RentCar.Application.Contracts.Repositories;
using RentCar.Application.Dtos.Requests;
using RentCar.Application.Mappings;
using RentCar.Application.Services;
using RentCar.Application.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCart.Application.Test.Services
{
    public class TaxServiceTests
    {
        private readonly Mock<ITaxRepository> _taxRepositoryMock;
        private readonly Mock<ILogger<TaxService>> _loggerMock;
        private readonly TaxService _taxService;

        public TaxServiceTests()
        {
            _taxRepositoryMock = new Mock<ITaxRepository>();
            _loggerMock = new Mock<ILogger<TaxService>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            },
            null);

            _taxService = new TaxService(
                _taxRepositoryMock.Object,
                new TaxDtoValidator(),
                config.CreateMapper(),
                _loggerMock.Object
            );
        }
    }
}
