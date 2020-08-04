namespace ApiTemplate.Mappers.SystemSettingGroupMappers
{
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Constants;
    using ViewModels.SystemSettingGroupViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class SystemSettingGroupToSystemSettingGroupMapper : IMapper<Models.SystemSettingGroup, SystemSettingGroup>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;

        public SystemSettingGroupToSystemSettingGroupMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
        }
        public void Map(Models.SystemSettingGroup source, SystemSettingGroup destination)
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
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Order = source.Order;
            //destination.Url = _urlHelper.AbsoluteRouteUrl(SystemSettingGroupsControllerRoute.GetSystemSettingGroup, new { source.Id });
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                    this.httpContextAccessor.HttpContext,
                    SystemSettingGroupsControllerRoute.GetSystemSettingGroup,
                    new { source.Id })).ToString();
        }
    }
}