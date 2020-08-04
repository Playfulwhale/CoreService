namespace ApiTemplate.Mappers.SystemSettingMappers
{
    using Services;
    using ViewModels.SystemSettingViewModels;
    using Boxed.Mapping;
    using System;

    public class SystemSettingToSaveSystemSettingMapper :
        IMapper<Models.SystemSetting, SaveSystemSetting>,
        IMapper<SaveSystemSetting, Models.SystemSetting>,
        IMapper<SaveSystemSettingWithOutKey, Models.SystemSetting>
    {
        private readonly IClockService _clockService;

        public SystemSettingToSaveSystemSettingMapper(IClockService clockService) =>
            _clockService = clockService;

        public void Map(Models.SystemSetting source, SaveSystemSetting destination)
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
            destination.Key = source.Key;
            destination.Value = source.Value;
            destination.Data = source.Data;
            destination.DataType = source.DataType;
            destination.Description = source.Description;
            destination.GroupId = source.GroupId;
        }

        public void Map(SaveSystemSetting source, Models.SystemSetting destination)
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
            destination.Key = source.Key;
            destination.Value = source.Value;
            destination.Data = source.Data;
            destination.DataType = source.DataType;
            destination.Description = source.Description;
            destination.GroupId = source.GroupId;
        }

        public void Map(SaveSystemSettingWithOutKey source, Models.SystemSetting destination)
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

            if (destination.CreatedAt == DateTimeOffset.MinValue)
            {
                destination.CreatedAt = now;
            }
            destination.ModifiedAt = now;

            destination.Name = source.Name;
            destination.Value = source.Value;
            destination.Data = source.Data;
            destination.DataType = source.DataType;
            destination.Description = source.Description;
            destination.GroupId = source.GroupId;
        }
    }
}