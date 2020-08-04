namespace ApiTemplate.Commands.PackageSubscriberCommands
{
    using ViewModels.PackageSubscriberViewModels;
    using Boxed.AspNetCore;

    public interface IGetAllPackageSubscriberCommand : IAsyncCommand<QueryOptions>
    {
    }
}