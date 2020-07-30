namespace ApiTemplate.Repositories
{
    using Data;
    using Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class CountryRepository : ICountryRepository
    {      
        private readonly DataContext _context;
        public CountryRepository(DataContext context)
        {
            _context = context;           
        }
 
        public async Task<Country> Add(Country country, CancellationToken cancellationToken)
        {
            await _context.Countries.AddAsync(country, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return country;
        }

        public async Task Delete(Country country, CancellationToken cancellationToken)
        {          
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Country> Get(int countryId, CancellationToken cancellationToken)
        {
            var country = await _context.Countries.FindAsync(countryId);
            return country;
        }

        public async Task<ICollection<Country>> GetPage(int page, int count, CancellationToken cancellationToken)
        {
            var countries = await _context.Countries.ToListAsync(cancellationToken);
            var pageCountries = countries
                .Skip(count * (page - 1))
                .Take(count)
                .ToList();
            if (pageCountries.Count == 0)
            {
                pageCountries = null;
            }

            return pageCountries;
        }
        public async Task<ICollection<Country>> PublicGetPage(int page, int count, CancellationToken cancellationToken)
        {
            var countries = await _context.Countries.ToListAsync(cancellationToken);
            countries = countries.Where(x => x.Active).ToList();
            var pageCountries = countries
                .Skip(count * (page - 1))
                .Take(count)
                .ToList();
            if (pageCountries.Count == 0)
            {
                pageCountries = null;
            }

            return pageCountries;
        }
 
        // Get countryId match with CountryCode
        public async Task<Country> GetCountryByCode(string code, CancellationToken cancellationToken)
        {
            return await _context.Countries.FirstOrDefaultAsync(x => x.IsoCode2 == code || x.IsoCode3 == code, cancellationToken);
        }

        public async Task<(int totalCount, int totalPages)> GetTotalPages(int count, CancellationToken cancellationToken)
        {
            var countries = await _context.Countries.ToListAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(countries.Count / (double)count);
            return (countries.Count, totalPages);
        }

        public async Task<(int totalCount, int totalPages)> PublicGetTotalPages(int count, CancellationToken cancellationToken)
        {
            var countries = await _context.Countries.ToListAsync(cancellationToken);
            countries = countries.Where(x => x.Active).ToList();
            var totalPages = (int)Math.Ceiling(countries.Count / (double)count);
            return (countries.Count, totalPages);
        }

        public async Task<Country> Update(Country country, CancellationToken cancellationToken)
        {           
            var existingCountry =  await _context.Countries.FirstOrDefaultAsync(x => x.Id == country.Id, cancellationToken);

            if (existingCountry != null)
            {
                existingCountry.Name = country.Name;
                existingCountry.IsoCode2 = country.IsoCode2;
                existingCountry.IsoCode3 = country.IsoCode3;
                existingCountry.AddressFormat = country.AddressFormat;
                existingCountry.PostcodeRequired = country.PostcodeRequired;
                //existingCountry.Status = country.Status;
                existingCountry.Default = country.Default;

                _context.Entry(existingCountry).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return country;
        }
      
        public async Task<Country> ToggleStatus(int countryId, CancellationToken cancellationToken)
        {
            var existingCountry = await _context.Countries.FindAsync(countryId);
            existingCountry.Active = !existingCountry.Active || !existingCountry.Active;
            _context.Entry(existingCountry).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return existingCountry;
        }

        public async Task<List<Country>> GetAll (CancellationToken cancellationToken)
        {
            var country = await _context.Countries.ToListAsync(cancellationToken);
            return country;
        }
        public async Task<List<Country>> PublicGetAll(CancellationToken cancellationToken)
        {
            var country = await _context.Countries.ToListAsync(cancellationToken);
            country = country.Where(x => x.Active).ToList();
            return country;
        }
        
    }
}
