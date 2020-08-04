namespace ApiTemplate.Controllers
{
    using Commands.PaymentMethodCommands;
    using ViewModels.PaymentMethodViewModels;
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
    public class PaymentMethodsController : ControllerBase
    {
        /// <summary>
        /// Gets the All PaymentMethod .
        /// </summary>
        [HttpGet("", Name = PaymentMethodsControllerRoute.GetPaymentMethodAll)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of PaymentMethods for the specified page.", typeof(List<PaymentMethod>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public Task<IActionResult> GetAll(
            [FromServices] IGetPaymentMethodAllCommand command,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync();

        /// <summary>
        /// Gets the paymentMethodItems with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The paymentMethodItemss unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the paymentMethodItems or a 404 Not Found if a paymentMethodItems with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{id}", Name = PaymentMethodsControllerRoute.GetPaymentMethod)]
        [SwaggerResponse(StatusCodes.Status200OK, "The PaymentMethod with the specified unique identifier.", typeof(PaymentMethod))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The PaymentMethod has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A PaymentMethod with the specified unique identifier could not be found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetPaymentMethodCommand command,
            int id,
            CancellationToken cancellationToken) =>
            command.ExecuteAsync(id);

        /// <summary>
        /// Creates a new paymentMethod.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="paymentMethod">The paymentMethod to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created paymentMethod or a 400 Bad Request if the paymentMethod is
        /// invalid.</returns>
        [HttpPost("", Name = PaymentMethodsControllerRoute.PostPaymentMethod)]
        [SwaggerResponse(StatusCodes.Status201Created, "The paymentMethod was created.", typeof(PaymentMethod))]
        //[Authorize(Policy = ApplicationPolicies.Root, Roles = RoleNames.Root)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The paymentMethod is invalid.")]
        [SwaggerResponse(StatusCodes.Status409Conflict, "The paymentMethod code is existed")]
        public Task<IActionResult> Post(
            [FromServices] IPostPaymentMethodCommand command,
            [FromBody] SavePaymentMethod paymentMethod,
            CancellationToken cancellationToken) => command.ExecuteAsync(paymentMethod);

        /// <summary>
        /// Updates Status of PaymentMethod with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The PaymentMethod identifier.</param>
        /// <param name="savePaymentMethod">The savePaymentMethod to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated PaymentMethod, a 400 Bad Request if the PaymentMethod is invalid or a
        /// or a 404 Not Found if a PaymentMethod with the specified unique identifier was not found.</returns>
        [HttpPut("/paymentMethods/{id}", Name = PaymentMethodsControllerRoute.PutPaymentMethod)]
        [SwaggerResponse(StatusCodes.Status200OK, "The PaymentMethod was updated.", typeof(PaymentMethod))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The PaymentMethod is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A PaymentMethod with the specified unique identifier could not be found.")]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The specified Accept MIME type is not acceptable.")]
        public Task<IActionResult> Put(
            [FromServices] IPutPaymentMethodCommand command,
            int id,
            [FromBody] SavePaymentMethod savePaymentMethod,
            CancellationToken cancellationToken) => command.ExecuteAsync(id, savePaymentMethod);

        /// <summary>
        /// Delete a PaymentMethod with the specified unique identifier.
        /// </summary>

        [HttpDelete("/paymentMethods/{id}", Name = PaymentMethodsControllerRoute.DeletePaymentMethod)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The PaymentMethod with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A PaymentMethod with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeletePaymentMethodCommand command,
            int id,
            CancellationToken cancellationToken) => command.ExecuteAsync(id);

    }
}
