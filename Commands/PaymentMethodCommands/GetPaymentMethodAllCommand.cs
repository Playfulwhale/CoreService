namespace ApiTemplate.Commands.PaymentMethodCommands
{
    using Repositories;
    using ViewModels.PaymentMethodViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetPaymentMethodAllCommand : IGetPaymentMethodAllCommand
    {
        private readonly IPaymentMethodRepository _PaymentMethodRepository;
        private readonly IMapper<Models.PaymentMethod, PaymentMethod> _paymentMethodMapper;

        public GetPaymentMethodAllCommand(
            IPaymentMethodRepository PaymentMethodRepository,
            IMapper<Models.PaymentMethod, PaymentMethod> paymentMethodMapper)
        {
            _PaymentMethodRepository = PaymentMethodRepository;
            _paymentMethodMapper = paymentMethodMapper;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var paymentMethods = await _PaymentMethodRepository.GetAll(cancellationToken);
            if (paymentMethods == null)
            {
                return new NoContentResult();
            }
            var viewPaymentMethod = _paymentMethodMapper.MapList(paymentMethods);

            return new OkObjectResult(viewPaymentMethod);
        }
    }
}
