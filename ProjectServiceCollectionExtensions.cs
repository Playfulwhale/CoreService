namespace ApiTemplate
{
    using ApiTemplate.Commands;
    using ApiTemplate.Commands.ContactUsCommands;
    using ApiTemplate.Commands.CountryCommands;
    using ApiTemplate.Commands.ZoneCommands;
    using ApiTemplate.Mappers;
    using ApiTemplate.Mappers.ContactUsMappers;
    using ApiTemplate.Mappers.CountryMappers;
    using ApiTemplate.Mappers.ZoneMappers;
    using ApiTemplate.Repositories;
    using ApiTemplate.Services;
    using ApiTemplate.ViewModels;
    using ApiTemplate.ViewModels.ContactUsViewModels;
    using ApiTemplate.ViewModels.CountryViewModels;
    using ApiTemplate.ViewModels.ZoneViewModels;
    using Boxed.Mapping;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods add project services.
    /// </summary>
    /// <remarks>
    /// AddSingleton - Only one instance is ever created and returned.
    /// AddScoped - A new instance is created and returned for each request/response cycle.
    /// AddTransient - A new instance is created and returned each time.
    /// </remarks>
    public static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectCommands(this IServiceCollection services) =>
            services
                .AddSingleton<IDeleteCarCommand, DeleteCarCommand>()
                .AddSingleton<IGetCarCommand, GetCarCommand>()
                .AddSingleton<IGetCarPageCommand, GetCarPageCommand>()
                .AddSingleton<IPatchCarCommand, PatchCarCommand>()
                .AddSingleton<IPostCarCommand, PostCarCommand>()
                .AddSingleton<IPutCarCommand, PutCarCommand>()

                .AddScoped<IDeleteContactUsCommand, DeleteContactUsCommand>()
                .AddScoped<IGetContactUsCommand, GetContactUsCommand>()
                .AddScoped<IPostContactUsCommand, PostContactUsCommand>()
                .AddScoped<IPutContactUsCommand, PutContactUsCommand>()

                .AddScoped<IDeleteCountryCommand, DeleteCountryCommand>()
                .AddScoped<IGetCountryCommand, GetCountryCommand>()
                .AddScoped<IGetCountryPageCommand, GetCountryPageCommand>()
                .AddScoped<IPublicGetCountryAllCommand, PublicGetCountryAllCommand>()
                .AddScoped<IPublicGetZoneAllByCountryCodeCommand, PublicGetZoneAllByCountryCodeCommand>()
                .AddScoped<IPatchCountryCommand, PatchCountryCommand>()
                .AddScoped<IPostCountryCommand, PostCountryCommand>()
                .AddScoped<IPutCountryCommand, PutCountryCommand>()
                .AddScoped<IPutCountryActiveCommand, PutCountryActiveCommand>()
                .AddScoped<IGetZoneAllByCountryCommand, GetZoneAllByCountryCommand>()

                .AddScoped<IDeleteZoneCommand, DeleteZoneCommand>()
                .AddScoped<IGetZoneCommand, GetZoneCommand>()
                .AddScoped<IGetZonePageCommand, GetZonePageCommand>()
                .AddScoped<IGetZoneAllCommand, GetZoneAllCommand>()
                .AddScoped<IPatchZoneCommand, PatchZoneCommand>()
                .AddScoped<IPostZoneCommand, PostZoneCommand>()
                .AddScoped<IPutZoneCommand, PutZoneCommand>()
                .AddScoped<IPutZoneActiveCommand, PutZoneActiveCommand>();

        public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
            services
                .AddScoped<IMapper<Models.ContactUs, ContactUs>, ContactUsToContactUsMapper>()
                .AddScoped<IMapper<Models.ContactUs, SaveContactUs>, ContactUsToSaveContactUsMapper>()
                .AddScoped<IMapper<SaveContactUs, Models.ContactUs>, ContactUsToSaveContactUsMapper>()

                .AddScoped<IMapper<Models.Country, Country>, CountryToCountryMapper>()
                .AddScoped<IMapper<Models.Country, PublicCountry>, PublicCountryToCountryMapper>()
                .AddScoped<IMapper<Models.Country, SaveCountry>, CountryToSaveCountryMapper>()
                .AddScoped<IMapper<SaveCountry, Models.Country>, CountryToSaveCountryMapper>()

                .AddScoped<IMapper<Models.Zone, Zone>, ZoneToZoneMapper>()
                .AddScoped<IMapper<Models.Zone, PublicZone>, PublicZoneToZoneMapper>()
                .AddScoped<IMapper<Models.Zone, SaveZone>, ZoneToSaveZoneMapper>()
                .AddScoped<IMapper<SaveZone, Models.Zone>, ZoneToSaveZoneMapper>()

                .AddSingleton<IMapper<Models.Car, Car>, CarToCarMapper>()
                .AddSingleton<IMapper<Models.Car, SaveCar>, CarToSaveCarMapper>()
                .AddSingleton<IMapper<SaveCar, Models.Car>, CarToSaveCarMapper>();

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddScoped<IContactUsRepository, ContactUsRepository>()
                .AddScoped<ICountryRepository, CountryRepository>()
                .AddScoped<IZoneRepository, ZoneRepository>()
                .AddSingleton<ICarRepository, CarRepository>();

        public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
            services
                .AddSingleton<IClockService, ClockService>();
    }
}
