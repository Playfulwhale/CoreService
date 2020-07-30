namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using ViewModels.CountryViewModels;

    public class PatchCountryCommand : IPatchCountryCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IObjectModelValidator _objectModelValidator;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper<Models.Country, Country> _countryToCountryMapper;
        private readonly IMapper<Models.Country, SaveCountry> _countryToSaveCountryMapper;
        private readonly IMapper<SaveCountry, Models.Country> _saveCountryToCountryMapper;

        public PatchCountryCommand(
            IActionContextAccessor actionContextAccessor,
            IObjectModelValidator objectModelValidator,
            ICountryRepository countryRepository,
            IMapper<Models.Country, Country> countryToCountryMapper,
            IMapper<Models.Country, SaveCountry> countryToSaveCountryMapper,
            IMapper<SaveCountry, Models.Country> saveCountryToCountryMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _objectModelValidator = objectModelValidator;
            _countryRepository = countryRepository;
            _countryToCountryMapper = countryToCountryMapper;
            _countryToSaveCountryMapper = countryToSaveCountryMapper;
            _saveCountryToCountryMapper = saveCountryToCountryMapper;
        }

        public async Task<IActionResult> ExecuteAsync(
            int countryId,
            JsonPatchDocument<SaveCountry> patch,
            CancellationToken cancellationToken)
        {
            var country = await _countryRepository.Get(countryId, cancellationToken);
            if (country == null)
            {
                return new NotFoundResult();
            }

            var saveCountry = _countryToSaveCountryMapper.Map(country);


            var modelState = _actionContextAccessor.ActionContext.ModelState;
            patch.ApplyTo(saveCountry, modelState);
            _objectModelValidator.Validate(
                _actionContextAccessor.ActionContext,
                validationState: null,
                prefix: null,
                model: saveCountry);
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(modelState);
            }

            _saveCountryToCountryMapper.Map(saveCountry, country);
            await _countryRepository.Update(country, cancellationToken);
            var countryViewModel = _countryToCountryMapper.Map(country);

            return new OkObjectResult(countryViewModel);
        }
    }
}
