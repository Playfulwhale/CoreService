namespace ApiTemplate.Commands.CurrencyCommands
{
    using Boxed.AspNetCore;

    public interface IDeleteCurrencyCommand : IAsyncCommand<int>
    {
    }
}
