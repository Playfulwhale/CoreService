namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using ViewModels.CountryViewModels;

    public class PutCountryCommand : IPutCountryCommand
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper<Models.Country, Country> _countryToCountryMapper;
        private readonly IMapper<SaveCountry, Models.Country> _saveCountryToCountryMapper;
        public PutCountryCommand(
            ICountryRepository countryRepository,
            IMapper<Models.Country, Country> countryToCountryMapper,
            IMapper<SaveCountry, Models.Country> saveCountryToCountryMapper)
        {
            _countryRepository = countryRepository;
            _countryToCountryMapper = countryToCountryMapper;
            _saveCountryToCountryMapper = saveCountryToCountryMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int countryId, SaveCountry saveCountry, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.Get(countryId, cancellationToken);
            if (country == null)
            {
                return new NotFoundResult();
            }

            _saveCountryToCountryMapper.Map(saveCountry, country);
            country = await _countryRepository.Update(country, cancellationToken);
            var countryViewModel = _countryToCountryMapper.Map(country);

            return new OkObjectResult(countryViewModel);
        }
    }
}
