namespace ApiTemplate.Commands.SystemSettingCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Constants;
    using Repositories;
    using ViewModels.SystemSettingViewModels;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class PostSystemSettingCommand : IPostSystemSettingCommand
    {
        private readonly ISystemSettingRepository _systemSettingRepository;
        private readonly ISystemSettingGroupRepository _systemSettingGroupRepository;
        private readonly IMapper<Models.SystemSetting, SystemSetting> _systemSettingToSystemSettingMapper;
        private readonly IMapper<SaveSystemSetting, Models.SystemSetting> _saveSystemSettingToSystemSettingMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostSystemSettingCommand(
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

        public async Task<IActionResult> ExecuteAsync(SaveSystemSetting saveSystemSetting, CancellationToken cancellationToken)
        {
            if (await _systemSettingGroupRepository.Get(saveSystemSetting.GroupId, cancellationToken) == null)
            {
                return new NotFoundResult();
            }

            var systemSetting = await _systemSettingRepository.GetByKey(saveSystemSetting.Key, cancellationToken);
            if (systemSetting != null)
            {
                return new NotFoundResult();
            }
            systemSetting = _saveSystemSettingToSystemSettingMapper.Map(saveSystemSetting);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            //systemSetting.CreatedBy = userId;
            //systemSetting.ModifiedBy = userId;

            systemSetting = await _systemSettingRepository.Add(systemSetting, cancellationToken);
            var systemSettingViewModel = _systemSettingToSystemSettingMapper.Map(systemSetting);

            return new CreatedAtRouteResult(
                SystemSettingsControllerRoute.GetSystemSetting,
                new {systemSettingViewModel.Key },
                systemSettingViewModel);
        }
    }
}