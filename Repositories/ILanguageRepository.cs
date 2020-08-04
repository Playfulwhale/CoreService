namespace ApiTemplate.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public interface ILanguageRepository
    {
        Task<Language> Add(Language language, CancellationToken cancellationToken);

        Task Delete(Language language, CancellationToken cancellationToken);

        Task<Language> Get(int languageId, CancellationToken cancellationToken);

        Task<List<Language>> GetAll(CancellationToken cancellationToken);

        Task<ICollection<Language>> GetPage(int page, int count, CancellationToken cancellationToken);

        Task<(int totalCount, int totalPages)> GetTotalPages(int count, CancellationToken cancellationToken);

        Task<Language> Update(Language language, CancellationToken cancellationToken);

        Task<Language> UpdateDefault(Language language, CancellationToken cancellationToken);

        Task<Language> UpdateActive(Language language, CancellationToken cancellationToken);
    }
}
