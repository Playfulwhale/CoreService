namespace ApiTemplate.Controllers
{
    using Commands.PaidPackageCommands;
    using Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.PaidPackageViewModels;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    
    public class PaidPackagesController : ControllerBase
    {
        /// <summary>
        /// Creates a new PaidPackage.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="paidPackage">The PaidPackage to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created PaidPackage or a 400 Bad Request if the PaidPackage is
        /// invalid.</returns>
        [HttpPost("", Name = PaidPackagesControllerRoute.PostPaidPackage)]
        [SwaggerResponse(StatusCodes.Status201Created, "The PaidPackage was created.", typeof(PaidPackage))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The PaidPackage is invalid.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Post(
            [FromServices] IPostPaidPackageCommand command,
            [FromBody] SavePaidPackage paidPackage,
            CancellationToken cancellationToken) => command.ExecuteAsync(paidPackage);

        /// <summary>
        /// Gets a collection of PaidPackage
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the PaidPackage or a 404 Not Found if a PaidPackage with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("", Name = PaidPackagesControllerRoute.GetAllPaidPackage)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of PaidPackage for the specified page.", typeof(List<PaidPackage>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> GetAll(
            [FromServices] IGetAllPaidPackageCommand command,
            CancellationToken cancellationToken) => command.ExecuteAsync(cancellationToken);

        /// <summary>
        /// Gets a collection of PaidPackage for public
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the PaidPackage or a 404 Not Found if a PaidPackage with the specified unique
        /// identifier was not found.</returns>
        /// 
        [AllowAnonymous]
        [HttpGet("/public/paidpackages", Name = PaidPackagesControllerRoute.GetAllPublicPaidPackage)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of PaidPackage for the specified page.", typeof(List<PaidPackage>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> PublicGetAll(
            [FromServices] IPublicGetAllPaidPackageCommand command,
            CancellationToken cancellationToken) => command.ExecuteAsync(cancellationToken);

        /// <summary>
        /// Gets the paidPackage with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The paidPackages unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the paidPackage or a 404 Not Found if a paidPackage with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{id}", Name = PaidPackagesControllerRoute.GetPaidPackage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The paidPackage with the specified unique identifier.", typeof(PaidPackage))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The paidPackage has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A paidPackage with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetPaidPackageCommand command,
            int id,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync(id);
        
        /// <summary>
        /// Gets the paidPackage with the specified unique identifier for public.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The paidPackages unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the paidPackage or a 404 Not Found if a paidPackage with the specified unique
        /// identifier was not found.</returns>
        /// 
        [AllowAnonymous]
        [HttpGet("/public/paidpackages/{id}", Name = PaidPackagesControllerRoute.GetPublicPaidPackage)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The paidPackage with the specified unique identifier.", typeof(PaidPackage))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The paidPackage has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "A paidPackage with the specified unique identifier could not be found.")]
        public Task<IActionResult> GetPublic(
            [FromServices] IPublicGetPaidPackageCommand command,
            int id,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync(id);

        /// <summary>
        /// Updates Status of PaidPackage with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The PaidPackage identifier.</param>
        /// <param name="savePaidPackage">The savePaidPackage of PaidPackage.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated PaidPackage, a 400 Bad Request if the PaidPackage is invalid or a
        /// or a 404 Not Found if a PaidPackage with the specified unique identifier was not found.</returns>
        [HttpPut("{id}", Name = PaidPackagesControllerRoute.PutPaidPackage)]
        [SwaggerResponse(StatusCodes.Status200OK, "The PaidPackage was updated.", typeof(PaidPackage))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The PaidPackage is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A PaidPackage with the specified unique identifier could not be found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Put(
            [FromServices] IPutPaidPackageCommand command,
            int id,
            SavePaidPackage savePaidPackage,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, savePaidPackage, cancellationToken);

        /// <summary>
        /// Deletes the PaidPackage with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The PaidPackage unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the PaidPackage was deleted or a 404 Not Found if a PaidPackage with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{id}", Name = PaidPackagesControllerRoute.DeletePaidPackage)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The PaidPackage with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A PaidPackage with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeletePaidPackageCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, cancellationToken);

    }
}