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
    using System.Linq;

    public class PublicGetCountryAllCommand : IPublicGetCountryAllCommand
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper<Models.Country, PublicCountry> _countryMapper;
        public PublicGetCountryAllCommand(
            ICountryRepository countryRepository,
            IMapper<Models.Country, PublicCountry> countryMapper)
        {
            _countryRepository = countryRepository;
            _countryMapper = countryMapper;
        }

        public async Task<IActionResult> ExecuteAsync(string all ,PageOptions pageOptions, CancellationToken cancellationToken)
        {
            if (all != "1")
            {
                var countries = await _countryRepository.PublicGetPage(pageOptions.Page.Value, pageOptions.Count.Value, cancellationToken);
                if (countries == null)
                {
                    return new NoContentResult();
                }


                var (totalCount, totalPages) = await _countryRepository.PublicGetTotalPages(pageOptions.Count.Value, cancellationToken);

                var countryViewModels = _countryMapper.MapList(countries);
                var page = new PageResult<PublicCountry>()
                {
                    Count = pageOptions.Count.Value,
                    Items = countryViewModels,
                    Page = pageOptions.Page.Value,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                };

               
                return new OkObjectResult(page);
            }
           else
            {
                var country = await _countryRepository.PublicGetAll(cancellationToken);
                var countryViewModels = _countryMapper.MapList(country);
                if (country == null)
                {
                    return new NoContentResult();
                }
                return new OkObjectResult(countryViewModels);
            }
           
        }

    }
}
