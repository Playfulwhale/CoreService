namespace ApiTemplate.Controllers
{
    using Commands.MenuCommands;
    using ViewModels.MenuViewModels;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    
    public class MenusController : ControllerBase
    {
        [HttpGet("", Name = MenusControllerRoute.GetMenuAll)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of Menus for the specified page.", typeof(List<Menu>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
       public Task<IActionResult> GetAll(
            [FromServices] IGetMenuAllCommand command,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync();

        /// <summary>
        /// Gets the menuItems with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The menuItemss unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the menuItems or a 404 Not Found if a menuItems with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{id}", Name = MenusControllerRoute.GetMenu)]
        [SwaggerResponse(StatusCodes.Status200OK, "The Menu with the specified unique identifier.", typeof(Menu))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The Menu has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Menu with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetMenuCommand command,
            int id,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync(id);

        /// <summary>
        /// Creates a new menu.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="menu">The menu to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created menu or a 400 Bad Request if the menu is
        /// invalid.</returns>
        [HttpPost("", Name = MenusControllerRoute.PostMenu)]
        [SwaggerResponse(StatusCodes.Status201Created, "The menu was created.", typeof(Menu))]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The menu is invalid.")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "The menu code is existed")]
        public Task<IActionResult> Post(
            [FromServices] IPostMenuCommand command,
            [FromBody] SaveMenu menu,
            CancellationToken cancellationToken) => command.ExecuteAsync(menu);

        /// <summary>
        /// Updates Status of Menu with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The Menu identifier.</param>
        /// <param name="saveMenu">The saveMenu to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated Menu, a 400 Bad Request if the Menu is invalid or a
        /// or a 404 Not Found if a Menu with the specified unique identifier was not found.</returns>
        [HttpPut("/menus/{id}", Name = MenusControllerRoute.PutMenu)]
        [SwaggerResponse(StatusCodes.Status200OK, "The Menu was updated.", typeof(Menu))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The Menu is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Menu with the specified unique identifier could not be found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Put(
            [FromServices] IPutMenuCommand command,
            int id,
            [FromBody] SaveMenu saveMenu,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, saveMenu);

        [HttpDelete("{id}", Name = MenusControllerRoute.DeleteMenu)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The Menu with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Menu with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeleteMenuCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id);

        /// <summary>
        /// Gets the menuItems with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="position">The menuItemss to get.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the menuItems or a 404 Not Found if a menuItems with the specified unique
        /// identifier was not found.</returns>
        /// 
        [AllowAnonymous]
        [HttpGet("/public/menus", Name = MenusControllerRoute.PublicGetMenu)]
        [SwaggerResponse(StatusCodes.Status200OK, "The Menu with the specified unique identifier.", typeof(PublicMenu))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The Menu has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Menu with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IPublicGetMenuCommand command,
            string position,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync(position);
    }
}
