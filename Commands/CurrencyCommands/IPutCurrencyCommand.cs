namespace ApiTemplate.Commands.CurrencyCommands
{
    using Boxed.AspNetCore;
    using ViewModels.CurrencyViewModels;

    public interface IPutCurrencyCommand : IAsyncCommand<int, SaveCurrency>
    {
    }
}
