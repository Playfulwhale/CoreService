namespace ApiTemplate.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public interface ICurrencyRepository
    {
        Task<Currency> Add(Currency currency, CancellationToken cancellationToken);

        Task Delete(Currency currency, CancellationToken cancellationToken);

        Task<Currency> Get(int currencyId, CancellationToken cancellationToken);

        Task<ICollection<Currency>> GetPage(int page, int count, CancellationToken cancellationToken);

        Task<List<Currency>> GetAll(CancellationToken cancellationToken);

        Task<(int totalCount, int totalPages)> GetTotalPages(int count, CancellationToken cancellationToken);

        Task<Currency> Update(Currency currency, CancellationToken cancellationToken);

        Task<Currency> ToggleStatus(Currency currency, CancellationToken cancellationToken);

        Task<Currency> Default(Currency currency, CancellationToken cancellationToken);
    }
}
