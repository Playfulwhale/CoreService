namespace ApiTemplate.Commands.CurrencyCommands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.CurrencyViewModels;

    public class PutCurrencyToggleStatusCommand : IPutCurrencyToggleStatusCommand
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper<Models.Currency, Currency> _currencyToCurrencyMapper;
        private readonly IMapper<SaveCurrency, Models.Currency> _saveCurrencyToCurrencyMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PutCurrencyToggleStatusCommand(
            ICurrencyRepository currencyRepository,
            IMapper<Models.Currency, Currency> currencyToCurrencyMapper,
            IMapper<SaveCurrency, Models.Currency> saveCurrencyToCurrencyMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _currencyRepository = currencyRepository;
            _currencyToCurrencyMapper = currencyToCurrencyMapper;
            _saveCurrencyToCurrencyMapper = saveCurrencyToCurrencyMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var currency = await _currencyRepository.Get(id, cancellationToken);
            if (currency == null)
            {
                return new NotFoundResult();
            }

            //// add created by
            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();

            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            //currency.ModifiedBy = userId;
            //// end created by

            currency = await _currencyRepository.ToggleStatus(currency, cancellationToken);
            var currencyViewModel = _currencyToCurrencyMapper.Map(currency);

            return new OkObjectResult(currencyViewModel);
        }
    }
}
