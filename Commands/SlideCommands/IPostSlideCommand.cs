namespace ApiTemplate.Commands.SlideCommands
{
    using ViewModels.SlideViewModels;
    using Boxed.AspNetCore;
    public interface IPostSlideCommand : IAsyncCommand<SaveSlide>
    {
    }
}
