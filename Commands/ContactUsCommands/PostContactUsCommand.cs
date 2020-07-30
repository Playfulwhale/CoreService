namespace ApiTemplate.Commands.ContactUsCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Constants;
    using Repositories;
    using ViewModels.ContactUsViewModels;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class PostContactUsCommand : IPostContactUsCommand
    {
        private readonly IContactUsRepository _contactUsRepository;
        private readonly IMapper<Models.ContactUs, ContactUs> _contactUsToContactUsMapper;
        private readonly IMapper<SaveContactUs, Models.ContactUs> _saveContactUsToContactUsMapper;

        public PostContactUsCommand(
            IContactUsRepository contactUsRepository,
            IMapper<Models.ContactUs, ContactUs> contactUsToContactUsMapper,
            IMapper<SaveContactUs, Models.ContactUs> saveContactUsToContactUsMapper)
        {
            _contactUsRepository = contactUsRepository;
            _contactUsToContactUsMapper = contactUsToContactUsMapper;
            _saveContactUsToContactUsMapper = saveContactUsToContactUsMapper;
        }

        public async Task<IActionResult> ExecuteAsync(SaveContactUs saveContactUs, CancellationToken cancellationToken)
        {
            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();

            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            var contactUs = _saveContactUsToContactUsMapper.Map(saveContactUs);
            //contactUs.CreatedBy = userId;
            //contactUs.ModifiedBy = userId;

            contactUs = await _contactUsRepository.Add(contactUs, cancellationToken);
            var contactUsViewModel = _contactUsToContactUsMapper.Map(contactUs);

            return new CreatedAtRouteResult(
                ContactUsControllerRoute.GetContactUs,
                new { ContactUsId = contactUsViewModel.Id },
                contactUsViewModel);
        }
    }
}