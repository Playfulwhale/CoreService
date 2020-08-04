namespace ApiTemplate.Mappers.SystemSettingGroupMappers
{
    using Services;
    using ViewModels.SystemSettingGroupViewModels;
    using Boxed.Mapping;
    using System;

    public class SystemSettingGroupToSaveSystemSettingGroupMapper :
        IMapper<Models.SystemSettingGroup, SaveSystemSettingGroup>,
        IMapper<SaveSystemSettingGroup, Models.SystemSettingGroup>
    {
        private readonly IClockService _clockService;

        public SystemSettingGroupToSaveSystemSettingGroupMapper(IClockService clockService) =>
            _clockService = clockService;

        public void Map(Models.SystemSettingGroup source, SaveSystemSettingGroup destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Order = source.Order;
        }

        public void Map(SaveSystemSettingGroup source, Models.SystemSettingGroup destination)
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

            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Order = source.Order;
        }
    }
}