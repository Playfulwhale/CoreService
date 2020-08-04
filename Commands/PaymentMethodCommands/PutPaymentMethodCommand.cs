namespace ApiTemplate.Commands.PaymentMethodCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.PaymentMethodViewModels;
    public class PutPaymentMethodCommand : IPutPaymentMethodCommand
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper<Models.PaymentMethod, PaymentMethod> _paymentMethodToPaymentMethodMapper;
        private readonly IMapper<SavePaymentMethod, Models.PaymentMethod> _paymentMethodToSavePaymentMethodMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PutPaymentMethodCommand(
            IPaymentMethodRepository paymentMethodRepository,
            IMapper<Models.PaymentMethod, PaymentMethod> paymentMethodToPaymentMethodMapper,
            IMapper<SavePaymentMethod, Models.PaymentMethod> paymentMethodToSavePaymentMethodMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _paymentMethodToPaymentMethodMapper = paymentMethodToPaymentMethodMapper;
            _paymentMethodToSavePaymentMethodMapper = paymentMethodToSavePaymentMethodMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(int paymentMethodId, SavePaymentMethod savePaymentMethod, CancellationToken cancellationToken)
        {
            var paymentMethod = await _paymentMethodRepository.Get(paymentMethodId, cancellationToken);
            if (paymentMethod == null)
            {
                return new NotFoundResult();
            }
            _paymentMethodToSavePaymentMethodMapper.Map(savePaymentMethod, paymentMethod);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            //paymentMethod.ModifiedBy = userId;

            paymentMethod = await _paymentMethodRepository.Update(paymentMethod, cancellationToken);
            var paymentMethodViewModel = _paymentMethodToPaymentMethodMapper.Map(paymentMethod);

            return new OkObjectResult(paymentMethodViewModel);
        }
    }
}
