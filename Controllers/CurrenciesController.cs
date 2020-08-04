using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Commands.CurrencyCommands;
using ApiTemplate.Constants;
using ApiTemplate.ViewModels.CurrencyViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiTemplate.Controllers
{
   // 
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CurrenciesController : ControllerBase
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
        /// Returns an Allow HTTP header with the allowed HTTP methods for a currency with the specified unique identifier.
        /// </summary>
        /// <param name="currencyId">The currencies unique identifier.</param>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("{id}")]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
        public IActionResult Options(int currencyId)
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
        /// Deletes the currency with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The currencies unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the currency was deleted or a 404 Not Found if a currency with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{id}", Name = CurrenciesControllerRoute.DeleteCurrency)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The currency with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A currency with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeleteCurrencyCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, cancellationToken);

        /// <summary>
        /// Gets the currency with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The currencies unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the currency or a 404 Not Found if a currency with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{id}", Name = CurrenciesControllerRoute.GetCurrency)]
        [HttpHead("{id}", Name = CurrenciesControllerRoute.HeadCurrency)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The currency with the specified unique identifier.", typeof(Currency))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The currency has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A currency with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetCurrencyCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, cancellationToken);

        /// <summary>
        /// Gets a collection of currencies using the specified page number and number of items per page.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="all">The query command.</param>
        /// <param name="pageOptions">The page options.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of currencies, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("", Name = CurrenciesControllerRoute.GetCurrencyPage)]
        [HttpHead("", Name = CurrenciesControllerRoute.HeadCurrencyPage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of currencies for the specified page.", typeof(PageResult<Currency>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public Task<IActionResult> GetPage(
            [FromServices] IGetCurrencyPageCommand command,
            string all,
            [FromQuery] PageOptions pageOptions,
            CancellationToken cancellationToken) => command.ExecuteAsync(all, pageOptions, cancellationToken);

        /// <summary>
        /// Gets a collection of currencies.
        /// </summary>
        /// 
        [AllowAnonymous]
        [HttpGet("/public/currencies", Name = CurrenciesControllerRoute.PublicGetCurrencyAll)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of currencies for the specified page.", typeof(PageResult<PublicCurrency>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public Task<IActionResult> PublicGetAll(
         [FromServices] IPublicGetCurrencyAllCommand command,
         CancellationToken cancellationToken) => command.ExecuteAsync(cancellationToken);

        ///// <summary>
        ///// Patches the currency with the specified unique identifier.
        ///// </summary>
        ///// <param name="command">The action command.</param>
        ///// <param name="currencyId">The currencies unique identifier.</param>
        ///// <param name="patch">The patch document. See http://jsonpatch.com.</param>
        ///// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        ///// <returns>A 200 OK if the currency was patched, a 400 Bad Request if the patch was invalid or a 404 Not Found
        ///// if a currency with the specified unique identifier was not found.</returns>
        //[HttpPatch("{currencyId}", Name = CurrenciesControllerRoute.PatchCurrency)]
        //[SwaggerResponse(StatusCodes.Status200OK, "The patched currency with the specified unique identifier.", typeof(Currency))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "The patch document is invalid.")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "A currency with the specified unique identifier could not be found.")]
        //public Task<IActionResult> Patch(
        //    [FromServices] IPatchCurrencyCommand command,
        //    int currencyId,
        //    [FromBody] JsonPatchDocument<SaveCurrency> patch,
        //    CancellationToken cancellationToken) => command.ExecuteAsync(currencyId, patch);

        /// <summary>
        /// Creates a new currency.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="currency">The currency to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created currency or a 400 Bad Request if the currency is
        /// invalid.</returns>
        [HttpPost("", Name = CurrenciesControllerRoute.PostCurrency)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status201Created, "The currency was created.", typeof(Currency))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The currency is invalid.")]
        public Task<IActionResult> Post(
            [FromServices] IPostCurrencyCommand command, 
            [FromBody] SaveCurrency currency,
            CancellationToken cancellationToken) => command.ExecuteAsync(currency, cancellationToken);

        /// <summary>
        /// Updates an existing currency with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The currency identifier.</param>
        /// <param name="currency">The currency to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated currency, a 400 Bad Request if the currency is invalid or a
        /// or a 404 Not Found if a currency with the specified unique identifier was not found.</returns>
        [HttpPut("{id}", Name = CurrenciesControllerRoute.PutCurrency)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The currency was updated.", typeof(Currency))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The currency is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A currency with the specified unique identifier could not be found.")]
        public Task<IActionResult> Put(
            [FromServices] IPutCurrencyCommand command,
            int id,
            [FromBody] SaveCurrency currency,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, currency);

        /// <summary>
        ///Change isDefault of language
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The language identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated Currency, a 400 Bad Request if the Currency is invalid or a
        /// or a 404 Not Found if a Currency with the specified unique identifier was not found.</returns>
        [HttpPut("{id}/Toggle", Name = CurrenciesControllerRoute.PutCurrencyToggleStatus)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The Currency was updated.", typeof(Currency))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The Currency is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Currency with the specified unique identifier could not be found.")]
        public Task<IActionResult> ToggleStatus(
            [FromServices] IPutCurrencyToggleStatusCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id);

        /// <summary>
        ///Change Default of language
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The language identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated Currency, a 400 Bad Request if the language is invalid or a
        /// or a 404 Not Found if a Currency with the specified unique identifier was not found.</returns>
        [HttpPut("{id}/Default", Name = CurrenciesControllerRoute.PutCurrencyDefault)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The Currency was updated.", typeof(Currency))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The Currency is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Currency with the specified unique identifier could not be found.")]
        public Task<IActionResult> Default(
            [FromServices] IPutCurrencyDefaultCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id);
    }
}
