namespace ApiTemplate.Commands.LanguageCommands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Net.Http.Headers;
    using ViewModels.LanguageViewModels;

    public class GetLanguageCommand : IGetLanguageCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper<Models.Language, Language> _languageMapper;

        public GetLanguageCommand(
            IActionContextAccessor actionContextAccessor,
            ILanguageRepository languageRepository,
            IMapper<Models.Language, Language> languageMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _languageRepository = languageRepository;
            _languageMapper = languageMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int languageId, CancellationToken cancellationToken)
        {
            var language = await _languageRepository.Get(languageId, cancellationToken);
            if (language == null)
            {
                return new NotFoundResult();
            }

            var httpContext = _actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= language.ModifiedAt))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var languageViewModel = _languageMapper.Map(language);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, language.ModifiedAt.ToString());
            return new OkObjectResult(languageViewModel);
        }
    }
}
