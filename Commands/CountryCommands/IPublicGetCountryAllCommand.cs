namespace ApiTemplate.Commands.CountryCommands
{
    using ApiTemplate.ViewModels.CountryViewModels;
    using Boxed.AspNetCore;
    using System;

    public interface IPublicGetCountryAllCommand : IAsyncCommand<String,PageOptions>
    {
    }
}
