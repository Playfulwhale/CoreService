namespace ApiTemplate.Commands.PaidPackageCommands
{
    using Boxed.AspNetCore;
    using ViewModels.PaidPackageViewModels;

    public interface IPostPaidPackageCommand : IAsyncCommand<SavePaidPackage>
    {
    }
}