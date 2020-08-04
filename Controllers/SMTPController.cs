namespace ApiTemplate.Controllers
{
    using Commands.SMTPCommands;
    using ViewModels.SMTPViewModels;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;
    using Microsoft.AspNetCore.Authorization;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    
    public class SmtpController : ControllerBase
    {
        /// <summary>
        /// Test connecton to Server.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="smtpConnection">The configuration test.</param>
        /// <returns>A 201 Created response containing the newly created slide or a 400 Bad Request if the test is
        /// invalid.</returns>
        [HttpPost("check", Name = SmtpControllerRoute.SmtpConnection)]
        [SwaggerResponse(StatusCodes.Status201Created, "The test was valid.", typeof(SmtpConnection))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The test is invalid.")]
        public IActionResult Post(
            [FromServices] IPostSmtpConnectionCommand command,
            [FromBody] SmtpConnection smtpConnection) => command.Execute(smtpConnection);
    }
}