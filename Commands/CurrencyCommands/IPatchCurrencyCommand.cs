namespace ApiTemplate.Commands.CurrencyCommands
{
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.JsonPatch;
    using ViewModels.CurrencyViewModels;

    public interface IPatchCurrencyCommand : IAsyncCommand<int, JsonPatchDocument<SaveCurrency>>
    {
    }
}
