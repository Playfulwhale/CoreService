namespace ApiTemplate.Mappers.SystemSettingMappers
{
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Constants;
    using ViewModels.SystemSettingViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class SystemSettingToSystemSettingMapper : IMapper<Models.SystemSetting, SystemSetting>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;

        public SystemSettingToSystemSettingMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
        }

        public void Map(Models.SystemSetting source, SystemSetting destination)
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
            destination.Key = source.Key;
            destination.Value = source.Value;
            destination.Data = source.Data;
            destination.DataType = source.DataType;
            destination.Description = source.Description;
            destination.GroupId = source.GroupId;
            //destination.Url = _urlHelper.AbsoluteRouteUrl(SystemSettingsControllerRoute.GetSystemSetting, new { source.Key });
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
                this.httpContextAccessor.HttpContext,
                SystemSettingsControllerRoute.GetSystemSetting,
                new { source.Key })).ToString();
        }
    }
}