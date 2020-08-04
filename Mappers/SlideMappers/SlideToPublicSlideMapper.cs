namespace ApiTemplate.Mappers.SlideMappers
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System;
    using System.Linq;
    using ViewModels.SlideViewModels;

    public class SlideToPublicSlideMapper : IMapper<Models.Slide, PublicSlide>
    {
        
        private readonly IActionContextAccessor _actionContextAccessor;
        public SlideToPublicSlideMapper(
            IActionContextAccessor actionContextAccessor)
        {
            _actionContextAccessor = actionContextAccessor;
        }

        public void Map(Models.Slide source, PublicSlide destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            // Lấy mã ngôn ngữ
            var httpContext = _actionContextAccessor.ActionContext.HttpContext;
            var languageCode = httpContext.Request.Headers["Accept-Language"];
            var slideContent = source.SlideContents.FirstOrDefault(x => x.LanguageCode == languageCode.ToString().Split(',')[0]);

            destination.Id = source.Id;
            destination.Link = source.Link;
            destination.ImageUrl = source.ImageUrl;
            destination.OpenNewTab = source.OpenNewTab;
            destination.Caption = slideContent != null ? slideContent.Caption : "";
            destination.Title = slideContent != null ? slideContent.Title : "";
        }
    }
}
