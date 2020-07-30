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
    using Commands.ZoneCommands;
    using ViewModels.ZoneViewModels;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion(ApiVersionName.V1)]
    public class ZonesController : ControllerBase
    {
        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions]
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
        /// Returns an Allow HTTP header with the allowed HTTP methods for a zone with the specified unique identifier.
        /// </summary>
        /// <param name="zoneId">The zones unique identifier.</param>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("{zoneId}")]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
        public IActionResult Options(int zoneId)
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
        /// Deletes the zone with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="zoneId">The zones unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the zone was deleted or a 404 Not Found if a zone with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{zoneId}", Name = ZonesControllerRoute.DeleteZone)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The zone with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A zone with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeleteZoneCommand command,
            int zoneId,
            CancellationToken cancellationToken) => command.ExecuteAsync(zoneId, cancellationToken);

        /// <summary>
        /// Gets the zone with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="zoneId">The zones unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the zone or a 404 Not Found if a zone with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{zoneId}", Name = ZonesControllerRoute.GetZone)]
        [HttpHead("{zoneId}", Name = ZonesControllerRoute.HeadZone)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The zone with the specified unique identifier.", typeof(Zone))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The zone has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A zone with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetZoneCommand command,
            int zoneId,
            CancellationToken cancellationToken) => command.ExecuteAsync(zoneId, cancellationToken);

        /// <summary>
        /// Gets a collection of zones using the specified page number and number of items per page.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="all">The all query command.</param>
        /// <param name="pageOptions">The page options.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of zones, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("", Name = ZonesControllerRoute.GetZonePage)]
        [HttpHead("", Name = ZonesControllerRoute.HeadZonePage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of zones for the specified page.", typeof(PageResult<Zone>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public Task<IActionResult> GetPage(
            [FromServices] IGetZonePageCommand command,
            string all,
            [FromQuery] PageOptions pageOptions,
            CancellationToken cancellationToken) => command.ExecuteAsync(all, pageOptions, cancellationToken);
      

        /// <summary>
        /// Patches the zone with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="zoneId">The zones unique identifier.</param>
        /// <param name="patch">The patch document. See http://jsonpatch.com.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK if the zone was patched, a 400 Bad Request if the patch was invalid or a 404 Not Found
        /// if a zone with the specified unique identifier was not found.</returns>
        [HttpPatch("{zoneId}", Name = ZonesControllerRoute.PatchZone)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The patched zone with the specified unique identifier.", typeof(Zone))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The patch document is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A zone with the specified unique identifier could not be found.")]
        public Task<IActionResult> Patch(
            [FromServices] IPatchZoneCommand command,
            int zoneId,
            [FromBody] JsonPatchDocument<SaveZone> patch,
            CancellationToken cancellationToken) => command.ExecuteAsync(zoneId, patch, cancellationToken);

        /// <summary>
        /// Creates a new zone.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="zone">The zone to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created zone or a 400 Bad Request if the zone is
        /// invalid.</returns>
        [HttpPost("", Name = ZonesControllerRoute.PostZone)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status201Created, "The zone was created.", typeof(Zone))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The zone is invalid.")]
        public Task<IActionResult> Post(
            [FromServices] IPostZoneCommand command,
            [FromBody] SaveZone zone,
            CancellationToken cancellationToken) => command.ExecuteAsync(zone, cancellationToken);

        /// <summary>
        /// Updates an existing zone with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="zoneId">The zone identifier.</param>
        /// <param name="zone">The zone to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated zone, a 400 Bad Request if the zone is invalid or a
        /// or a 404 Not Found if a zone with the specified unique identifier was not found.</returns>
        [HttpPut("{zoneId}", Name = ZonesControllerRoute.PutZone)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The zone was updated.", typeof(Zone))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The zone is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A zone with the specified unique identifier could not be found.")]
        public Task<IActionResult> Put(
            [FromServices] IPutZoneCommand command,
            int zoneId,
            [FromBody] SaveZone zone,
            CancellationToken cancellationToken) => command.ExecuteAsync(zoneId, zone, cancellationToken);

        /// <summary>
        /// Toggle a zone status with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="zoneId">The zone identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated zone, a 400 Bad Request if the zone is invalid or a
        /// or a 404 Not Found if a zone with the specified unique identifier was not found.</returns>
        [HttpPut("{zoneId}/toggle", Name = ZonesControllerRoute.PutZoneActive)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The zone was updated.", typeof(Zone))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The zone is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A zone with the specified unique identifier could not be found.")]
        public Task<IActionResult> ToggleStatus(
            [FromServices] IPutZoneActiveCommand command,
            int zoneId,
            CancellationToken cancellationToken) => command.ExecuteAsync(zoneId, cancellationToken);
    }
}