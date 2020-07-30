namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.ZoneViewModels;

    public class PublicGetZoneAllByCountryCodeCommand : IPublicGetZoneAllByCountryCodeCommand
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper<Models.Zone, PublicZone> _zoneMapper;
        public PublicGetZoneAllByCountryCodeCommand(
            IZoneRepository zoneRepository,
            ICountryRepository countryRepository,
            IMapper<Models.Zone, PublicZone> zoneMapper)
        {
            _zoneRepository = zoneRepository;
            _countryRepository = countryRepository;
            _zoneMapper = zoneMapper;
        }

        public async Task<IActionResult> ExecuteAsync(string all,string code,ViewModels.CountryViewModels.PageOptions pageOptions,CancellationToken cancellationToken)
        {
            var country = await _countryRepository.GetCountryByCode(code, cancellationToken);
            if (country == null)
            {
                return new NoContentResult();
            }
            if (all == "1")
            {
                var zone = await _zoneRepository.PublicGetZoneAll(country.Id, cancellationToken);
                var zoneViewModels = _zoneMapper.MapList(zone);
                if (zone == null)
                {
                    return new NoContentResult();
                }
                return new OkObjectResult(zoneViewModels);
            }
            var zones = await _zoneRepository.PublicGetZonePage(country.Id, pageOptions.Page.Value, pageOptions.Count.Value, cancellationToken);
            if (zones == null)
            {
                return new NoContentResult();
            }
            var totalCount = zones.Count;
            var totalPages = (int)Math.Ceiling(zones.Count / (double)pageOptions.Count.Value);
            var countryViewModels = _zoneMapper.MapList(zones);
            var page = new PageResult<PublicZone>()
            {
                Count = pageOptions.Count.Value,
                Items = countryViewModels,
                Page = pageOptions.Page.Value,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };

             return new OkObjectResult(page);
        }
        
    }
}
