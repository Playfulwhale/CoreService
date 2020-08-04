namespace ApiTemplate.Commands.CurrencyCommands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using ViewModels.CurrencyViewModels;

    public class PatchCurrencyCommand : IPatchCurrencyCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IObjectModelValidator _objectModelValidator;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper<Models.Currency, Currency> _currencyToCurrencyMapper;
        private readonly IMapper<Models.Currency, SaveCurrency> _currencyToSaveCurrencyMapper;
        private readonly IMapper<SaveCurrency, Models.Currency> _saveCurrencyToCurrencyMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatchCurrencyCommand(
            IActionContextAccessor actionContextAccessor,
            IObjectModelValidator objectModelValidator,
            ICurrencyRepository currencyRepository,
            IMapper<Models.Currency, Currency> currencyToCurrencyMapper,
            IMapper<Models.Currency, SaveCurrency> currencyToSaveCurrencyMapper,
            IMapper<SaveCurrency, Models.Currency> saveCurrencyToCurrencyMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _actionContextAccessor = actionContextAccessor;
            _objectModelValidator = objectModelValidator;
            _currencyRepository = currencyRepository;
            _currencyToCurrencyMapper = currencyToCurrencyMapper;
            _currencyToSaveCurrencyMapper = currencyToSaveCurrencyMapper;
            _saveCurrencyToCurrencyMapper = saveCurrencyToCurrencyMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(
            int id,
            JsonPatchDocument<SaveCurrency> patch,
            CancellationToken cancellationToken)
        {
            var currency = await _currencyRepository.Get(id, cancellationToken);
            if (currency == null)
            {
                return new NotFoundResult();
            }

            var saveCurrency = _currencyToSaveCurrencyMapper.Map(currency);

            // add created by
            var user = _httpContextAccessor.HttpContext.User;
            if (user == null)
                return new NotFoundResult();

            var claims = user.Claims.ToList();
            if (claims.Count < 1)
                return new NotFoundResult();

            var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub").Value;
            
            currency.ModifiedBy = userId;
            // end created by

            var modelState = _actionContextAccessor.ActionContext.ModelState;
            patch.ApplyTo(saveCurrency, modelState);
            _objectModelValidator.Validate(
                _actionContextAccessor.ActionContext,
                validationState: null,
                prefix: null,
                model: saveCurrency);
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(modelState);
            }

            _saveCurrencyToCurrencyMapper.Map(saveCurrency, currency);
            await _currencyRepository.Update(currency, cancellationToken);
            var currencyViewModel = _currencyToCurrencyMapper.Map(currency);

            return new OkObjectResult(currencyViewModel);
        }
    }
}