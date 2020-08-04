namespace ApiTemplate.Commands.SystemSettingGroupCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels.SystemSettingGroupViewModels;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.ViewModels.SystemSettingViewModels;

    public class GetAllSystemSettingGroupCommand : IGetAllSystemSettingGroupCommand
    {
        private readonly ISystemSettingGroupRepository _systemSettingGroupRepository;
        private readonly IMapper<Models.SystemSettingGroup, SystemSettingGroup> _systemSettingGroupMapper;
        private readonly ISystemSettingRepository _systemSettingRepository;
        private readonly IMapper<Models.SystemSetting, SystemSetting> _systemSettingMapper;

        public GetAllSystemSettingGroupCommand(
            ISystemSettingGroupRepository systemSettingGroupRepository,
            IMapper<Models.SystemSettingGroup, SystemSettingGroup> systemSettingGroupMapper,
            ISystemSettingRepository systemSettingRepository,
            IMapper<Models.SystemSetting, SystemSetting> systemSettingMapper)
        {
            _systemSettingGroupRepository = systemSettingGroupRepository;
            _systemSettingGroupMapper = systemSettingGroupMapper;
            _systemSettingRepository = systemSettingRepository;
            _systemSettingMapper = systemSettingMapper;
        }

        public async Task<IActionResult> ExecuteAsync(string all, CancellationToken cancellationToken)
        {
            if(all == "1")
            {
                var listSystemSettingGroup = await _systemSettingGroupRepository.GetAll(cancellationToken);
                var systemSettings = await _systemSettingRepository.GetAll(cancellationToken);

                var systemGroupViewModels = new List<SystemGroup>();

                foreach (var systemSettingGroup in listSystemSettingGroup)
                {
                    var systemGroupViewModel = new SystemGroup();
                    var systemSettingGroupViewModel = _systemSettingGroupMapper.Map(systemSettingGroup);

                    systemGroupViewModel.Id = systemSettingGroupViewModel.Id;
                    systemGroupViewModel.Name = systemSettingGroupViewModel.Name;
                    systemGroupViewModel.Description = systemSettingGroupViewModel.Description;
                    systemGroupViewModel.Order = systemSettingGroupViewModel.Order;
                    systemGroupViewModel.Url = systemSettingGroupViewModel.Url;

                    var listSystemSetting = new List<SystemSetting>();
                    foreach (var systemSetting in systemSettings)
                    {
                        if (systemSetting.GroupId != systemSettingGroup.Id) continue;
                        var systemSettingViewModel = _systemSettingMapper.Map(systemSetting);
                        listSystemSetting.Add(systemSettingViewModel);
                    }
                    systemGroupViewModel.ListSystemSetting = listSystemSetting;
                    systemGroupViewModels.Add(systemGroupViewModel);
                }

                return new OkObjectResult(systemGroupViewModels);
            }

            var systemSettingGroups = await _systemSettingGroupRepository.GetAll(cancellationToken);

            var systemSettingGroupViewModels = new List<SystemSettingGroup>();

            foreach (var systemSettingGroup in systemSettingGroups)
            {
                var systemSettingGroupViewModel = _systemSettingGroupMapper.Map(systemSettingGroup);
                systemSettingGroupViewModels.Add(systemSettingGroupViewModel);
            }

            return new OkObjectResult(systemSettingGroupViewModels);
        }
    }
}