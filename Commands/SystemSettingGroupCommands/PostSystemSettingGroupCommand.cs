namespace ApiTemplate.Commands.SystemSettingGroupCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Constants;
    using Repositories;
    using ViewModels.SystemSettingGroupViewModels;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class PostSystemSettingGroupCommand : IPostSystemSettingGroupCommand
    {
        private readonly ISystemSettingGroupRepository _systemSettingGroupRepository;
        private readonly IMapper<Models.SystemSettingGroup, SystemSettingGroup> _systemSettingGroupToSystemSettingGroupMapper;
        private readonly IMapper<SaveSystemSettingGroup, Models.SystemSettingGroup> _saveSystemSettingGroupToSystemSettingGroupMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostSystemSettingGroupCommand(
            ISystemSettingGroupRepository systemSettingGroupRepository,
            IMapper<Models.SystemSettingGroup, SystemSettingGroup> systemSettingGroupToSystemSettingGroupMapper,
            IMapper<SaveSystemSettingGroup, Models.SystemSettingGroup> saveSystemSettingGroupToSystemSettingGroupMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _systemSettingGroupRepository = systemSettingGroupRepository;
            _systemSettingGroupToSystemSettingGroupMapper = systemSettingGroupToSystemSettingGroupMapper;
            _saveSystemSettingGroupToSystemSettingGroupMapper = saveSystemSettingGroupToSystemSettingGroupMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(SaveSystemSettingGroup saveSystemSettingGroup, CancellationToken cancellationToken)
        {
            var systemSettingGroup = _saveSystemSettingGroupToSystemSettingGroupMapper.Map(saveSystemSettingGroup);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            //systemSettingGroup.CreatedBy = userId;
            //systemSettingGroup.ModifiedBy = userId;

            systemSettingGroup = await _systemSettingGroupRepository.Add(systemSettingGroup, cancellationToken);
            var systemSettingGroupViewModel = _systemSettingGroupToSystemSettingGroupMapper.Map(systemSettingGroup);

            return new CreatedAtRouteResult(
                SystemSettingGroupsControllerRoute.GetSystemSettingGroup,
                new { Id = systemSettingGroupViewModel.Id },
                systemSettingGroupViewModel);
        }
    }
}