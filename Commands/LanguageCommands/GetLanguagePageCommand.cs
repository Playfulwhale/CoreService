namespace ApiTemplate.Commands.LanguageCommands
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Constants;
    using Data;
    using Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.LanguageViewModels;

    public class GetLanguagePageCommand : IGetLanguagePageCommand
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper<Models.Language, Language> _languageMapper;

        public GetLanguagePageCommand(
            ILanguageRepository languageRepository,
            IMapper<Models.Language, Language> languageMapper)
        {
            
            _languageRepository = languageRepository;
            _languageMapper = languageMapper;
        }

        public async Task<IActionResult> ExecuteAsync(string all, PageOptions pageOptions, CancellationToken cancellationToken)
        {
            if(all == "1")
            {
                var language = await _languageRepository.GetAll(cancellationToken);
                if (language == null)
                {
                    return new NoContentResult();
                }

                return new OkObjectResult(language);
            }

            var languages = await _languageRepository.GetPage(pageOptions.Page.Value, pageOptions.Count.Value, cancellationToken);
            if (languages == null)
            {
                return new NoContentResult();
            }

            var (totalCount, totalPages) = await _languageRepository.GetTotalPages(pageOptions.Count.Value, cancellationToken);
            var languageViewModels = _languageMapper.MapList(languages);
            var page = new PageResult<Language>()
            {
                Count = pageOptions.Count.Value,
                Items = languageViewModels,
                Page = pageOptions.Page.Value,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };


            return new OkObjectResult(page);
        }
    }
}
