namespace ApiTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly DataContext _context;

        public CurrencyRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<Currency> Add(Currency currency, CancellationToken cancellationToken)
        {
            await _context.Currencies.AddAsync(currency, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return currency;
        }

        public async Task Delete(Currency currency, CancellationToken cancellationToken)
        {
            if (await _context.Currencies.ContainsAsync(currency, cancellationToken))
            {
                _context.Currencies.Remove(currency);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Currency> Get(int id, CancellationToken cancellationToken)
        {
            var currency = await _context.Currencies.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return currency;
        }

        public async Task<ICollection<Currency>> GetPage(int page, int count, CancellationToken cancellationToken)
        {
            var listCurrenies = await _context.Currencies.ToListAsync(cancellationToken);
            var pageCurrencies = listCurrenies.Skip(count * (page - 1))
                .Take(count)
                .ToList();
            if (pageCurrencies.Count == 0)
            {
                pageCurrencies = null;
            }

            return pageCurrencies;
        }

        public async Task<(int totalCount, int totalPages)> GetTotalPages(int count, CancellationToken cancellationToken)
        {
            var totalPages = (int)Math.Ceiling(await _context.Currencies.CountAsync(cancellationToken) / (double)count);
            return (await _context.Currencies.CountAsync(cancellationToken), totalPages);
        }

        public async Task<Currency> Update(Currency currency, CancellationToken cancellationToken)
        {
            _context.Entry(currency).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return currency;
        }

        public async Task<Currency> ToggleStatus(Currency currency, CancellationToken cancellationToken)
        {
            var existingCurrency = await _context.Currencies.FirstOrDefaultAsync(x => x.Id == currency.Id, cancellationToken);
            if (existingCurrency == null) return null;
            existingCurrency.Active = !currency.Active;
            _context.Entry(currency).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return existingCurrency;
        }

        public async Task<Currency> Default(Currency currency, CancellationToken cancellationToken)
        {
            var existingCurrency = await _context.Currencies.FirstOrDefaultAsync(x => x.Id == currency.Id, cancellationToken);
            var currenciesDefault = await _context.Currencies.Where(x => x.Default).ToListAsync(cancellationToken);

            if (currenciesDefault != null)
            {
                foreach (var item in currenciesDefault)
                {
                    item.Default = false;
                }
                _context.Entry(currency).State = EntityState.Modified;
            }
            existingCurrency.Default = true;
            await _context.SaveChangesAsync(cancellationToken);
            return currency;           
        }

        public async Task<List<Currency>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Currencies.ToListAsync(cancellationToken);
        }
    }
}
