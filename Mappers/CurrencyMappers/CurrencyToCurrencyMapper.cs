namespace ApiTemplate.Mappers.CurrencyMappers
{
    using Boxed.Mapping;
    using Constants;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System;
    using ViewModels.CurrencyViewModels;

    public class CurrencyToCurrencyMapper : IMapper<Models.Currency, Currency>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LinkGenerator linkGenerator;

        public CurrencyToCurrencyMapper(
           IHttpContextAccessor httpContextAccessor,
           LinkGenerator linkGenerator)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.linkGenerator = linkGenerator;
        }
        public void Map(Models.Currency source, Currency destination)
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
            destination.SymbolLeft = source.SymbolLeft;
            destination.SymbolRight = source.SymbolRight;
            destination.Value = source.Value;
            destination.Default = source.Default;
            
            destination.Active = source.Active;
            //destination.Url = urlHelper.AbsoluteRouteUrl(CurrenciesControllerRoute.GetCurrency, new { source.Id });
            destination.Url = new Uri(this.linkGenerator.GetUriByRouteValues(
               this.httpContextAccessor.HttpContext,
               CurrenciesControllerRoute.GetCurrency,
               new { source.Id })).ToString();
        }
    }
}