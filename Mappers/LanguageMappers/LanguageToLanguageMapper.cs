namespace ApiTemplate.Mappers.LanguageMappers
{
    using Boxed.Mapping;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System;
    using ViewModels.LanguageViewModels;

    public class LanguageToLanguageMapper : IMapper<Models.Language, Language>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;

        public LanguageToLanguageMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
        }

        public void Map(Models.Language source, Language destination)
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
            destination.Code = source.Code;
            destination.Flag = source.Flag;
            destination.IsDefault = source.IsDefault;
            destination.Active = source.Active;
            //destination.Url = urlHelper.AbsoluteRouteUrl(LanguagesControllerRoute.GetLanguage, new { source.Id });
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
               this.httpContextAccessor.HttpContext,
               LanguagesControllerRoute.GetLanguage,
               new { source.Id })).ToString();
        }
    }
}
