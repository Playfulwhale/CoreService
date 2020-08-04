namespace ApiTemplate.Mappers.SlideContentMappers
{
    using ViewModels.SlideContentViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;
    public class SlideContentToSlideContentMapper : IMapper<Models.SlideContent, SlideContent>
    {

        public void Map(Models.SlideContent source, SlideContent destination)
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
            destination.Caption = source.Caption;
            destination.LanguageCode = source.LanguageCode;

        }
    }
}
