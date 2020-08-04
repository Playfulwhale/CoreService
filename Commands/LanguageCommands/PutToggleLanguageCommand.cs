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

    public class PutToggleLanguageCommand : IPutToggleLanguageCommand
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper<Models.Language, Language> _languageToLanguageMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PutToggleLanguageCommand(
            ILanguageRepository languageRepository,
            IMapper<Models.Language, Language> languageToLanguageMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _languageRepository = languageRepository;
            _languageToLanguageMapper = languageToLanguageMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(int languageId, CancellationToken cancellationToken)
        {
            var language = await _languageRepository.Get(languageId, cancellationToken);
            if (language == null)
            {
                return new NotFoundResult();
            }
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

            language.Active = language.Active ? !language.Active : true;
            language = await _languageRepository.UpdateActive(language, cancellationToken);
            var languageViewModel = _languageToLanguageMapper.Map(language);

            return new OkObjectResult(languageViewModel);
        }
    }
}
