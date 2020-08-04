namespace ApiTemplate.Commands.CurrencyCommands
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Constants;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.CurrencyViewModels;

    public class GetCurrencyPageCommand :  IGetCurrencyPageCommand
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper<Models.Currency, Currency> _carMapper;

        public GetCurrencyPageCommand(
            ICurrencyRepository currencyRepository,
            IMapper<Models.Currency, Currency> carMapper)
        {
            _currencyRepository = currencyRepository;
            _carMapper = carMapper;
        }

        public async Task<IActionResult> ExecuteAsync(string all, PageOptions pageOptions, CancellationToken cancellationToken)
        {
            if(all == "1")
            {
                var currency = await _currencyRepository.GetAll(cancellationToken);
                if (currency == null)
                {
                    return new NoContentResult();
                }

                return new OkObjectResult(currency);
            }
           

            var cars = await _currencyRepository.GetPage(pageOptions.Page.Value, pageOptions.Count.Value, cancellationToken);
            if (cars == null)
            {
                return new NoContentResult();
            }

            var (totalCount, totalPages) = await _currencyRepository.GetTotalPages(pageOptions.Count.Value, cancellationToken);
            var carViewModels = _carMapper.MapList(cars);
            var page = new PageResult<Currency>()
            {
                Count = pageOptions.Count.Value,
                Items = carViewModels,
                Page = pageOptions.Page.Value,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };


            return new OkObjectResult(page);
        }

    }
}
