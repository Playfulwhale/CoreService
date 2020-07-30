namespace ApiTemplate.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;


    public interface ICountryRepository
    {
        Task<Country> Add(Country country, CancellationToken cancellationToken);

        Task Delete(Country country, CancellationToken cancellationToken);

        Task<Country> Get(int countryId, CancellationToken cancellationToken);

        Task<ICollection<Country>> GetPage(int page, int count, CancellationToken cancellationToken); 

        Task<ICollection<Country>> PublicGetPage(int page, int count, CancellationToken cancellationToken);

        Task<List<Country>> GetAll(CancellationToken cancellationToken);

        Task<List<Country>> PublicGetAll(CancellationToken cancellationToken);

        Task<(int totalCount, int totalPages)> GetTotalPages(int count, CancellationToken cancellationToken);

        Task<(int totalCount, int totalPages)> PublicGetTotalPages(int count, CancellationToken cancellationToken);

        Task<Country> Update(Country country, CancellationToken cancellationToken); 

        Task<Country> ToggleStatus(int countryId, CancellationToken cancellationToken);

        Task<Country> GetCountryByCode(string code, CancellationToken cancellationToken);
    

    }
}
