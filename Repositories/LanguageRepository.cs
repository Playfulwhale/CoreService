namespace ApiTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class LanguageRepository : ILanguageRepository
    {
        private readonly DataContext _context;

        public LanguageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Language> Add(Language language, CancellationToken cancellationToken)
        {
            await _context.Languages.AddAsync(language, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return language;
        }

        public async Task Delete(Language language, CancellationToken cancellationToken)
        {
            if (await _context.Languages.ContainsAsync(language, cancellationToken))
            {
                if (language.IsDefault == false)
                {
                    _context.Languages.Remove(language);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
        }

        public async Task<Language> Get(int id, CancellationToken cancellationToken)
        {
            var language = await _context.Languages.FindAsync(id);
            return language;
        }



        public async Task<ICollection<Language>> GetPage(int page, int count, CancellationToken cancellationToken)
        {
            var laguages = await _context.Languages.ToListAsync(cancellationToken);
            var pageLanguages = laguages
                .Skip(count * (page - 1))
                .Take(count)
                .ToList();
            if (pageLanguages.Count == 0)
            {
                pageLanguages = null;
            }

            return pageLanguages;
        }

        public async Task<(int totalCount, int totalPages)> GetTotalPages(int count, CancellationToken cancellationToken)
        {
            var totalPages = (int)Math.Ceiling(await _context.Languages.CountAsync(cancellationToken) / (double)count);
            return (_context.Languages.Count(), totalPages);
        }

        public async Task<Language> Update(Language language, CancellationToken cancellationToken)
        {

            var existingLanguage = await _context.Languages.FirstOrDefaultAsync(x => x.Id == language.Id, cancellationToken);
            if (existingLanguage != null)
            {
                existingLanguage.Code = language.Code;
                existingLanguage.Title = language.Title;
                existingLanguage.Flag = language.Flag;
                existingLanguage.IsDefault = language.IsDefault;
            }

            _context.Entry(language).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return language;
        }

        public async Task<Language> UpdateDefault(Language language, CancellationToken cancellationToken)
        {
            // Tìm language đã tồn tại trong db
            var existingLanguage = await _context.Languages.FirstOrDefaultAsync(x => x.Id == language.Id, cancellationToken);

            // Tìm language có IsDefault = true
            var langugagesDefault = await _context.Languages.Where(x => x.IsDefault == true).ToListAsync();

            if (langugagesDefault != null)
            {
                foreach(var item in langugagesDefault)
                {
                    item.IsDefault = false;
                }
                _context.Entry(language).State = EntityState.Modified;
            }
            existingLanguage.IsDefault = true;
            await _context.SaveChangesAsync(cancellationToken);
            return language;
        }

        public async Task<Language> UpdateActive(Language language, CancellationToken cancellationToken)
        {
            var existingLanguage = await _context.Languages.FirstOrDefaultAsync(x => x.Id == language.Id, cancellationToken);
            if (existingLanguage != null) existingLanguage.Active = language.Active;
            _context.Entry(language).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return language;
        }

        public async Task<List<Language>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Languages.ToListAsync(cancellationToken);
        }
    }
}
