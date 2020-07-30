namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.AspNetCore;

    public interface IGetCountryCommand : IAsyncCommand<int>
    {
    }
}
