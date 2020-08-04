namespace ApiTemplate.Commands.LanguageCommands
{
    using Boxed.AspNetCore;
    using ViewModels.LanguageViewModels;

    public interface IPutLanguageCommand : IAsyncCommand<int, SaveLanguage>
    {
    }
}
