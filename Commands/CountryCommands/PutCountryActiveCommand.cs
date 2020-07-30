namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.CountryViewModels;

    public class PutCountryActiveCommand : IPutCountryActiveCommand
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper<Models.Country, Country> _countryToCountryMapper;
        //private readonly IMapper<SaveCountry, Models.Country> saveCountryToCountryMapper;

        public PutCountryActiveCommand(
            ICountryRepository countryRepository,
            IMapper<Models.Country, Country> countryToCountryMapper)
        {
            _countryRepository = countryRepository;
            _countryToCountryMapper = countryToCountryMapper;
            //this.saveCountryToCountryMapper = saveCountryToCountryMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int countryId, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.Get(countryId, cancellationToken);
            if (country == null)
            {
                return new NotFoundResult();
            }

            // add created by

            country = await _countryRepository.ToggleStatus(countryId, cancellationToken);
            var countryViewModel = _countryToCountryMapper.Map(country);

            return new OkObjectResult(countryViewModel);
        }
    }
}
