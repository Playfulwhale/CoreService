namespace ApiTemplate.Mappers.CurrencyMappers
{
    using System;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.CurrencyViewModels;

    public class PublicCurrencyToCurrencyMapper : IMapper<Models.Currency, PublicCurrency>
    {

        public void Map(Models.Currency source, PublicCurrency destination)
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

        }
    }
}