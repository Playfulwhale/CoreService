namespace ApiTemplate.Commands.ZoneCommands
{
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Constants;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.ZoneViewModels;

    public class GetZonePageCommand :IGetZonePageCommand
    {
        private readonly IZoneRepository zoneRepository;
        private readonly IMapper<Models.Zone, Zone> zoneMapper;

        public GetZonePageCommand(
            IZoneRepository zoneRepository,
            IMapper<Models.Zone, Zone> zoneMapper)
        {
            this.zoneRepository = zoneRepository;
            this.zoneMapper = zoneMapper;
        }

        public async Task<IActionResult> ExecuteAsync(string all, PageOptions pageOptions, CancellationToken cancellationToken)
        {
            if(all == "1")
            {
                var zone = await zoneRepository.GetAll(cancellationToken);
                if (zone == null)
                {
                    return new NotFoundResult();
                }

                return new OkObjectResult(zone);
            }

            var zones = await zoneRepository.GetPage(pageOptions.Page.Value, pageOptions.Count.Value, cancellationToken);
            if (zones == null)
            {
                return new NotFoundResult();
            }

            var (totalCount, totalPages) = await zoneRepository.GetTotalPages(pageOptions.Count.Value, cancellationToken);
            var zoneViewModels = zoneMapper.MapList(zones);
            var page = new PageResult<Zone>()
            {
                Count = pageOptions.Count.Value,
                Items = zoneViewModels,
                Page = pageOptions.Page.Value,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };

            

            return new OkObjectResult(page);
        }

    }
}
