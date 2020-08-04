namespace ApiTemplate.Commands.LanguageCommands
{
    using Boxed.AspNetCore;
    using ViewModels.LanguageViewModels;

    public interface IGetLanguagePageCommand : IAsyncCommand<string, PageOptions>
    {
    }
}
