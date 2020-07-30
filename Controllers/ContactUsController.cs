namespace ApiTemplate.Controllers
{
    using Commands.ContactUsCommands;
    using Constants;
    using ViewModels.ContactUsViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion(ApiVersionName.V1)]
    public class ContactUsController : ControllerBase
    {
        /// <summary>
        /// Creates a new ContactUs.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="contactUs">The ContactUs to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created ContactUs or a 400 Bad Request if the ContactUs is
        /// invalid.</returns>
        /// 
        
        [HttpPost("/public/contacts", Name = ContactUsControllerRoute.PostContactUs)]
        [SwaggerResponse(StatusCodes.Status201Created, "The ContactUs was created.", typeof(ContactUs))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The ContactUs is invalid.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Post(
            [FromServices] IPostContactUsCommand command,
            [FromBody] SaveContactUs contactUs,
            CancellationToken cancellationToken) => command.ExecuteAsync(contactUs);

        /// <summary>
        /// Gets a collection of ContactUs using the specified page number and number of items per page.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of ContactUs, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("/contacts", Name = ContactUsControllerRoute.GetContactUs)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of ContactUs for the specified page.", typeof(List<ContactUs>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Get(
            [FromServices] IGetContactUsCommand command,
            CancellationToken cancellationToken) => command.ExecuteAsync();

        /// <summary>
        /// Updates Status of ContactUs with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The ContactUs identifier.</param>
        /// <param name="status">The Status of ContactUs.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated ContactUs, a 400 Bad Request if the ContactUs is invalid or a
        /// or a 404 Not Found if a ContactUs with the specified unique identifier was not found.</returns>
        [HttpPut("/contacts/{id}", Name = ContactUsControllerRoute.PutContactUs)]
        [SwaggerResponse(StatusCodes.Status200OK, "The ContactUs was updated.", typeof(ContactUs))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The ContactUs is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A ContactUs with the specified unique identifier could not be found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Put(
            [FromServices] IPutContactUsCommand command,
            int id,
            string status,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, status);
    }
}