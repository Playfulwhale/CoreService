namespace ApiTemplate.Commands.ZoneCommands
{
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.ZoneViewModels;

    public class PutZoneActiveCommand : IPutZoneActiveCommand
    {
        private readonly IZoneRepository zoneRepository;
        private readonly IMapper<Models.Zone, Zone> zoneToZoneMapper;
        //private readonly IMapper<SaveZone, Models.Zone> saveZoneToZoneMapper;
        public PutZoneActiveCommand(
            IZoneRepository zoneRepository,
            IMapper<Models.Zone, Zone> zoneToZoneMapper)
        {
            this.zoneRepository = zoneRepository;
            this.zoneToZoneMapper = zoneToZoneMapper;
            //this.saveZoneToZoneMapper = saveZoneToZoneMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int zoneId, CancellationToken cancellationToken)
        {
            var zone = await zoneRepository.Get(zoneId, cancellationToken);
            if (zone == null)
            {
                return new NotFoundResult();
            }

            zone = await zoneRepository.ToggleStatus(zoneId, cancellationToken);
            var zoneViewModel = zoneToZoneMapper.Map(zone);

            return new OkObjectResult(zoneViewModel);
        }
    }
}
