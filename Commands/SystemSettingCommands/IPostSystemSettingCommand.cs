namespace ApiTemplate.Commands.SystemSettingCommands
{
    using Boxed.AspNetCore;
    using ViewModels.SystemSettingViewModels;

    public interface IPostSystemSettingCommand : IAsyncCommand<SaveSystemSetting>
    {
    }
}