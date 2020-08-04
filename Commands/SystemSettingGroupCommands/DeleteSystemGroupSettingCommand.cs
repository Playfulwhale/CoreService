namespace ApiTemplate.Commands.SystemSettingGroupCommands
{
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteSystemSettingGroupCommand : IDeleteSystemSettingGroupCommand
    {
        private readonly ISystemSettingGroupRepository _systemSettingGroupRepository;

        public DeleteSystemSettingGroupCommand(ISystemSettingGroupRepository systemSettingGroupRepository)
        {
            _systemSettingGroupRepository = systemSettingGroupRepository;
        }

        public async Task<IActionResult> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var systemSettingGroup = await _systemSettingGroupRepository.Get(id, cancellationToken);
            if (systemSettingGroup == null)
            {
                return new NotFoundResult();
            }

            await _systemSettingGroupRepository.Delete(systemSettingGroup, cancellationToken);

            return new NoContentResult();
        }
    }
}