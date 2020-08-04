namespace ApiTemplate.Commands.PackageSubscriberCommands
{
    using ViewModels.PackageSubscriberViewModels;
    using Boxed.AspNetCore;

    public interface IGetAllPackageSubscriberByUserCommand : IAsyncCommand<int, QueryOptions>
    {
    }
}