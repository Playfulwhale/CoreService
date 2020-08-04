namespace ApiTemplate.Commands.PaidPackageCommands
{
    using ViewModels.PaidPackageViewModels;
    using Boxed.AspNetCore;

    public interface IPutPaidPackageCommand : IAsyncCommand<int, SavePaidPackage>
    {
    }
}