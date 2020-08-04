namespace ApiTemplate.Commands.SystemSettingGroupCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels.SystemSettingViewModels;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllSystemSettingGroupParamCommand : IGetAllSystemSettingGroupParamCommand
    {
        private readonly ISystemSettingRepository _systemSettingRepository;
        private readonly IMapper<Models.SystemSetting, SystemSetting> _systemSettingMapper;

        public GetAllSystemSettingGroupParamCommand(
            ISystemSettingRepository systemSettingRepository,
            IMapper<Models.SystemSetting, SystemSetting> systemSettingMapper)
        {
            _systemSettingRepository = systemSettingRepository;
            _systemSettingMapper = systemSettingMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var systemSettings = await _systemSettingRepository.GetAll(cancellationToken);

            var systemSettingViewModels = new List<SystemSetting>();

            foreach (var systemSetting in systemSettings)
            {
                if (systemSetting.GroupId == id)
                {
                    var systemSettingViewModel = _systemSettingMapper.Map(systemSetting);
                    systemSettingViewModels.Add(systemSettingViewModel);
                }
            }

            return new OkObjectResult(systemSettingViewModels);
        }
    }
}