using Microsoft.EntityFrameworkCore;
using RentCar.Application.Dtos.Responses;
using RentCar.Domain.Entities;
using RentCar.Infrastructure.Repositories;
using System.ClientModel.Primitives;

namespace RentCart.Application.Test.Repositories
{
    public class TaxRepositoryTest
    {
        private DbContextOptions<RentCarContext> GetTokenOptions()
        {
            return new DbContextOptionsBuilder<RentCarContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task AddTaxAsync_ShouldAddTax()
        {
            // Arrange
            using RentCarContext context = new(GetTokenOptions());
            TaxRepository repository = new(context);

            Tax tax = new() { TaxName = "Test Tax", TaxDescription = "Test description.", Rate = 10m };

            // Act
            var result = await repository.AddTaxAsync(tax);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Tax", result.TaxName);
            Assert.Equal("Test description.", result.TaxDescription);
            Assert.Equal(10m, result.Rate);
        }

        [Fact]
        public async Task GetTaxByIdAsync_ShouldReturnTax_WhenTaxExists()
        {
            // Arrange
            using RentCarContext context = new(GetTokenOptions());
            TaxRepository repository = new(context);

            Tax tax = new() { TaxName = "Existing Tax", TaxDescription = "Existing description.", Rate = 15m };

            context.Taxes.Add(tax);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetTaxByIdAsync(tax.TaxId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.TaxId > 0);
            Assert.Equal("Existing Tax", result.TaxName);
            Assert.Equal("Existing description.", result.TaxDescription);
            Assert.Equal(15m, result.Rate);
        }

        [Fact]
        public async Task GetTaxByIdAsync_ShouldReturnNull_WhenTaxDoesNotExist()
        {
            // Arrange
            using RentCarContext context = new(GetTokenOptions());
            TaxRepository repository = new(context);

            // Act
            var result = await repository.GetTaxByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTaxesAsync_ShouldReturnAllTaxes()
        {
            // Arrange
            using RentCarContext context = new(GetTokenOptions());
            TaxRepository repository = new(context);

            context.Taxes.AddRange(
                new Tax() { TaxName = "Tax 1", TaxDescription = "Description 1", Rate = 5m },
                new Tax() { TaxName = "Tax 2", TaxDescription = "Description 2", Rate = 10m }
            );

            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetTaxesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            Assert.Equal("Tax 1", result.First().TaxName);
            Assert.Equal("Description 1", result.First().TaxDescription);
            Assert.Equal(5m, result.First().Rate);

            Assert.Equal("Tax 2", result.Last().TaxName);
            Assert.Equal("Description 2", result.Last().TaxDescription);
            Assert.Equal(10m, result.Last().Rate);
        }

        [Fact]
        public async Task UpdateTaxAsync_ShouldUpdateExistingTax()
        {
            // Arrange
            using RentCarContext context = new(GetTokenOptions());
            TaxRepository repository = new(context);

            Tax tax = new() { TaxName = "Old Tax", TaxDescription = "Old description.", Rate = 20m };

            context.Taxes.Add(tax);
            await context.SaveChangesAsync();

            Tax updatedTax = new()
            {
                TaxName = "Updated Tax",
                TaxDescription = "Updated description.",
                Rate = 25m
            };

            // Act
            var result = await repository.UpdateTaxAsync(tax.TaxId, updatedTax);
            var fetchedTax = await repository.GetTaxByIdAsync(tax.TaxId);

            // Assert
            Assert.Equal(ResponseStatus.Success, result);
            Assert.NotNull(fetchedTax);
            Assert.Equal("Updated Tax", fetchedTax.TaxName);
            Assert.Equal("Updated description.", fetchedTax.TaxDescription);
            Assert.Equal(25m, fetchedTax.Rate);
        }

        [Fact]
        public async Task UpdateTaxAsync_ShouldReturnNotFound_WhenTaxDoesNotExist()
        {
            // Arrange
            using RentCarContext context = new(GetTokenOptions());
            TaxRepository repository = new(context);

            Tax updatedTax = new()
            {
                TaxName = "Non-existing Tax",
                TaxDescription = "Non-existing description.",
                Rate = 30m
            };

            // Act
            var result = await repository.UpdateTaxAsync(999, updatedTax);

            // Assert
            Assert.Equal(ResponseStatus.NotFound, result);
        }

        [Fact]
        public async Task RemoveTaxAsync_NotAllowedToRemoveRegularTax_ReturnsNotAllowed()
        {
            // Arrange
            using RentCarContext context = new(GetTokenOptions());
            TaxRepository repository = new(context);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Ensure the order ID when executing all tests together.
            context.Taxes.Add(new()
            {
                TaxName = "Tax regular",
                TaxDescription = "Regular Description.",
                Rate = 15m
            });
            await context.SaveChangesAsync();

            context.Taxes.Add(new()
            {
                TaxName = "Tax reduced",
                TaxDescription = "Reduced Description.",
                Rate = 5m
            });
            await context.SaveChangesAsync();

            int taxId = 1;

            // Act
            var result = await repository.RemoveTaxAsync(taxId);

            // Assert
            Assert.Equal(ResponseStatus.NotAllowed, result);
        }

        [Fact]
        public async Task RemoveTaxAsync_NotAllowedToRemoveReducedTax_ReturnsNotAllowed()
        {
            // Arrange
            using RentCarContext context = new(GetTokenOptions());
            TaxRepository repository = new(context);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Ensure the order ID when executing all tests together.
            context.Taxes.Add(new()
            {
                TaxName = "Tax regular",
                TaxDescription = "Regular Description.",
                Rate = 15m
            });
            await context.SaveChangesAsync();

            context.Taxes.Add(new()
            {
                TaxName = "Tax reduced",
                TaxDescription = "Reduced Description.",
                Rate = 5m
            });
            await context.SaveChangesAsync();

            int taxId = 2;

            // Act
            var result = await repository.RemoveTaxAsync(taxId);

            // Assert
            Assert.Equal(ResponseStatus.NotAllowed, result);
        }

        [Fact]
        public async Task RemoveTaxAsync_ShouldRemoveTax_WhenTaxIsRemovable()
        {
            // Arrange
            using RentCarContext context = new(GetTokenOptions());
            TaxRepository repository = new(context);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Ensure the order ID when executing all tests together.
            context.Taxes.Add(new()
            {
                TaxName = "Tax regular",
                TaxDescription = "Regular Description.",
                Rate = 15m
            });
            await context.SaveChangesAsync();

            context.Taxes.Add(new()
            {
                TaxName = "Tax reduced",
                TaxDescription = "Reduced Description.",
                Rate = 5m
            });
            await context.SaveChangesAsync();

            Tax taxExtra = new()
            {
                TaxName = "Tax extra",
                TaxDescription = "Extra Description.",
                Rate = 0m
            };

            context.Taxes.Add(taxExtra);
            await context.SaveChangesAsync();

            int taxId = taxExtra.TaxId;

            // Act
            var result = await repository.RemoveTaxAsync(taxId);
            var fetchedTax = await repository.GetTaxByIdAsync(taxId);

            // Assert
            Assert.Equal(ResponseStatus.Success, result);
            Assert.Null(fetchedTax);
        }

        [Fact]
        public async Task RemoveTaxAsync_ShouldReturnNotFound_WhenTaxDoesNotExist()
        {
            // Arrange
            using RentCarContext context = new(GetTokenOptions());
            TaxRepository repository = new(context);
            // Act

            var result = await repository.RemoveTaxAsync(3);

            // Assert
            Assert.Equal(ResponseStatus.NotFound, result);
        }

        [Fact]
        public async Task RemoveTaxAsync_TaxIsInUse_ReturnsConfict()
        {
            // Arrange
            using RentCarContext context = new(GetTokenOptions());
            TaxRepository repository = new(context);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Ensure the order ID when executing all tests together.
            context.Taxes.Add(new()
            {
                TaxName = "Tax regular",
                TaxDescription = "Regular Description.",
                Rate = 15m
            });
            await context.SaveChangesAsync();

            context.Taxes.Add(new()
            {
                TaxName = "Tax reduced",
                TaxDescription = "Reduced Description.",
                Rate = 5m
            });
            await context.SaveChangesAsync();

            Tax taxExtra = new()
            {
                TaxName = "Tax extra",
                TaxDescription = "Extra Description.",
                Rate = 0m
            };

            context.Taxes.Add(taxExtra);
            await context.SaveChangesAsync();

            Rental rental = new()
            {
                TaxId = taxExtra.TaxId,
            };
            context.Rentals.Add(rental);
            await context.SaveChangesAsync();

            int taxId = taxExtra.TaxId;

            // Act
            var result = await repository.RemoveTaxAsync(taxId);
            
            // Assert
            Assert.Equal(ResponseStatus.Conflict, result);
        }
    }
}
