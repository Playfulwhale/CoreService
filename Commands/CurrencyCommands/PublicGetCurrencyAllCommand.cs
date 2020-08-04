namespace ApiTemplate.Commands.CurrencyCommands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using ViewModels.CurrencyViewModels;
    using System.Linq;

    public class PublicGetCurrencyAllCommand : IPublicGetCurrencyAllCommand
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper<Models.Currency, PublicCurrency> _currencyMapper;

        public PublicGetCurrencyAllCommand(
            ICurrencyRepository currencyRepository,
            IMapper<Models.Currency, PublicCurrency> currencyMapper)
        {
            _currencyRepository = currencyRepository;
            _currencyMapper = currencyMapper;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var currency = await _currencyRepository.GetAll(cancellationToken);
            if (currency == null)
            {
                return new NotFoundResult();
            }
            currency = currency.Where(x => x.Active).ToList();
            var currencyViewModel = _currencyMapper.MapList(currency);
            return new OkObjectResult(currencyViewModel);
        }
    }
}
