namespace ApiTemplate.Commands.CurrencyCommands
{
    using Boxed.AspNetCore;
    using ViewModels.CurrencyViewModels;

    public interface IPostCurrencyCommand : IAsyncCommand<SaveCurrency>
    {
    }
}
