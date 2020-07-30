namespace ApiTemplate.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Swashbuckle.AspNetCore.Annotations;
    using Commands.CountryCommands;
    using ViewModels.CountryViewModels;



    //[Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    [ApiVersion(ApiVersionName.V1)]
    public class CountriesController : ControllerBase
    {
        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
        public IActionResult Options()
        {
            HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Post);
            return Ok();
        }

        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods for a country with the specified unique identifier.
        /// </summary>
        /// <param name="countryId">The countries unique identifier.</param>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("{countryId}")]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
        public IActionResult Options(int countryId)
        {
            HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Delete,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Patch,
                HttpMethods.Post,
                HttpMethods.Put);
            return Ok();
        }


        /// <summary>
        /// Deletes the country with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="countryId">The countries unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the country was deleted or a 404 Not Found if a country with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{countryId}", Name = CountriesControllerRoute.DeleteCountry)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The country with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A country with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeleteCountryCommand command,
            int countryId,
            CancellationToken cancellationToken) => command.ExecuteAsync(countryId, cancellationToken);

        /// <summary>
        /// Gets the country with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="countryId">The countries unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the country or a 404 Not Found if a country with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{countryId}", Name = CountriesControllerRoute.GetCountry)]
        [HttpHead("{countryId}", Name = CountriesControllerRoute.HeadCountry)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The country with the specified unique identifier.", typeof(Country))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The country has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A country with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetCountryCommand command,
            int countryId,
            CancellationToken cancellationToken) => command.ExecuteAsync(countryId, cancellationToken);

        /// <summary>
        /// Gets a collection of countries using the specified page number and number of items per page.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="all">The query command.</param>
        /// <param name="pageOptions">The page options.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of countries, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("", Name = CountriesControllerRoute.GetCountryPage)]
        [HttpHead("", Name = CountriesControllerRoute.HeadCountryPage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of countries for the specified page.", typeof(PageResult<Country>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public Task<IActionResult> GetPage(
            [FromServices] IGetCountryPageCommand command,
            [FromQuery]string all,
            [FromQuery] PageOptions pageOptions,
            CancellationToken cancellationToken) => command.ExecuteAsync(all, pageOptions, cancellationToken);

        /// <summary>
        /// Gets a collection of countries.
        /// </summary>
        /// 
        
        [HttpGet("/public/countries", Name = CountriesControllerRoute.PublicGetCountryAll)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of currencies for the specified page.", typeof(PageResult<PublicCountry>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public Task<IActionResult> PublicGetAll(
           [FromServices] IPublicGetCountryAllCommand command,
            string all,
            [FromQuery] PageOptions pageOptions,
            CancellationToken cancellationToken) => command.ExecuteAsync(all, pageOptions, cancellationToken);

        /// <summary>
        /// Gets Zone All of countryId
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet("{countryId}/zones", Name = CountriesControllerRoute.GetZoneAllByCountry)]
        [HttpHead("{countryId}/zones", Name = CountriesControllerRoute.GetZoneAllByCountry)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of countries for the specified page.", typeof(PageResult<Country>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public Task<IActionResult> GetZoneAllByCountry(
            [FromServices] IGetZoneAllByCountryCommand command,
            CancellationToken cancellationToken, int countryId) => command.ExecuteAsync(countryId, cancellationToken);

        /// <summary>
        /// Gets Zone All of countryId
        /// </summary>
        /// 
        
        [HttpGet("/public/countries/{code}/zones", Name = CountriesControllerRoute.PublicGetZoneAllByCountryCode)]
        //[HttpHead("{countryId}/zones", Name = CountriesControllerRoute.GetZoneAllByCountry)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of countries for the specified page.", typeof(PageResult<ViewModels.ZoneViewModels.PublicZone>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public Task<IActionResult> PublicGetZoneAllByCountryCode(
            [FromServices] IPublicGetZoneAllByCountryCodeCommand command,
            string all, 
            string code,
            [FromQuery] PageOptions pageOptions,
            CancellationToken cancellationToken) => command.ExecuteAsync(all , code, pageOptions, cancellationToken);


        /// <summary>
        /// Patches the country with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="countryId">The countries unique identifier.</param>
        /// <param name="patch">The patch document. See http://jsonpatch.com.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK if the country was patched, a 400 Bad Request if the patch was invalid or a 404 Not Found
        /// if a country with the specified unique identifier was not found.</returns>
        [HttpPatch("{countryId}", Name = CountriesControllerRoute.PatchCountry)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The patched country with the specified unique identifier.", typeof(Country))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The patch document is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A country with the specified unique identifier could not be found.")]
        public Task<IActionResult> Patch(
            [FromServices] IPatchCountryCommand command,
            int countryId,
            [FromBody] JsonPatchDocument<SaveCountry> patch,
            CancellationToken cancellationToken) => command.ExecuteAsync(countryId, patch, cancellationToken);

        /// <summary>
        /// Creates a new country.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="country">The country to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created country or a 400 Bad Request if the country is
        /// invalid.</returns>
        [HttpPost("", Name = CountriesControllerRoute.PostCountry)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status201Created, "The country was created.", typeof(Country))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The country is invalid.")]
        public Task<IActionResult> Post(
            [FromServices] IPostCountryCommand command,
            [FromBody] SaveCountry country,
            CancellationToken cancellationToken) => command.ExecuteAsync(country, cancellationToken);

        /// <summary>
        /// Updates an existing country with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="country">The country to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated country, a 400 Bad Request if the country is invalid or a
        /// or a 404 Not Found if a country with the specified unique identifier was not found.</returns>
        [HttpPut("{countryId}", Name = CountriesControllerRoute.PutCountry)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The country was updated.", typeof(Country))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The country is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A country with the specified unique identifier could not be found.")]
        public Task<IActionResult> Put(
            [FromServices] IPutCountryCommand command,
            int countryId,
            [FromBody] SaveCountry country,
            CancellationToken cancellationToken) => command.ExecuteAsync(countryId, country, cancellationToken);       

        /// <summary>
        /// Toggle a country status with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated country, a 400 Bad Request if the country is invalid or a
        /// or a 404 Not Found if a country with the specified unique identifier was not found.</returns>
        [HttpPut("{countryId}/toggle", Name = CountriesControllerRoute.PutCountryActive)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The country was updated.", typeof(Country))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The country is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A country with the specified unique identifier could not be found.")]
        public Task<IActionResult> ToggleStatus(
            [FromServices] IPutCountryActiveCommand command,
            int countryId,
            CancellationToken cancellationToken) => command.ExecuteAsync(countryId, cancellationToken);
    }
}