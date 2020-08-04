namespace ApiTemplate.Commands.SystemSettingGroupCommands
{
    using Boxed.AspNetCore;
    using ViewModels.SystemSettingGroupViewModels;
    
    public interface IPutSystemSettingGroupCommand : IAsyncCommand<int, SaveSystemSettingGroup>
    {
    }
}