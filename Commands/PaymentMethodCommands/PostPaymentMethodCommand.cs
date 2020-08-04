namespace ApiTemplate.Commands.PaymentMethodCommands
{
    using Constants;
    using Repositories;
    using ViewModels.PaymentMethodViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    public class PostPaymentMethodCommand : IPostPaymentMethodCommand
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper<Models.PaymentMethod, PaymentMethod> _paymentMethodToPaymentMethodMapper;
        private readonly IMapper<SavePaymentMethod, Models.PaymentMethod> _paymentMethodToSavePaymentMethodMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostPaymentMethodCommand(
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

        public async Task<IActionResult> ExecuteAsync(SavePaymentMethod savePaymentMethod, CancellationToken cancellationToken)
        {
            var paymentMethod = _paymentMethodToSavePaymentMethodMapper.Map(savePaymentMethod);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            //paymentMethod.CreatedBy = userId;

            paymentMethod = await _paymentMethodRepository.Add(paymentMethod, cancellationToken);
            var paymentMethodViewModel = _paymentMethodToPaymentMethodMapper.Map(paymentMethod);

            return new CreatedAtRouteResult(
                PaymentMethodsControllerRoute.GetPaymentMethod,
                new { id = paymentMethodViewModel.Id },
                paymentMethodViewModel);
        }
    }
}
