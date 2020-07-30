namespace ApiTemplate.Commands.ZoneCommands
{
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteZoneCommand : IDeleteZoneCommand
    {
        private readonly IZoneRepository zoneRepository;

        public DeleteZoneCommand(IZoneRepository zoneRepository) =>
            this.zoneRepository = zoneRepository;

        public async Task<IActionResult> ExecuteAsync(int zoneId, CancellationToken cancellationToken)
        {
            var zone = await zoneRepository.Get(zoneId, cancellationToken);
            if (zone == null)
            {
                return new NotFoundResult();
            }

            await zoneRepository.Delete(zone, cancellationToken);

            return new NoContentResult();
        }
    }
}
