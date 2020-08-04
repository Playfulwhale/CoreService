namespace ApiTemplate.Commands.SystemSettingCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Repositories;
    using ViewModels.SystemSettingViewModels;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetSystemSettingCommand : IGetSystemSettingCommand
    {
        private readonly ISystemSettingRepository _systemSettingRepository;
        private readonly IMapper<Models.SystemSetting, SystemSetting> _systemSettingMapper;

        public GetSystemSettingCommand(
            ISystemSettingRepository systemSettingRepository,
            IMapper<Models.SystemSetting, SystemSetting> systemSettingMapper)
        {
            _systemSettingRepository = systemSettingRepository;
            _systemSettingMapper = systemSettingMapper;
        }

        public async Task<IActionResult> ExecuteAsync(string key, CancellationToken cancellationToken)
        {
            var systemSetting = await _systemSettingRepository.GetByKey(key, cancellationToken);
            if (systemSetting == null)
            {
                return new NotFoundResult();
            }

            var systemSettingViewModel = _systemSettingMapper.Map(systemSetting);

            return new OkObjectResult(systemSettingViewModel);
        }
    }
}