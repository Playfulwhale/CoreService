namespace ApiTemplate.Commands.ZoneCommands
{
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.ZoneViewModels;

    public class PatchZoneCommand : IPatchZoneCommand
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IObjectModelValidator objectModelValidator;
        private readonly IZoneRepository zoneRepository;
        private readonly IMapper<Models.Zone, Zone> zoneToZoneMapper;
        private readonly IMapper<Models.Zone, SaveZone> zoneToSaveZoneMapper;
        private readonly IMapper<SaveZone, Models.Zone> saveZoneToZoneMapper;

        public PatchZoneCommand(
            IActionContextAccessor actionContextAccessor,
            IObjectModelValidator objectModelValidator,
            IZoneRepository zoneRepository,
            IMapper<Models.Zone, Zone> zoneToZoneMapper,
            IMapper<Models.Zone, SaveZone> zoneToSaveZoneMapper,
            IMapper<SaveZone, Models.Zone> saveZoneToZoneMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.objectModelValidator = objectModelValidator;
            this.zoneRepository = zoneRepository;
            this.zoneToZoneMapper = zoneToZoneMapper;
            this.zoneToSaveZoneMapper = zoneToSaveZoneMapper;
            this.saveZoneToZoneMapper = saveZoneToZoneMapper;
        }

        public async Task<IActionResult> ExecuteAsync(
            int zoneId,
            JsonPatchDocument<SaveZone> patch,
            CancellationToken cancellationToken)
        {
            var zone = await zoneRepository.Get(zoneId, cancellationToken);
            if (zone == null)
            {
                return new NotFoundResult();
            }

            var saveZone = zoneToSaveZoneMapper.Map(zone);

           

            var modelState = actionContextAccessor.ActionContext.ModelState;
            patch.ApplyTo(saveZone, modelState);
            objectModelValidator.Validate(
                actionContextAccessor.ActionContext,
                validationState: null,
                prefix: null,
                model: saveZone);
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(modelState);
            }

            saveZoneToZoneMapper.Map(saveZone, zone);
            await zoneRepository.Update(zone, cancellationToken);
            var zoneViewModel = zoneToZoneMapper.Map(zone);

            return new OkObjectResult(zoneViewModel);
        }
    }
}
