namespace ApiTemplate.Commands.PaymentMethodCommands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Net.Http.Headers;
    using ViewModels.PaymentMethodViewModels;

    public class GetPaymentMethodCommand : IGetPaymentMethodCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper<Models.PaymentMethod, PaymentMethod> _paymentMethodMapper;

        public GetPaymentMethodCommand(
            IActionContextAccessor actionContextAccessor,
            IPaymentMethodRepository paymentMethodRepository,
            IMapper<Models.PaymentMethod, PaymentMethod> paymentMethodMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _paymentMethodRepository = paymentMethodRepository;
            _paymentMethodMapper = paymentMethodMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int paymentMethodId, CancellationToken cancellationToken)
        {
            var paymentMethod = await _paymentMethodRepository.Get(paymentMethodId, cancellationToken);
            if (paymentMethod == null)
            {
                return new NotFoundResult();
            }

            var httpContext = _actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= paymentMethod.ModifiedAt))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var paymentMethodViewModel = _paymentMethodMapper.Map(paymentMethod);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, paymentMethod.ModifiedAt.ToString());
            return new OkObjectResult(paymentMethodViewModel);
        }
    }
}
