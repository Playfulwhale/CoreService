namespace ApiTemplate.Repositories
{
    using Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IZoneRepository
    {
        Task<Zone> Add(Zone zone, CancellationToken cancellationToken);

        Task Delete(Zone zone, CancellationToken cancellationToken);

        Task<Zone> Get(int zoneId, CancellationToken cancellationToken);

        Task<ICollection<Zone>> GetPage(int page, int count, CancellationToken cancellationToken);

        Task<List<Zone>> GetAll(CancellationToken cancellationToken);

        Task<(int totalCount, int totalPages)> GetTotalPages(int count, CancellationToken cancellationToken);

        Task<(int totalCount, int totalPages)> GetCountryIdTotalPages(int cuntryId, int count, CancellationToken cancellationToken);

        Task<Zone> Update(Zone zone, CancellationToken cancellationToken);

        Task<Zone> ToggleStatus(int zoneId, CancellationToken cancellationToken);

        Task<ICollection<Zone>> GetZoneCountryId(int countryId, CancellationToken cancellationToken);

        Task<ICollection<Zone>> GetCountryIdPage(int countryId, int page, int count, CancellationToken cancellationToken);

        Task<ICollection<Zone>> GetZoneAllByCountry(int countryId, CancellationToken cancellationToken);

        Task<List<Zone>> PublicGetZoneAll(int countryId, CancellationToken cancellationToken);

        Task<ICollection<Zone>> PublicGetZonePage(int countryId, int page, int count, CancellationToken cancellationToken);
    }
}
