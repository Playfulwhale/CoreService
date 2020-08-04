namespace ApiTemplate.Commands.LanguageCommands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using ViewModels.LanguageViewModels;

    public class PatchLanguageCommand : IPatchLanguageCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IObjectModelValidator _objectModelValidator;
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper<Models.Language, Language> _languageToLanguageMapper;
        private readonly IMapper<Models.Language, SaveLanguage> _languageToSaveLanguageMapper;
        private readonly IMapper<SaveLanguage, Models.Language> _saveLanguageToLanguageMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatchLanguageCommand(
            IActionContextAccessor actionContextAccessor,
            IObjectModelValidator objectModelValidator,
            ILanguageRepository languageRepository,
            IMapper<Models.Language, Language> languageToLanguageMapper,
            IMapper<Models.Language, SaveLanguage> languageToSaveLanguageMapper,
            IMapper<SaveLanguage, Models.Language> saveLanguageToLanguageMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _actionContextAccessor = actionContextAccessor;
            _objectModelValidator = objectModelValidator;
            _languageRepository = languageRepository;
            _languageToLanguageMapper = languageToLanguageMapper;
            _languageToSaveLanguageMapper = languageToSaveLanguageMapper;
            _saveLanguageToLanguageMapper = saveLanguageToLanguageMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(
            int languageId,
            JsonPatchDocument<SaveLanguage> patch,
            CancellationToken cancellationToken)
        {
            var language = await _languageRepository.Get(languageId, cancellationToken);
            if (language == null)
            {
                return new NotFoundResult();
            }

            var saveLanguage = _languageToSaveLanguageMapper.Map(language);

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

            var modelState = _actionContextAccessor.ActionContext.ModelState;
            patch.ApplyTo(saveLanguage, modelState);
            _objectModelValidator.Validate(
                _actionContextAccessor.ActionContext,
                validationState: null,
                prefix: null,
                model: saveLanguage);
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(modelState);
            }

            _saveLanguageToLanguageMapper.Map(saveLanguage, language);
            await _languageRepository.Update(language, cancellationToken);
            var languageViewModel = _languageToLanguageMapper.Map(language);

            return new OkObjectResult(languageViewModel);
        }
    }
}