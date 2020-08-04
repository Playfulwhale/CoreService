namespace ApiTemplate.Commands.MenuCommands
{
    using Boxed.AspNetCore;
    using ViewModels.MenuViewModels;

    public interface IPostMenuCommand : IAsyncCommand<SaveMenu>
    {
    }
}
