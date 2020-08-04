namespace ApiTemplate.Controllers
{
    using Commands.SystemSettingGroupCommands;
    using Constants;
    using ViewModels.SystemSettingGroupViewModels;
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
    public class SystemSettingGroupsController : ControllerBase
    {
        /// <summary>
        /// Creates a new SystemSettingGroup.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="systemSettingGroup">The SystemSettingGroup to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created SystemSettingGroup or a 400 Bad Request if the SystemSettingGroup is
        /// invalid.</returns>
        [HttpPost("/system/group", Name = SystemSettingGroupsControllerRoute.PostSystemSettingGroup)]
        [SwaggerResponse(StatusCodes.Status201Created, "The SystemSettingGroup was created.", typeof(SystemSettingGroup))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The SystemSettingGroup is invalid.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Post(
            [FromServices] IPostSystemSettingGroupCommand command,
            [FromBody] SaveSystemSettingGroup systemSettingGroup,
            CancellationToken cancellationToken) => command.ExecuteAsync(systemSettingGroup, cancellationToken);

        /// <summary>
        /// Gets all SystemSettingGroupes.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="all">The all query command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of SystemSettingGroups, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("/system/group", Name = SystemSettingGroupsControllerRoute.GetAllSystemSettingGroup)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of SystemSettingGroups for the specified page.", typeof(List<SystemSettingGroup>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> GetAll(
            [FromServices] IGetAllSystemSettingGroupCommand command,
            string all,
            CancellationToken cancellationToken) => command.ExecuteAsync(all, cancellationToken);

        /// <summary>
        /// Gets SystemSettingGroupes With ID.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The ID of SystemSettingGroup.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of SystemSettingGroups, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("/system/group/{id}", Name = SystemSettingGroupsControllerRoute.GetSystemSettingGroup)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of SystemSettingGroups for the specified page.", typeof(SystemSettingGroup))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Get(
            [FromServices] IGetSystemSettingGroupCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, cancellationToken);

        /// <summary>
        /// Gets all Param of SystemSettingGroupes With ID.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The ID of SystemSettingGroup.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of SystemSettingGroups, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("/system/group/{id}/params", Name = SystemSettingGroupsControllerRoute.GetAllSystemSettingGroupParam)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of SystemSettingGroups for the specified page.", typeof(SystemSettingGroup))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> GetParam(
            [FromServices] IGetAllSystemSettingGroupParamCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, cancellationToken);

        /// <summary>
        /// Updates an existing SystemSettingGroup with ID.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The ID of SystemSettingGroup.</param>
        /// <param name="systemSettingGroup">The SystemSettingGroup to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated SystemSettingGroup, a 400 Bad Request if the SystemSettingGroup is invalid or a
        /// or a 404 Not Found if a SystemSettingGroup with the specified unique identifier was not found.</returns>
        [HttpPut("/system/group/{id}", Name = SystemSettingGroupsControllerRoute.PutSystemSettingGroup)]
        [SwaggerResponse(StatusCodes.Status200OK, "The SystemSettingGroup was updated.", typeof(SystemSettingGroup))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The SystemSettingGroup is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A SystemSettingGroup with the specified unique identifier could not be found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Put(
            [FromServices] IPutSystemSettingGroupCommand command,
            int id,
            [FromBody] SaveSystemSettingGroup systemSettingGroup,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, systemSettingGroup, cancellationToken);

        /// <summary>
        /// Deletes the SystemSettingGroup with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The SystemSettingGroups unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the SystemSettingGroup was deleted or a 404 Not Found if a SystemSettingGroup with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("/system/group/{id}", Name = SystemSettingGroupsControllerRoute.DeleteSystemSettingGroup)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The SystemSettingGroup with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A SystemSettingGroup with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeleteSystemSettingGroupCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, cancellationToken);
    }
}