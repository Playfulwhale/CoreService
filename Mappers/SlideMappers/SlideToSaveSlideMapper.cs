namespace ApiTemplate.Mappers.SlideMappers
{
    using Services;
    using ViewModels.SlideContentViewModels;
    using ViewModels.SlideViewModels;
    using Boxed.Mapping;
    using System;

    public class SlideToSaveSlideMapper : IMapper<Models.Slide, SaveSlide>, IMapper<SaveSlide, Models.Slide>
    {
        private readonly IClockService _clockService;
        private readonly IMapper<Models.SlideContent, SaveSlideContent> _saveSlideContentToSlideContentMapper;
        private readonly IMapper<SaveSlideContent, Models.SlideContent> _slideContentToSaveSlideContentMapper;
        public SlideToSaveSlideMapper(IClockService clockService,
            IMapper<Models.SlideContent, SaveSlideContent> saveSlideContentToSlideContentMapper,
            IMapper<SaveSlideContent, Models.SlideContent> slideContentToSaveSlideContentMapper)
        {
             _clockService = clockService;
            _saveSlideContentToSlideContentMapper = saveSlideContentToSlideContentMapper;
            _slideContentToSaveSlideContentMapper = slideContentToSaveSlideContentMapper;

        }

        public void Map(Models.Slide source, SaveSlide destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.LinkType = source.LinkType;
            destination.Link = source.Link;
            destination.ImageUrl = source.ImageUrl;
            destination.OpenNewTab = source.OpenNewTab;
            destination.SortOrder = source.SortOrder;
            destination.Status = source.Status;
            destination.SlideContents = _saveSlideContentToSlideContentMapper.MapList(source.SlideContents);
        }

        public void Map(SaveSlide source, Models.Slide destination)
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

            destination.LinkType = source.LinkType;
            destination.Link = source.Link;
            destination.ImageUrl = source.ImageUrl;
            destination.OpenNewTab = source.OpenNewTab;
            destination.SortOrder = source.SortOrder;
            destination.Status = source.Status;
            destination.SlideContents = _slideContentToSaveSlideContentMapper.MapList(source.SlideContents);
        }
    }
}