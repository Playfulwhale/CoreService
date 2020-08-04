namespace ApiTemplate.Commands.CurrencyCommands
{
    using Boxed.AspNetCore;
    using ViewModels.CurrencyViewModels;

    public interface IGetCurrencyPageCommand : IAsyncCommand<string, PageOptions>
    {
    }
}
