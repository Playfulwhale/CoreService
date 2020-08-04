namespace ApiTemplate.Mappers.LanguageMappers
{
    using System;
    using Services;
    using Boxed.Mapping;
    using ViewModels.LanguageViewModels;

    public class LanguageToSaveLanguageMapper : IMapper<Models.Language, SaveLanguage>, IMapper<SaveLanguage, Models.Language>
    {
        private readonly IClockService clockService;

        public LanguageToSaveLanguageMapper(IClockService clockService) =>
            this.clockService = clockService;

        public void Map(Models.Language source, SaveLanguage destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Code = source.Code;
            destination.Flag = source.Flag;
            destination.IsDefault = source.IsDefault;
            destination.Title = source.Title;
            destination.Active = source.Active;
        }

        public void Map(SaveLanguage source, Models.Language destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var now = clockService.UtcNow;

            if (destination.CreatedAt == DateTimeOffset.MinValue)
            {
                destination.CreatedAt = now;
            }

            destination.Code = source.Code;
            destination.Flag = source.Flag;
            destination.IsDefault = source.IsDefault;
            destination.Title = source.Title;
            destination.Active = source.Active;
            destination.ModifiedAt = now;
        }
    }
}
