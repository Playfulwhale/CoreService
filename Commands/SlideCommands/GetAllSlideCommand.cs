namespace ApiTemplate.Commands.SlideCommands
{
    using Repositories;
    using ViewModels.SlideViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllSlideCommand : IGetAllSlideCommand
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IMapper<Models.Slide, Slide> _slideMapper;

        public GetAllSlideCommand(
            ISlideRepository slideRepository,
            IMapper<Models.Slide, Slide> slideMapper)
        {
            _slideRepository = slideRepository;
            _slideMapper = slideMapper;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var listSlide = await _slideRepository.GetAll(cancellationToken);
            var slideViewModels = _slideMapper.MapList(listSlide);

            return new OkObjectResult(slideViewModels);
        }
    }
}