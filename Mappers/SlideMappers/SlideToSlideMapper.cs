namespace ApiTemplate.Mappers.SlideMappers
{
    using Constants;
    using ViewModels.SlideContentViewModels;
    using ViewModels.SlideViewModels;
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class SlideToSlideMapper : IMapper<Models.Slide, Slide>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper<Models.SlideContent, SlideContent> _slideContentMapper;

        public SlideToSlideMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator,
           IMapper<Models.SlideContent, SlideContent> slideContentMapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
            _slideContentMapper = slideContentMapper;
        }

        public void Map(Models.Slide source, Slide destination)
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
            destination.LinkType = source.LinkType;
            destination.Link = source.Link;
            destination.ImageUrl = source.ImageUrl;
            destination.OpenNewTab = source.OpenNewTab;
            destination.SortOrder = source.SortOrder;
            destination.Status = source.Status;
            destination.SlideContents = _slideContentMapper.MapList(source.SlideContents);
            //destination.Url = _urlHelper.AbsoluteRouteUrl(SlidesControllerRoute.GetSlide, new { source.Id });
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                this.httpContextAccessor.HttpContext,
                SlidesControllerRoute.GetSlide,
                new { source.Id })).ToString();
        }
    }
}
