namespace ApiTemplate.Commands.SystemSettingGroupCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels.SystemSettingGroupViewModels;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetSystemSettingGroupCommand : IGetSystemSettingGroupCommand
    {
        private readonly ISystemSettingGroupRepository _systemSettingGroupRepository;
        private readonly IMapper<Models.SystemSettingGroup, SystemSettingGroup> _systemSettingGroupMapper;

        public GetSystemSettingGroupCommand(
            ISystemSettingGroupRepository systemSettingGroupRepository,
            IMapper<Models.SystemSettingGroup, SystemSettingGroup> systemSettingGroupMapper)
        {
            _systemSettingGroupRepository = systemSettingGroupRepository;
            _systemSettingGroupMapper = systemSettingGroupMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var systemSettingGroup = await _systemSettingGroupRepository.Get(id, cancellationToken);
            if (systemSettingGroup == null)
            {
                return new NotFoundResult();
            }

            var systemSettingGroupViewModel = _systemSettingGroupMapper.Map(systemSettingGroup);

            return new OkObjectResult(systemSettingGroupViewModel);
        }
    }
}