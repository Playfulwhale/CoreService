
namespace ApiTemplate.Controllers
{
    using Commands.SystemSettingCommands;
    using Constants;
    using ViewModels.SystemSettingViewModels;
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
    
    public class SystemSettingsController : ControllerBase
    {
        /// <summary>
        /// Creates a new SystemSetting.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="systemSetting">The SystemSetting to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created SystemSetting or a 400 Bad Request if the SystemSetting is
        /// invalid.</returns>
        [HttpPost("/system", Name = SystemSettingsControllerRoute.PostSystemSetting)]
        [SwaggerResponse(StatusCodes.Status201Created, "The SystemSetting was created.", typeof(SystemSetting))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The SystemSetting is invalid.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Post(
            [FromServices] IPostSystemSettingCommand command,
            [FromBody] SaveSystemSetting systemSetting,
            CancellationToken cancellationToken) => command.ExecuteAsync(systemSetting);

        /// <summary>
        /// update a configurations SystemSettings.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="systemSettings">The SystemSetting to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created SystemSetting or a 400 Bad Request if the SystemSetting is
        /// invalid.</returns>
        [HttpPut("/system", Name = SystemSettingsControllerRoute.PostSystemSettings)]
        [SwaggerResponse(StatusCodes.Status201Created, "The SystemSetting was created.", typeof(SystemSetting))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The SystemSetting is invalid.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> PostList(
            [FromServices] IPutSystemSettingsCommand command,
            [FromBody] List<SaveSystemSetting> systemSettings,
            CancellationToken cancellationToken) => command.ExecuteAsync(systemSettings);

        /// <summary>
        /// Gets all SystemSettinges.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of SystemSettings, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("/system", Name = SystemSettingsControllerRoute.GetAllSystemSetting)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of SystemSettings for the specified page.", typeof(List<SystemSetting>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> GetAll(
            [FromServices] IGetAllSystemSettingCommand command,
            CancellationToken cancellationToken) => command.ExecuteAsync();

        /// <summary>
        /// Gets SystemSettinges With Key.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="key">The Key of SystemSetting.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of SystemSettings, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("/system/{key}", Name = SystemSettingsControllerRoute.GetSystemSetting)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of SystemSettings for the specified page.", typeof(SystemSetting))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Get(
            [FromServices] IGetSystemSettingCommand command,
            string key,
            CancellationToken cancellationToken) => command.ExecuteAsync(key);

        /// <summary>
        /// Updates an existing SystemSetting with Key.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="key">The Key of SystemSetting.</param>
        /// <param name="systemSetting">The SystemSetting to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated SystemSetting, a 400 Bad Request if the SystemSetting is invalid or a
        /// or a 404 Not Found if a SystemSetting with the specified unique identifier was not found.</returns>
        [HttpPut("/system/{key}", Name = SystemSettingsControllerRoute.PutSystemSetting)]
        [SwaggerResponse(StatusCodes.Status200OK, "The SystemSetting was updated.", typeof(SystemSetting))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The SystemSetting is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A SystemSetting with the specified unique identifier could not be found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Put(
            [FromServices] IPutSystemSettingCommand command,
            string key,
            [FromBody] SaveSystemSettingWithOutKey systemSetting,
            CancellationToken cancellationToken) => command.ExecuteAsync(key, systemSetting);

        /// <summary>
        /// Deletes the SystemSetting with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="key">The Key of SystemSetting.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the SystemSetting was deleted or a 404 Not Found if a SystemSetting with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("/system/{key}:", Name = SystemSettingsControllerRoute.DeleteSystemSetting)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The SystemSetting with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A SystemSetting with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeleteSystemSettingCommand command,
            string key,
            CancellationToken cancellationToken) => command.ExecuteAsync(key);
    }
}