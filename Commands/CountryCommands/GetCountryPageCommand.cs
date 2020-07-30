namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Constants;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.CountryViewModels;

    public class GetCountryPageCommand : IGetCountryPageCommand
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper<Models.Country, Country> _countryMapper;

        public GetCountryPageCommand(
            ICountryRepository countryRepository,
            IMapper<Models.Country, Country> countryMapper)
        {
            _countryRepository = countryRepository;
            _countryMapper = countryMapper;
        }

        public async Task<IActionResult> ExecuteAsync(string all, PageOptions pageOptions, CancellationToken cancellationToken)
        {
            if(all == "1")
            {
                var country = await _countryRepository.GetAll(cancellationToken);
                if (country == null)
                {
                    return new NoContentResult();
                }
                return new OkObjectResult(country);
            }
            else
            {
                var countries = await _countryRepository.GetPage(pageOptions.Page.Value, pageOptions.Count.Value, cancellationToken);
                if (countries == null)
                {
                    return new NoContentResult();
                }

                var (totalCount, totalPages) = await _countryRepository.GetTotalPages(pageOptions.Count.Value, cancellationToken);
                var countryViewModels = _countryMapper.MapList(countries);
                var page = new PageResult<Country>()
                {
                    Count = pageOptions.Count.Value,
                    Items = countryViewModels,
                    Page = pageOptions.Page.Value,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                };

                

                return new OkObjectResult(page);
            }
            
        }

    }
}
