namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.AspNetCore;
    using ViewModels.CountryViewModels;

    public interface IPostCountryCommand : IAsyncCommand<SaveCountry>
    {
    }
}
