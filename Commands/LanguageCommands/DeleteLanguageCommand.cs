namespace ApiTemplate.Commands.LanguageCommands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Repositories;
    using Microsoft.AspNetCore.Mvc;

    public class DeleteLanguageCommand : IDeleteLanguageCommand
    {
        private readonly ILanguageRepository _languageRepository;

        public DeleteLanguageCommand(ILanguageRepository languageRepository) =>
            _languageRepository = languageRepository;

        public async Task<IActionResult> ExecuteAsync(int languageId, CancellationToken cancellationToken)
        {
            var language = await _languageRepository.Get(languageId, cancellationToken);
            if (language == null)
            {
                return new NotFoundResult();
            }

            await _languageRepository.Delete(language, cancellationToken);

            return new NoContentResult();
        }
    }
}
