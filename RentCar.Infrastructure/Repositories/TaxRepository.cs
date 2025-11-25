using Microsoft.EntityFrameworkCore;
using RentCar.Application.Contracts.Repositories;
using RentCar.Application.Dtos.Responses;
using RentCar.Domain.Entities;

namespace RentCar.Infrastructure.Repositories
{
    public class TaxRepository(RentCarContext context) : ITaxRepository
    {
        private readonly RentCarContext _context = context;

        public async Task<Tax?> GetTaxByIdAsync(int taxId)
        {
            return await _context.Taxes.FindAsync(taxId);
        }

        public async Task<IEnumerable<Tax>> GetTaxesAsync()
        {
            return await _context.Taxes.ToListAsync();
        }

        public async Task<Tax> AddTaxAsync(Tax tax)
        {
            await _context.Taxes.AddAsync(tax);

            await _context.SaveChangesAsync();

            return tax;
        }

        public async Task<ResponseStatus> UpdateTaxAsync(int taxId, Tax tax)
        {
            Tax? existingTax = await GetTaxByIdAsync(taxId);

            if (existingTax == null)
            {
                return ResponseStatus.NotFound;
            }

            tax.TaxId = taxId;
            _context.Entry(existingTax).CurrentValues.SetValues(tax);

            await _context.SaveChangesAsync();

            return ResponseStatus.Success;
        }

        public async Task<ResponseStatus> RemoveTaxAsync(int taxId)
        {
            if (Tax.IsPermanentTax(taxId))
            {
                return ResponseStatus.NotAllowed;
            }

            Tax? tax = await _context.Taxes
                .Include(item => item.Rentals)
                .FirstOrDefaultAsync(item => item.TaxId == taxId);

            if (tax == null)
            {
                return ResponseStatus.NotFound;
            }

            if (!tax.IsRemovable())
            {
                return ResponseStatus.Conflict;
            }

            _context.Taxes.Remove(tax);

            await _context.SaveChangesAsync();

            return ResponseStatus.Success;
        }
    }
}
