namespace ApiTemplate.Commands.SystemSettingCommands
{
    using Boxed.AspNetCore;
    using ViewModels.SystemSettingViewModels;
    
    public interface IPutSystemSettingCommand : IAsyncCommand<string, SaveSystemSettingWithOutKey>
    {
    }
}