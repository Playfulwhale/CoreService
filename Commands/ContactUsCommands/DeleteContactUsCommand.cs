namespace ApiTemplate.Commands.ContactUsCommands
{
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteContactUsCommand : IDeleteContactUsCommand
    {
        private readonly IContactUsRepository _contactUsRepository;

        public DeleteContactUsCommand(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        public async Task<IActionResult> ExecuteAsync(int contactUsId, CancellationToken cancellationToken)
        {
            var contactUs = await _contactUsRepository.Get(contactUsId, cancellationToken);
            if (contactUs == null)
            {
                return new NotFoundResult();
            }

            await _contactUsRepository.Delete(contactUs, cancellationToken);

            return new NoContentResult();
        }
    }
}