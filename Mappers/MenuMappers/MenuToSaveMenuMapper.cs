namespace ApiTemplate.Mappers.MenuMappers
{
    using ViewModels.MenuViewModels;
    using Boxed.Mapping;
    using Services;
    using System;

    public class MenuToSaveMenuMapper : IMapper<SaveMenu, Models.Menu>
    {
        private readonly IClockService clockService;

        public MenuToSaveMenuMapper(IClockService clockService) =>
            this.clockService = clockService;

        public void Map(SaveMenu source, Models.Menu destination)
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

            destination.Name = source.Name;
            destination.Position = source.Position;
            destination.ModifiedAt = now;
        }
    }
}
