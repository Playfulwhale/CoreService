namespace ApiTemplate.Commands.PaymentMethodCommands
{
    using Boxed.AspNetCore;

    public interface IGetPaymentMethodCommand : IAsyncCommand<int>
    {
    }
}
