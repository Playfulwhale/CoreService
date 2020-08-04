namespace ApiTemplate.Commands.SystemSettingCommands
{
    using Boxed.AspNetCore;
    using System.Collections.Generic;
    using ViewModels.SystemSettingViewModels;

    public interface IPutSystemSettingsCommand : IAsyncCommand<List<SaveSystemSetting>>
    {
    }
}