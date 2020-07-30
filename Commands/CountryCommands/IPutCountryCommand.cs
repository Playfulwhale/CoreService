namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.AspNetCore;
    using ViewModels.CountryViewModels;

    public interface IPutCountryCommand : IAsyncCommand<int, SaveCountry>
    {
    }
}
