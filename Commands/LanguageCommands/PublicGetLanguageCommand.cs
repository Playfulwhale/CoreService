namespace ApiTemplate.Commands.LanguageCommands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using ViewModels.LanguageViewModels;
    using System.Linq;

    public class PublicGetLanguageCommand : IPublicGetLanguageCommand
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper<Models.Language, PublicLanguage> _languageMapper;

        public PublicGetLanguageCommand(
            ILanguageRepository languageRepository,
            IMapper<Models.Language, PublicLanguage> languageMapper)
        {
            _languageRepository = languageRepository;
            _languageMapper = languageMapper;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var languages = await _languageRepository.GetAll(cancellationToken);
            if (languages == null)
            {
                return new NotFoundResult();
            }
            languages = languages.Where(x => x.Active).ToList();
            var languageViewModel = _languageMapper.MapList(languages);            
            return new OkObjectResult(languageViewModel);
        }
    }
}
