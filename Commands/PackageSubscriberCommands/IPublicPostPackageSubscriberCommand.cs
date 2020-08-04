namespace ApiTemplate.Commands.PackageSubscriberCommands
{
    using Boxed.AspNetCore;
    using ViewModels.PackageSubscriberViewModels;

    public interface IPublicPostPackageSubscriberCommand : IAsyncCommand<PublicSavePackageSubscriber>
    {
    }
}