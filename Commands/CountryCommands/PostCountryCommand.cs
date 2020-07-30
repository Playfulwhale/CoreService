namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.Mapping;
    using Constants;
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using ViewModels.CountryViewModels;

    public class PostCountryCommand : IPostCountryCommand
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper<Models.Country, Country> _countryToCountryMapper;
        private readonly IMapper<SaveCountry, Models.Country> _saveCountryToCountryMapper;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public PostCountryCommand(
            ICountryRepository countryRepository,
            IMapper<Models.Country, Country> countryToCountryMapper,
            IMapper<SaveCountry, Models.Country> saveCountryToCountryMapper)
        {
            _countryRepository = countryRepository;
            _countryToCountryMapper = countryToCountryMapper;
            _saveCountryToCountryMapper = saveCountryToCountryMapper;
        }

        public async Task<IActionResult> ExecuteAsync(SaveCountry saveCountry, CancellationToken cancellationToken)
        {
            var listCountry = await _countryRepository.GetAll(cancellationToken);
            var selectCountry = listCountry.SingleOrDefault(x => x.Name == saveCountry.Name || x.IsoCode2 == saveCountry.IsoCode2); 
            if (selectCountry != null)
            {
                return new NoContentResult();
            }
            var country = _saveCountryToCountryMapper.Map(saveCountry);

            // add created by
            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();

            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub").Value;

            //if (country.CreatedBy == null)
            //    country.CreatedBy = userId;
            //country.ModifiedBy = userId;
            // end created by

            country = await _countryRepository.Add(country, cancellationToken);
            var countryViewModel = _countryToCountryMapper.Map(country);

            return new CreatedAtRouteResult(
                    CountriesControllerRoute.GetCountry,
                    new { countryId = countryViewModel.Id },
                    countryViewModel);                      
        }
    }
}
