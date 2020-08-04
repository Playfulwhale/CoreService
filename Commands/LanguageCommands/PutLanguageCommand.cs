namespace ApiTemplate.Commands.LanguageCommands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.LanguageViewModels;

    public class PutLanguageCommand : IPutLanguageCommand
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper<Models.Language, Language> _languageToLanguageMapper;
        private readonly IMapper<SaveLanguage, Models.Language> _saveLanguageToLanguageMapper;

        public PutLanguageCommand(
            ILanguageRepository languageRepository,
            IMapper<Models.Language, Language> languageToLanguageMapper,
            IMapper<SaveLanguage, Models.Language> saveLanguageToLanguageMapper)
        {
            _languageRepository = languageRepository;
            _languageToLanguageMapper = languageToLanguageMapper;
            _saveLanguageToLanguageMapper = saveLanguageToLanguageMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int languageId, SaveLanguage saveLanguage, CancellationToken cancellationToken)
        {
            var language = await _languageRepository.Get(languageId, cancellationToken);
            if (language == null)
            {
                return new NotFoundResult();
            }

            _saveLanguageToLanguageMapper.Map(saveLanguage, language);

            //// add created by
            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();

            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub").Value;
            
            //language.ModifiedBy = userId;
            //// end created by

            language = await _languageRepository.Update(language, cancellationToken);
            var languageViewModel = _languageToLanguageMapper.Map(language);

            return new OkObjectResult(languageViewModel);
        }
    }
}
