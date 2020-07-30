namespace ApiTemplate.Commands.ContactUsCommands
{
    using Repositories;
    using ViewModels.ContactUsViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetContactUsCommand : IGetContactUsCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IContactUsRepository _contactUsRepository;
        private readonly IMapper<Models.ContactUs, ContactUs> _contactUsMapper;

        public GetContactUsCommand(
            IActionContextAccessor actionContextAccessor,
            IContactUsRepository contactUsRepository,
            IMapper<Models.ContactUs, ContactUs> contactUsMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _contactUsRepository = contactUsRepository;
            _contactUsMapper = contactUsMapper;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var listContactUs = await _contactUsRepository.GetAll(cancellationToken);

            var contactUsViewModels = new List<ContactUs>();

            foreach (var contactUs in listContactUs)
            {
                var contactUsViewModel = _contactUsMapper.Map(contactUs);
                contactUsViewModels.Add(contactUsViewModel);
            }

            return new OkObjectResult(contactUsViewModels);
        }
    }
}