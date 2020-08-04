namespace ApiTemplate.Controllers
{
    using Commands.SlideCommands;
    using Constants;
    using ViewModels.SlideViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    
    public class SlidesController : ControllerBase
    {
        /// <summary>
        /// Deletes the slide with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The slides unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the slide was deleted or a 404 Not Found if a slide with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{id}", Name = SlidesControllerRoute.DeleteSlide)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The slide with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A slide with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeleteSlideCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id);

        /// <summary>
        /// Gets the slide with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The slides unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the slide or a 404 Not Found if a slide with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{id}", Name = SlidesControllerRoute.GetSlide)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The slide with the specified unique identifier.", typeof(Slide))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The slide has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A slide with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetSlideCommand command,
            int id,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync(id);

        /// <summary>
        /// Gets a collection of slides
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the slide or a 404 Not Found if a slide with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("", Name = SlidesControllerRoute.GetSlideAll)]
        [SwaggerResponse(StatusCodes.Status200OK, "The slide with the specified unique identifier.", typeof(Slide))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The slide has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A slide with the specified unique identifier could not be found.")]
        public Task<IActionResult> GetAll(
            [FromServices] IGetAllSlideCommand command,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync();

        /// <summary>
        /// Public Gets a collection of slides
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the slide or a 404 Not Found if a slide with the specified unique
        /// identifier was not found.</returns>
        /// 
        [AllowAnonymous]
        [HttpGet("/public/slides", Name = SlidesControllerRoute.PublicGetSlideAll)]
        [SwaggerResponse(StatusCodes.Status200OK, "The slide with the specified unique identifier.", typeof(Slide))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The slide has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A slide with the specified unique identifier could not be found.")]
        public Task<IActionResult> PublicGetAll(
            [FromServices] IPublicIGetAllSlideCommand command,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync();
        

        /// <summary>
        /// Creates a new slide.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="slide">The slide to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created slide or a 400 Bad Request if the slide is
        /// invalid.</returns>
        [HttpPost("", Name = SlidesControllerRoute.PostSlide)]
        [SwaggerResponse(StatusCodes.Status201Created, "The slide was created.", typeof(Slide))]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The slide is invalid.")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "The slide code is existed")]
        public Task<IActionResult> Post(
            [FromServices] IPostSlideCommand command,
            [FromBody] SaveSlide slide,
            CancellationToken cancellationToken) => command.ExecuteAsync(slide);

        /// <summary>
        /// Updates an existing slide with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The slide identifier.</param>
        /// <param name="slide">The slide to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated slide, a 400 Bad Request if the slide is invalid or a
        /// or a 404 Not Found if a slide with the specified unique identifier was not found.</returns>
        [HttpPut("{id}", Name = SlidesControllerRoute.PutSlide)]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status200OK, "The slide was updated.", typeof(Slide))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The slide is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A slide with the specified unique identifier could not be found.")]
        public Task<IActionResult> Put(
            [FromServices] IPutSlideCommand command,
            int id,
            [FromBody] SaveSlide slide,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, slide);

    }
}