namespace ApiTemplate.Commands.ZoneCommands
{
    using Boxed.AspNetCore;
    using ViewModels.ZoneViewModels;

    public interface IPostZoneCommand : IAsyncCommand<SaveZone>
    {
    }
}
