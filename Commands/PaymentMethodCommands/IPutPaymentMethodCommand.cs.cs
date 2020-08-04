namespace ApiTemplate.Commands.PaymentMethodCommands
{
    using ViewModels.PaymentMethodViewModels;
    using Boxed.AspNetCore;
    public interface IPutPaymentMethodCommand : IAsyncCommand<int, SavePaymentMethod>
    {
    }
}
