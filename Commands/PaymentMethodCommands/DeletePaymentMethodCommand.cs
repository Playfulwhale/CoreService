namespace ApiTemplate.Commands.PaymentMethodCommands
{
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeletePaymentMethodCommand : IDeletePaymentMethodCommand
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public DeletePaymentMethodCommand(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<IActionResult> ExecuteAsync(int paymentMethodId, CancellationToken cancellationToken)
        {
            var paymentMethod = await _paymentMethodRepository.Get(paymentMethodId, cancellationToken);
            if (paymentMethod == null)
            {
                return new NotFoundResult();
            }

            await _paymentMethodRepository.Delete(paymentMethod, cancellationToken);

            return new NoContentResult();
        }
    }
}