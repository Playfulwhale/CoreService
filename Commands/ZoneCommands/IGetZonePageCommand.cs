namespace ApiTemplate.Commands.ZoneCommands
{
    using Boxed.AspNetCore;
    using ViewModels.ZoneViewModels;

    public interface IGetZonePageCommand : IAsyncCommand<string, PageOptions>
    {
    }
}
