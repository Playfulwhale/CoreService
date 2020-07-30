namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.AspNetCore;
   
    public interface IDeleteCountryCommand : IAsyncCommand<int>
    {
    }
}
