namespace ApiTemplate.Commands.SystemSettingCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels.SystemSettingViewModels;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class PutSystemSettingCommand : IPutSystemSettingCommand
    {
        private readonly ISystemSettingRepository _systemSettingRepository;
        private readonly IMapper<Models.SystemSetting, SystemSetting> _systemSettingToSystemSettingMapper;
        private readonly IMapper<SaveSystemSetting, Models.SystemSetting> _saveSystemSettingToSystemSettingMapper;
        private readonly IMapper<SaveSystemSettingWithOutKey, Models.SystemSetting> _saveSystemSettingWithOutKeyToSystemSettingMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PutSystemSettingCommand(
            ISystemSettingRepository systemSettingRepository,
            IMapper<Models.SystemSetting, SystemSetting> systemSettingToSystemSettingMapper,
            IMapper<SaveSystemSetting, Models.SystemSetting> saveSystemSettingToSystemSettingMapper,
            IMapper<SaveSystemSettingWithOutKey, Models.SystemSetting> saveSystemSettingWithOutKeyToSystemSettingMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _systemSettingRepository = systemSettingRepository;
            _systemSettingToSystemSettingMapper = systemSettingToSystemSettingMapper;
            _saveSystemSettingToSystemSettingMapper = saveSystemSettingToSystemSettingMapper;
            _saveSystemSettingWithOutKeyToSystemSettingMapper = saveSystemSettingWithOutKeyToSystemSettingMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(string key, SaveSystemSettingWithOutKey saveSystemSetting, CancellationToken cancellationToken)
        {
            var systemSetting = await _systemSettingRepository.GetByKey(key, cancellationToken);
            if (systemSetting == null)
            {
                return new NotFoundResult();
            }

            _saveSystemSettingWithOutKeyToSystemSettingMapper.Map(saveSystemSetting, systemSetting);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            //systemSetting.ModifiedBy = userId;

            systemSetting = await _systemSettingRepository.Update(systemSetting, cancellationToken);
            var systemSettingViewModel = _systemSettingToSystemSettingMapper.Map(systemSetting);

            return new OkObjectResult(systemSettingViewModel);
        }
    }
}