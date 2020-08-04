namespace ApiTemplate.Commands.SystemSettingCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Repositories;
    using ViewModels.SystemSettingViewModels;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllSystemSettingCommand : IGetAllSystemSettingCommand
    {
        private readonly ISystemSettingRepository _systemSettingRepository;
        private readonly IMapper<Models.SystemSetting, SystemSetting> _systemSettingMapper;

        public GetAllSystemSettingCommand(
            ISystemSettingRepository systemSettingRepository,
            IMapper<Models.SystemSetting, SystemSetting> systemSettingMapper)
        {
            _systemSettingRepository = systemSettingRepository;
            _systemSettingMapper = systemSettingMapper;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var systemSettings = await _systemSettingRepository.GetAll(cancellationToken);

            var systemSettingViewModels = _systemSettingMapper.MapList(systemSettings);

            return new OkObjectResult(systemSettingViewModels);
        }
    }
}