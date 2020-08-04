namespace ApiTemplate.Commands.MenuItemCommands
{
    using Boxed.AspNetCore;
    using ViewModels.MenuItemViewModels;

    public interface IPostMenuItemCommand : IAsyncCommand<int,SaveMenuItem>
    {
    }
}