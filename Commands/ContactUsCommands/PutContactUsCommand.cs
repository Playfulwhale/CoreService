namespace ApiTemplate.Commands.ContactUsCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using ViewModels.ContactUsViewModels;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class PutContactUsCommand : IPutContactUsCommand
    {
        private readonly IContactUsRepository _contactUsRepository;
        private readonly IMapper<Models.ContactUs, ContactUs> _contactUsToContactUsMapper;
        private readonly IMapper<SaveContactUs, Models.ContactUs> _saveContactUsToContactUsMapper;
        private readonly IMapper<Models.ContactUs, SaveContactUs> _contactUsToSaveContactUsMapper;

        public PutContactUsCommand(
            IContactUsRepository contactUsRepository,
            IMapper<Models.ContactUs, ContactUs> contactUsToContactUsMapper,
            IMapper<SaveContactUs, Models.ContactUs> saveContactUsToContactUsMapper,
            IMapper<Models.ContactUs, SaveContactUs> contactUsToSaveContactUsMapper)
        {
            _contactUsRepository = contactUsRepository;
            _contactUsToContactUsMapper = contactUsToContactUsMapper;
            _saveContactUsToContactUsMapper = saveContactUsToContactUsMapper;
            _contactUsToSaveContactUsMapper = contactUsToSaveContactUsMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int id, string status, CancellationToken cancellationToken)
        {
            var contactUs = await _contactUsRepository.Get(id, cancellationToken);
            if (contactUs == null)
            {
                return new NotFoundResult();
            }
            var saveContactUs = new SaveContactUs();
            _contactUsToSaveContactUsMapper.Map(contactUs, saveContactUs);
            saveContactUs.Status = status;

            _saveContactUsToContactUsMapper.Map(saveContactUs, contactUs);

            contactUs = await _contactUsRepository.Update(contactUs, cancellationToken);
            var contactUsViewModel = _contactUsToContactUsMapper.Map(contactUs);

            return new OkObjectResult(contactUsViewModel);
        }
    }
}