namespace ApiTemplate.Commands.SystemSettingGroupCommands
{
    using Boxed.AspNetCore;
    using ViewModels.SystemSettingGroupViewModels;

    public interface IPostSystemSettingGroupCommand : IAsyncCommand<SaveSystemSettingGroup>
    {
    }
}