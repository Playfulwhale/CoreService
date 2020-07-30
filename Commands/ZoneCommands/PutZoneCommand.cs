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

    public class PutZoneCommand : IPutZoneCommand
    {
        private readonly IZoneRepository zoneRepository;
        private readonly IMapper<Models.Zone, Zone> zoneToZoneMapper;
        private readonly IMapper<SaveZone, Models.Zone> saveZoneToZoneMapper;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public PutZoneCommand(
            IZoneRepository zoneRepository,
            IMapper<Models.Zone, Zone> zoneToZoneMapper,
            IMapper<SaveZone, Models.Zone> saveZoneToZoneMapper)
        {
            this.zoneRepository = zoneRepository;
            this.zoneToZoneMapper = zoneToZoneMapper;
            this.saveZoneToZoneMapper = saveZoneToZoneMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int zoneId, SaveZone saveZone, CancellationToken cancellationToken)
        {
            var zone = await zoneRepository.Get(zoneId, cancellationToken);
            if (zone == null)
            {
                return new NotFoundResult();
            }

            saveZoneToZoneMapper.Map(saveZone, zone);

            // add created by
            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();

            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub").Value;
            
            //zone.ModifiedBy = userId;
            // end created by

            zone = await zoneRepository.Update(zone, cancellationToken);
            var zoneViewModel = zoneToZoneMapper.Map(zone);

            return new OkObjectResult(zoneViewModel);
        }
    }
}
