using AutoMapper;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using RentCar.Application.Contracts.Repositories;
using RentCar.Application.Dtos.Requests;
using RentCar.Application.Dtos.Responses;
using RentCar.Application.Mappings;
using RentCar.Application.Services;
using RentCar.Application.Validators;
using RentCar.Domain.Entities;
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
                cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
            },
            NullLoggerFactory.Instance);

            _taxService = new TaxService(
                _taxRepositoryMock.Object,
                new TaxDtoValidator(),
                config.CreateMapper(),
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task GetTaxByIdAsync_NotFound_ReturnsNotFoundResponse()
        {
            // Arrange
            int taxId = 1;
            _taxRepositoryMock.Setup(repo => repo.GetTaxByIdAsync(taxId))
                .ReturnsAsync((Tax?)null);

            // Act
            var result = await _taxService.GetTaxByIdAsync(taxId);

            // Assert
            Assert.Equal(ResponseStatus.NotFound, result.Status);
            Assert.Equal($"Tax with ID {taxId} not found.", result.Message);
        }

        [Fact]
        public async Task GetTaxByIdAsync_Found_ReturnsSuccessResponse()
        {
            // Arrange
            int taxId = 1;
            var tax = new Tax { TaxId = taxId, TaxName = "VAT", Rate = 15.0m };
            _taxRepositoryMock.Setup(repo => repo.GetTaxByIdAsync(taxId))
                .ReturnsAsync(tax);

            // Act
            var result = await _taxService.GetTaxByIdAsync(taxId);

            // Assert
            Assert.Equal(ResponseStatus.Success, result.Status);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal(1, result.Data.First().TaxId);
            Assert.Equal("VAT", result.Data.First().TaxName);
            Assert.Equal(15.0m, result.Data.First().Rate);
        }

        [Fact]
        public async Task GetTaxesAsync_ReturnsAllTaxes()
        {
            // Arrange
            var taxes = new List<Tax>
            {
                new() { TaxId = 1, TaxName = "VAT", Rate = 15.0m },
                new() { TaxId = 2, TaxName = "Service Tax", Rate = 5.0m }
            };

            _taxRepositoryMock.Setup(repo => repo.GetTaxesAsync())
                .ReturnsAsync(taxes);

            // Act
            var result = await _taxService.GetTaxesAsync();

            // Assert
            Assert.Equal(ResponseStatus.Success, result.Status);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count());

            Assert.Equal(1, result.Data.First().TaxId);
            Assert.Equal("VAT", result.Data.First().TaxName);
            Assert.Equal(15.0m, result.Data.First().Rate);

            Assert.Equal(2, result.Data.Last().TaxId);
            Assert.Equal("Service Tax", result.Data.Last().TaxName);
            Assert.Equal(5.0m, result.Data.Last().Rate);
        }

        [Fact]
        public async Task CreateTaxAsync_InvalidTaxNameNull_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = null!,
                Rate = 5.0m
            };

            // Act
            var result = await _taxService.CreateTaxAsync(invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Tax name", result.Message);
        }

        [Fact]
        public async Task CreateTaxAsync_InvalidTaxNameEmpty_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = string.Empty,
                Rate = 5.0m
            };

            // Act
            var result = await _taxService.CreateTaxAsync(invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Tax name", result.Message);
        }

        [Fact]
        public async Task CreateTaxAsync_InvalidTaxNameTooLong_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = new string('a', 101),
                Rate = 5.0m
            };

            // Act
            var result = await _taxService.CreateTaxAsync(invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Tax name", result.Message);
        }

        [Fact]
        public async Task CreateTaxAsync_InvalidTaxDescriptionTooHigh_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = "valid tax name",
                TaxDescription = new string('a', 251),
                Rate = 5.0m
            };

            // Act
            var result = await _taxService.CreateTaxAsync(invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Tax description", result.Message);
        }

        [Fact]
        public async Task CreateTaxAsync_InvalidRateTooHigh_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = "valid tax name",
                Rate = 100.01m
            };

            // Act
            var result = await _taxService.CreateTaxAsync(invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Rate", result.Message);
        }

        [Fact]
        public async Task CreateTaxAsync_InvalidRateTooLow_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = "valid tax name",
                Rate = -0.01m
            };

            // Act
            var result = await _taxService.CreateTaxAsync(invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Rate", result.Message);
        }

        [Fact]
        public async Task CreateTaxAsync_ValidTax_ReturnsSuccessResponse()
        {
            // Arrange
            var validTaxDto = new TaxRequestDto
            {
                TaxName = "VAT",
                TaxDescription = "Value Added Tax",
                Rate = 15.0m
            };

            _taxRepositoryMock.Setup(repo => repo.AddTaxAsync(It.IsAny<Tax>()))
                .ReturnsAsync((Tax tax) =>
                {
                    tax.TaxId = 1;

                    return tax;
                });

            // Act
            var result = await _taxService.CreateTaxAsync(validTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.Success, result.Status);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal(1, result.Data.First().TaxId);
            Assert.Equal("VAT", result.Data.First().TaxName);
            Assert.Equal("Value Added Tax", result.Data.First().TaxDescription);
            Assert.Equal(15.0m, result.Data.First().Rate);
        }

        [Fact]
        public async Task UpdateTaxAsync_InvalidTaxNameNull_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = null!,
                Rate = 5.0m
            };

            // Act
            var result = await _taxService.UpdateTaxAsync(1, invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Tax name", result.Message);
        }

        [Fact]
        public async Task UpdateTaxAsync_InvalidTaxNameEmpty_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = string.Empty,
                Rate = 5.0m
            };

            // Act
            var result = await _taxService.UpdateTaxAsync(1, invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Tax name", result.Message);
        }

        [Fact]
        public async Task UpdateTaxAsync_InvalidTaxNameTooLong_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = new string('a', 101),
                Rate = 5.0m
            };

            // Act
            var result = await _taxService.UpdateTaxAsync(1, invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Tax name", result.Message);
        }

        [Fact]
        public async Task UpdateTaxAsync_InvalidTaxDescriptionTooHigh_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = "valid tax name",
                TaxDescription = new string('a', 251),
                Rate = 5.0m
            };

            // Act
            var result = await _taxService.UpdateTaxAsync(1, invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Tax description", result.Message);
        }

        [Fact]
        public async Task UpdateTaxAsync_InvalidRateTooHigh_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = "valid tax name",
                Rate = 100.01m
            };

            // Act
            var result = await _taxService.UpdateTaxAsync(1, invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Rate", result.Message);
        }

        [Fact]
        public async Task UpdateTaxAsync_InvalidRateTooLow_ReturnsValidationError()
        {
            // Arrange
            var invalidTaxDto = new TaxRequestDto
            {
                TaxName = "valid tax name",
                Rate = -0.01m
            };

            // Act
            var result = await _taxService.UpdateTaxAsync(1, invalidTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.ValidationError, result.Status);
            Assert.Contains("Rate", result.Message);
        }

        [Fact]
        public async Task UpdateTaxAsync_TaxIdNotFound_ReturnsNotFoundResponse()
        {
            // Arrange
            int taxId = 1;

            TaxRequestDto validTaxDto = new()
            {
                TaxName = "New VAT",
                TaxDescription = "New Description",
                Rate = 11.0m
            };

            _taxRepositoryMock.Setup(repo => repo.UpdateTaxAsync(taxId, It.IsAny<Tax>()))
                .ReturnsAsync(ResponseStatus.NotFound);

            // Act
            var result = await _taxService.UpdateTaxAsync(taxId, validTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.NotFound, result.Status);
        }

        [Fact]
        public async Task UpdateTaxAsync_ValidTax_ReturnsSuccessResponse()
        {
            // Arrange
            int taxId = 1;

            TaxRequestDto validTaxDto = new()
            {
                TaxName = "New VAT",
                TaxDescription = "New Description",
                Rate = 11.0m
            };

            _taxRepositoryMock.Setup(repo => repo.UpdateTaxAsync(taxId, It.IsAny<Tax>()))
                .ReturnsAsync(ResponseStatus.Success);

            // Act
            var result = await _taxService.UpdateTaxAsync(taxId, validTaxDto);

            // Assert
            Assert.Equal(ResponseStatus.Success, result.Status);
        }

        [Fact]
        public async Task DeleteTaxAsync_PermanentTax_ReturnsNotAllowedResponse()
        {
            // Arrange
            int taxId = 1;

            _taxRepositoryMock.Setup(repo => repo.RemoveTaxAsync(taxId))
                .ReturnsAsync(ResponseStatus.NotAllowed);

            // Act
            var result = await _taxService.DeleteTaxAsync(taxId);

            // Assert
            Assert.Equal(ResponseStatus.NotAllowed, result.Status);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task DeleteTaxAsync_PermanentTax_ReturnsNotFoundResponse()
        {
            // Arrange
            int taxId = 1;

            _taxRepositoryMock.Setup(repo => repo.RemoveTaxAsync(taxId))
                .ReturnsAsync(ResponseStatus.NotFound);

            // Act
            var result = await _taxService.DeleteTaxAsync(taxId);

            // Assert
            Assert.Equal(ResponseStatus.NotFound, result.Status);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task DeleteTaxAsync_PermanentTax_ReturnsConflictResponse()
        {
            // Arrange
            int taxId = 1;

            _taxRepositoryMock.Setup(repo => repo.RemoveTaxAsync(taxId))
                .ReturnsAsync(ResponseStatus.Conflict);

            // Act
            var result = await _taxService.DeleteTaxAsync(taxId);

            // Assert
            Assert.Equal(ResponseStatus.Conflict, result.Status);
            Assert.NotNull(result.Message);
        }

        [Fact]
        public async Task DeleteTaxAsync_PermanentTax_ReturnsSuccessResponse()
        {
            // Arrange
            int taxId = 1;

            _taxRepositoryMock.Setup(repo => repo.RemoveTaxAsync(taxId))
                .ReturnsAsync(ResponseStatus.Success);

            // Act
            var result = await _taxService.DeleteTaxAsync(taxId);

            // Assert
            Assert.Equal(ResponseStatus.Success, result.Status);
            Assert.Null(result.Message);
        }
    }
}
