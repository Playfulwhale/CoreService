namespace ApiTemplate.Commands.CurrencyCommands
{
    using Boxed.AspNetCore;

    public interface IGetCurrencyCommand : IAsyncCommand<int>
    {
    }
}
