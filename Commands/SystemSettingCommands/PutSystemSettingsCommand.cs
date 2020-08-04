namespace ApiTemplate.Commands.SystemSettingCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels.SystemSettingViewModels;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class PutSystemSettingsCommand : IPutSystemSettingsCommand
    {
        private readonly ISystemSettingRepository _systemSettingRepository;
        private readonly ISystemSettingGroupRepository _systemSettingGroupRepository;
        private readonly IMapper<Models.SystemSetting, SystemSetting> _systemSettingToSystemSettingMapper;
        private readonly IMapper<SaveSystemSetting, Models.SystemSetting> _saveSystemSettingToSystemSettingMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PutSystemSettingsCommand(
            ISystemSettingRepository systemSettingRepository,
            ISystemSettingGroupRepository systemSettingGroupRepository,
            IMapper<Models.SystemSetting, SystemSetting> systemSettingToSystemSettingMapper,
            IMapper<SaveSystemSetting, Models.SystemSetting> saveSystemSettingToSystemSettingMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _systemSettingRepository = systemSettingRepository;
            _systemSettingGroupRepository = systemSettingGroupRepository;
            _systemSettingToSystemSettingMapper = systemSettingToSystemSettingMapper;
            _saveSystemSettingToSystemSettingMapper = saveSystemSettingToSystemSettingMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(List<SaveSystemSetting> saveSystemSettings, CancellationToken cancellationToken)
        {
            var systemSettings = _saveSystemSettingToSystemSettingMapper.MapList(saveSystemSettings);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            

            //foreach(var item in systemSettings)
            //{
            //    item.ModifiedBy = userId;
            //}
            systemSettings = await _systemSettingRepository.AddList(systemSettings, cancellationToken);

            var systemSettingViewModel = _systemSettingToSystemSettingMapper.MapList(systemSettings);

            return new OkObjectResult(systemSettingViewModel);
        }
    }
}