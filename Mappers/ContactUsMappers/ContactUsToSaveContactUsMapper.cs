namespace ApiTemplate.Mappers.ContactUsMappers
{
    using Boxed.Mapping;
    using Services;
    using System;
    using ViewModels.ContactUsViewModels;

    public class ContactUsToSaveContactUsMapper : IMapper<Models.ContactUs, SaveContactUs>, IMapper<SaveContactUs, Models.ContactUs>
    {
        private readonly IClockService _clockService;

        public ContactUsToSaveContactUsMapper(IClockService clockService)
        {
            _clockService = clockService;
        }
            

        public void Map(Models.ContactUs source, SaveContactUs destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Title = source.Title;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Email = source.Email;
            destination.Phone = source.Phone;
            destination.Message = source.Message;
            destination.Status = source.Status;
        }

        public void Map(SaveContactUs source, Models.ContactUs destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var now = _clockService.UtcNow;

            if (destination.CreatedAt == null)
            {
                destination.CreatedAt = now;
            }
            destination.ModifiedAt = now;

            destination.Title = source.Title;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Email = source.Email;
            destination.Phone = source.Phone;
            destination.Message = source.Message;
            destination.Status = source.Status;
        }
    }
}