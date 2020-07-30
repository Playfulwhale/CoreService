namespace ApiTemplate.Commands.ZoneCommands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;

    public class GetZoneAllCommand : IGetZoneAllCommand
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IZoneRepository zoneRepository;

        public GetZoneAllCommand(
            IActionContextAccessor actionContextAccessor,
            IZoneRepository zoneRepository)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.zoneRepository = zoneRepository;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var zone = await zoneRepository.GetAll(cancellationToken);
            if (zone == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(zone);
        }
    }
}
