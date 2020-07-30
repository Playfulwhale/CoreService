namespace ApiTemplate.Commands.ZoneCommands
{
    using Boxed.AspNetCore;
    using ViewModels.ZoneViewModels;

    public interface IPutZoneCommand : IAsyncCommand<int, SaveZone>
    {
    }
}
