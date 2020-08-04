namespace ApiTemplate.Controllers
{
    using Commands.LanguageCommands;
    using Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.LanguageViewModels;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    
    public class LanguagesController : ControllerBase
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
        /// Returns an Allow HTTP header with the allowed HTTP methods for a language with the specified unique identifier.
        /// </summary>
        /// <param name="id">The languages unique identifier.</param>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("{id}")]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
        public IActionResult Options(int id)
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
        /// Deletes the language with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The languages unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the language was deleted or a 404 Not Found if a language with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{id}", Name = LanguagesControllerRoute.DeleteLanguage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The language with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A language with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeleteLanguageCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id);

        /// <summary>
        /// Gets the language with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The languages unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the language or a 404 Not Found if a language with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{id}", Name = LanguagesControllerRoute.GetLanguage)]
        [HttpHead("{id}", Name = LanguagesControllerRoute.HeadLanguage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The language with the specified unique identifier.", typeof(Language))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The language has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A language with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetLanguageCommand command,
            int id,
            CancellationToken cancellationToken) => 
            command.ExecuteAsync(id);

        /// <summary>
        /// Gets a collection of public languages
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the language or a 404 Not Found if a language with the specified unique
        /// identifier was not found.</returns>
        /// 
        [AllowAnonymous]
        [HttpGet("/public/languages", Name = LanguagesControllerRoute.GetLanguagePublic)]
        [HttpHead("/public/languages", Name = LanguagesControllerRoute.HeadLanguagePublic)]
        [SwaggerResponse(StatusCodes.Status200OK, "The language with the specified unique identifier.", typeof(Language))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The language has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A language with the specified unique identifier could not be found.")]
        public Task<IActionResult> GetPublic(
            [FromServices] IPublicGetLanguageCommand command,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync();

        /// <summary>
        /// Gets a collection of languages using the specified page number and number of items per page.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="all">The all query command.</param>
        /// <param name="pageOptions">The page options.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of languages, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("", Name = LanguagesControllerRoute.GetLanguagePage)]
        [HttpHead("", Name = LanguagesControllerRoute.HeadLanguagePage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of languages for the specified page.", typeof(PageResult<Language>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public Task<IActionResult> GetPage(
            [FromServices] IGetLanguagePageCommand command,
            string all,
            [FromQuery] PageOptions pageOptions,
            CancellationToken cancellationToken) => command.ExecuteAsync(all, pageOptions, cancellationToken);

        /// <summary>
        /// Patches the language with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The languages unique identifier.</param>
        /// <param name="patch">The patch document. See http://jsonpatch.com.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK if the language was patched, a 400 Bad Request if the patch was invalid or a 404 Not Found
        /// if a language with the specified unique identifier was not found.</returns>
        [HttpPatch("{id}", Name = LanguagesControllerRoute.PatchLanguage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The patched language with the specified unique identifier.", typeof(Language))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The patch document is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A language with the specified unique identifier could not be found.")]
        public Task<IActionResult> Patch(
            [FromServices] IPatchLanguageCommand command,
            int id,
            [FromBody] JsonPatchDocument<SaveLanguage> patch,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, patch);

        /// <summary>
        /// Creates a new language.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="language">The language to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created language or a 400 Bad Request if the language is
        /// invalid.</returns>
        [HttpPost("", Name = LanguagesControllerRoute.PostLanguage)]
        [SwaggerResponse(StatusCodes.Status201Created, "The language was created.", typeof(Language))]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The language is invalid.")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "The language code is existed")]
        public Task<IActionResult> Post(
            [FromServices] IPostLanguageCommand command,
            [FromBody] SaveLanguage language,
            CancellationToken cancellationToken) => command.ExecuteAsync(language);

        /// <summary>
        /// Updates an existing language with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The language identifier.</param>
        /// <param name="language">The language to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated language, a 400 Bad Request if the language is invalid or a
        /// or a 404 Not Found if a language with the specified unique identifier was not found.</returns>
        [HttpPut("{id}", Name = LanguagesControllerRoute.PutLanguage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The language was updated.", typeof(Language))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The language is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A language with the specified unique identifier could not be found.")]
        public Task<IActionResult> Put(
            [FromServices] IPutLanguageCommand command,
            int id,
            [FromBody] SaveLanguage language,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, language);

        /// <summary>
        ///Change isDefault of language
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The language identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated language, a 400 Bad Request if the language is invalid or a
        /// or a 404 Not Found if a language with the specified unique identifier was not found.</returns>
        [HttpPut("{id}/Default", Name = LanguagesControllerRoute.PutSetDefaultLanguage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The language was updated.", typeof(Language))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The language is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A language with the specified unique identifier could not be found.")]
        public Task<IActionResult> Put(
            [FromServices] IPutSetDefaultLanguageCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id);

        /// <summary>
        ///Toggle Active of language
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The language identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated language, a 400 Bad Request if the language is invalid or a
        /// or a 404 Not Found if a language with the specified unique identifier was not found.</returns>
        [HttpPut("{id}/Toggle", Name = LanguagesControllerRoute.PutToggleLanguage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The language was updated.", typeof(Language))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The language is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A language with the specified unique identifier could not be found.")]
        public Task<IActionResult> PutToggle(
            [FromServices] IPutToggleLanguageCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id);
    }
}
