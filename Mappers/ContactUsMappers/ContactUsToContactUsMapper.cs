namespace ApiTemplate.Mappers.ContactUsMappers
{
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Constants;
    using ViewModels.ContactUsViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class ContactUsToContactUsMapper : IMapper<Models.ContactUs, ContactUs>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;

        public ContactUsToContactUsMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
        }

        public void Map(Models.ContactUs source, ContactUs destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Id = source.Id;
            destination.Title = source.Title;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Email = source.Email;
            destination.Phone = source.Phone;
            destination.Message = source.Message;
            destination.Status = source.Status;
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                this.httpContextAccessor.HttpContext,
                ContactUsControllerRoute.GetContactUs,
                new { source.Id })).ToString();
        }
    }
}