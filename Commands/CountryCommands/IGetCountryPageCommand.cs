namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.AspNetCore;
    using ViewModels.CountryViewModels;

    public interface IGetCountryPageCommand : IAsyncCommand<string, PageOptions>
    {
    }
}
