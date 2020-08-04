namespace ApiTemplate.Commands.LanguageCommands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Constants;
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using ViewModels.LanguageViewModels;

    public class PostLanguageCommand : IPostLanguageCommand
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper<Models.Language, Language> _languageToLanguageMapper;
        private readonly IMapper<SaveLanguage, Models.Language> _saveLanguageToLanguageMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostLanguageCommand(
            ILanguageRepository languageRepository,
            IMapper<Models.Language, Language> languageToLanguageMapper,
            IMapper<SaveLanguage, Models.Language> saveLanguageToLanguageMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _languageRepository = languageRepository;
            _languageToLanguageMapper = languageToLanguageMapper;
            _saveLanguageToLanguageMapper = saveLanguageToLanguageMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(SaveLanguage saveLanguage, CancellationToken cancellationToken)
        {
            var listLanguage = await _languageRepository.GetAll(cancellationToken);

            var count = listLanguage.SingleOrDefault(x => x.Code == saveLanguage.Code);
            if(count != null)
            {
                 return new ConflictResult();
            }
            
            var language = _saveLanguageToLanguageMapper.Map(saveLanguage);

            //// add created by
            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();

            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub").Value;

            //if (language.CreatedBy == null)
            //    language.CreatedBy = userId;
            //language.ModifiedBy = userId;
            //// end created by

            language = await _languageRepository.Add(language, cancellationToken);
            if(language.IsDefault == true)
            {
                language = await _languageRepository.UpdateDefault(language, cancellationToken);
            }
            var languageViewModel = _languageToLanguageMapper.Map(language);

            return new CreatedAtRouteResult(LanguagesControllerRoute.GetLanguage, new {id=languageViewModel.Id},
                languageViewModel);
        }
    }
}
