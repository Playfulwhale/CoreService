namespace ApiTemplate.Mappers.SlideContentMappers
{
    using Services;
    using ViewModels.SlideContentViewModels;
    using Boxed.Mapping;
    using System;

    public class SlideContentToSaveSlideContentMapper : IMapper<Models.SlideContent, SaveSlideContent>, IMapper<SaveSlideContent, Models.SlideContent>
    {
        private readonly IClockService _clockService;

        public SlideContentToSaveSlideContentMapper(IClockService clockService) =>
            _clockService = clockService;

        public void Map(Models.SlideContent source, SaveSlideContent destination)
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
            destination.Caption = source.Caption;
            destination.LanguageCode = source.LanguageCode;
        }

        public void Map(SaveSlideContent source, Models.SlideContent destination)
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
            destination.Caption = source.Caption;
            destination.LanguageCode = source.LanguageCode;
        }
    }
}