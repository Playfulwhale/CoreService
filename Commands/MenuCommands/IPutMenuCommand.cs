namespace ApiTemplate.Commands.MenuCommands
{
    using ViewModels.MenuViewModels;
    using Boxed.AspNetCore;
    public interface IPutMenuCommand : IAsyncCommand<int, SaveMenu>
    {
    }
}
