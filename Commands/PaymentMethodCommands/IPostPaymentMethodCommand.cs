namespace ApiTemplate.Commands.PaymentMethodCommands
{
    using Boxed.AspNetCore;
    using ViewModels.PaymentMethodViewModels;

    public interface IPostPaymentMethodCommand : IAsyncCommand<SavePaymentMethod>
    {
    }
}
