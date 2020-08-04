namespace ApiTemplate.Commands.CurrencyCommands
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
    using ViewModels.CurrencyViewModels;

    public class GetCurrencyCommand : IGetCurrencyCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper<Models.Currency, Currency> _currencyMapper;

        public GetCurrencyCommand(
            IActionContextAccessor actionContextAccessor,
            ICurrencyRepository currencyRepository,
            IMapper<Models.Currency, Currency> currencyMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _currencyRepository = currencyRepository;
            _currencyMapper = currencyMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var currency = await _currencyRepository.Get(id, cancellationToken);
            if (currency == null)
            {
                return new NotFoundResult();
            }

            var httpContext = _actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= currency.ModifiedAt))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var currencyViewModel = _currencyMapper.Map(currency);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, currency.ModifiedAt.ToString());
            return new OkObjectResult(currencyViewModel);
        }
    }
}
