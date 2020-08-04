namespace ApiTemplate.Commands.LanguageCommands
{
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.JsonPatch;
    using ViewModels.LanguageViewModels;

    public interface IPatchLanguageCommand : IAsyncCommand<int, JsonPatchDocument<SaveLanguage>>
    {
    }
}
