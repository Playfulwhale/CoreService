namespace ApiTemplate.Commands.CountryCommands
{
    using ApiTemplate.ViewModels.CountryViewModels;
    using Boxed.AspNetCore;

    public interface IPublicGetZoneAllByCountryCodeCommand : IAsyncCommand<string,string,PageOptions>
    {
    }
}
