namespace ApiTemplate
{
    using ApiTemplate.Commands.ContactUsCommands;
    using ApiTemplate.Commands.CountryCommands;
    using ApiTemplate.Commands.ZoneCommands;
    using ApiTemplate.Mappers.ContactUsMappers;
    using ApiTemplate.Mappers.CountryMappers;
    using ApiTemplate.Mappers.ZoneMappers;
    using ApiTemplate.Repositories;
    using ApiTemplate.Services;
    using ApiTemplate.ViewModels.ContactUsViewModels;
    using ApiTemplate.ViewModels.CountryViewModels;
    using ApiTemplate.ViewModels.ZoneViewModels;
    using Boxed.Mapping;
    using Commands.CurrencyCommands;
    using Commands.LanguageCommands;
    using Commands.MenuCommands;
    using Commands.MenuItemCommands;
    using Commands.PackageSubscriberCommands;
    using Commands.PaidPackageCommands;
    using Commands.PaymentMethodCommands;
    using Commands.SlideCommands;
    using Commands.SMTPCommands;
    using Commands.SystemSettingCommands;
    using Commands.SystemSettingGroupCommands;
    using Mappers.CurrencyMappers;
    using Mappers.LanguageMappers;
    using Mappers.MenuItemMappers;
    using Mappers.MenuMappers;
    using Mappers.PackageSubscriberMappers;
    using Mappers.PaidPackageMappers;
    using Mappers.PaymentMethodMappers;
    using Mappers.SlideContentMappers;
    using Mappers.SlideMappers;
    using Mappers.SystemSettingGroupMappers;
    using Mappers.SystemSettingMappers;
    using Microsoft.Extensions.DependencyInjection;
    using ViewModels.CurrencyViewModels;
    using ViewModels.LanguageViewModels;
    using ViewModels.MenuItemViewModels;
    using ViewModels.MenuViewModels;
    using ViewModels.PackageSubscriberViewModels;
    using ViewModels.PaidPackageViewModels;
    using ViewModels.PaymentMethodViewModels;
    using ViewModels.SlideContentViewModels;
    using ViewModels.SlideViewModels;
    using ViewModels.SystemSettingGroupViewModels;
    using ViewModels.SystemSettingViewModels;

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
                .AddScoped<IDeleteContactUsCommand, DeleteContactUsCommand>()
                .AddScoped<IGetContactUsCommand, GetContactUsCommand>()
                .AddScoped<IPostContactUsCommand, PostContactUsCommand>()
                .AddScoped<IPutContactUsCommand, PutContactUsCommand>()

                .AddScoped<IDeleteSystemSettingCommand, DeleteSystemSettingCommand>()
                .AddScoped<IGetSystemSettingCommand, GetSystemSettingCommand>()
                .AddScoped<IGetAllSystemSettingCommand, GetAllSystemSettingCommand>()
                .AddScoped<IPostSystemSettingCommand, PostSystemSettingCommand>()
                .AddScoped<IPutSystemSettingsCommand, PutSystemSettingsCommand>()
                .AddScoped<IPutSystemSettingCommand, PutSystemSettingCommand>()

                .AddScoped<IDeleteSystemSettingGroupCommand, DeleteSystemSettingGroupCommand>()
                .AddScoped<IGetSystemSettingGroupCommand, GetSystemSettingGroupCommand>()
                .AddScoped<IGetAllSystemSettingGroupCommand, GetAllSystemSettingGroupCommand>()
                .AddScoped<IGetAllSystemSettingGroupParamCommand, GetAllSystemSettingGroupParamCommand>()
                .AddScoped<IPostSystemSettingGroupCommand, PostSystemSettingGroupCommand>()
                .AddScoped<IPutSystemSettingGroupCommand, PutSystemSettingGroupCommand>()

                .AddScoped<IGetSlideCommand, GetSlideCommand>()
                .AddScoped<IGetAllSlideCommand, GetAllSlideCommand>()
                .AddScoped<IPostSlideCommand, PostSlideCommand>()
                .AddScoped<IDeleteSlideCommand, DeleteSlideCommand>()
                .AddScoped<IPutSlideCommand, PutSlideCommand>()
                .AddScoped<IPostSmtpConnectionCommand, PostSmtpConnectionCommand>()

                .AddScoped<IPostSmtpConnectionCommand, PostSmtpConnectionCommand>()

                .AddScoped<IPublicIGetAllSlideCommand, PublicGetAllSlideCommand>()
                .AddScoped<IPostPaidPackageCommand, PostPaidPackageCommand>()
                .AddScoped<IPutPaidPackageCommand, PutPaidPackageCommand>()
                .AddScoped<IGetPaidPackageCommand, GetPaidPackageCommand>()
                .AddScoped<IDeletePaidPackageCommand, DeletePaidPackageCommand>()
                .AddScoped<IGetAllPaidPackageCommand, GetAllPaidPackageCommand>()
                .AddScoped<IPublicGetAllPaidPackageCommand, PublicGetAllPaidPackageCommand>()
                .AddScoped<IPublicGetPaidPackageCommand, PublicGetPaidPackageCommand>()
                .AddScoped<IPublicPostPackageSubscriberCommand, PublicPostPackageSubscriberCommand>()

                .AddScoped<IDeleteLanguageCommand, DeleteLanguageCommand>()
                .AddScoped<IGetLanguageCommand, GetLanguageCommand>()
                .AddScoped<IGetLanguagePageCommand, GetLanguagePageCommand>()
                .AddScoped<IPublicGetLanguageCommand, PublicGetLanguageCommand>()
                .AddScoped<IPatchLanguageCommand, PatchLanguageCommand>()
                .AddScoped<IPostLanguageCommand, PostLanguageCommand>()
                .AddScoped<IPutSetDefaultLanguageCommand, PutSetDefaultLanguageCommand>()
                .AddScoped<IPutToggleLanguageCommand, PutToggleLanguageCommand>()
                .AddScoped<IPutLanguageCommand, PutLanguageCommand>()

                .AddScoped<IDeletePaymentMethodCommand, DeletePaymentMethodCommand>()
                .AddScoped<IGetPaymentMethodCommand, GetPaymentMethodCommand>()
                .AddScoped<IGetPaymentMethodAllCommand, GetPaymentMethodAllCommand>()
                .AddScoped<IPostPaymentMethodCommand, PostPaymentMethodCommand>()
                .AddScoped<IPutPaymentMethodCommand, PutPaymentMethodCommand>()

                .AddScoped<IDeleteCurrencyCommand, DeleteCurrencyCommand>()
                .AddScoped<IGetCurrencyCommand, GetCurrencyCommand>()
                .AddScoped<IGetCurrencyPageCommand, GetCurrencyPageCommand>()
                .AddScoped<IPostCurrencyCommand, PostCurrencyCommand>()
                .AddScoped<IPublicGetCurrencyAllCommand, PublicGetCurrencyAllCommand>()
                .AddScoped<IPutCurrencyCommand, PutCurrencyCommand>()
                .AddScoped<IPutCurrencyToggleStatusCommand, PutCurrencyToggleStatusCommand>()
                .AddScoped<IPutCurrencyDefaultCommand, PutCurrencyDefaultCommand>()

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
                .AddScoped<IPutZoneActiveCommand, PutZoneActiveCommand>()

                .AddScoped<IGetMenuAllCommand, GetMenuAllCommand>()
                .AddScoped<IGetMenuCommand, GetMenuCommand>()
                .AddScoped<IPostMenuCommand, PostMenuCommand>()
                .AddScoped<IPutMenuCommand, PutMenuCommand>()
                .AddScoped<IDeleteMenuCommand, DeleteMenuCommand>()
                .AddScoped<IPublicGetMenuCommand, PublicGetMenuCommand>()

                .AddScoped<IPostMenuItemCommand, PostMenuItemCommand>()
                .AddScoped<IGetMenuAllCommand, GetMenuAllCommand>()
                .AddScoped<IPutMenuItemCommand, PutMenuItemCommand>()
                .AddScoped<IGetMenuItemCommand, GetMenuItemCommand>()
                .AddScoped<IDeleteMenuItemCommand, DeleteMenuItemCommand>()

                .AddScoped<IGetPackageSubscriberCommand, GetPackageSubscriberCommand>()
                .AddScoped<IGetAllPackageSubscriberByUserCommand, GetAllPackageSubscriberByUserCommand>()
                .AddScoped<IGetAllPackageSubscriberCommand, GetAllPackageSubscriberCommand>()
                .AddScoped<IPostPackageSubscriberCommand, PostPackageSubscriberCommand>()
                .AddScoped<IPublicGetAllPackageSubscriberByUserCommand, PublicGetAllPackageSubscriberByUserCommand>()
                .AddScoped<IDeletePackageSubscriberCommand, DeletePackageSubscriberCommand>();

        public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
            services
                .AddScoped<IMapper<Models.ContactUs, ContactUs>, ContactUsToContactUsMapper>()
                .AddScoped<IMapper<Models.ContactUs, SaveContactUs>, ContactUsToSaveContactUsMapper>()
                .AddScoped<IMapper<SaveContactUs, Models.ContactUs>, ContactUsToSaveContactUsMapper>()

                .AddScoped<IMapper<Models.SystemSetting, SystemSetting>, SystemSettingToSystemSettingMapper>()
                .AddScoped<IMapper<Models.SystemSetting, SaveSystemSetting>, SystemSettingToSaveSystemSettingMapper>()
                .AddScoped<IMapper<SaveSystemSetting, Models.SystemSetting>, SystemSettingToSaveSystemSettingMapper>()
                .AddScoped<IMapper<SaveSystemSettingWithOutKey, Models.SystemSetting>, SystemSettingToSaveSystemSettingMapper>()

                .AddScoped<IMapper<Models.SystemSettingGroup, SystemSettingGroup>, SystemSettingGroupToSystemSettingGroupMapper>()
                .AddScoped<IMapper<Models.SystemSettingGroup, SaveSystemSettingGroup>, SystemSettingGroupToSaveSystemSettingGroupMapper>()
                .AddScoped<IMapper<SaveSystemSettingGroup, Models.SystemSettingGroup>, SystemSettingGroupToSaveSystemSettingGroupMapper>()

                .AddScoped<IMapper<Models.SlideContent, SlideContent>, SlideContentToSlideContentMapper>()
                .AddScoped<IMapper<Models.SlideContent, SaveSlideContent>, SlideContentToSaveSlideContentMapper>()
                .AddScoped<IMapper<SaveSlideContent, Models.SlideContent>, SlideContentToSaveSlideContentMapper>()

                .AddScoped<IMapper<Models.Slide, Slide>, SlideToSlideMapper>()
                .AddScoped<IMapper<Models.Slide, SaveSlide>, SlideToSaveSlideMapper>()
                .AddScoped<IMapper<SaveSlide, Models.Slide>, SlideToSaveSlideMapper>()
                .AddScoped<IMapper<Models.Slide, PublicSlide>, SlideToPublicSlideMapper>()

                .AddScoped<IMapper<Models.PaidPackage, PaidPackage>, PaidPackageToPaidPackageMapper>()
                .AddScoped<IMapper<SavePaidPackage, Models.PaidPackage>, PaidPackageToSavePaidPackageMapper>()
                .AddScoped<IMapper<Models.PaidPackage, PublicPaidPackage>, PublicPaidPackageToPaidPackageMapper>()
                .AddScoped<IMapper<Models.PaidPackagePrice, PaidPackagePrice>, PaidPackagePriceToPaidPackagePriceMapper>()
                .AddScoped<IMapper<PaidPackagePrice, Models.PaidPackagePrice>, PaidPackagePriceToSavePaidPackagePriceMapper>()

                .AddScoped<IMapper<Models.Language, Language>, LanguageToLanguageMapper>()
                .AddScoped<IMapper<Models.Language, PublicLanguage>, PublicLanguageToLanguageMapper>()
                .AddScoped<IMapper<Models.Language, SaveLanguage>, LanguageToSaveLanguageMapper>()
                .AddScoped<IMapper<SaveLanguage, Models.Language>, LanguageToSaveLanguageMapper>()

                .AddScoped<IMapper<Models.PaymentMethod, PaymentMethod>, PaymentMethodToPaymentMethodMapper>()
                .AddScoped<IMapper<Models.PaymentMethod, SavePaymentMethod>, PaymentMethodToSavePaymentMethodMapper>()
                .AddScoped<IMapper<SavePaymentMethod, Models.PaymentMethod>, PaymentMethodToSavePaymentMethodMapper>()

                .AddScoped<IMapper<Models.Country, Country>, CountryToCountryMapper>()
                .AddScoped<IMapper<Models.Country, PublicCountry>, PublicCountryToCountryMapper>()
                .AddScoped<IMapper<Models.Country, SaveCountry>, CountryToSaveCountryMapper>()
                .AddScoped<IMapper<SaveCountry, Models.Country>, CountryToSaveCountryMapper>()

                .AddScoped<IMapper<Models.Currency, Currency>, CurrencyToCurrencyMapper>()
                .AddScoped<IMapper<Models.Currency, PublicCurrency>, PublicCurrencyToCurrencyMapper>()
                .AddScoped<IMapper<Models.Currency, SaveCurrency>, CurrencyToSaveCurrencyMapper>()
                .AddScoped<IMapper<SaveCurrency, Models.Currency>, CurrencyToSaveCurrencyMapper>()

                .AddScoped<IMapper<Models.Zone, Zone>, ZoneToZoneMapper>()
                .AddScoped<IMapper<Models.Zone, PublicZone>, PublicZoneToZoneMapper>()
                .AddScoped<IMapper<Models.Zone, SaveZone>, ZoneToSaveZoneMapper>()
                .AddScoped<IMapper<SaveZone, Models.Zone>, ZoneToSaveZoneMapper>()

                .AddScoped<IMapper<Models.Menu, PublicMenu>, PublicMenuToMenuMapper>()
                .AddScoped<IMapper<Models.MenuItem, AdminMenuItem>, MenuItemToAdminMenuItemMapper>()
                .AddScoped<IMapper<Models.Menu, AdminMenu>, AdminMenuToMenuMapper>()
                .AddScoped<IMapper<Models.Menu, Menu>, MenuToMenuMapper>()
                .AddScoped<IMapper<SaveMenu, Models.Menu>, MenuToSaveMenuMapper>()

                .AddScoped<IMapper<Models.MenuItem, MenuItem>, MenuItemToMenuItemMapper>()
                .AddScoped<IMapper<Models.MenuItem, PublicMenuItem>, PublicMenuItemToMenuItemMapper>()
                .AddScoped<IMapper<Models.Menu, PublicMenu>, PublicMenuToMenuMapper>()
                .AddScoped<IMapper<Models.MenuItemDescription, MenuItemDescription>, MenuItemDescriptionToMenuItemDescriptionMapper>()
                .AddScoped<IMapper<SaveMenuItemDescription, Models.MenuItemDescription>, MenuItemDescriptionToSaveMenuItemDescriptionMapper>()
                .AddScoped<IMapper<SaveMenuItem, Models.MenuItem>, MenuItemToSaveMenuItemMapper>()

                .AddScoped<IMapper<Models.PackageSubscriber, PackageSubscriber>, PackageSubscriberToPackageSubscriberMapper>()
                .AddScoped<IMapper<Models.PackageSubscriber, PublicPackageSubscriber>, PackageSubscriberToPublicPackageSubscriberMapper>()
                .AddScoped<IMapper<SavePackageSubscriber, Models.PackageSubscriber>, PackageSubscriberToSavePackageSubscriberMapper>();


        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddScoped<IContactUsRepository, ContactUsRepository>()
                .AddScoped<ISystemSettingRepository, SystemSettingRepository>()
                .AddScoped<ISystemSettingGroupRepository, SystemSettingGroupRepository>()
                .AddScoped<ISlideRepository, SlideRepository>()
                .AddScoped<IPaidPackageRepository, PaidPackageRepository>()
                .AddScoped<ICountryRepository, CountryRepository>()
                .AddScoped<IZoneRepository, ZoneRepository>()
                .AddScoped<IPaymentMethodRepository, PaymentMethodRepository>()
                .AddScoped<ILanguageRepository, LanguageRepository>()
                .AddScoped<ICurrencyRepository, CurrencyRepository>()
                .AddScoped<ICountryRepository, CountryRepository>()
                .AddScoped<IMenuRepository, MenuRepository>()
                .AddScoped<IMenuItemRepository, MenuItemRepository>()
                .AddScoped<IPackageSubscriberRepository, PackageSubscriberRepository>();

        public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
            services
                .AddSingleton<IClockService, ClockService>();
    }
}
