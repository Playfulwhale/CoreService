namespace ApiTemplate.Commands.ZoneCommands
{
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Net.Http.Headers;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.ZoneViewModels;

    public class GetZoneCommand : IGetZoneCommand
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IZoneRepository zoneRepository;
        private readonly IMapper<Models.Zone, Zone> zoneMapper;

        public GetZoneCommand(
            IActionContextAccessor actionContextAccessor,
            IZoneRepository zoneRepository,
            IMapper<Models.Zone, Zone> zoneMapper)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.zoneRepository = zoneRepository;
            this.zoneMapper = zoneMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int zoneId, CancellationToken cancellationToken)
        {
            var zone = await zoneRepository.Get(zoneId, cancellationToken);
            if (zone == null)
            {
                return new NotFoundResult();
            }

            var httpContext = actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= zone.ModifiedAt))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var zoneViewModel = zoneMapper.Map(zone);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, zone.ModifiedAt.ToString());
            return new OkObjectResult(zoneViewModel);
        }
    }
}
