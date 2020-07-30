namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.JsonPatch;
    using ViewModels.CountryViewModels;

    public interface IPatchCountryCommand : IAsyncCommand<int, JsonPatchDocument<SaveCountry>>
    {
    }
}
