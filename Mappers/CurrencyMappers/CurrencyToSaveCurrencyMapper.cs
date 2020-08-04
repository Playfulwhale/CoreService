namespace ApiTemplate.Mappers.CurrencyMappers
{
    using System;
    using Services;
    using Boxed.Mapping;
    using ViewModels.CurrencyViewModels;

    public class CurrencyToSaveCurrencyMapper : IMapper<Models.Currency, SaveCurrency>, IMapper<SaveCurrency, Models.Currency>
    {
        private readonly IClockService clockService;

        public CurrencyToSaveCurrencyMapper(IClockService clockService) =>
            this.clockService = clockService;

        public void Map(Models.Currency source, SaveCurrency destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Title = source.Title;
            destination.Code = source.Code;
            destination.SymbolLeft = source.SymbolLeft;
            destination.SymbolRight = source.SymbolRight;
            destination.Value = source.Value;
            destination.Default = source.Default;
            destination.Active = source.Active;

        }

        public void Map(SaveCurrency source, Models.Currency destination)
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

            destination.Title = source.Title;
            destination.Code = source.Code;
            destination.SymbolLeft = source.SymbolLeft;
            destination.SymbolRight = source.SymbolRight;
            destination.Value = source.Value;
            destination.Default = source.Default;
            
            destination.Active = source.Active;
            destination.ModifiedAt = now;
        }
    }
}
