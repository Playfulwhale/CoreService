namespace ApiTemplate.Commands.PaymentMethodCommands
{
    using Boxed.AspNetCore;

    public interface IDeletePaymentMethodCommand : IAsyncCommand<int>
    {
    }
}