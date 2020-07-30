namespace ApiTemplate.Commands.ZoneCommands
{
    using Boxed.Mapping;
    using Constants;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.ZoneViewModels;

    public class PostZoneCommand : IPostZoneCommand
    {
        private readonly IZoneRepository zoneRepository;
        private readonly IMapper<Models.Zone, Zone> zoneToZoneMapper;
        private readonly IMapper<SaveZone, Models.Zone> saveZoneToZoneMapper;

        public PostZoneCommand(
            IZoneRepository zoneRepository,
            IMapper<Models.Zone, Zone> zoneToZoneMapper,
            IMapper<SaveZone, Models.Zone> saveZoneToZoneMapper)
        {
            this.zoneRepository = zoneRepository;
            this.zoneToZoneMapper = zoneToZoneMapper;
            this.saveZoneToZoneMapper = saveZoneToZoneMapper;
        }

        public async Task<IActionResult> ExecuteAsync(SaveZone saveZone, CancellationToken cancellationToken)
        {
            var listZone = await zoneRepository.GetZoneCountryId(saveZone.CountryId, cancellationToken);
            var selectZone = listZone.SingleOrDefault(x => x.Title == saveZone.Title || x.Code == saveZone.Code);
            if(selectZone != null)
            {
                return new NoContentResult();
            }
            var zone = saveZoneToZoneMapper.Map(saveZone);
            // add created by
            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();

            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub").Value;

            //if (zone.CreatedBy == null)
            //    zone.CreatedBy = userId;
            //zone.ModifiedBy = userId;
            // end created by

            zone = await zoneRepository.Add(zone, cancellationToken);
            var zoneViewModel = zoneToZoneMapper.Map(zone);
            return new CreatedAtRouteResult(
                ZonesControllerRoute.GetZone,
                new { zoneId = zoneViewModel.Id },
                zoneViewModel);
        }
    }
}
