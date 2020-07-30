namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.ZoneViewModels;

    public class GetZoneAllByCountryCommand : IGetZoneAllByCountryCommand
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper<Models.Zone, Zone> _zoneMapper;
        public GetZoneAllByCountryCommand(
            IZoneRepository zoneRepository,
            ICountryRepository countryRepository,
            IMapper<Models.Zone, Zone> zoneMapper)
        {
            _zoneRepository = zoneRepository;
            _countryRepository = countryRepository;
            _zoneMapper = zoneMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int countryId, CancellationToken cancellationToken)
        {
            var zones = await _zoneRepository.GetZoneAllByCountry(countryId, cancellationToken);
            if (zones == null)
            {
                return new NotFoundResult();
            }
            var zoneViewModels = _zoneMapper.MapList(zones);
            return new OkObjectResult(zoneViewModels);
        }
    }
}
