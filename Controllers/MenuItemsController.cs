namespace ApiTemplate.Controllers
{
    using Commands.MenuItemCommands;
    using Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.MenuItemViewModels;

    [Route("menu/items")]
    [ApiController]
    [ApiVersion("1.0")]
    
    public class MenuItemsController : ControllerBase
    {
        /// <summary>
        /// Creates a new MenuItem.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id"></param>
        /// <param name="menuItem">The MenuItem to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created MenuItem or a 400 Bad Request if the MenuItem is
        /// invalid.</returns>
        [HttpPost("/menus/{id}/items", Name = MenuItemsControllerRoute.PostMenuItem)]
        [SwaggerResponse(StatusCodes.Status201Created, "The MenuItem was created.", typeof(MenuItem))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The MenuItem is invalid.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Post(
            [FromServices] IPostMenuItemCommand command,
            int id,
            [FromBody] SaveMenuItem menuItem,
            CancellationToken cancellationToken) => command.ExecuteAsync(id,menuItem);
        

        [HttpGet("/menus/items/{id}", Name = MenuItemsControllerRoute.GetMenuItem)]
        [SwaggerResponse(StatusCodes.Status200OK, "The Menu with the specified unique identifier.", typeof(MenuItem))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The Menu has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Menu with the specified unique identifier could not be found.")]
        public Task<IActionResult> GetMenuItem(
            [FromServices] IGetMenuItemCommand command,
            int id,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync(id);

        /// <summary>
        /// Updates Status of MenuItem with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The Menu identifier.</param>
        /// <param name="saveMenuItem">The saveMenuItem to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated Menu, a 400 Bad Request if the Menu is invalid or a
        /// or a 404 Not Found if a Menu with the specified unique identifier was not found.</returns>
        [HttpPut("/menus/items/{id}", Name = MenuItemsControllerRoute.PutMenu)]
        [SwaggerResponse(StatusCodes.Status200OK, "The Menu was updated.", typeof(MenuItem))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The Menu is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Menu with the specified unique identifier could not be found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Put(
            [FromServices] IPutMenuItemCommand command,
            int id,
            [FromBody] SaveMenuItem saveMenuItem,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, saveMenuItem);

        /// <summary>
        /// Deletes the MenuItem with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The MenuItem unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the MenuItem was deleted or a 404 Not Found if a MenuItem with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("/menus/items/{id}", Name = MenuItemsControllerRoute.DeleteMenuItem)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The MenuItem with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A MenuItem with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeleteMenuItemCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id);
    }
}
