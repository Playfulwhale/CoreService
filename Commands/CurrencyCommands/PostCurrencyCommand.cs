namespace ApiTemplate.Commands.CurrencyCommands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Constants;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.CurrencyViewModels;

    public class PostCurrencyCommand : IPostCurrencyCommand
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper<Models.Currency, Currency> _currencyToCurrencyMapper;
        private readonly IMapper<SaveCurrency, Models.Currency> _saveCurrencyToCurrencyMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostCurrencyCommand(
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

        public async Task<IActionResult> ExecuteAsync(SaveCurrency saveCurrency, CancellationToken cancellationToken)
        {
            var listCurrency = await _currencyRepository.GetAll(cancellationToken);
            var selectCurrency = listCurrency.SingleOrDefault(x => x.Title == saveCurrency.Title || x.Code == saveCurrency.Code);
            if(selectCurrency != null)
            {
                return new NoContentResult();
            }
            var currency = _saveCurrencyToCurrencyMapper.Map(saveCurrency);

            //// add created by
            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();

            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub").Value;

            //if (currency.CreatedBy == null)
            //    currency.CreatedBy = userId;
            //currency.ModifiedBy = userId;
            //// end created by

            await _currencyRepository.Add(currency, cancellationToken);
            if(currency.Default == true)
            {
                await _currencyRepository.Default(currency, cancellationToken);
            }
            var currencyViewModel = _currencyToCurrencyMapper.Map(currency);
            return new CreatedAtRouteResult(
                CurrenciesControllerRoute.GetCurrency,
                new { id = currencyViewModel.Id },
                currencyViewModel);
        }
    }
}
