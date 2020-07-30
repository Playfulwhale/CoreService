namespace ApiTemplate.Commands.ZoneCommands
{
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.JsonPatch;
    using ViewModels.ZoneViewModels;

    public interface IPatchZoneCommand : IAsyncCommand<int, JsonPatchDocument<SaveZone>>
    {
    }
}
