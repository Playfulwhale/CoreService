namespace ApiTemplate.Commands.MenuItemCommands
{
    using ViewModels.MenuItemViewModels;
    using Boxed.AspNetCore;
    public interface IPutMenuItemCommand : IAsyncCommand<int, SaveMenuItem>
    {
    }
}
