namespace ApiTemplate.Commands.CountryCommands
{
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Net.Http.Headers;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.CountryViewModels;

    public class GetCountryCommand : IGetCountryCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper<Models.Country, Country> _countryMapper;

        public GetCountryCommand(
            IActionContextAccessor actionContextAccessor,
            ICountryRepository countryRepository,
            IMapper<Models.Country, Country> countryMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _countryRepository = countryRepository;
            _countryMapper = countryMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int countryId, CancellationToken cancellationToken)
        {
            var country = await _countryRepository.Get(countryId, cancellationToken);
            if (country == null)
            {
                return new NotFoundResult();
            }

            var httpContext = _actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= country.ModifiedAt))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var countryViewModel = _countryMapper.Map(country);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, country.ModifiedAt.ToString());
            return new OkObjectResult(countryViewModel);
        }
    }
}
