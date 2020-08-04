namespace ApiTemplate.Controllers
{
    using Commands.PackageSubscriberCommands;
    using Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.PackageSubscriberViewModels;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    
    public class PackageSubscribersController : ControllerBase
    {
        /// <summary>
        /// Creates a new PackageSubscriber.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="packageSubscriber">The PackageSubscriber to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created PackageSubscriber or a 400 Bad Request if the PackageSubscriber is
        /// invalid.</returns>
        [HttpPost("", Name = PackageSubscribersControllerRoute.PostPackageSubscriber)]
        [SwaggerResponse(StatusCodes.Status201Created, "The PackageSubscriber was created.", typeof(PackageSubscriber))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The PackageSubscriber is invalid.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Post(
            [FromServices] IPostPackageSubscriberCommand command,
            [FromBody] SavePackageSubscriber packageSubscriber,
            CancellationToken cancellationToken) => command.ExecuteAsync(packageSubscriber);

        /// <summary>
        /// Creates a new PackageSubscriber.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="packageSubscriber">The PackageSubscriber to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created PackageSubscriber or a 400 Bad Request if the PackageSubscriber is
        /// invalid.</returns>
        /// 
        [AllowAnonymous]
        [HttpPost("/public/packageSubscribers", Name = PackageSubscribersControllerRoute.PostPublicPackageSubscriber)]
        [SwaggerResponse(StatusCodes.Status201Created, "The PackageSubscriber was created.", typeof(PackageSubscriber))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The PackageSubscriber is invalid.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> PublicPost(
            [FromServices] IPublicPostPackageSubscriberCommand command,
            [FromBody] PublicSavePackageSubscriber packageSubscriber,
            CancellationToken cancellationToken) => command.ExecuteAsync(packageSubscriber);

        /// <summary>
        /// Gets a collection of PackageSubscriber by userId
        /// </summary>
        /// <param name="userId">The unique identifier.</param>
        /// <param name="queryOptions">The option query.</param>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the PackageSubscriber or a 404 Not Found if a PackageSubscriber with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("user/{userId}", Name = PackageSubscribersControllerRoute.GetAllPackageSubscriberByUser)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of PackageSubscriber for the specified page.", typeof(List<PackageSubscriber>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> GetAllByUser(
            [FromServices] IGetAllPackageSubscriberByUserCommand command,
            int userId,
            [FromQuery] QueryOptions queryOptions,
            CancellationToken cancellationToken) => command.ExecuteAsync(userId, queryOptions, cancellationToken);

        /// <summary>
        /// Gets a collection of PackageSubscriber
        /// </summary>
        /// <param name="queryOptions">The option query.</param>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the PackageSubscriber or a 404 Not Found if a PackageSubscriber with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("", Name = PackageSubscribersControllerRoute.GetAllPackageSubscriber)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of PackageSubscriber for the specified page.", typeof(List<PackageSubscriber>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> GetAll(
            [FromServices] IGetAllPackageSubscriberCommand command,
            [FromQuery] QueryOptions queryOptions,
            CancellationToken cancellationToken) => command.ExecuteAsync(queryOptions, cancellationToken);


        /// <summary>
        /// Gets a collection of PackageSubscriber by userId for public
        /// </summary>
        /// <param name="userId">The unique identifier.</param>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the PackageSubscriber or a 404 Not Found if a PackageSubscriber with the specified unique
        /// identifier was not found.</returns>
        /// 
        [AllowAnonymous]
        [HttpGet("/public/packageSubscribers/user/{userId}", Name = PackageSubscribersControllerRoute.PublicGetAllPackageSubscriberByUser)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of PackageSubscriber for the specified page.", typeof(List<PackageSubscriber>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> PublicGetAllByUser(
            [FromServices] IPublicGetAllPackageSubscriberByUserCommand command,
            int userId,
            CancellationToken cancellationToken) => command.ExecuteAsync(userId, cancellationToken);

        /// <summary>
        /// Gets the packageSubscriber with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The packageSubscribers unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the packageSubscriber or a 404 Not Found if a packageSubscriber with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{id}", Name = PackageSubscribersControllerRoute.GetPackageSubscriber)]
        [SwaggerResponse(StatusCodes.Status200OK, "The packageSubscriber with the specified unique identifier.", typeof(PackageSubscriber))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The packageSubscriber has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A packageSubscriber with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetPackageSubscriberCommand command,
            int id,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync(id);

        /// <summary>
        /// Deletes the PackageSubscriber with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The PackageSubscriber unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the PackageSubscriber was deleted or a 404 Not Found if a PackageSubscriber with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{id}", Name = PackageSubscribersControllerRoute.DeletePackageSubscriber)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The PackageSubscriber with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A PackageSubscriber with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeletePackageSubscriberCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id);

    }
}